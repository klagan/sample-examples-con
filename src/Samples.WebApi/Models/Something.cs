using System.Collections.Generic;
using Samples.WebApi.Contracts;

namespace Samples.WebApi.Models
{
    public class SomeModel : ILinks
    {
        public SomeModel()
        {
            Links = new List<LinkModel>();
        }

        public string Name { get; set; }

        public string Data { get; set; }

        public List<LinkModel> Links { get; set; }
    }
}
