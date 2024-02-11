using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal enum OrderStatus : int
    {
        Create = 1,
        InProgress = 2,
        Done = 3
    }
}
