using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public interface IReceiverInfo
    {
        string Receiver {  get; set; }
        string Data { get; set; }
    }
}
