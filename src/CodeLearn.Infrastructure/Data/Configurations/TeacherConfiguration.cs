using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Teachers.ValueObjects;
using CodeLearn.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                teacher => teacher.Value,
                id => new TeacherId(id))
            .ValueGeneratedOnAdd();

        builder.Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Patronymic)
            .HasMaxLength(50);

        builder.ToTable("Teacher", DatabaseSchemes.Person);
    }
}