using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Helpers;
using System;
using System.Net.Http;


namespace PortalTask.Tests
{
    class DeletePostTest : BaseTest
    {
        private const int newPostId = 100;

        public override void Run()
        {
            HttpResponseMessage response = Client.DeleteAsync($"{postsEndpoint}/{newPostId}").Result;

            Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");
            Reporter.LogInfo("Status code is :" + response.StatusCode.ToString());
        }
    }
}
