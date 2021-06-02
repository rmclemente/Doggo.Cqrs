using Doggo.Application.Notifications;
using Moq.AutoMock;

namespace Doggo.Application.Tests.Fixtures
{
    public partial class ApplicationServiceFixture
    {
        public DomainNotificationHandler GetDomainNotificationHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<DomainNotificationHandler>();
        }
    }
}
