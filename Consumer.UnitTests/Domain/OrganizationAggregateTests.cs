namespace Consumer.UnitTests.Domain
{
    public class OrganizationAggregateTests
    {
        [Fact]
        public void Create_organization_success()
        {

            Guid guid = Guid.NewGuid();
            string name = "name";

            Organization org = new(guid, name);

            Assert.NotNull(org);
        }

        [Fact]
        public void Invalid_organization_name()
        {

            Guid guid = Guid.NewGuid();
            string name = "";

            Assert.Throws<ConsumerDomainException>(() => new Organization(guid, name));
        }
    }
}
