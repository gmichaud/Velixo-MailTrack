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
    class DeliveryRequestHandler : PostmarkWebhookRequestHandlerBase<DeliveryData>
    {
        public override void ProcessRequest(DeliveryData requestData)
        {

            //Test with curl:
            /*
             curl http://admin%40panova:admin@198.48.200.206:8888/AcumaticaDemo60/mailtrack/delivery \
              -X POST \
              -H "Content-Type: application/json" \
              -d '{ "ServerID": 23, "MessageID": "883953f4-6105-42a2-a16a-77a8eac79483", "Recipient": "john@example.com", "Tag": "welcome-email", "DeliveredAt": "2014-08-01T13:28:10.2735393-04:00", "Details": "Test delivery webhook details" }'
            */
        }
    }
}
