using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Port.Queries.GetPortById
{
    public class GetPortById : IRequest<PortVm>
    { 
        public string Id { get; set; }
        public GetPortById(string id)
        {
            Id = id;
        }
    }
}
