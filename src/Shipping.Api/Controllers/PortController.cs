using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Features.Ship.Queries.GetShipByIdQuery;
using Shipping.Application.Features.Ship;
using Shipping.Application.Features.Port.Queries.GetClosetPortQuery;
using Shipping.Application.Features.Port;
using Shipping.Application.Features.Port.Queries.GetAllPortsQuery;
using Shipping.Application.Features.Port.Queries.GetPortById;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PortController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ship/{id}", Name = "GetClosetPort")]
        [ProducesResponseType(typeof(ArrivalVm), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetCloset(string id)
        {
            var command = new GetClosetPortQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPortById")]
        [ProducesResponseType(typeof(ArrivalVm), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetPortById(string id)
        {
            var command = new GetPortById(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetAllPorts")]
        [ProducesResponseType(typeof(List<ShipVm>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAllPorts()
        {
            var command = new GetAllPortsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
