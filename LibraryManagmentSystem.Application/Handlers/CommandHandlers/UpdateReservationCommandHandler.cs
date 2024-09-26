using AutoMapper;
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
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Unit>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.Id);
            // var member = _mapper.Map<Core.Entities.Member>(request);

            _mapper.Map(request, reservation);
            await _reservationRepository.UpdateReservationAsync(reservation);
            return Unit.Value;
        }

    }
}
