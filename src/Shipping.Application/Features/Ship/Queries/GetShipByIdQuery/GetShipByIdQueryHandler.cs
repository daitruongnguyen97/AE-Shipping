using Shipping.Application.Features.Ship.Commands.DeleteShipCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;

namespace Shipping.Application.Features.Ship.Queries.GetShipByIdQuery
{
    public class GetShipByIdQueryHandler : IRequestHandler<GetShipByIdQuery, ShipVm>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private readonly IMapper _mapper;

        public GetShipByIdQueryHandler(ILinqRepository<ShipEntity> shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShipVm> Handle(GetShipByIdQuery request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.Id);
            var query = await _shipRepository.List(s => s.Id == guid).FirstOrDefaultAsync(cancellationToken);
            if (query == null)
            {
                throw new Exception("Ship is not existing");
            }
            var result = _mapper.Map<ShipVm>(query);
            return result;
        }
    }
}
