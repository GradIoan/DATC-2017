using Newtonsoft.Json;

namespace Hal.Client
{
    class _links4
    {
        Self self;

        [JsonProperty("self")]
        internal Self Self
        {
            get
            {
                return self;
            }

            set
            {
                self = value;
            }
        }
    }
}