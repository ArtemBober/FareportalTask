using PortalTask.Base;
using System;
using NUnit.Framework;
using PortalTask.Requests;
using PortalTask.SerializationModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace PortalTask.Tests
{
    public class PhotoWithTitleTest : BaseTest
    {
        private const string photoTitleSnipet = "ad et natus qui";
        private const string usersEmail = "Sincere@april.biz";

        public override void Run()
        {
            int? albumUserId = null;

            //getting the "albumUserId' that corresponds to 'id' in Users request
            HttpResponseMessage responseUsers = Client.GetAsync(usersEndpoint).Result;
            Assert.IsTrue(responseUsers.IsSuccessStatusCode, $"Current status code is {responseUsers.StatusCode.ToString()}");

            List<UsersModel> parsedUsersResponse = JsonConvert.DeserializeObject<List<UsersModel>>(responseUsers.Content.ReadAsStringAsync().Result);

            albumUserId = parsedUsersResponse.FirstOrDefault(u => u.Email.Equals(usersEmail)).Id;

            //getting the response for albums that "albumUserId' has
            HttpResponseMessage responseAlbums = Client.GetAsync($"{albumsEndpoint}/?userId={albumUserId}").Result;
            Assert.IsTrue(responseAlbums.IsSuccessStatusCode, $"Current status code is {responseAlbums.StatusCode.ToString()}");

            List<AlbumsModel> parsedAlbumsResponse = JsonConvert.DeserializeObject<List<AlbumsModel>>(responseAlbums.Content.ReadAsStringAsync().Result);

            //getting response for Photos and evaluate the given results
            HttpResponseMessage responsePhotos = Client.GetAsync(photosEndpoint).Result;
            Assert.IsTrue(responsePhotos.IsSuccessStatusCode, $"Current status code is {responsePhotos.StatusCode.ToString()}");

            List<PhotosModel> parsedPhotosResponse = JsonConvert.DeserializeObject<List<PhotosModel>>(responsePhotos.Content.ReadAsStringAsync().Result);

            Assert.True(parsedPhotosResponse.Any(p => p.AlbumId == albumUserId && p.Title.Equals(photoTitleSnipet)));
        }
    }
}
