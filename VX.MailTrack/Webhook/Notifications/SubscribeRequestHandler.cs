using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Common;

namespace VX.MailTrack.Webhook.Notifications
{
    class SubscribeRequestHandler : RequestHandlerBase<SubscribeRequest>
    {
        public override void ProcessRequest(SubscribeRequest requestData)
        {
            if(requestData == null)
            {
                //Unsubscribe this user from *all* push notification
                PXDatabase.Delete<VXUserPushNotification>(
                    new PXDataFieldRestrict<VXUserPushNotification.userID>(PXAccess.GetUserID())
                    );
            }
            else
            {
                //Register endpoint. The page will always refresh its subscription on load.
                try
                {
                    PXDatabase.Insert<VXUserPushNotification>(
                        new PXDataFieldAssign<VXUserPushNotification.userID>(PXAccess.GetUserID()),
                        new PXDataFieldAssign<VXUserPushNotification.endpoint>(requestData.Endpoint),
                        new PXDataFieldAssign<VXUserPushNotification.authKey>(requestData.Keys["auth"]),
                        new PXDataFieldAssign<VXUserPushNotification.receiverKey>(requestData.Keys["p256dh"]));
                }
                catch (PXDatabaseException e)
                {
                    if (e.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
                    {
                        PXDatabase.Update<VXUserPushNotification>(
                            new PXDataFieldAssign<VXUserPushNotification.authKey>(requestData.Keys["auth"]),
                            new PXDataFieldAssign<VXUserPushNotification.receiverKey>(requestData.Keys["p256dh"]),
                            new PXDataFieldRestrict<VXUserPushNotification.userID>(PXAccess.GetUserID()),
                            new PXDataFieldRestrict<VXUserPushNotification.endpoint>(requestData.Endpoint));
                    }
                }
            }
        }
    }
}
