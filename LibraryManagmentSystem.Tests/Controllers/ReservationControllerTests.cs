using AutoMapper;
using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.ReservationQueries;
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
    public class ReservationControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IReservationRepository> _reservationServiceMock; // Add this line
        private readonly ReservationController _controller;

        public ReservationControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _reservationServiceMock = new Mock<IReservationRepository>(); // Initialize the mock
            _controller = new ReservationController(_mediatorMock.Object, _mapperMock.Object, _reservationServiceMock.Object); // Pass the mock
        }

        [Fact]
        public async Task Index_ReturnsPartialView_WithReservations()
        {
            // Arrange
            var reservations = new List<ReservationDto> { new ReservationDto { Id = 1, BookTitle = "Test Book" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationsQuery>(), default)).ReturnsAsync(reservations);

            // Act
            var result = await _controller.Index();

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Index", partialViewResult.ViewName);
            Assert.Equal(reservations, partialViewResult.Model);
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
        //    var command = new CreateReservationCommand();
        //    _controller.ModelState.AddModelError("BookId", "Required");

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
            var command = new UpdateReservationCommand { Id = 1, BookId = 1 };
            _controller.ModelState.AddModelError("BookId", "The BookId field is required."); // Simulate invalid model state

            // Act
            var result = await _controller.Edit(command);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UpdateReservationCommand>(viewResult.Model);
            Assert.Equal(command.BookId, model.BookId);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync((ReservationDto)null);

            // Act
            var result = await _controller.Edit(reservationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithReservation_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservationDto = new ReservationDto { Id = reservationId, BookTitle = "Test Book" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync(reservationDto);

            // Act
            var result = await _controller.Delete(reservationId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ReservationDto>(viewResult.Model);
            Assert.Equal(reservationDto.Id, model.Id);
            Assert.Equal(reservationDto.BookTitle, model.BookTitle);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 10;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync((ReservationDto)null);

            // Act
            var result = await _controller.Delete(reservationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithReservationDto_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservationDto = new ReservationDto { Id = reservationId, BookTitle = "Test Book" };

            // Mock the mediator to return the reservation DTO
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync(reservationDto);

            // Act
            var result = await _controller.Details(reservationId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ReservationDto>(viewResult.Model);
            Assert.Equal(reservationDto.Id, model.Id);
            Assert.Equal(reservationDto.BookTitle, model.BookTitle);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 10; // ID that does not exist

            // Mock the mediator to return null for the non-existent reservation
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync((ReservationDto)null);

            // Act
            var result = await _controller.Details(reservationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
