using System;

namespace Doggo.Application.Dtos
{
    public class BreedResponse : BreedDto
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
    }
}
