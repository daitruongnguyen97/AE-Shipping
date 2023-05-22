using Shipping.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Port
{
    public class PortVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Geolocation { get; set; }
    }
}
