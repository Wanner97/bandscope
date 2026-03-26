using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Rating).HasColumnName("Rating").HasDefaultValue(1).IsRequired();
            builder.Property(x => x.Comment).HasColumnName("Comment").HasMaxLength(int.MaxValue).IsUnicode().IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();

            builder.HasOne(r => r.Artist)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.ArtistId);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);
        }
    }
}
