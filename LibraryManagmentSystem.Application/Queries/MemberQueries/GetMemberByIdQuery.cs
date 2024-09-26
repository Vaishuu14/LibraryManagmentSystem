using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Queries.MemberQueries
{
    public class GetMemberByIdQuery : IRequest<MemberDto>
    {
        public int Id { get; set; }
        //public GetMemberByIdQuery(int id)
        //{
        //    Id = id;
        //}

    }
}
