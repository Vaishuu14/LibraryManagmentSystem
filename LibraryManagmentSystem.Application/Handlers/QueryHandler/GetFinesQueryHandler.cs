using AutoMapper;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.FineQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.QueryHandler
{
    public class GetFinesQueryHandler : IRequestHandler<GetFinesQuery, List<FineDto>>
    {
        private readonly IFineRepository _fineRepository;
        private readonly IMapper _mapper;

        public GetFinesQueryHandler(IFineRepository fineRepository, IMapper mapper)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }

        public async Task<List<FineDto>> Handle(GetFinesQuery request, CancellationToken cancellationToken)
        {
            var fines = await _fineRepository.GetAllAsync();
            return _mapper.Map<List<FineDto>>(fines);
        }

    }
}
