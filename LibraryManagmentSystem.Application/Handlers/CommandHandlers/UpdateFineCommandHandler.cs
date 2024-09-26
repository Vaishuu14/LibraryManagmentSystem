using AutoMapper;
using LibraryManagmentSystem.Application.Commands.FineCommands;
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
    public class UpdateFineCommandHandler : IRequestHandler<UpdateFineCommand, Unit>
    {
        private readonly IFineRepository _fineRepository;
        private readonly IMapper _mapper;

        public UpdateFineCommandHandler(IFineRepository fineRepository, IMapper mapper)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFineCommand request, CancellationToken cancellationToken)
        {
            var fine = await _fineRepository.GetByIdAsync(request.FineId);
            // var member = _mapper.Map<Core.Entities.Member>(request);

            _mapper.Map(request, fine);
            await _fineRepository.UpdateAsync(fine);
            return Unit.Value;
        }

    }
}
