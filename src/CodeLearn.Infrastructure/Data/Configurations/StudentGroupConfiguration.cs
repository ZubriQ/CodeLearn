using CodeLearn.Domain.StudentGroups;
using CodeLearn.Domain.StudentGroups.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
    public void Configure(EntityTypeBuilder<StudentGroup> builder)
    {
        ConfigureStudentGroupTable(builder);
    }

    private static void ConfigureStudentGroupTable(EntityTypeBuilder<StudentGroup> builder)
    {
        builder.ToTable("StudentGroup", DatabaseSchemes.Test);

        builder.HasKey(sg => sg.Id);

        builder
            .Property(sg => sg.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                groupId => groupId.Value,
                value => StudentGroupId.Create(value));

        builder
            .Property(sg => sg.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(sg => sg.EnrolmentYear)
            .IsRequired();
    }
}