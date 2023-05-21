using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipEntity = Shipping.Domain.Model.Ship;
namespace Shipping.Application.Features.Ship.Commands.DeleteShipCommand
{
    public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand, Unit>
    {
        private readonly ILinqRepository<ShipEntity> _shipRepository;
        private const string queryDeleteShip = @"UPDATE [dbo].[Ship] SET [Deleted] = 1 WHERE ";

        public DeleteShipCommandHandler(ILinqRepository<ShipEntity> shipRepository)
        {
            _shipRepository = shipRepository ?? throw new ArgumentNullException(nameof(shipRepository));
        }

        public async Task<Unit> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            var script = queryDeleteShip;
            var filter = new List<string>();
            var data = new List<string>();
            var ids = string.Empty;
            var page = request.Ids.Count / 2000;
            for (int i = 0; i <= page; i++)
            {
                data = request.Ids.Skip(2000 * i).Take(2000).ToList();
                if (!data.Any())
                    continue;
                ids = string.Join(",", data.Select(q => $"'{q}'").ToList());
                filter.Add($"{script} [Id] IN ({ids})");
            }

            var query = string.Join("; \n", filter);

            await _shipRepository.ExecuteSqlCommand(query);
            return Unit.Value;
        }
    }
}
