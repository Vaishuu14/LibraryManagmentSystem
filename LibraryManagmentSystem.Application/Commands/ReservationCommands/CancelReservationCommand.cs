using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.ReservationCommands
{
    public class CancelReservationCommand : IRequest<Unit>
    {
        public int Id { get; set; }

    }
}
