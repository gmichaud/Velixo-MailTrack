using System;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack
{
    public class CREmailActivityMaintExt : PXGraphExtension<CREmailActivityMaint>
    {
        public PXSelect<VXEMailEvent, Where<VXEMailEvent.noteID, Equal<Current<CRSMEmail.noteID>>>, OrderBy<Desc<VXEMailEvent.eventDate>>> Events;
    }
}