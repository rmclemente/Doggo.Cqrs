using Doggo.Application.Dtos;
using Doggo.Application.Tests.Fixtures;
using Doggo.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Doggo.Application.Tests.Mapper
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class BreedMappingTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "Breed Mapper Tests";
        private readonly ApplicationServiceFixture _fixture;

        public BreedMappingTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Map To Breed When Mapping From CreateBreedCommand")]
        [Trait(TestType, TestCategory)]
        public void Map_ShouldMapToBreed_WhenMappingFromCreateBreedCommand()
        {
            //arrange
            var command = _fixture.GenerateValidCreateBreedCommand();
            var mapper = _fixture.GetMapper();

            //Act
            var breed = mapper.Map<Breed>(command);

            //Assert
            breed.IsValid().Should().BeTrue();
            breed.Name.Should().Be(command.Name);
            breed.Type.Should().Be(command.Type);
            breed.Family.Should().Be(command.Family);
            breed.Origin.Should().Be(command.Origin);
            breed.OtherNames.Should().Be(command.OtherNames);
        }

        [Fact(DisplayName = "Should Create Breed When Casting From CreateBreedCommand")]
        [Trait(TestType, TestCategory)]
        public void ExplicitOperator_ShouldCreateBreed_WhenCastingFromCreateBreedCommand()
        {
            //arrange
            var command = _fixture.GenerateValidCreateBreedCommand();

            //Act
            var breed = (Breed)command;

            //Assert
            breed.IsValid().Should().BeTrue();
            breed.Name.Should().Be(command.Name);
            breed.Type.Should().Be(command.Type);
            breed.Family.Should().Be(command.Family);
            breed.Origin.Should().Be(command.Origin);
            breed.OtherNames.Should().Be(command.OtherNames);
        }

        [Fact(DisplayName = "Mapping From Valid UpdateBreedCommand Create Valid Breed")]
        [Trait(TestType, TestCategory)]
        public void Map_ShouldMapToBreed_WhenMappingFromUpdateBreedCommand()
        {
            //arrange
            var command = _fixture.GenerateValidUpdateBreedCommand();
            var mapper = _fixture.GetMapper();

            //Act
            var breed = mapper.Map<Breed>(command);

            //Assert
            breed.IsValid().Should().BeTrue();
            breed.Name.Should().Be(command.Name);
            breed.Type.Should().Be(command.Type);
            breed.Family.Should().Be(command.Family);
            breed.Origin.Should().Be(command.Origin);
            breed.OtherNames.Should().Be(command.OtherNames);
        }

        [Fact(DisplayName = "Should Create Breed When Casting From UpdateBreedCommand")]
        [Trait(TestType, TestCategory)]
        public void ExplicitOperator_ShouldCreateBreed_WhenCastingFromUpdateBreedCommand()
        {
            //arrange
            var command = _fixture.GenerateValidUpdateBreedCommand();

            //Act
            var breed = (Breed)command;

            //Assert
            breed.IsValid().Should().BeTrue();
            breed.Name.Should().Be(command.Name);
            breed.Type.Should().Be(command.Type);
            breed.Family.Should().Be(command.Family);
            breed.Origin.Should().Be(command.Origin);
            breed.OtherNames.Should().Be(command.OtherNames);
        }

        [Fact(DisplayName = "Should Map To BreedResponse When Mapping From Breed")]
        [Trait(TestType, TestCategory)]
        public void Map_ShouldMapToBreedResponse_WhenMappingFromBreed()
        {
            //arrange
            var breed = _fixture.GenerateValidBreed();
            var mapper = _fixture.GetMapper();

            //Act
            var response = mapper.Map<BreedResponse>(breed);

            //Assert
            breed.IsValid().Should().BeTrue();
            response.Id.Should().Be(breed.Id);
            response.UniqueId.Should().Be(breed.UniqueId);
            response.Name.Should().Be(breed.Name);
            response.Type.Should().Be(breed.Type);
            response.Family.Should().Be(breed.Family);
            response.Origin.Should().Be(breed.Origin);
            response.OtherNames.Should().Be(breed.OtherNames);
        }

        [Fact(DisplayName = "Should Map To Breed When Mapping From BreedResponse")]
        [Trait(TestType, TestCategory)]
        public void Map_ShouldMapToBreed_WhenMappingFromBreedResponse()
        {
            //arrange
            var response = _fixture.GenerateValidBreedResponse();
            var mapper = _fixture.GetMapper();

            //Act
            var breed = mapper.Map<Breed>(response);

            //Assert
            breed.IsValid().Should().BeTrue();
            breed.Id.Should().Be(response.Id);
            breed.UniqueId.Should().Be(response.UniqueId);
            breed.Name.Should().Be(response.Name);
            breed.Type.Should().Be(response.Type);
            breed.Family.Should().Be(response.Family);
            breed.Origin.Should().Be(response.Origin);
            breed.OtherNames.Should().Be(response.OtherNames);
        }
    }
}
