using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Queries.ReservationQueries
{
    public class GetReservationByIdQuery : IRequest<ReservationDto>
    {
        public int Id { get; set; }
        public GetReservationByIdQuery(int id)
        {
            Id = id;
        }

    }
}
