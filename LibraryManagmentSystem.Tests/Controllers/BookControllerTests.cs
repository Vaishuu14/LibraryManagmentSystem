using AutoMapper;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.BookQueries;
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
    public class BookControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBookRepository> _bookServiceMock;
        private readonly BookController _controller;
       


        public BookControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _bookServiceMock = new Mock<IBookRepository>();
            _controller = new BookController(_mediatorMock.Object, _mapperMock.Object, _bookServiceMock.Object);

        }

        [Fact]
        public async Task Index_ReturnsPartialView_WithBooks()
        {
            // Arrange
            var books = new List<BookDto> { new BookDto { Id = 1, Title = "Test Book" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), default)).ReturnsAsync(books);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Equal(books, viewResult.Model);
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
        //    var command = new CreateBookCommand();
        //    _controller.ModelState.AddModelError("Title", "Required");

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
            var command = new UpdateBookCommand { Id = 1, Title = "Invalid Book" };
            _controller.ModelState.AddModelError("Title", "The Title field is required."); // Simulate invalid model state

            // Act
            var result = await _controller.Edit(command);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UpdateBookCommand>(viewResult.Model);
            Assert.Equal(command.Title, model.Title);
        }

        

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync((BookDto)null);

            // Act
            var result = await _controller.Edit(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithBook_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var bookDto = new BookDto { Id = bookId, Title = "Test Book" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync(bookDto);

            // Act
            var result = await _controller.Delete(bookId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BookDto>(viewResult.Model);
            Assert.Equal(bookDto.Id, model.Id);
            Assert.Equal(bookDto.Title, model.Title);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 10;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync((BookDto)null);

            // Act
            var result = await _controller.Delete(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithBookDto_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var bookDto = new BookDto { Id = bookId, Title = "Test Book" };

            // Mock the mediator to return the book DTO
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync(bookDto);

            // Act
            var result = await _controller.Details(bookId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BookDto>(viewResult.Model);
            Assert.Equal(bookDto.Id, model.Id);
            Assert.Equal(bookDto.Title, model.Title);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 10; // ID that does not exist

            // Mock the mediator to return null for the non-existent book
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync((BookDto)null);

            // Act
            var result = await _controller.Details(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }






    }
}
