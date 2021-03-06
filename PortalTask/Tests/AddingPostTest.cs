﻿using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Helpers;
using PortalTask.Requests;
using System;
using System.Net.Http;
using System.Text;

namespace PortalTask.Tests
{
    public class AddingPostTest : BaseTest
    {
        private const int postsUserId = 5;
        private const string postsTitle = "Artem's posting test";
        private const string postsBody = "Artem's posting test body";
        private const int newlyCreatedPostId = 101;

        public override void Run()
        {
            PostsModel request = new PostsModel(postsUserId, postsTitle, postsBody);

            string json = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Reporter.LogInfo("Request: " + json);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = Client.PostAsync(postsEndpoint, content).Result;

            PostsModel parsedResponse = JsonConvert.DeserializeObject<PostsModel>(response.Content.ReadAsStringAsync().Result);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");
                Assert.AreEqual(newlyCreatedPostId, parsedResponse.Id);
            });
        }
    }
}
