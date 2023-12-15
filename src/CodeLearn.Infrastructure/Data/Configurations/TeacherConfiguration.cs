using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        ConfigureTeacherTable(builder);
    }

    private static void ConfigureTeacherTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teacher", DatabaseSchemes.Person);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                teacher => teacher.Value,
                id => TeacherId.Create(id));

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