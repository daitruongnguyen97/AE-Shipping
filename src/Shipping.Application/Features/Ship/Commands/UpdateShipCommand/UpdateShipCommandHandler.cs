using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;
namespace Shipping.Application.Features.Ship.Commands.UpdateShipCommand
{
    public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand, string>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private readonly IMapper _mapper;

        public UpdateShipCommandHandler(ILinqRepository<ShipEntity> shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.Id);
            var existing = _shipRepository.List(s => s.Id == guid).FirstOrDefault();
            if(existing == null)
            {
                throw new Exception("Ship is not existing");
            }

            var duplicatedName = _shipRepository.List(s => s.Id != guid && s.Name == request.Name).FirstOrDefault();
            if(duplicatedName != null)
            {
                throw new Exception("Ship Name is already existed.");
            }

            _mapper.Map(request, existing, typeof(UpdateShipCommand), typeof(ShipEntity));
            await _shipRepository.UpdateAsync(existing, cancellationToken);
            return request.Id.ToString();
        }
    }
}
