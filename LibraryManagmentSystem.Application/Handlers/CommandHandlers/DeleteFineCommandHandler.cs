using LibraryManagmentSystem.Application.Commands.FineCommands;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class DeleteFineCommandHandler : IRequestHandler<DeleteFineCommand,Unit>
    {
        private readonly IFineRepository _fineRepository;

        public DeleteFineCommandHandler(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        public async Task<Unit> Handle(DeleteFineCommand request, CancellationToken cancellationToken)
        {
            await _fineRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }

    }
}
