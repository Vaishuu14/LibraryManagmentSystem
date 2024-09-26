using AutoMapper;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.Queries.MemberQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberService;

        public MemberController(IMediator mediator, IMapper mapper, IMemberRepository memberService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _memberService = memberService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _mediator.Send(new GetMembersQuery());
            return Ok(members);
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> DetailsOfMember(int id)
        {
            var query = new GetMemberByIdQuery{Id=id};
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFound if the member doesn't exist
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(DetailsOfMember), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> EditMember([FromBody] UpdateMemberCommand command)
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
        public async Task<IActionResult> DeleteMember(int id)
        {
            var command = new DeleteMemberCommand{Id = id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
