using Ganz.Domain.Enttiies.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ganz.Domain.FluentApiConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.UserName).IsRequired();
            builder.Property(x=> x.UserName).HasMaxLength(64);
        }
    }
}
