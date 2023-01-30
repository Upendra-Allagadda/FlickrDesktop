using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Helper
{
    public class Feed
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime Modified { get; set; }

        public string Generator { get; set; }

        [JsonProperty("items")]
        public Photo[] Photos { get; set; }
    }
}
