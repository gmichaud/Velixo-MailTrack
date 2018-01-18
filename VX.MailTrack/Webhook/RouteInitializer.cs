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
            RouteTable.Routes.Add(new Route($"{MailTrackRouteBase}/delivery", _container.Resolve<RouteHandler<DeliveryRequestHandler>>()));
            RouteTable.Routes.Add(new Route($"{MailTrackRouteBase}/bounce", _container.Resolve<RouteHandler<BounceRequestHandler>>()));
            RouteTable.Routes.Add(new Route($"{MailTrackRouteBase}/open", _container.Resolve<RouteHandler<OpenRequestHandler>>()));
            RouteTable.Routes.Add(new Route($"{MailTrackRouteBase}/click", _container.Resolve<RouteHandler<ClickRequestHandler>>()));
        }
    }
}
