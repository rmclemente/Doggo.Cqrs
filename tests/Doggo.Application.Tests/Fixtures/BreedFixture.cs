using Bogus;
using Doggo.Application.Commands.Breeds;
using Doggo.Application.Dtos;
using Doggo.Domain.Entities;
using Moq.AutoMock;
using System;

namespace Doggo.Application.Tests.Fixtures
{
    public partial class ApplicationServiceFixture
    {
        public CreateBreedCommand GenerateValidCreateBreedCommand()
        {
            return new Faker<CreateBreedCommand>("pt_BR")
                .CustomInstantiator(c => new CreateBreedCommand
                (
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    null,
                    c.Random.String2(10, 100)
                )).Generate();
        }

        public CreateBreedCommand GenerateInvalidCreateBreedCommand()
        {
            return new Faker<CreateBreedCommand>("pt_BR")
                .CustomInstantiator(c => new CreateBreedCommand
                (
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    null,
                    c.Random.String2(101, 110)
                )).Generate();
        }

        public UpdateBreedCommand GenerateValidUpdateBreedCommand()
        {
            return new Faker<UpdateBreedCommand>("pt_BR")
                .CustomInstantiator(c => new UpdateBreedCommand
                (
                    Guid.NewGuid(),
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    c.Random.String2(10, 100),
                    null,
                    c.Random.String2(10, 100)
                )).Generate();
        }

        public UpdateBreedCommand GenerateInvalidUpdateBreedCommand()
        {
            return new Faker<UpdateBreedCommand>("pt_BR")
                .CustomInstantiator(c => new UpdateBreedCommand
                (
                    Guid.Empty,
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    c.Random.String2(101, 110),
                    null,
                    c.Random.String2(101, 110)
                )).Generate();
        }

        public Breed GenerateValidBreed()
        {
            return new Faker<Breed>("pt_BR")
                        .CustomInstantiator(c => new Breed(c.Random.String2(10, 100), c.Random.String2(10, 100), c.Random.String2(10, 100), c.Random.String2(10, 100), null, c.Random.String2(10, 100)))
                        .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                        .Generate();
        }

        public Breed GenerateInvalidBreed()
        {
            return new Faker<Breed>("pt_BR")
                .CustomInstantiator(c => new Breed(c.Random.String2(101, 110), c.Random.String2(101, 110), c.Random.String2(101, 110), c.Random.String2(101, 110), null, c.Random.String2(101, 110)))
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .Generate();
        }

        public BreedResponse GenerateValidBreedResponse()
        {
            return new Faker<BreedResponse>("pt_BR")
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.UniqueId, Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Random.String2(10, 100))
                .RuleFor(p => p.Type, f => f.Random.String2(10, 100))
                .RuleFor(p => p.Family, f => f.Random.String2(10, 100))
                .RuleFor(p => p.Origin, f => f.Random.String2(10, 100))
                .RuleFor(p => p.OtherNames, f => f.Random.String2(10, 100));
        }

        //public BreedService GetBreedService()
        //{
        //    Mocker = new AutoMocker();
        //    return Mocker.CreateInstance<BreedService>();
        //}

        public CreateBreedCommandHandler GetCreateBreedCommandHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<CreateBreedCommandHandler>();
        }

        public UpdateBreedCommandHandler GetUpdateBreedCommandHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<UpdateBreedCommandHandler>();
        }
    }
}
