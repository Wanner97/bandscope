using BandScope.Common;
using BandScope.Common.Enums;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nickname).HasColumnName("Nickname").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(int.MaxValue).IsUnicode().IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            builder.HasMany(u => u.FavoriteArtists)
                .WithMany(a => a.FavorizedByUsers);

            SeedMasterData(builder);
        }

        private void SeedMasterData(EntityTypeBuilder<User> builder)
        {
            var usersToAdd = new List<User>()
            {
                new User
                {
                    Id = 1,
                    Nickname = "admin",
                    Email = "admin@email.com",
                    PasswordHash = "$2a$11$Aa4ftSwm2jEX4mDoHaYgO.G7kJP1kx9cyCUeVTueGjyZDs5xQyt92",
                    RoleId = RoleEnum.Administrator
                },

                new User
                {
                    Id = 2,
                    Nickname = "user",
                    Email = "user@email.com",
                    PasswordHash = "$2a$11$0ygvDwnmeSk24lCWTfLg9e/L7qhTnHnI0wCHbsPOXRpcELHcxsPwa",
                    RoleId = RoleEnum.User
                }
            };

            builder.HasData(usersToAdd);
        }
    }
}
