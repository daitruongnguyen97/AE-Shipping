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
            var guid = Guid.NewGuid();
            var command = new GetPortById(guid.ToString());
            var handler = new GetPortByIdHandler(_mockMapper.Object, _mockPortRepo.Object);
            try
            {
                await handler.Handle(command, default);
            }
            catch (Exception ex)
            {
                _mockPortRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
                if (ex.Message.Contains("existing")) Assert.Pass();
                Assert.Fail();
            }
        }

        [Test]
        public async Task Get_Closet_Port_Query()
        {
            var guid = Guid.NewGuid();
            var command = new GetClosetPortQuery(guid.ToString());
            var handler = new GetClosetPortQueryHandler(_mockShipRepo.Object, _mockMapper.Object, _mockPortRepo.Object);
            try
            {
                await handler.Handle(command, default);
            }
            catch (Exception ex)
            {
                _mockShipRepo.Verify(s => s.List(x => x.Id == guid), Times.Once);
                Assert.Pass();
            }
        }
    }
}