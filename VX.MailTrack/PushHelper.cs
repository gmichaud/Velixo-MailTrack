using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPush;

namespace VX.MailTrack
{
    static class PushHelper
    {
        public static async Task SendPushNotificationAsync(VXUserPushNotification pushNotification, string message)
        {
            var subject = @"mailto:example@example.com";
            const string publicKey = @"BAHU3QNr2PvPSufR9hKYgV7daemnktvh6AYqBqta9brwWK6S064nzJQCadUT4LD3fyJMs5FiKh9uW_v_t4aU4eI";
            const string privateKey = @"hoIs3IDTJvGbPUgK2qhwFuREX5deqKhBVNAN0a70_dg";

            var subscription = new PushSubscription(pushNotification.Endpoint, pushNotification.ReceiverKey, pushNotification.AuthKey);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            var webPushClient = new WebPushClient();
            try
            {
                await webPushClient.SendNotificationAsync(subscription, message, vapidDetails);
            }
            catch (AggregateException ae) //Gone -> delete
            {
                ae.Handle((ex) =>
                {
                    if (ex is WebPushException)
                    {
                        var xpe = (WebPushException)ex;
                        switch (xpe.StatusCode)
                        {
                            case System.Net.HttpStatusCode.Gone:
                                //TODO: Delete endpoint, it's no longer allowed
                                return true;
                            default:
                                //TODO: Log failure
                                return false;
                        }
                    }

                    return false; // Let anything other exception stop the application.
                });
            }
        }
    }
}
