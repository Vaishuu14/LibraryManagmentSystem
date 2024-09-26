using AutoMapper;
using LibraryManagmentSystem.Application.Commands.FineCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class CreateFineCommandHandler : IRequestHandler<CreateFineCommand, FineDto>
    {
        private readonly IFineRepository _fineRepository;
        private readonly IMapper _mapper;

        public CreateFineCommandHandler(IFineRepository fineRepository, IMapper mapper)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }

        public async Task<FineDto> Handle(CreateFineCommand request, CancellationToken cancellationToken)
        {
            var fine = _mapper.Map<Fine>(request);
            await _fineRepository.AddAsync(fine);
            return _mapper.Map<FineDto>(fine);
        }

    }
}
