using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Helpers;
using PortalTask.Requests;
using System;
using System.Net.Http;
using System.Text;

namespace PortalTask.Tests
{
    class UpdatingPostTest : BaseTest
    {
        private const string requestTitle = "Artem's update test";
        private const string requestBody = "Artem's update body";
        private const int updatingPostId = 2;
        private const int updatingUserId = 6;

        public override void Run()
        {
            PostsModel request = new PostsModel(updatingUserId, requestTitle, requestBody);

            string json = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Reporter.LogInfo("Request: " + json);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = Client.PutAsync($"{postsEndpoint}/{updatingPostId}", content).Result;

            PostsModel parsedResponse = JsonConvert.DeserializeObject<PostsModel>(response.Content.ReadAsStringAsync().Result);


            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");
                Assert.AreEqual(updatingPostId, parsedResponse.Id);
                Assert.AreEqual(updatingUserId, parsedResponse.UserId);
                Assert.AreEqual(requestBody, parsedResponse.Body);
                Assert.AreEqual(requestTitle, parsedResponse.Title);
            });
        }
    }
}