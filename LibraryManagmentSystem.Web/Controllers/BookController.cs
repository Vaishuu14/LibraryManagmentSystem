using AutoMapper;
using LibraryManagmentSystem.Application.Commands;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.Queries.BookQueries;
using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookService;

        public BookController(IMediator mediator, IMapper mapper, IBookRepository bookService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return PartialView("Index", books);  
        }


        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors if model state is invalid
                return View(command);
            }

            var result = await _mediator.Send(command);
            TempData["SuccessMessage"] = "Book added successfully!";
            return RedirectToAction("Create");
            // return RedirectToAction("Index");
        }




        // GET: /Book/Edit/{id}
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var bookDto = await _bookService.GetByIdAsync(id);
            if (bookDto == null)
            {
                return NotFound();
            }
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(bookDto);
            return View(updateBookCommand);
        }

        // POST: /Book/Edit
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateBookCommand command)
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
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }


        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }


        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFound if the book doesn't exist
            }
            return View(result);
        }
    }
}
