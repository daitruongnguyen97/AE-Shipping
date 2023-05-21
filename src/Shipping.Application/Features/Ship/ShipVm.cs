﻿using Shipping.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship
{
    public class ShipVm
    {
        public string Name { get; set; }
        public Location Geolocation { get; set; }
        public long Velocity { get; set; }
    }
}
