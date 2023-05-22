using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shipping.Application.Features.Port.Queries.GetAllPortsQuery;
using Shipping.Application.Features.Port.Queries.GetClosetPortQuery;

namespace Shipping.API.Controllers.Tests
{
    [TestClass]
    public class PortControllerTests
    {
        private readonly IMediator _mediator;
        public PortControllerTests(IMediator mediator)
        {
            _mediator = mediator;
        }

        [TestMethod()]
        public async Task GetClosetTest()
        {
            var handler = new GetClosetPortQuery("");
            var result = await _mediator.Send(handler);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPortByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task GetAllPortsTest()
        {
            try
            {
                var handler = new GetAllPortsQuery();
                var result = await _mediator.Send(handler);
                if (result.Count != 6)
                {
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}