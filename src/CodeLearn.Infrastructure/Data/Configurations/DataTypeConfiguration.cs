using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
{
    public void Configure(EntityTypeBuilder<DataType> builder)
    {
        ConfigureDataTypeTable(builder);
    }

    private static void ConfigureDataTypeTable(EntityTypeBuilder<DataType> builder)
    {
        builder.ToTable("DataType", DatabaseSchemes.Test);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                testing => testing.Value,
                id => DataTypeId.Create(id));

        builder
            .Property(dt => dt.SystemName)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(dt => dt.Alias)
            .HasMaxLength(7)
            .IsRequired();
    }
}