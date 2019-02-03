using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Samples.WebApi.Tests
{
    public class BasicTest
    {
        private readonly HttpClient _client;

        public BasicTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TestStartup>());

            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async System.Threading.Tasks.Task TestGet(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "api/values/");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async System.Threading.Tasks.Task TestGetById(string method, int? id)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/values/{id}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
