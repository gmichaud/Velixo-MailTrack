﻿using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PX.Data;
using PX.Objects.CR;

namespace VX.MailTrack.Webhook.SendGrid
{
    class EventRequestHandler : WebhookRequestHandlerBase<List<SendGridEvent>>
    {
        public override void ProcessRequest(List<SendGridEvent> requestData)
        {
            var graph = PXGraph.CreateInstance<EmailEventProcess>();

            foreach(var sgevent in requestData)
            {
                ProcessEvent(graph, sgevent);
            }
        }

        private void ProcessEvent(EmailEventProcess graph, SendGridEvent e)
        {
            if (!String.IsNullOrEmpty(e.MessageID) && !String.IsNullOrEmpty(e.SendGridMessageID))
            {
                //Create link between internal Message ID and SendGrid MessageID for future events that will not include our own Message ID.
                VXEmailID association = graph.RemoteMessageIds.Select(e.SendGridMessageID);
                if(association == null)
                {
                    association = new VXEmailID();
                    association.RemoteMessageID = e.SendGridMessageID;
                    association.InternalMessageID = e.MessageID;
                    graph.RemoteMessageIds.Insert(association);
                    graph.Actions.PressSave();
                }
            }

            //Locate Acumatica e-mail
            SMEmail email = (SMEmail)graph.EmailByRemoteId.Select(e.SendGridMessageID);
            if(email == null)
            {
                PXTrace.WriteError("VX.MailTrack No e-mail message found in Acumatica for SendGrid MessageID " + e.SendGridMessageID);
                return;
            }
            
            //Insert event; duplicates are possible as per the SendGrid documentation, but since EventID is a key we will simply discard it when inserting
            var emailEvent = new VXEMailEvent();
            emailEvent.EventID = e.EventID;
            emailEvent.EventDate = UnixTimeStampToDateTime(e.Timestamp);
            emailEvent.NoteID = email.NoteID;

            var pushMessage = new PushHelper.PushMessage();
            pushMessage.NoteID = email.NoteID;
            pushMessage.RefNoteID = email.RefNoteID;
            pushMessage.Body = email.Subject;

            switch (e.EventType.ToLower())
            {
                case "dropped":
                    pushMessage.Title = $"Message Dropped {e.Email}";
                    emailEvent.EventType = MailEventType.Dropped;
                    emailEvent.Description = e.Reason;
                    break;
                case "delivered":
                    pushMessage.Title = $"Messages Delivered to {e.Email}";
                    emailEvent.EventType = MailEventType.Delivered;
                    break;
                case "bounce":
                    pushMessage.Title = $"Message Bounced {e.Email}";
                    emailEvent.EventType = MailEventType.Bounce;
                    emailEvent.Description = e.Reason;
                    break;
                case "open":
                    pushMessage.Title = $"Message Opened by {e.Email}";
                    emailEvent.EventType = MailEventType.Open;
                    emailEvent.Description = e.UserAgent;
                    break;
                case "click":
                    pushMessage.Title = $"Link Clicked by {e.Email}";
                    pushMessage.Body += $" ({ e.Url})";
                    emailEvent.EventType = MailEventType.Click;
                    emailEvent.Description = e.Url;
                    break;
                default:
                    emailEvent.EventType = MailEventType.Unknown;
                    emailEvent.Description = "Unhandled event type: " + e.EventType;
                    break;
            }
            
            graph.Events.Insert(emailEvent);
            graph.Actions.PressSave();
            
            foreach(VXUserPushNotification pushNotification in graph.PushNotifications.Select(email.CreatedByID))
            {
                //Just fire and forget
                var task = PushHelper.SendPushNotificationAsync(pushNotification, pushMessage);
            }
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeStamp);
        }
    }
}
