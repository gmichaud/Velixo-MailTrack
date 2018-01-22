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
        internal const string MailTrackRouteBase = "MailTrack";

        public RouteInitializer(ILifetimeScope container)
        {
            _container = container;
        }

        public void InitializeRoutes()
        {
            RouteTable.Routes.Add(new Route($"{MailTrackRouteBase}/event", _container.Resolve<RouteHandler<SendGrid.EventRequestHandler>>()));
        }
    }
}
