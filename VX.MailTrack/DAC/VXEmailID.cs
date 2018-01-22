using System;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack
{
    [Serializable]
    public class VXEmailID : IBqlTable
    {
        public class remoteMessageID : IBqlField { }
        [PXDBString(255, IsKey = true)]
        public string RemoteMessageID { get; set; }

        public class internalMessageID : IBqlField { }
        [PXDBString(255)]
        public string InternalMessageID { get; set; }
    }
}