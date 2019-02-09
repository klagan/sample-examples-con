using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Samples.WebApi.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Description = "Default GET verb", OperationId="kamtest", Tags=new[]{"kamTag", "testTag"})]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("newget")]
        [SwaggerOperation(Description = "New GET verb", OperationId = "newGet", Tags = new[] {"kamTag", "testTag"})]
        public ActionResult<string> NewGet()
        {
            return "newGet response";
        }


        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            return 999;
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
