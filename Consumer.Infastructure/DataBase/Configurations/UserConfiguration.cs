using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consumer.Infastructure.DataBase.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", ConsumerDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Guid);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Patronymic).IsRequired(false);

            builder
                .Property<Guid?>("_organizationGuid")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrganizationGuid")
                .IsRequired(false);

            builder.HasOne<Organization>()
                .WithMany(x => x.Users)
                .HasForeignKey("_organizationGuid")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}