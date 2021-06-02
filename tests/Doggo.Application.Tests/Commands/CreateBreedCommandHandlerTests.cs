using Doggo.Application.Tests.Fixtures;
using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentAssertions;
using Moq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Doggo.Application.Tests.Commands
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class CreateBreedCommandHandlerTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "CreateBreedCommandHandler Tests";
        private readonly ApplicationServiceFixture _fixture;

        public CreateBreedCommandHandlerTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Add And Commit Once And Should Return Command Response With Status Code Created")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldAddAndCommitOnce_And_ShouldReturnCommandResponseWithStatusCodeCreated()
        {
            //arrange
            var handler = _fixture.GetCreateBreedCommandHandler();
            var command = _fixture.GenerateValidCreateBreedCommand();

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().BeOfType<CommandResponse>();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.Add(It.IsAny<Breed>()), Times.Once);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(default), Times.Once);
        }
    }
}
