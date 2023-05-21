using Shipping.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship.Commands.UpdateShipCommand
{
    public class UpdateShipCommand : IRequest<string>
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Geolocation { get; set; }
        public long? Velocity { get; set; }
    }
}
