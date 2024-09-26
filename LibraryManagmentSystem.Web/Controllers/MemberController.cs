using AutoMapper;
using LibraryManagmentSystem.Application.Commands;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.MemberQueries;
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
    [Route("Member")]
    public class MemberController : Controller
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

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var members = await _mediator.Send(new GetMembersQuery());
            return PartialView("Index", members);
        }

        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateMemberCommand command)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors if model state is invalid
                return View(command);
            }

            var result = await _mediator.Send(command);
            TempData["SuccessMessage"] = "Member added successfully!";
            return RedirectToAction("Create");
            // return RedirectToAction("Index");
        }


        // GET: /Member/Edit/{id}
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var memberDto = await _memberService.GetByIdAsync(id);
            if (memberDto == null)
            {
                return NotFound();
            }
            var updateMemberCommand = _mapper.Map<UpdateMemberCommand>(memberDto);
            return View(updateMemberCommand);
        }

        // POST: /Member/Edit
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateMemberCommand command)
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
            var query = new GetMemberByIdQuery { Id=id};
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFoundResult if the member does not exist
            }
            return View(result);
        }


        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new DeleteMemberCommand { Id = id };
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }


        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetMemberByIdQuery{ Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Return NotFoundResult if the member does not exist
            }
            return View(result);
        }
    }
}
