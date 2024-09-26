using AutoMapper;
using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Application.Queries.ReservationQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationService;

        public ReservationController(IMediator mediator, IMapper mapper, IReservationRepository reservationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _mediator.Send(new GetReservationsQuery());
            return Ok(reservations);
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> DetailsOfReservation(int id)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFound if the reservation doesn't exist
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(DetailsOfReservation), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> EditReservation([FromBody] UpdateReservationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var command = new CancelReservationCommand{Id=id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
