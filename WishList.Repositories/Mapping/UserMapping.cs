using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishList.Domain.Entities;

namespace WishList.Repositories.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnType("varchar(150)").IsRequired();

            builder.Property(x => x.Email).HasColumnType("varchar(150)").IsRequired();
        }
    }
}
