namespace Samples.WebApi.Models
{
    public class LinkModel
    {
        public LinkModel (string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }

        public string Href { get; }
        public string Rel { get; }
        public string Method { get; }
    }
}
