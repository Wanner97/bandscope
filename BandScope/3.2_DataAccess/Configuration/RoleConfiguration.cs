using BandScope.Common;
using BandScope.Common.Enums;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "enum");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();

            SeedMasterData(builder);
        }

        private void SeedMasterData(EntityTypeBuilder<Role> builder)
        {
            var rolesToAdd = new List<Role>();
            foreach (RoleEnum enumVal in Enum.GetValues(typeof(RoleEnum)))
            {
                rolesToAdd.Add(new Role { Id = enumVal, Description = enumVal.ToString() });
            }

            builder.HasData(rolesToAdd);
        }
    }
}
