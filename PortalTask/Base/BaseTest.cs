using System;
using System.Net.Http;

namespace PortalTask.Base
{
    public abstract class BaseTest
    {
        protected const string BaseUlr = "http://jsonplaceholder.typicode.com/";
        protected const string postsEndpoint = "posts";
        protected const string commentsEndpoint = "comments";
        protected const string photosEndpoint = "photos";
        protected const string usersEndpoint = "users";
        protected const string albumsEndpoint = "albums";
        protected const string todosEndpoint = "todos";


        protected HttpClient Client { get; set; }


        protected BaseTest()
        {
            Client = new HttpClient { BaseAddress = new Uri(BaseUlr) };
        }

        public abstract void Run();
    }
}
