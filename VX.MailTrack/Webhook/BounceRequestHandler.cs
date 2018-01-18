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
    class BounceRequestHandler : PostmarkWebhookRequestHandlerBase<BounceData>
    {
        public override void ProcessRequest(BounceData requestData)
        {
            throw new NotImplementedException();
        }
    }
}
