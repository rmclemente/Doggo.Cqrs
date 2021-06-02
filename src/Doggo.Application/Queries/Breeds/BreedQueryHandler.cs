using AutoMapper;
using Doggo.Application.Dtos;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Application.Queries.Breeds
{
    public class BreedQueryHandler : IRequestHandler<GetAllBreedsQuery, QueryResponse>,
                                     IRequestHandler<GetBreedByUniqueIdQuery, QueryResponse>,
                                     IRequestHandler<ExistBreedByUniqueIdQuery, QueryResponse>
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedQueryHandler(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        public async Task<QueryResponse> Handle(GetAllBreedsQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<BreedResponse>>(await _breedRepository.GetAll());
            return new QueryResponse(result);
        }

        public async Task<QueryResponse> Handle(GetBreedByUniqueIdQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<BreedResponse>(await _breedRepository.Get(request.UniqueId));
            return new QueryResponse(result);
        }

        public async Task<QueryResponse> Handle(ExistBreedByUniqueIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _breedRepository.Any(breed => breed.UniqueId == request.UniqueId);
            return new QueryResponse(result);
        }
    }
}
