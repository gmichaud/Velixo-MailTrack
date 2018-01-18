using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VX.MailTrack.Webhook
{
    class BounceRequestHandler : IHttpHandler
    {
        private readonly ILifetimeScope _container;

        public BounceRequestHandler(ILifetimeScope container)
        {
            _container = container;
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("<H1>Bounce</H1>");
        }
    }
}
