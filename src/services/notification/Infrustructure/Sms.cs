using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public class Sms : IReceiverInfo
    {
        public string Receiver { get; set; }
        public string Data { get; set; }
    }
}
