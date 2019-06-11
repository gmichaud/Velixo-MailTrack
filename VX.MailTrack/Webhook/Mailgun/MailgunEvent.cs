using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VX.MailTrack.Webhook.Mailgun
{
    [JsonConverter(typeof(JsonPathConverter))]
    class MailgunEvent
    {
        [JsonProperty("event-data.event")]
        public string EventType { get; set; }

        [JsonProperty("event-data.id")]
        public string EventID { get; set; }

        [JsonProperty("event-data.message.headers.message-id")]
        public string MessageID { get; set; }

        [JsonProperty("event-data.recipient")]
        public string Email { get; set; }

        [JsonProperty("event-data.client-info.user-agent")]
        public string UserAgent { get; set; }

        [JsonProperty("event-data.url")]
        public string Url { get; set; }

        [JsonProperty("event-data.delivery-status.description")]
        public string DeliveryStatusDescription { get; set; }

        [JsonProperty("event-data.delivery-status.message")]
        public string DeliveryStatusMessage { get; set; }

        [JsonProperty("event-data.severity")]
        public string Severity { get; set; }

        [JsonProperty("event-data.timestamp")]
        public long Timestamp { get; set; }
    }
}
