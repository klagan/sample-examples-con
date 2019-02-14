using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Samples.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Samples.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ValuesController : ControllerBase
    {
        private IUrlHelper _urlHelper;

        public ValuesController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet]
        [SwaggerOperation(Description = "Default GET verb", OperationId="kamtest", Tags=new[]{"kamTag", "testTag"})]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public ActionResult<SomeModel> Get(int id)
        {
            var result = new SomeModel() {Name = "Kam"};

            var getUrl = Url.Link("GetById", new {id = id});

            result.Links.Add(new LinkModel(getUrl, "self", "GET"));
            
            Response.Headers.Add("location", getUrl);
            
            return Ok(result);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
