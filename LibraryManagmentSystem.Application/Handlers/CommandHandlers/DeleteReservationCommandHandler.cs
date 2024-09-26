using LibraryManagmentSystem.Application.Commands.ReservationCommands;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class DeleteReservationCommandHandler : IRequestHandler<CancelReservationCommand, Unit>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            await _reservationRepository.DeleteReservationAsync(request.Id);
            return Unit.Value;
        }

    }
}
