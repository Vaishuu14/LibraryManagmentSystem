using AutoMapper;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.Queries.BookQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
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

       // [Authorize]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return Ok(books);
        }

        [Authorize]
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> DetailsOfBook(int id)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFound if the book doesn't exist
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(DetailsOfBook), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> EditBook([FromBody] UpdateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
