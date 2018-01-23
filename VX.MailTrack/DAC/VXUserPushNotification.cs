using System;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack
{
    [Serializable]
    public class VXUserPushNotification : IBqlTable
    {
        public abstract class userID : IBqlField { }
        [PXDBGuid(false, IsKey = true)]
        [PXUIField(DisplayName = "User ID", Visible = false)]
        [PXDefault(typeof(AccessInfo.userID))]
        public Guid? UserID { get; set; }
        
        public abstract class endpoint : IBqlField { }
        [PXDBString(255, IsKey = true)]
        [PXUIField(DisplayName ="Endpoint")]
        public string Endpoint { get; set; }

        public abstract class receiverKey : IBqlField { }
        [PXDBString(255)]
        [PXUIField(DisplayName = "ReceiverKey")]
        public string ReceiverKey { get; set; }

        public abstract class authKey : IBqlField { }
        [PXDBString(255)]
        [PXUIField(DisplayName = "AuthKey")]
        public string AuthKey { get; set; }
    }
}
