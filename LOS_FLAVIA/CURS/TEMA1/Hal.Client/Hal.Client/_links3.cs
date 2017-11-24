using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class _links3
    {
        Self self;
        Next next;
        Page[] page;
        Beer[] beer;



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

        [JsonProperty("next")]
        internal Next Next
        {
            get
            {
                return next;
            }

            set
            {
                next = value;
            }
        }

        [JsonProperty("page")]
        internal Page[] Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }
        [JsonProperty("beer")]
        internal Beer[] Beer
        {
            get
            {
                return beer;
            }

            set
            {
                beer = value;
            }
        }
    }
}
