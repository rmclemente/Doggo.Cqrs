using Doggo.Application.Tests.Fixtures;
using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentAssertions;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Doggo.Application.Tests.Commands
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class UpdateBreedCommandHandlerTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "UpdateBreedCommandHandler Tests";
        private readonly ApplicationServiceFixture _fixture;

        public UpdateBreedCommandHandlerTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Commit Once And Should Return CommandResponse With StatusCode NoContent When Breed Exists")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldCommitOnce_And_ShouldReturnCommandResponseWithStatusCodeNoContent_WhenBreedExists()
        {
            //arrange
            var handler = _fixture.GetUpdateBreedCommandHandler();
            var command = _fixture.GenerateValidUpdateBreedCommand();
            var current = _fixture.GenerateValidBreed(); ;

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.Get(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(current));

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.UnitOfWork.Commit(CancellationToken.None))
                .Returns(Task.FromResult(true));

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().BeOfType<CommandResponse>();
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(default), Times.Once);
        }

        [Fact(DisplayName = "Should Never Commit And Should Return CommandResponse With StatusCode NotFound When Breed Does Not Exist")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldNeverCallCommitFromRepository_And_ShouldReturnCommandResponseWithStatusCodeNotFound_WhenBreedDoesNotExist()
        {
            //arrange
            var handler = _fixture.GetUpdateBreedCommandHandler();
            var command = _fixture.GenerateValidUpdateBreedCommand();
            Breed current = null;

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.Get(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(current));

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().BeOfType<CommandResponse>();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(CancellationToken.None), Times.Never);
        }
    }
}
