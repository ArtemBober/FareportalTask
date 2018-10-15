using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Requests;
using PortalTask.SerializationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PortalTask.Tests
{
    public class PostTitleTest : BaseTest
    {
        private const string postsTitleSnipet = "eos dolorem iste accusantium est eaque quam";
        private const string usersName = "Patricia Lebsack";

        public override void Run()
        {
            int? postsUserId = null;
            var client = new HttpClient { BaseAddress = new Uri(BaseUlr) };

            HttpResponseMessage responseUsers = client.GetAsync("users").Result;
            Assert.IsTrue(responseUsers.IsSuccessStatusCode, $"Current status code is {responseUsers.StatusCode.ToString()}");

            List<UsersModel> parsedUsersResponse = JsonConvert.DeserializeObject<List<UsersModel>>(responseUsers.Content.ReadAsStringAsync().Result);

            postsUserId = parsedUsersResponse.FirstOrDefault(u => u.Name.Equals(usersName)).Id;

            HttpResponseMessage responsePosts = client.GetAsync("posts").Result;
            Assert.IsTrue(responsePosts.IsSuccessStatusCode, $"Current status code is {responsePosts.StatusCode.ToString()}");

            List<PostsModel> parsedPostsResponse = JsonConvert.DeserializeObject<List<PostsModel>>(responsePosts.Content.ReadAsStringAsync().Result);

            Assert.True(parsedPostsResponse.Any
                (r => r.Title.Contains(postsTitleSnipet) && r.UserId == postsUserId));
        }
    }
}
