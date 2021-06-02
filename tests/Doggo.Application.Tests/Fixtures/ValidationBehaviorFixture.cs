using Doggo.Application.Commands.Breeds;
using Doggo.Application.PipelineBehaviors;
using Doggo.Infra.CrossCutting.Communication;
using Doggo.Infra.CrossCutting.Communication.Messages;
using Moq.AutoMock;

namespace Doggo.Application.Tests.Fixtures
{
    public partial class ApplicationServiceFixture
    {
        public ValidationBehavior<CreateBreedCommand, CommandResponse> GetValidationBehaviorCreateBreedCommandRequest()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<ValidationBehavior<CreateBreedCommand, CommandResponse>>();
        }

        public MediatorHandler GetIMediatorHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<MediatorHandler>();
        }
    }
}
