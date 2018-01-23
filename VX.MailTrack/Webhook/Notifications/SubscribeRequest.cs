using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VX.MailTrack.Webhook.Notifications
{
    class SubscribeRequest
    {
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
        [JsonProperty("keys")]
        public IDictionary<string, string> Keys { get; set; }
    }
}
