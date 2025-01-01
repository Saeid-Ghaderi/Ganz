using Ganz.Domain.Catalogs.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ganz.Domain.FluentApiConfigurations.Catalogs.Features
{
    internal sealed class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {

        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features", "Catalog");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new FeatureId(v));

            builder.Property(e => e.Title).HasMaxLength(128).IsRequired();

            builder.Property(e => e.Description).HasMaxLength(1024);

        }

    }
}
