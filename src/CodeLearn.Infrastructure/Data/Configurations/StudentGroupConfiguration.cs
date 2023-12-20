using CodeLearn.Domain.Students.Entities;
using CodeLearn.Domain.Students.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
    public void Configure(EntityTypeBuilder<StudentGroup> builder)
    {
        ConfigureStudentGroupTable(builder);
    }

    private static void ConfigureStudentGroupTable(EntityTypeBuilder<StudentGroup> builder)
    {
        builder.ToTable("StudentGroup", DatabaseSchemes.Person);

        builder.HasKey(sg => sg.Id);

        builder
            .Property(sg => sg.Id)
            .ValueGeneratedNever()
            .HasConversion(
                groupId => groupId.Value,
                value => StudentGroupId.Create(value));

        builder
            .Property(sg => sg.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(sg => sg.Year)
            .IsRequired();
    }
}