using Shipping.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship.Commands.DeleteShipCommand
{
    public class DeleteShipCommand : IRequest<Unit>
    {
        public List<string> Ids { get; set; }
        public DeleteShipCommand(List<string> ids)
        {
            Ids = ids;
        }
    }
}
