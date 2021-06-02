using Doggo.Application.Notifications;
using Doggo.Application.Tests.Fixtures;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Doggo.Application.Tests.Notifications
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class DomainNotificationHandlerTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "DomainNotificationHandler Tests";
        private readonly ApplicationServiceFixture _fixture;

        public DomainNotificationHandlerTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Add Notification To NotificationContext DomainNotification List")]
        [Trait(TestType, TestCategory)]
        public async Task Handle_ShouldAddNotificationToNotificationContextDomainNotificationList()
        {
            //arrange
            var notificationContext = new DomainNotificationContext();
            var handler = new DomainNotificationHandler(notificationContext);
            var notification = new DomainNotification("Chave", "Notificação");

            //act
            await handler.Handle(notification, CancellationToken.None);

            //assert
            notificationContext.HasDomainNotification.Should().BeTrue();
            notificationContext.DomainNotifications.Should().HaveCountGreaterThan(0);
        }
    }
}
