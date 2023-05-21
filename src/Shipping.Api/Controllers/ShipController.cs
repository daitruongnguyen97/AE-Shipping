using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Features.Ship;
using Shipping.Application.Features.Ship.Commands.CreateShipCommand;
using Shipping.Application.Features.Ship.Commands.DeleteShipCommand;
using Shipping.Application.Features.Ship.Commands.UpdateShipCommand;
using Shipping.Application.Features.Ship.Queries.GetAllShipsQuery;
using Shipping.Application.Features.Ship.Queries.GetShipByIdQuery;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddShip")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddShip([FromBody] CreateShipCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateShip")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateShip(string id, [FromBody] UpdateShipCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetAllShips")]
        [ProducesResponseType(typeof(List<ShipVm>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetAllShips()
        {
            var command = new GetAllShipsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetShipById")]
        [ProducesResponseType(typeof(ShipVm), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> GetShipById(string id)
        {
            var command = new GetShipByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteShipById")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteShipById(string id)
        {
            var command = new DeleteShipCommand(new List<string> { id });
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("delete",Name = "DeleteShip")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteShips(DeleteShipCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
