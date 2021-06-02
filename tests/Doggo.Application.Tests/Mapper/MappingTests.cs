using AutoMapper;
using Doggo.Application.AutoMapper;
using Xunit;

namespace Doggo.Application.Tests.Mapper
{
    public class MappingTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "MappingGlobalConfiguration Tests";

        public MappingTests() { }

        [Fact(DisplayName = "Mapping Configuration Should be Valid")]
        [Trait(TestType, TestCategory)]
        public void MappingConfiguration_ShouldBeValid()
        {
            //https://docs.automapper.org/en/stable/Configuration-validation.html
            //arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new Mappings()));

            //Act - Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
