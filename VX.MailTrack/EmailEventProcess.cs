using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack
{
    public class EmailEventProcess : PXGraph<EmailEventProcess>
    {
        public PXSelectJoin<CRSMEmail, InnerJoin<VXEmailID, On<CRSMEmail.messageId, Equal<VXEmailID.internalMessageID>>>, Where<VXEmailID.remoteMessageID, Equal<Required<VXEmailID.remoteMessageID>>>> EmailByRemoteId;
        public PXSelect<CRSMEmail, Where<CRSMEmail.messageId, Equal<Required<CRSMEmail.messageId>>>> EmailById;

        public PXSelect<VXEMailEvent, Where<VXEMailEvent.noteID, Equal<Required<VXEMailEvent.noteID>>>> Events;
        public PXSelect<VXEmailID, Where<VXEmailID.remoteMessageID, Equal<Required<VXEmailID.remoteMessageID>>>> RemoteMessageIds;
        public PXSelect<VXUserPushNotification, Where<VXUserPushNotification.userID, Equal<Required<VXUserPushNotification.userID>>, 
            Or<VXUserPushNotification.userID, Equal<Required<VXUserPushNotification.userID>>>>> PushNotifications;
    }
}
