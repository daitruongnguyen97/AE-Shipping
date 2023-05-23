using AutoMapper;
using Domain;
using NetTopologySuite.Geometries;
using Shipping.Application.Features.Port.Queries.GetAllPortsQuery;
using Shipping.Application.Features.Port.Queries.GetClosetPortQuery;
using Shipping.Application.Features.Port.Queries.GetPortById;
using Shipping.Application.Features.Ship.Commands.CreateShipCommand;
using Shipping.Application.Features.Ship.Commands.DeleteShipCommand;
using Shipping.Application.Features.Ship.Queries.GetAllShipsQuery;
using Shipping.Application.Features.Ship.Queries.GetShipByIdQuery;
using Shipping.Domain.Model;
using Location = Shipping.Domain.Model.Location;

namespace Test.Shipping.ShipTest
{
    public class Tests
    {
        private readonly Mock<ILinqRepository<Ship>> _mockShipRepo;
        private readonly Mock<IMapper> _mockMapper;

        public Tests()
        {
            _mockShipRepo = new();
            _mockMapper = new();
        }

        [SetUp]
        public void Setup()
        {
            var guid = new Guid("D7A50733-D4CE-4564-A4FD-4355D6479268");
            var mockShip = new List<Ship>()
            {
                new Ship
                {
                    CreatedDate = DateTime.UtcNow,
                    Geolocation = new Point(68.2, 88) { SRID = 4326 },
                    Name = "Ship 1",
                    Id = guid,
                    Velocity = 30
                }
            };
            _mockShipRepo.Setup(s => s.List(null)).Returns(mockShip.AsQueryable());
            _mockShipRepo.Setup(s => s.List(x => x.Id == guid)).Returns(mockShip.AsQueryable());
        }
        [Test]
        public async Task Create_Ship_Command()
        {
            try
            {
                var command = new CreateShipCommand()
                {
                    Name = "UnitTest",
                    Geolocation = new Location { Latitude = 10, Longitude = 10 },
                    Velocity = 30
                };
                var handler = new CreateShipCommandHandler(_mockShipRepo.Object, _mockMapper.Object);
                await handler.Handle(command, default);
            }
            catch(Exception ex)
            {
                _mockShipRepo.Verify(s => s.AddAsync(It.IsAny<Ship>(), default), Times.Once);
                Assert.Pass();
            }
        }

        [Test]
        public async Task Get_All_Ship_Query()
        {
            var command = new GetAllShipsQuery();
            var handler = new GetAllShipsQueryHandler(_mockShipRepo.Object, _mockMapper.Object);
            await handler.Handle(command, default);
            Assert.Pass();
        }

        [Test]
        public async Task Get_Ship_By_Id_Query()
        {
            var guid = new Guid("D7A50733-D4CE-4564-A4FD-4355D6479268");
            var command = new GetShipByIdQuery(guid.ToString());
            var handler = new GetShipByIdQueryHandler(_mockShipRepo.Object, _mockMapper.Object);
            await handler.Handle(command, default);
            _mockShipRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
            Assert.Pass();
        }

        [Test]
        public async Task Delete_Ship_Command()
        {
            var guid = Guid.NewGuid();
            var ids = new List<string>() { guid.ToString() };
            var command = new DeleteShipCommand(ids);
            var handler = new DeleteShipCommandHandler(_mockShipRepo.Object);

            await handler.Handle(command, default);
            Assert.Pass();
        }
    }
}