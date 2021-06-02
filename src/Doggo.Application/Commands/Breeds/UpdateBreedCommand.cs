using Doggo.Domain.Entities;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentValidation;
using System;

namespace Doggo.Application.Commands.Breeds
{
    public class UpdateBreedCommand : Command
    {
        public Guid UniqueId { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }
        public DateTime? DateOfOrigin { get; private set; }
        public string OtherNames { get; private set; }

        public UpdateBreedCommand(Guid uniqueId, string name, string type, string family, string origin, DateTime? dateOfOrigin, string otherNames)
        {
            AggregateId = uniqueId;
            UniqueId = uniqueId;
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
            DateOfOrigin = dateOfOrigin;
            OtherNames = otherNames;
        }

        public static explicit operator Breed(UpdateBreedCommand command)
        {
            return new Breed(command.Name,
                command.Type,
                command.Family,
                command.Origin,
                command.DateOfOrigin,
                command.OtherNames);
        }
    }

    public class UpdateBreedCommandValidator : AbstractValidator<UpdateBreedCommand>
    {
        public UpdateBreedCommandValidator()
        {
            RuleFor(p => p.UniqueId).NotEmpty();
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Type).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Family).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Origin).NotEmpty().MaximumLength(100);
            RuleFor(p => p.OtherNames).NotEmpty().MaximumLength(100);
        }
    }
}
