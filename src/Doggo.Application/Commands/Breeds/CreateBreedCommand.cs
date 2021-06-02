using Doggo.Domain.Entities;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentValidation;
using System;

namespace Doggo.Application.Commands.Breeds
{
    public class CreateBreedCommand : Command
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }
        public DateTime? DateOfOrigin { get; private set; }
        public string OtherNames { get; private set; }

        public CreateBreedCommand(string name, string type, string family, string origin, DateTime? dateOfOrigin, string otherNames)
        {
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
            DateOfOrigin = dateOfOrigin;
            OtherNames = otherNames;
        }

        public static explicit operator Breed(CreateBreedCommand command)
        {
            return new Breed(command.Name,
                command.Type,
                command.Family,
                command.Origin,
                command.DateOfOrigin,
                command.OtherNames);
        }
    }

    public class CreateBreedCommandValidator : AbstractValidator<CreateBreedCommand>
    {
        public CreateBreedCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Type).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Family).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Origin).NotEmpty().MaximumLength(100);
            RuleFor(p => p.OtherNames).NotEmpty().MaximumLength(100);
        }
    }
}
