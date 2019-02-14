using System.Collections.Generic;
using Samples.WebApi.Models;

namespace Samples.WebApi.Contracts
{
    internal interface ILinks
    {
        List<LinkModel> Links { get; }
    }
}
