using AutoMapper;
using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Application.Queries.ReservationQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Web.Controllers
{
    [ApiController]
    [Route("Reservation")]
    public class ReservationController : Controller
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

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var reservations = await _mediator.Send(new GetReservationsQuery());
            return PartialView("Index", reservations);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateReservationCommand command)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors if model state is invalid
                return View(command);
            }

            var result = await _mediator.Send(command);
            TempData["SuccessMessage"] = "Reservation added successfully!";
            return RedirectToAction("Create");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var reservationDto = await _reservationService.GetReservationByIdAsync(id);
            if (reservationDto == null)
            {
                return NotFound();
            }
            var updateReservationCommand = _mapper.Map<UpdateReservationCommand>(reservationDto);
            return View(updateReservationCommand);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateReservationCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result == null)
            {
            return NotFound(); 

            }
            return View(result);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new CancelReservationCommand { Id = id };
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound();
            }
            return View(result);
        }
    }
}
