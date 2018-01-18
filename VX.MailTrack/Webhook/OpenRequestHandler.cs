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
    class OpenRequestHandler : PostmarkWebhookRequestHandlerBase<OpenData>
    {
        public override void ProcessRequest(OpenData requestData)
        {
            throw new NotImplementedException();
        }
    }
}
