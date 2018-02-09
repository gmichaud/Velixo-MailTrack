using System;
using System.Collections;
using PX.Data;
using PX.SM;

namespace VX.MailTrack
{
    public class SMAccessPersonalMaintExt : PXGraphExtension<SMAccessPersonalMaint>
    {
        public PXAction<Users> PushNotifications;
        [PXButton]
        [PXUIField(DisplayName = "Configure Push Notifications")]
        public void pushNotifications()
        {
            throw new PXRedirectToUrlException("~/Frames/MailTrackPushSetup.aspx", PXBaseRedirectException.WindowMode.NewWindow, "Push Notifications");
        }
    }
}