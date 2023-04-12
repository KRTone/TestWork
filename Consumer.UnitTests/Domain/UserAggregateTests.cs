using Consumer.Domain.Events;
using System;

namespace Consumer.UnitTests.Domain
{
    public class UserAggregateTests
    {
        [Theory]
        [InlineData("", "e", "n", "l")]
        [InlineData("p", "", "n", "l")]
        [InlineData("p", "e", "", "l")]
        [InlineData("p", "e", "n", "")]
        public void Invalid_user(string phone, string email, string name, string lastName, Guid guid = default, string? patronymic = default)
        {
            Assert.Throws<ConsumerDomainException>(() => new User(guid, phone, email, name, lastName, patronymic));
        }

        [Fact]
        public void Create_user_success()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "lastName";
            string patronymic = "patronymic";

            User user = new(guid, phone, email, name, lastName, patronymic);
        }

        [Fact]
        public void Create_user_null_patronymic_success()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "lasnName";
            string patronymic = "";

            User user = new(guid, phone, email, name, lastName, patronymic);

            Assert.NotNull(user);
        }

        [Fact]
        public void Create_new_user_rises_new_event()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "lastName";
            string patronymic = "patronymic";
            int expectedResult = 1;

            User user = new(guid, phone, email, name, lastName, patronymic);

            Assert.Equal(user.DomainEvents.Count, expectedResult);
        }

        [Fact]
        public void Move_user_to_organization()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "lastName";
            string patronymic = "patronymic";
            User user = new(guid, phone, email, name, lastName, patronymic);

            user.MoveToOrganization(guid);

            Assert.Contains(user.DomainEvents, ev => ev is UserMovedToOrganizationEvent);
        }
    }
}
