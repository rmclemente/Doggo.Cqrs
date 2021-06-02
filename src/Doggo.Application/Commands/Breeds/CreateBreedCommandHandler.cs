using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Application.Commands.Breeds
{
    public class CreateBreedCommandHandler : IRequestHandler<CreateBreedCommand, CommandResponse>
    {
        private readonly IBreedRepository _breedRepository;

        public CreateBreedCommandHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<CommandResponse> Handle(CreateBreedCommand request, CancellationToken cancellationToken)
        {
            var breed = (Breed)request;
            _breedRepository.Add(breed);
            await _breedRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResponse(breed.UniqueId, HttpStatusCode.Created);
        }
    }
}
