using AutoMapper;
using Domain;
using NetTopologySuite.Geometries;
using Shipping.Application.Features.Port.Queries.GetAllPortsQuery;
using Shipping.Application.Features.Port.Queries.GetClosetPortQuery;
using Shipping.Application.Features.Port.Queries.GetPortById;
using Shipping.Domain.Model;

namespace Test.Shipping.PortTest
{
    public class Tests
    {
        private readonly Mock<ILinqRepository<Port>> _mockPortRepo;
        private readonly Mock<ILinqRepository<Ship>> _mockShipRepo;
        private readonly Mock<IMapper> _mockMapper;

        public Tests()
        {
            _mockPortRepo = new();
            _mockShipRepo = new();
            _mockMapper = new();
        }

        [SetUp]
        public void Setup()
        {
            var shipGuid = new Guid("D7A50733-D4CE-4564-A4FD-4355D6479268");
            var portGuid = new Guid("7B259406-2E49-413E-AF5D-EF16015C876A");
            var mockShip = new List<Ship>()
            {
                new Ship
                {
                    CreatedDate = DateTime.UtcNow,
                    Geolocation = new Point(68.2, 88) { SRID = 4326 },
                    Name = "Ship 1",
                    Id = shipGuid,
                    Velocity = 30
                }
            };

            var mockPort = new List<Port>()
            {
                new Port
                {
                    CreatedDate = DateTime.UtcNow,
                    Geolocation = new Point(76.2, 75.2) { SRID = 4326 },
                    Name = "Port 2",
                    Id = portGuid
                },
            };
            _mockShipRepo.Setup(s => s.List(null)).Returns(mockShip.AsQueryable());
            _mockShipRepo.Setup(s => s.List(x => x.Id == shipGuid)).Returns(mockShip.AsQueryable());

            _mockPortRepo.Setup(s => s.List(null)).Returns(mockPort.AsQueryable());
            _mockPortRepo.Setup(s => s.List(x => x.Id == portGuid)).Returns(mockPort.AsQueryable());
        }

        [Test]
        public async Task Get_All_Port_Query()
        {
            var command = new GetAllPortsQuery();
            var handler = new GetAllPortsQueryHandler(_mockMapper.Object, _mockPortRepo.Object);
            await handler.Handle(command, default);
            Assert.Pass();
        }

        [Test]
        public async Task Get_Port_By_Id_Query()
        {
            var guid = new Guid("7B259406-2E49-413E-AF5D-EF16015C876A");
            var command = new GetPortById(guid.ToString());
            var handler = new GetPortByIdHandler(_mockMapper.Object, _mockPortRepo.Object);
            await handler.Handle(command, default);
            _mockPortRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
            Assert.Pass();
        }

        [Test]
        public async Task Get_Closet_Port_Query()
        {
            var guid = new Guid("D7A50733-D4CE-4564-A4FD-4355D6479268");
            var command = new GetClosetPortQuery(guid.ToString());
            var handler = new GetClosetPortQueryHandler(_mockShipRepo.Object, _mockMapper.Object, _mockPortRepo.Object);
            await handler.Handle(command, default);
            _mockShipRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
            Assert.Pass();
        }
    }
}