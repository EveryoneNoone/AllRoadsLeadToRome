﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public interface IReceiverInfo
    {
        public string? Id { get; set; }
        public string Receiver {  get; set; }
        public string Data { get; set; }
    }
}
