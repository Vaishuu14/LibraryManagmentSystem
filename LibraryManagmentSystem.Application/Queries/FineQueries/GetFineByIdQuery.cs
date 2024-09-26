using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Queries.FineQueries
{
    public class GetFineByIdQuery : IRequest<FineDto>
    {
        public int Id { get; set; }
        public GetFineByIdQuery(int id)
        {
            Id = id;
        }

    }
}
