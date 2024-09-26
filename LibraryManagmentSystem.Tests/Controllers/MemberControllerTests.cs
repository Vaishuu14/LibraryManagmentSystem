using AutoMapper;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.Queries.MemberQueries;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Domain.Interfaces;
using LibraryManagmentSystem.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagmentSystem.Tests
{
    public class MemberControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IMemberRepository> _mockMemberRepository;
        private readonly MemberController _controller;

        public MemberControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockMemberRepository = new Mock<IMemberRepository>();
            _controller = new MemberController(_mockMediator.Object, _mockMapper.Object, _mockMemberRepository.Object);
        }

        [Fact]
        public async Task GetMembers_ReturnsOkResult_WithListOfMembers()
        {
            // Arrange
            var members = new List<MemberDto>
            {
                new MemberDto { Id = 1, FirstName = "Member 1", Email = "member1@example.com" },
                new MemberDto { Id = 2, FirstName = "Member 2", Email = "member2@example.com" }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetMembersQuery>(), default))
                         .ReturnsAsync(members);

            // Act
            var result = await _controller.GetMembers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMembers = Assert.IsType<List<MemberDto>>(okResult.Value);
            Assert.Equal(2, returnMembers.Count);
        }

        [Fact]
        public async Task DetailsOfMember_ReturnsOkResult_WithMember()
        {
            // Arrange
            var member = new MemberDto { Id = 1, FirstName = "Member 1", Email = "member1@example.com" };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync(member);

            // Act
            var result = await _controller.DetailsOfMember(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMember = Assert.IsType<MemberDto>(okResult.Value);
            Assert.Equal(1, returnMember.Id);
        }

        [Fact]
        public async Task CreateMember_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new CreateMemberCommand { FirstName = "", Email = "" }; // Invalid command
            _controller.ModelState.AddModelError("FirstName", "Name is required.");

            // Act
            var result = await _controller.CreateMember(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        [Fact]
        public async Task EditMember_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new UpdateMemberCommand { Id = 1, FirstName = "", Email = "" }; // Invalid command
            _controller.ModelState.AddModelError("FirstName", "Name is required.");

            // Act
            var result = await _controller.EditMember(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteMember_ReturnsNoContent()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteMemberCommand>(), default))
                         .ReturnsAsync(MediatR.Unit.Value);

            // Act
            var result = await _controller.DeleteMember(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}

