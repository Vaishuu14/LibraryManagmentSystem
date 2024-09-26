using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Queries.LoanQueries
{
    public class GetLoanByIdQuery : IRequest<LoanDto>
    {
        public int Id { get; set; }
        public GetLoanByIdQuery(int id)
        {
            Id = id;
        }

    }
}
