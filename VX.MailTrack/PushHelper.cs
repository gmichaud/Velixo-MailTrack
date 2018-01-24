using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using WebPush;

namespace VX.MailTrack
{
    static class PushHelper
    {
        public class PushMessage
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
            [JsonProperty("noteID")]
            public Guid? NoteID { get; set; }
            [JsonProperty("refNoteID")]
            public Guid? RefNoteID { get; set; }
        }

        public static async Task SendPushNotificationAsync(VXUserPushNotification pushNotification, PushMessage pushMessage)
        {
            var subject = @"mailto:info@velixo.com";
            const string publicKey = @"BAHU3QNr2PvPSufR9hKYgV7daemnktvh6AYqBqta9brwWK6S064nzJQCadUT4LD3fyJMs5FiKh9uW_v_t4aU4eI";
            const string privateKey = @"hoIs3IDTJvGbPUgK2qhwFuREX5deqKhBVNAN0a70_dg";

            var subscription = new PushSubscription(pushNotification.Endpoint, pushNotification.ReceiverKey, pushNotification.AuthKey);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            var webPushClient = new WebPushClient();
            try
            {
                await webPushClient.SendNotificationAsync(subscription, JsonConvert.SerializeObject(pushMessage), vapidDetails);
            }
            catch (AggregateException ae)
            {
                ae.Handle((ex) =>
                {
                    if (ex is WebPushException)
                    {
                        var xpe = (WebPushException)ex;
                        switch (xpe.StatusCode)
                        {
                            case System.Net.HttpStatusCode.Gone:
                                //TODO: Delete endpoint from VXUserPushNotification
                                return true;
                            default:
                                PXTrace.WriteError(ex);
                                return false;
                        }
                    }

                    PXTrace.WriteError(ex);
                    return false;
                });
            }
        }
    }
}
