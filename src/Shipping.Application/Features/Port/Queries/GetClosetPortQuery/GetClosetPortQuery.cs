using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Port.Queries.GetClosetPortQuery
{
    public class GetClosetPortQuery : IRequest<ArrivalVm>
    {
        public string ShipId { get; set; }
        public GetClosetPortQuery(string id)
        {
            ShipId = id;
        }
    }
}
