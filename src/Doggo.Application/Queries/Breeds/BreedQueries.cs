using Doggo.Infra.CrossCutting.Communication.Messages;
using System;

namespace Doggo.Application.Queries.Breeds
{
    public class GetAllBreedsQuery : Query { }

    public class GetBreedByUniqueIdQuery : Query
    {
        public Guid UniqueId { get; set; }
    }

    public class ExistBreedByUniqueIdQuery : Query
    {
        public Guid UniqueId { get; set; }
    }
}
