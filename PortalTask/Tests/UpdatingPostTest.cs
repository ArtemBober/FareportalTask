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
        private readonly string requestTitle = "Artem's update test";
        private readonly string requestBody = "Artem's update body";


        public override void Run()
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseUlr) };

            PostsModel request = new PostsModel(6, requestTitle, requestBody);

            string json = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Reporter.LogInfo("Request: " + json);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("posts/2", content).Result;

            PostsModel parsedResponse = JsonConvert.DeserializeObject<PostsModel>(response.Content.ReadAsStringAsync().Result);


            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");
                Assert.AreEqual(2, parsedResponse.Id);
                Assert.AreEqual(6, parsedResponse.UserId);
                Assert.AreEqual(requestBody, parsedResponse.Body);
                Assert.AreEqual(requestTitle, parsedResponse.Title);
            });
        }
    }
}