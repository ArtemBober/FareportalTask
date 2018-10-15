using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Helpers;
using System;
using System.Net.Http;


namespace PortalTask.Tests
{
    class DeletePostTest : BaseTest
    {
        public override void Run()
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseUlr) };
            
            HttpResponseMessage response = client.DeleteAsync("posts/100").Result;

            Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");
            Reporter.LogInfo("Status code is :" + response.StatusCode.ToString());
        }
    }
}
