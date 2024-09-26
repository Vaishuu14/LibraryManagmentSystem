using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Queries.MemberQueries
{
    public class GetMembersQuery : IRequest<List<MemberDto>>
    {

    }
}
