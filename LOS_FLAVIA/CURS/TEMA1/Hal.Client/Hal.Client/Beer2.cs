using Newtonsoft.Json;

namespace Hal.Client
{
    internal class Beer2
    {
        string id;
        string name;
        _links4 link;

        [JsonProperty("id")]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        [JsonProperty("_links")]
        internal _links4 Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }
    }
}