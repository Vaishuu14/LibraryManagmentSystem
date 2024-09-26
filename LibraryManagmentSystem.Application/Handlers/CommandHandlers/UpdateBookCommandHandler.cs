using AutoMapper;
using LibraryManagmentSystem.Application.Commands.BookCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler< UpdateBookCommand , Unit >
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            //var book = _mapper.Map<LibraryManagmentSystem.Domain.Entities.Book>(request);

            _mapper.Map(request, book);
            await _bookRepository.UpdateAsync(book);
            return Unit.Value;
        }

    }
}
