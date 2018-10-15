namespace PortalTask.Base
{
    public abstract class BaseTest
    {
        protected const string BaseUlr = "http://jsonplaceholder.typicode.com/";
        //protected const string BaseUlrComments = "https://jsonplaceholder.typicode.com/comments/";
        //protected const string BaseUlrPhotos = "https://jsonplaceholder.typicode.com/photos/";
        //protected const string BaseUlrTodos = "https://jsonplaceholder.typicode.com/todos/";

        public abstract void Run();
    }
}
