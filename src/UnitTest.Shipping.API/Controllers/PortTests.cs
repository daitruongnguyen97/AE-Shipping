using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shipping.API.Controllers;
using Shipping.Application.Features.Port.Queries.GetClosetPortQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shipping.API.Controllers.Tests
{

    [TestClass()]

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
        public void GetAllPortsTest()
        {
            Assert.Fail();
        }
    }
}