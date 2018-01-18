using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VX.MailTrack.Webhook
{
    class DeliveryData
    {
        /* Sample  
         {
            "ServerId": 23,
            "MessageID": "883953f4-6105-42a2-a16a-77a8eac79483",
            "Recipient": "john@example.com",
            "Tag": "welcome-email",
            "DeliveredAt": "2014-08-01T13:28:10.2735393-04:00",
            "Details": "Test delivery webhook details"
        }
        */

        public int ServerId;
        public Guid MessageID;
        public string Recipient;
        public string Tag;
        public DateTime DeliveredAt;
        public string Details;
    }
}
