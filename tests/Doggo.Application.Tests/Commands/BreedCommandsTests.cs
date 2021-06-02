using Doggo.Application.Commands.Breeds;
using Doggo.Application.Tests.Fixtures;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Doggo.Application.Tests.Commands
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class BreedCommandsTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "BreedCommands Tests";
        private readonly ApplicationServiceFixture _fixture;

        public BreedCommandsTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "CreateBreedCommand Should Be Valid When Validation Rules Are Satisfied")]
        [Trait(TestType, TestCategory)]
        public void CreateBreedCommandValidator_ShouldBeValid_WhenValidationRulesAreSatisfied()
        {
            //arrange
            var command = new CreateBreedCommand("Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act
            var result = new CreateBreedCommandValidator().TestValidate(command);

            //assert
            result.IsValid.Should().BeTrue();
            command.Timestamp.Should().NotBeAfter(DateTime.Now);
            command.MessageType.Should().Be(nameof(CreateBreedCommand));
        }

        [Fact(DisplayName = "CreateBreedCommand Should Be Invalid When At Least One Validation Rules Are Not Satisfied")]
        [Trait(TestType, TestCategory)]
        public void CreateBreedCommandValidator_ShouldBeInvalid_WhenAtLeastOneValidationRulesAreNotSatisfied()
        {
            //arrange
            var command = _fixture.GenerateInvalidCreateBreedCommand();

            //act
            var result = new CreateBreedCommandValidator().TestValidate(command);

            //assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(p => p.Name);
            result.ShouldHaveValidationErrorFor(p => p.Type);
            result.ShouldHaveValidationErrorFor(p => p.Family);
            result.ShouldHaveValidationErrorFor(p => p.Origin);
            result.ShouldHaveValidationErrorFor(p => p.OtherNames);
        }

        [Fact(DisplayName = "UpdateBreedCommand Should Be Valid When Validation Rules Are Satisfied")]
        [Trait(TestType, TestCategory)]
        public void UpdateBreedCommandValidator_ShouldBeValid_WhenValidationRulesAreSatisfied()
        {
            //arrange
            //var command = _fixture.GenerateValidUpdateBreedCommand();
            var command = new UpdateBreedCommand(Guid.NewGuid(), "Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act
            var result = new UpdateBreedCommandValidator().TestValidate(command);

            //assert
            result.IsValid.Should().BeTrue();
            command.AggregateId.Should().Be(command.UniqueId);
            command.Timestamp.Should().NotBeAfter(DateTime.Now);
            command.MessageType.Should().Be(nameof(UpdateBreedCommand));
        }

        [Fact(DisplayName = "UpdateBreedCommand Should Be Invalid When At Least One Validation Rules Are Not Satisfied")]
        [Trait(TestType, TestCategory)]
        public void UpdateBreedCommandValidator_ShouldBeInvalid_WhenAtLeastOneValidationRulesAreNotSatisfied()
        {
            //arrange
            var command = _fixture.GenerateInvalidUpdateBreedCommand();

            //act
            var result = new UpdateBreedCommandValidator().TestValidate(command);

            //assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(p => p.UniqueId);
            result.ShouldHaveValidationErrorFor(p => p.Name);
            result.ShouldHaveValidationErrorFor(p => p.Type);
            result.ShouldHaveValidationErrorFor(p => p.Family);
            result.ShouldHaveValidationErrorFor(p => p.Origin);
            result.ShouldHaveValidationErrorFor(p => p.OtherNames);
        }
    }
}
