using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;
namespace Shipping.Application.Features.Ship.Commands.CreateShipCommand
{
    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, string>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private readonly IMapper _mapper;

        public CreateShipCommandHandler(ILinqRepository<ShipEntity> shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var existing = _shipRepository.List(s => s.Name == request.Name).FirstOrDefault();
            if(existing != null)
            {
                throw new Exception("Ship Name is already existed.");
            }
            var entity = _mapper.Map<ShipEntity>(request);
            var result = await _shipRepository.AddAsync(entity, cancellationToken);
            return result.Id.ToString();
        }
    }
}
