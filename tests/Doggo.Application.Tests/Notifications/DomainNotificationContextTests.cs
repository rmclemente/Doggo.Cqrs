using Doggo.Application.Notifications;
using Doggo.Application.Tests.Fixtures;
using FluentAssertions;
using System;
using Xunit;

namespace Doggo.Application.Tests.Notifications
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class DomainNotificationContextTests
    {
        public const string TestType = "Application";
        public const string TestCategory = "DomainNotificationContext Tests";
        private readonly ApplicationServiceFixture _fixture;

        public DomainNotificationContextTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Should Add Notification To DomainNotification List")]
        [Trait(TestType, TestCategory)]
        public void AddNotification_ShouldAddNotificationToDomainNotificationList()
        {
            //arrange
            var context = new DomainNotificationContext();
            var notification = new DomainNotification("Chave", "Notificação");

            //act
            context.AddNotification(notification);

            //assert
            notification.DomainNotificationId.Should().NotBe(Guid.Empty);
            context.HasDomainNotification.Should().BeTrue();
            context.DomainNotifications.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Should Clear Notification From DomainNotification List")]
        [Trait(TestType, TestCategory)]
        public void Dispose_ShouldClearNotificationFromDomainNotificationList()
        {
            //arrange
            var context = new DomainNotificationContext();
            var notification = new DomainNotification("Chave", "Notificação");
            context.AddNotification(notification);

            //act
            context.Dispose();

            //assert
            context.HasDomainNotification.Should().BeFalse();
            context.DomainNotifications.Should().HaveCount(0);
        }
    }
}
