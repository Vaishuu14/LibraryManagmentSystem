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
    public class GetFineByIdQueryHandler : IRequestHandler<GetFineByIdQuery, FineDto>
    {
        private readonly IFineRepository _fineRepository;
        private readonly IMapper _mapper;

        public GetFineByIdQueryHandler(IFineRepository fineRepository, IMapper mapper)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }

        public async Task<FineDto> Handle(GetFineByIdQuery request, CancellationToken cancellationToken)
        {
            var fine = await _fineRepository.GetByIdAsync(request.Id);
            return _mapper.Map<FineDto>(fine);
        }

    }
}
