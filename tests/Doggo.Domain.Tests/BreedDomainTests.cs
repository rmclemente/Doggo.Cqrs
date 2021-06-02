using Doggo.Domain.Entities;
using Doggo.Domain.Tests.Fixtures;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Doggo.Domain.Tests.Parametrizacao
{
    [Collection(nameof(DomainFixtureCollection))]
    public class BreedDomainTests
    {
        public const string TestType = "Domain";
        public const string TestCategory = "Breed Tests";
        private readonly DomainFixture _fixture;

        public BreedDomainTests(DomainFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "BreedValidator Should Be Valid When Validation Rules Are Satisfied")]
        [Trait(TestType, TestCategory)]
        public void TestValidate_ShouldBeValid_WhenValidationRulesAreSatisfied()
        {
            //arrange
            var Breed = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act
            var result = new BreedValidator().TestValidate(Breed);

            //assert
            Breed.IsValid().Should().BeTrue("no validation errors ocurred");
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "BreedValidator Should Be Invalid When At Least One Validation Rules Are Not Satisfied")]
        [Trait(TestType, TestCategory)]
        public void TestValidate_ShouldBeInvalid_WhenAtLeastOneValidationRulesAreNotSatisfied()
        {
            //arrange
            var breed = new Breed(null, null, null, null, null, null);
            var breed2 = DomainFixture.GenerateInvalidBreed();

            //act
            var result = new BreedValidator().TestValidate(breed);
            var result2 = new BreedValidator().TestValidate(breed2);

            //Assert
            breed.IsValid().Should().BeFalse();
            breed2.IsValid().Should().BeFalse();
            result.IsValid.Should().BeFalse();
            result2.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(p => p.Name);
            result.ShouldHaveValidationErrorFor(p => p.Type);
            result.ShouldHaveValidationErrorFor(p => p.Family);
            result.ShouldHaveValidationErrorFor(p => p.Origin);
            result.ShouldHaveValidationErrorFor(p => p.OtherNames);
            result2.ShouldHaveValidationErrorFor(p => p.Name);
            result2.ShouldHaveValidationErrorFor(p => p.Type);
            result2.ShouldHaveValidationErrorFor(p => p.Family);
            result2.ShouldHaveValidationErrorFor(p => p.Origin);
            result2.ShouldHaveValidationErrorFor(p => p.OtherNames);
        }

        [Fact(DisplayName = "Should Copy Equality Properties From Another Breed When Equality Properties Are Different")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldCopyEqualityPropertiesFromAnotherBreed_WhenEqualityPropertiesAreDifferent()
        {
            //arrange
            var breed1 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");
            var breed2 = new Breed("Yorkshire Terrier", "Toy", "Terrier", "England", null, "York");

            //act - Assert
            breed1.CopyDataFrom(breed2).Should().BeTrue();
            breed1.Equals(breed2).Should().BeTrue();
        }

        [Fact(DisplayName = "Should Not Copy Equality Properties From Another Breed When Equality Properties Are Equals")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldNotCopyEqualityPropertiesFromAnotherBreed_WhenEqualityPropertiesAreEquals()
        {
            //arrange
            var breed1 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");
            var breed2 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act - Assert
            breed1.Equals(breed2).Should().BeTrue();
            breed1.CopyDataFrom(breed2).Should().BeFalse();
        }

        [Fact(DisplayName = "Should Be Equals When Equality Properties Are Equals")]
        [Trait(TestType, TestCategory)]
        public void Equals_ShouldBeEquals_WhenEqualityPropertiesAreEquals()
        {
            //arrange
            var breed1 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");
            var breed2 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act - Assert
            breed1.Equals(breed2).Should().BeTrue();
            breed1.GetHashCode().Should().Be(breed2.GetHashCode());
        }

        [Fact(DisplayName = "Two Breeds Should Not Be Equals When Equality Properties Are Not Equals")]
        [Trait(TestType, TestCategory)]
        public void Equals_ShouldNotBeEquals_WhenEqualityPropertiesAreNotEquals()
        {
            //arrange
            var breed1 = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");
            var breed2 = new Breed("Yorkshire Terrier", "Toy", "Terrier", "England", null, "York");

            //act - Assert
            breed1.Equals(breed2).Should().BeFalse();
            breed1.GetHashCode().Should().NotBe(breed2.GetHashCode());
        }
    }
}
