using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VX.MailTrack.Webhook.SendGrid
{
    class SendGridEvent
    {
        [JsonProperty("sg_event_id")]
        public string EventID { get; set; }
        [JsonProperty("event")]
        public string EventType { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("sg_message_id")]
        public string SendGridMessageID { get; set; }
        [JsonProperty("smtp-id")]
        public string MessageID { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("useragent")]
        public string UserAgent { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
