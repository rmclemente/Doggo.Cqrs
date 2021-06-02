using AutoMapper;
using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Application.Commands.Breeds
{
    public class UpdateBreedCommandHandler : IRequestHandler<UpdateBreedCommand, CommandResponse>
    {
        private readonly IBreedRepository _breedRepository;

        public UpdateBreedCommandHandler(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<CommandResponse> Handle(UpdateBreedCommand request, CancellationToken cancellationToken)
        {
            var entity = await _breedRepository.Get(request.UniqueId, false);
            if (entity is null) return new CommandResponse(HttpStatusCode.NotFound);
            entity.CopyDataFrom((Breed)request);
            await _breedRepository.UnitOfWork.Commit(cancellationToken);
            return new CommandResponse(HttpStatusCode.NoContent);
        }
    }
}
