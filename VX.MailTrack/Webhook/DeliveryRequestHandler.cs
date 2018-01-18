using Autofac;
using Newtonsoft.Json;
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
    class DeliveryRequestHandler : IHttpHandler
    {
        private readonly ILifetimeScope _container;

        public DeliveryRequestHandler(ILifetimeScope container)
        {
            _container = container;
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            string jsonString = null;

            context.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            var deliveryData = JsonConvert.DeserializeObject<DeliveryData>(jsonString);

            context.Response.StatusCode = 204; //No data;
            context.Response.End();
        }
    }
}
