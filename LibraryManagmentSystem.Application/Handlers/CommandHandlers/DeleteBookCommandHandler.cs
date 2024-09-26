using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using LibraryManagmentSystem.Domain.Entities;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            

            await _bookRepository.DeleteAsync(request.Id);
            

            return Unit.Value;
        }
    }
}
