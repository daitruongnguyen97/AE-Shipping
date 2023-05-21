using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship.Queries.GetShipByIdQuery
{
    public class GetShipByIdQuery : IRequest<ShipVm>
    {
        public GetShipByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get;set; }
    }
}
