using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PortalTask.Tests
{
    public class CommentsEmailTest : BaseTest
    {
        private const string bodySnipet = "ipsum dolorem";
        private const string email = "Marcia@name.biz";

        public override void Run()
        {
            HttpResponseMessage response = Client.GetAsync(commentsEndpoint).Result;
            Assert.IsTrue(response.IsSuccessStatusCode, $"Current status code is {response.StatusCode.ToString()}");

            List <CommentsModel> parsedResponse = JsonConvert.DeserializeObject<List<CommentsModel>>(response.Content.ReadAsStringAsync().Result);

            Assert.True(parsedResponse.Any(r => r.Body.Contains(bodySnipet)
                    && r.Email.Equals(email)));
        }
    }
}
