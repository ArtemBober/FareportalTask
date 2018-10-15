using Newtonsoft.Json;
using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net;
using System.Collections;
using System.Drawing;
using System.IO;
using PortalTask.Helpers;
using System.Text;

namespace PortalTask.Tests
{
    public class ImageBinaryComparisonTest : BaseTest
    {
        private const int photoId = 4;
        private const string photoPath = @"D:\d32776.png";

        public override void Run()
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseUlr) };
            var webClient = new WebClient();

            //geting url for photo from response for relevant photoId
            HttpResponseMessage responsePhotos = client.GetAsync("photos").Result;
            Assert.IsTrue(responsePhotos.IsSuccessStatusCode, $"Current status code is {responsePhotos.StatusCode.ToString()}");

            List<PhotosModel> parsedPhotosResponse = JsonConvert.DeserializeObject<List<PhotosModel>>(responsePhotos.Content.ReadAsStringAsync().Result);

            var photoURL = parsedPhotosResponse.First(p => p.Id == photoId).Url;

            //downloading the photo to byte array from the given link
            byte[] photoURLBytes = webClient.DownloadData(photoURL);

            //reading the image from file and converting it into byte array
            byte[] photoFromFile = File.ReadAllBytes(photoPath);

            //evaluating of the results
            Assert.IsTrue(ByteArrayCompare(photoURLBytes, photoFromFile), "Photos are different");
        }

        private bool ByteArrayCompare(byte[] a1, byte[] a2)
        => StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
    }
}
