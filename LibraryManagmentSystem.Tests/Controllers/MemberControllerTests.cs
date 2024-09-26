using AutoMapper;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.MemberQueries;
using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using LibraryManagmentSystem.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagmentSystem.Tests.Controllers
{
    public class MemberControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMemberRepository> _memberServiceMock;
        private readonly MemberController _controller;

        public MemberControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _memberServiceMock = new Mock<IMemberRepository>();
            _controller = new MemberController(_mediatorMock.Object, _mapperMock.Object, _memberServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsPartialViewResult_WithMembers()
        {
            // Arrange
            var members = new List<MemberDto> { new MemberDto { Id = 1, FirstName = "Test Member" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMembersQuery>(), default)).ReturnsAsync(members);

            // Act
            var result = await _controller.Index();

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Index", partialViewResult.ViewName); // Ensure this matches your view name
            Assert.Equal(members, partialViewResult.Model);
        }

        [Fact]
        public async Task Index_ReturnsPartialViewResult_WithNoMembers()
        {
            // Arrange
            var members = new List<MemberDto>(); // No members
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMembersQuery>(), default)).ReturnsAsync(members);

            // Act
            var result = await _controller.Index();

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Index", partialViewResult.ViewName); // Ensure this matches your view name
            Assert.Empty((List<MemberDto>)partialViewResult.Model); // Ensure model is empty
        }


        //[Fact]
        //public void Create_ReturnsView()
        //{
        //    // Act
        //    var result = _controller.Create();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    Assert.Null(viewResult.Model);
        //}

        //[Fact]
        //public async Task Create_Post_InvalidModel_ReturnsView()
        //{
        //    // Arrange
        //    var command = new CreateMemberCommand();
        //    _controller.ModelState.AddModelError("Name", "Required");

        //    // Act
        //    var result = await _controller.Create(command);

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    Assert.Equal(command, viewResult.Model);
        //}

        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var command = new UpdateMemberCommand { Id = 1, FirstName = "Invalid Member" };
            _controller.ModelState.AddModelError("Name", "The Name field is required."); // Simulate invalid model state

            // Act
            var result = await _controller.Edit(command);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UpdateMemberCommand>(viewResult.Model);
            Assert.Equal(command.FirstName, model.FirstName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberId = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync((MemberDto)null);

            // Act
            var result = await _controller.Edit(memberId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithMember_WhenMemberExists()
        {
            // Arrange
            var memberId = 1;
            var memberDto = new MemberDto { Id = memberId, FirstName = "Test Member" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync(memberDto);

            // Act
            var result = await _controller.Delete(memberId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MemberDto>(viewResult.Model);
            Assert.Equal(memberDto.Id, model.Id);
            Assert.Equal(memberDto.FirstName, model.FirstName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberId = 10;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync((MemberDto)null);

            // Act
            var result = await _controller.Delete(memberId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithMemberDto_WhenMemberExists()
        {
            // Arrange
            var memberId = 1;
            var memberDto = new MemberDto { Id = memberId, FirstName = "Test Member" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync(memberDto);

            // Act
            var result = await _controller.Details(memberId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MemberDto>(viewResult.Model);
            Assert.Equal(memberDto.Id, model.Id);
            Assert.Equal(memberDto.FirstName, model.FirstName);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberId = 10; // ID that does not exist

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), default))
                         .ReturnsAsync((MemberDto)null);

            // Act
            var result = await _controller.Details(memberId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
