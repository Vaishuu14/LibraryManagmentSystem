using AutoMapper;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.BookQueries;
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
    public class BookControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockBookRepository = new Mock<IBookRepository>();
            _controller = new BookController(_mockMediator.Object, _mockMapper.Object, _mockBookRepository.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var books = new List<BookDto>
            {
                new BookDto { Id = 1, Title = "Book 1", Author = "Author 1" },
                new BookDto { Id = 2, Title = "Book 2", Author = "Author 2" }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), default))
                         .ReturnsAsync(books);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsType<List<BookDto>>(okResult.Value);
            Assert.Equal(2, returnBooks.Count);
        }

        [Fact]
        public async Task DetailsOfBook_ReturnsOkResult_WithBook()
        {
            // Arrange
            var book = new BookDto { Id = 1, Title = "Book 1", Author = "Author 1" };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync(book);

            // Act
            var result = await _controller.DetailsOfBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<BookDto>(okResult.Value);
            Assert.Equal(1, returnBook.Id);
        }

        [Fact]
        public async Task DetailsOfBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                         .ReturnsAsync((BookDto)null);

            // Act
            var result = await _controller.DetailsOfBook(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateBook_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var command = new CreateBookCommand { Title = "New Book", Author = "New Author" };
            var createdBook = new BookDto { Id = 1, Title = "New Book", Author = "New Author" };

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), default))
                         .ReturnsAsync(createdBook);

            // Act
            var result = await _controller.CreateBook(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnBook = Assert.IsType<BookDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnBook.Id);
        }

        [Fact]
        public async Task EditBook_ReturnsNoContent()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Title = "Updated Book", Author = "Updated Author" };

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), default))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.EditBook(command);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent()
        {
            // Arrange
            var command = new DeleteBookCommand(1);

            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteBookCommand>(), default))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
