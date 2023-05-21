using Domain.Common;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Domain.Model
{
    public class Ship : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Point Geolocation { get; set; }
        [Required]
        public long Velocity { get; set; }
    }
}
