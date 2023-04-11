using Consumer.Domain.Events;

namespace Consumer.UnitTests.Domain
{
    public class UserAggregateTests
    {
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
        public void Invalid_user_phone()
        {
            Guid guid = Guid.NewGuid();
            string phone = "";
            string email = "someemail";
            string name = "name";
            string lastName = "lastName";
            string patronymic = "patronymic";

            Assert.Throws<ConsumerDomainException>(() => new User(guid, phone, email, name, lastName, patronymic));
        }

        [Fact]
        public void Invalid_user_email()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "";
            string name = "name";
            string lastName = "lastName";
            string patronymic = "patronymic";

            Assert.Throws<ConsumerDomainException>(() => new User(guid, phone, email, name, lastName, patronymic));
        }

        [Fact]
        public void Invalid_user_name()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "";
            string lastName = "lasName";
            string patronymic = "patronymic";

            Assert.Throws<ConsumerDomainException>(() => new User(guid, phone, email, name, lastName, patronymic));
        }

        [Fact]
        public void Invalid_user_last_name()
        {
            Guid guid = Guid.NewGuid();
            string phone = "somephone";
            string email = "someemail";
            string name = "name";
            string lastName = "";
            string patronymic = "patronymic";

            Assert.Throws<ConsumerDomainException>(() => new User(guid, phone, email, name, lastName, patronymic));
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
