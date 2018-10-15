using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Helpers;
using PortalTask.SerializationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PortalTask.Tests
{
    public class TODOsTest : BaseTest
    {
        private const string usersName1 = "Leanne Graham";
        private const string usersName2 = "Ervin Howell";

        public override void Run()
        {
            int? todosUserId1 = null;
            int? todosUserId2 = null;

            int? completedNumberUser1 = null;
            int? completedNumberUser2 = null;

            var client = new HttpClient { BaseAddress = new Uri(BaseUlr) };

            //getting the "todosUserId1' that corresponds to 'id' in Users request
            HttpResponseMessage responseUsers = client.GetAsync("users").Result;
            Assert.IsTrue(responseUsers.IsSuccessStatusCode, $"Current status code is {responseUsers.StatusCode.ToString()}");

            List<UsersModel> parsedUsersResponse = JsonConvert.DeserializeObject<List<UsersModel>>(responseUsers.Content.ReadAsStringAsync().Result);

            todosUserId1 = parsedUsersResponse.FirstOrDefault(u => u.Name.Equals(usersName1)).Id;

            //getting the response for albums that "todosUserId1' has
            HttpResponseMessage responseTODOs1 = client.GetAsync($"todos/?userId={todosUserId1}").Result;
            Assert.IsTrue(responseTODOs1.IsSuccessStatusCode, $"Current status code is {responseTODOs1.StatusCode.ToString()}");

            List<TODOsModel> parsedTODOsResponse1 = JsonConvert.DeserializeObject<List<TODOsModel>>(responseTODOs1.Content.ReadAsStringAsync().Result);


            //getting the "todosUserId2' that corresponds to 'id' in Users request
            todosUserId2 = parsedUsersResponse.FirstOrDefault(u => u.Name.Equals(usersName2)).Id;

            //getting the response for albums that "todosUserId1' has
            HttpResponseMessage responseTODOs2 = client.GetAsync($"todos/?userId={todosUserId2}").Result;
            Assert.IsTrue(responseTODOs1.IsSuccessStatusCode, $"Current status code is {responseTODOs1.StatusCode.ToString()}");

            List<TODOsModel> parsedTODOsResponse2 = JsonConvert.DeserializeObject<List<TODOsModel>>(responseTODOs2.Content.ReadAsStringAsync().Result);

            //evaluation of given results
            completedNumberUser1 = parsedTODOsResponse1.Count(t => t.Completed);
            Reporter.LogInfo($"Number of completed TODOs for {usersName1} is: {completedNumberUser1}");
            
            completedNumberUser2 = parsedTODOsResponse2.Count(t => t.Completed);
            Reporter.LogInfo($"Number of completed TODOs for {usersName2} is: {completedNumberUser2}");

            Assert.True((completedNumberUser1 - completedNumberUser2) >= 3, $"{usersName1} doesn't have more than 3 TODOos comparing to {usersName2}");
        }
    }
}
