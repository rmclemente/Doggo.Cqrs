using Doggo.Domain.Objects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Doggo.Domain.Entities
{
    public class Breed : AuditableEntity<int, Breed>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }
        public DateTime? DateOfOrigin { get; private set; }
        public string OtherNames { get; private set; }

        public Breed(string name, string type, string family, string origin, DateTime? dateOfOrigin, string otherNames)
        {
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
            DateOfOrigin = dateOfOrigin;
            OtherNames = otherNames;
        }

        protected Breed() { }

        public override bool IsValid()
        {
            ValidationResult = new BreedValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        protected override IEnumerable<PropertyInfo> GetEqualityProperties()
        {
            yield return GetType().GetProperty(nameof(Name));
            yield return GetType().GetProperty(nameof(Type));
            yield return GetType().GetProperty(nameof(Family));
            yield return GetType().GetProperty(nameof(Origin));
            yield return GetType().GetProperty(nameof(DateOfOrigin));
            yield return GetType().GetProperty(nameof(OtherNames));
        }
    }

    public class BreedValidator : AbstractValidator<Breed>
    {
        public BreedValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Type).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Family).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Origin).NotEmpty().MaximumLength(100);
            RuleFor(p => p.OtherNames).NotEmpty().MaximumLength(100);
        }
    }
}
