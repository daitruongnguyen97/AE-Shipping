using Shipping.Application.Features.Ship.Queries.GetShipByIdQuery;
using Shipping.Application.Features.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;
using PortEntity = Shipping.Domain.Model.Port;
using NetTopologySuite.Geometries;

namespace Shipping.Application.Features.Port.Queries.GetClosetPortQuery
{
    public class GetClosetPortQueryHandler : IRequestHandler<GetClosetPortQuery, ArrivalVm>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private readonly ILinqRepository<PortEntity> _portRepository;
        private readonly IMapper _mapper;

        public GetClosetPortQueryHandler(ILinqRepository<ShipEntity> shipRepository, IMapper mapper, ILinqRepository<PortEntity> portRepository)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
            _portRepository = portRepository ?? throw new ArgumentNullException(nameof(portRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ArrivalVm> Handle(GetClosetPortQuery request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.ShipId);
            DateTime? arrivalTime = null;
            var result = new ArrivalVm();

            var ship = _shipRepository.List(s => s.Id == guid).FirstOrDefault();
            if(ship == null)
            {
                throw new Exception("Ship is not existing");
            }

            var ports = await _portRepository.List().OrderBy(s => s.Geolocation.Distance(ship.Geolocation)).ToListAsync(cancellationToken);
            
            foreach(var port in ports)
            {
                if(IsShorterThanAWeek(port.Geolocation, ship, out arrivalTime))
                {
                    result.Port = _mapper.Map<PortVm>(port);
                    result.EstimatedArrivalTime = arrivalTime.Value;
                    break;
                }
            }

            if (arrivalTime == null)
            {
                throw new Exception("There is no Port available nearby");
            }
            return result;
        }

        private bool IsShorterThanAWeek(Point port, ShipEntity ship, out DateTime? arrivalTime)
        {
            var distance = port.Distance(ship.Geolocation);
            var estimated = distance / ship.Velocity;
            arrivalTime = null; 
            if (estimated >= 168)
            {
                return false;
            }
            arrivalTime = DateTime.UtcNow.AddHours(estimated);
            return true;

        }
    }
}
