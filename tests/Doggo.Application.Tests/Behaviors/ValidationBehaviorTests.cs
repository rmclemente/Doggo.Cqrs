using Doggo.Application.Commands.Breeds;
using Doggo.Application.Notifications;
using Doggo.Application.PipelineBehaviors;
using Doggo.Application.Tests.Fixtures;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Doggo.Application.Tests.Behaviors
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class ValidationBehaviorTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "ValidationBehavior Tests";
        private readonly ApplicationServiceFixture _fixture;

        public ValidationBehaviorTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Publish DomainNotifications And ExitPipeline When Command Is Invalid")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldPublishDomainNotifications_WhenCommandIsInvalid()
        {
            //arrange
            var command = _fixture.GenerateInvalidCreateBreedCommand();
            var commandHandler = _fixture.GetCreateBreedCommandHandler();
            var requestHandler = new RequestHandlerDelegate<CommandResponse>(() => commandHandler.Handle(command, default));
            var mediatorHandler = _fixture.GetIMediatorHandler();
            var behavior = new ValidationBehavior<CreateBreedCommand, CommandResponse>(new List<CreateBreedCommandValidator>()
            { new CreateBreedCommandValidator() }, mediatorHandler);

            //act
            var result = await behavior.Handle(command, CancellationToken.None, requestHandler);

            //assert
            result.Should().BeOfType<CommandResponse>();
            result.Result.Should().BeNull();
            result.StatusCode.Should().Be(0);
            _fixture.Mocker.GetMock<IMediator>().Verify(p => p.Publish(It.IsAny<DomainNotification>(),default), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Should Not Publish Domain Notification When Command Is Valid")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldNotPublishDomainNotification_WhenCommandIsValid()
        {
            //arrange
            //Mock order matters!
            //Setup a mock right after it creation
            //mediatorHandlerMock mut be the last to GetMock<IMediator>().Verify works
            var command = _fixture.GenerateValidCreateBreedCommand();
            var commandHandlerMock = _fixture.GetCreateBreedCommandHandler();
            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var mediatorHandlerMock = _fixture.GetIMediatorHandler();
            var requestHandler = new RequestHandlerDelegate<CommandResponse>(() => commandHandlerMock.Handle(command, default));
            var behavior = new ValidationBehavior<CreateBreedCommand, CommandResponse>(new List<CreateBreedCommandValidator>()
            { new CreateBreedCommandValidator() }, mediatorHandlerMock);

            //act
            var result = await behavior.Handle(command, default, requestHandler);

            //assert
            result.Should().BeOfType<CommandResponse>();
            result.Result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            _fixture.Mocker.GetMock<IMediator>().Verify(p => p.Publish(It.IsAny<DomainNotification>(), default), Times.Never);
        }
    }
}
