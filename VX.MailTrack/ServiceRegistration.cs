using Autofac;
using PX.Data.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PX.Export.Authentication;

namespace VX.MailTrack
{
    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Webhook.RouteHandler<>)).InstancePerDependency();
            builder.RegisterType<Webhook.SendGrid.EventRequestHandler>().SingleInstance();
            builder.ActivateOnApplicationStart<Webhook.RouteInitializer>(e => e.InitializeRoutes());

            builder
                .RegisterInstance(new LocationSettings
                {
                    Path = "/" + Webhook.RouteInitializer.MailTrackRouteBase,
                    Providers =
                    {
                        new ProviderSettings
                        {
                            Name = "basic",
                            Type = typeof (PX.SM.PXBasicAuthenticationModule).AssemblyQualifiedName
                        }
                    }
                });
        }
    }
}
