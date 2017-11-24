using Newtonsoft.Json;

namespace Hal.Client
{
     class Beer
    {
        string href;

        [JsonProperty("href")]
        public string Href
        {
            get
            {
                return href;
            }

            set
            {
                href = value;
            }
        }
    }
}