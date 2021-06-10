using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using asp.net.core.mvc.demo;
using asp.net.core.mvc.demo.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace asp.net.core.mvc.tests
{
    public class IntegrationTests
    {
        private WebApplicationFactory<Startup> webApplicationFactory;

        [SetUp]
        public void SetUp()
        {
            webApplicationFactory =
                new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => { });
                });
        }

        [TestCase("models/create")]
        [TestCase("/models/create")]
        public async Task givenValidPostPath_dontReturnNotFound(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                var model = new MLModel("name1", "type1", "version1", "status1");
                var content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.Default, "application/json");
                var response = await httpClient.PostAsync(path, content);
                Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [TestCase("models/read/name1")]
        [TestCase("/models/read/name1")]
        public async Task givenValidGetPath_dontReturnNotFound(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(path);
                Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [TestCase("models/update")]
        [TestCase("/models/update")]
        public async Task givenValidPutPath_dontReturnNotFound(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                var model = new MLModel("name1", "type1", "version1", "status1");
                var content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.Default, "application/json");
                var response = await httpClient.PutAsync(path, content);
                Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [TestCase("models/delete/name1")]
        [TestCase("/models/delete/name1")]
        public async Task givenValidDeletePath_dontReturnNotFound(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                var response = await httpClient.DeleteAsync(path);
                Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task givenInvalidGetPath_returnNotFound()
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("/model/read");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [TestCase("models/create")]
        public async Task givenBadPostModel_returnBadRequest(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                MLModel model = null;
                var content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.Default, "application/json");
                var response = await httpClient.PostAsync(path, content);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
            }
        }

        [TestCase("models/update")]
        public async Task givenBadPutModel_returnBadRequest(string path)
        {
            using (var httpClient = webApplicationFactory.CreateClient())
            {
                MLModel model = null;
                var content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.Default, "application/json");
                var response = await httpClient.PutAsync(path, content);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
            }
        }
    }
}
