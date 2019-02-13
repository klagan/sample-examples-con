using System;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace Samples.WebApi.Integration.Tests
{
    [Binding]
    public class GivenASampleTestSteps
    {
        [Given(@"a working webapi")]
        public void GivenAWorkingWebapi()
        {
            //ScenarioContext.Current.Pending();
        }

        [When(@"I call GET with a value of (.*)")]
        public async System.Threading.Tasks.Task WhenICallGETWithAValueOfAsync(int p0)
        {
            var client = new HttpClient() { BaseAddress = new Uri("http://localhost:55951/") };

            var response = await client.GetAsync("/api/v1/values");

            ScenarioContext.Current.Add("response", response);
        }

        [Then(@"I receive a response of HTTP (.*) OK")]
        public async System.Threading.Tasks.Task ThenIReceiveAResponseOfHTTPOKAsync(int p0)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>("response");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
