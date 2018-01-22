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
        public PXSelectJoin<SMEmail, InnerJoin<VXEmailID, On<SMEmail.messageId, Equal<VXEmailID.internalMessageID>>>, Where<VXEmailID.remoteMessageID, Equal<Required<VXEmailID.remoteMessageID>>>> EmailByRemoteId;
        public PXSelect<VXEMailEvent, Where<VXEMailEvent.noteID, Equal<Required<VXEMailEvent.noteID>>>> Events;
        public PXSelect<VXEmailID, Where<VXEmailID.remoteMessageID, Equal<Required<VXEmailID.remoteMessageID>>>> RemoteMessageIds;
    }
}
