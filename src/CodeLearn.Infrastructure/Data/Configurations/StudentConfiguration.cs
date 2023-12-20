using CodeLearn.Domain.Students;
using CodeLearn.Domain.Students.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        ConfigureStudentTable(builder);
    }

    private static void ConfigureStudentTable(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student", DatabaseSchemes.Person);

        builder.HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(
                studentId => studentId.Value,
                value => StudentId.Create(value));

        builder
            .HasOne(s => s.StudentGroup)
            .WithMany()
            .HasForeignKey(s => s.StudentGroupId)
            .IsRequired();

        builder
            .Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(t => t.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(t => t.Patronymic)
            .HasMaxLength(50);
    }
}