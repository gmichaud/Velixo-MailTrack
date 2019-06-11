using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VX.MailTrack
{
    public static class Common
    {
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeStamp);
        }
    }
}
