using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace VX.MailTrack.Webhook
{
    public class RouteInitializer
    {
        private readonly ILifetimeScope _container;
        internal const string MailTrackEventRoute = "MailTrack/event";
        internal const string MailTrackNotificationSubscribeRoute = "MailTrack/subscribe";
        internal const string MailTrackRedirectRoute = "MailTrack/redirect";

        public RouteInitializer(ILifetimeScope container)
        {
            _container = container;
        }

        public void InitializeRoutes()
        {
            RouteTable.Routes.Add(new Route($"{MailTrackEventRoute}", _container.Resolve<RouteHandler<SendGrid.EventRequestHandler>>()));
            RouteTable.Routes.Add(new Route($"{MailTrackNotificationSubscribeRoute}", _container.Resolve<RouteHandler<Notifications.SubscribeRequestHandler>>()));
        }
    }
}
