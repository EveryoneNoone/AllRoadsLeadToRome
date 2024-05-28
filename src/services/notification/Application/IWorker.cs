using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal interface IWorker
    {
        public void AddTemplate(string template);
        public void UpdateTemplate(string template);
        public void RemoveTemplate(string template);
        public void Send(IReceiverInfo receiverInfo);
    }
}
