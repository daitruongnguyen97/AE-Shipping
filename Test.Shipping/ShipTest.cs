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
            var guid = Guid.NewGuid();
            var command = new GetShipByIdQuery(guid.ToString());
            var handler = new GetShipByIdQueryHandler(_mockShipRepo.Object, _mockMapper.Object);
            try
            {
                await handler.Handle(command, default);
            }
            catch (Exception ex)
            {
                _mockShipRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
                if (ex.Message.Contains("existing")) Assert.Pass();
                Assert.Fail();
            }
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