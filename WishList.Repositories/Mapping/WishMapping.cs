using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishList.Domain.Entities;

namespace WishList.Repositories.Mapping
{
    public class WishMapping : IEntityTypeConfiguration<Wish>
    {
        public void Configure(EntityTypeBuilder<Wish> builder)
        {
            builder.ToTable("Wishes");

            builder.HasKey(x => new { x.UserId, x.ProductId });

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Wishes)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Wishes)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
