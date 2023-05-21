
using NetTopologySuite.Geometries;
using Shipping.Application.Features.Port;
using Shipping.Application.Features.Ship;
using Shipping.Application.Features.Ship.Commands.CreateShipCommand;
using Shipping.Application.Features.Ship.Commands.UpdateShipCommand;
using Shipping.Domain.Model;
using Location = Shipping.Domain.Model.Location;

namespace Shipping.Application.Mapping;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Ship, CreateShipCommand>()
        .ReverseMap()
        .ForMember(dest => dest.CreatedDate, act => act.MapFrom(s => DateTime.UtcNow))
        .ForMember(dest => dest.UpdatedDate, act => act.MapFrom(s => DateTime.UtcNow))
        .ForMember(dest => dest.Geolocation, act => act.MapFrom(s => new Point(s.Geolocation.Longitude, s.Geolocation.Latitude) { SRID = 4326 }))
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Ship, UpdateShipCommand>()
        .ReverseMap()
        .ForMember(dest => dest.UpdatedDate, act => act.MapFrom(s => DateTime.UtcNow))
        .ForMember(dest => dest.Geolocation, act => act.MapFrom(s => s.Geolocation == null ? null : new Point(s.Geolocation.Longitude, s.Geolocation.Latitude) { SRID = 4326 }))
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Ship, ShipVm>()
        .ForMember(dest => dest.Geolocation, act => act.MapFrom(s => new Location() {Longitude = s.Geolocation.X, Latitude = s.Geolocation.Y }))
        .ReverseMap()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Port, PortVm>()
        .ForMember(dest => dest.Geolocation, act => act.MapFrom(s => new Location() { Longitude = s.Geolocation.X, Latitude = s.Geolocation.Y }))
        .ReverseMap()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

}