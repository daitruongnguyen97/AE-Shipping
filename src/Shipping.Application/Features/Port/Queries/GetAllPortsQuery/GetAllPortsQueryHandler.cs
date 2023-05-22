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

namespace Shipping.Application.Features.Port.Queries.GetAllPortsQuery
{
    public class GetAllPortsQueryHandler : IRequestHandler<GetAllPortsQuery, List<PortVm>>
    {
        private readonly ILinqRepository<PortEntity> _portRepository;
        private readonly IMapper _mapper;

        public GetAllPortsQueryHandler(IMapper mapper, ILinqRepository<PortEntity> portRepository)
        {
            _portRepository = portRepository ?? throw new ArgumentNullException(nameof(portRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<PortVm>> Handle(GetAllPortsQuery request, CancellationToken cancellationToken)
        {
            var query = _portRepository.List().ToList();
            var result = _mapper.Map<List<PortVm>>(query);
            return result;
        }
    }
}
