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
            builder.RegisterType<Webhook.Mailgun.EventRequestHandler>().SingleInstance();
            builder.RegisterType<Webhook.Notifications.SubscribeRequestHandler>().SingleInstance();
            builder.ActivateOnApplicationStart<Webhook.RouteInitializer>(e => e.InitializeRoutes());

            builder
                .RegisterInstance(new LocationSettings
                {
                    Path = "/" + Webhook.RouteInitializer.MailTrackSendGridEventRoute,
                    Providers =
                    {
                        //Called by SendGrid, which will pass credentials as basic HTTP auth
                        new ProviderSettings
                        {
                            Name = "basic",
                            Type = typeof (PX.SM.PXBasicAuthenticationModule).AssemblyQualifiedName
                        }
                    }
                });

            builder
               .RegisterInstance(new LocationSettings
               {
                   Path = "/" + Webhook.RouteInitializer.MailTrackMailgunEventRoute,
                   Providers =
                   {
                        //Called by SendGrid, which will pass credentials as basic HTTP auth
                        new ProviderSettings
                        {
                            Name = "basic",
                            Type = typeof (PX.SM.PXBasicAuthenticationModule).AssemblyQualifiedName
                        }
                   }
               });

            builder
                .RegisterInstance(new LocationSettings
                {
                    Path = "/" + Webhook.RouteInitializer.MailTrackNotificationSubscribeRoute,
                    Providers =
                    {
                        //Called by the MailTrackPushSetup.html page for a logged in user 
                        new ProviderSettings
                        {
                            Name = "coockie",
                            Type = typeof (CoockieAuthenticationModule).AssemblyQualifiedName
                        },
                        new ProviderSettings
                        {
                            Name = "anonymous",
                            Type = typeof (AnonymousAuthenticationModule).AssemblyQualifiedName
                        }
                    }
                });
        }
    }
}
