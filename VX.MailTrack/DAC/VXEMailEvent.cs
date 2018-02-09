using System;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack
{
    public class MailEventType
    {
        public const string Dropped = "DR";
        public const string Delivered = "DE";
        public const string Bounce = "BO";
        public const string Open = "OP";
        public const string Click = "CL";
        public const string Unknown = "ZZ";
    }

    [Serializable]
    public class VXEMailEvent : IBqlTable
    {
        public class eventID : IBqlField { }
        [PXDBString(64, IsKey = true)]
        public string EventID { get; set; }

        public class noteID : IBqlField { }
        [PXDBGuid]
        public Guid? NoteID { get; set; }

        public class eventType : IBqlField { }
        [PXDBString(2, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Event Type", Enabled = false)]
        [PXDefault]
        [PXStringList(new string[] { MailEventType.Dropped, MailEventType.Delivered, MailEventType.Bounce, MailEventType.Open, MailEventType.Click, MailEventType.Unknown }, new string[] { "Dropped", "Delivered", "Bounce", "Open", "Click", "Unknown" })]
        public string EventType { get; set; }

        public class eventDate : IBqlField { }
        [PXDBDateAndTime]
        [PXDefault]
        [PXUIField(DisplayName = "Event Date", Enabled = false)]
        public DateTime? EventDate { get; set; }

        public class email : IBqlField { }
        [PXDBString(255, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Email", Enabled = false)]
        public string EMail { get; set; }

        public class description : IBqlField { }
        [PXDBString(255, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Description", Enabled = false)]
        public string Description { get; set; }
    }
}