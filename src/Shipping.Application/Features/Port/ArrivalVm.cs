using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Port
{
    public class ArrivalVm
    {
        public PortVm Port { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
    }
}
