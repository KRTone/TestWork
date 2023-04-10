using Consumer.Domain.Aggregates.OrganizationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consumer.Infastructure.DataBase.Configurations
{
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations", ConsumerDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Guid);

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Users)
                .WithOne()
                .HasForeignKey("_organizationGuid")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .Metadata
                .FindNavigation(nameof(Organization.Users))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}