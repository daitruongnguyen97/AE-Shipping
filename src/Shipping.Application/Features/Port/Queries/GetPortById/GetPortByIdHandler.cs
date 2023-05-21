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

namespace Shipping.Application.Features.Port.Queries.GetPortById
{
    public class GetPortByIdHandler : IRequestHandler<GetPortById, PortVm>
    {
        private readonly ILinqRepository<PortEntity> _portRepository;
        private readonly IMapper _mapper;

        public GetPortByIdHandler(IMapper mapper, ILinqRepository<PortEntity> portRepository)
        {
            _portRepository = portRepository ?? throw new ArgumentNullException(nameof(portRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PortVm> Handle(GetPortById request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.Id);
            var query = await _portRepository.List(s => s.Id == guid).FirstOrDefaultAsync(cancellationToken);
            if (query == null)
            {
                throw new Exception("Port is not existing");
            }
            var result = _mapper.Map<PortVm>(query);
            return result;
        }
    }
}
