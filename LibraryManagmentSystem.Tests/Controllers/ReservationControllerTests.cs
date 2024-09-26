using AutoMapper;
using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Application.Queries.ReservationQueries;
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
    public class ReservationControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly ReservationController _controller;

        public ReservationControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockReservationRepository = new Mock<IReservationRepository>();
            _controller = new ReservationController(_mockMediator.Object, _mockMapper.Object, _mockReservationRepository.Object);
        }

        [Fact]
        public async Task GetReservations_ReturnsOkResult_WithListOfReservations()
        {
            // Arrange
            var reservations = new List<ReservationDto>
            {
                new ReservationDto { Id = 1, MemberId = 1, BookId = 1, ReservedDate = DateTime.Now },
                new ReservationDto { Id = 2, MemberId = 2, BookId = 2, ReservedDate = DateTime.Now }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetReservationsQuery>(), default))
                         .ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetReservations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnReservations = Assert.IsType<List<ReservationDto>>(okResult.Value);
            Assert.Equal(2, returnReservations.Count);
        }

        [Fact]
        public async Task DetailsOfReservation_ReturnsOkResult_WithReservation()
        {
            // Arrange
            var reservation = new ReservationDto { Id = 1, MemberId = 1, BookId = 1, ReservedDate = DateTime.Now };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetReservationByIdQuery>(), default))
                         .ReturnsAsync(reservation);

            // Act
            var result = await _controller.DetailsOfReservation(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnReservation = Assert.IsType<ReservationDto>(okResult.Value);
            Assert.Equal(1, returnReservation.Id);
        }

        [Fact]
        public async Task CreateReservation_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new CreateReservationCommand { MemberId = 0, BookId = 0 }; // Invalid command
            _controller.ModelState.AddModelError("MemberId", "MemberId is required.");

            // Act
            var result = await _controller.CreateReservation(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task EditReservation_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new UpdateReservationCommand { Id = 1, MemberId = 0, BookId = 0 }; // Invalid command
            _controller.ModelState.AddModelError("MemberId", "MemberId is required.");

            // Act
            var result = await _controller.EditReservation(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteReservation_ReturnsNoContent()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<CancelReservationCommand>(), default))
                         .ReturnsAsync(MediatR.Unit.Value);

            // Act
            var result = await _controller.DeleteReservation(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
