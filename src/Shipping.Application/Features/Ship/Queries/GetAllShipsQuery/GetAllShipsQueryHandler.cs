using Shipping.Application.Features.Ship.Commands.DeleteShipCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;

namespace Shipping.Application.Features.Ship.Queries.GetAllShipsQuery
{
    public class GetAllShipsQueryHandler : IRequestHandler<GetAllShipsQuery, List<ShipVm>>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private readonly IMapper _mapper;

        public GetAllShipsQueryHandler(ILinqRepository<ShipEntity> shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ShipVm>> Handle(GetAllShipsQuery request, CancellationToken cancellationToken)
        {
            var query = _shipRepository.List().ToList();
            var result = _mapper.Map<List<ShipVm>>(query);
            return result;
        }
    }
}
