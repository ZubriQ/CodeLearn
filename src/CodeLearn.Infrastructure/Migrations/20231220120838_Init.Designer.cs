﻿// <auto-generated />
using System;
using CodeLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeLearn.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231220120838_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CodeLearn.Domain.ExerciseSubmissions.ExerciseSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("SubmissionType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("ExerciseSubmission", "Test");

                    b.HasDiscriminator<string>("SubmissionType").HasValue("ExerciseSubmission");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.Entities.DataType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("DataType", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.Entities.ExerciseTopic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("ExerciseTopic", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ExerciseType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<Guid>("TestingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TestingId");

                    b.ToTable("Exercise", "Test");

                    b.HasDiscriminator<string>("ExerciseType").HasValue("Exercise");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CodeLearn.Domain.QuestionChoices.QuestionChoice", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Explanation")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("QuestionChoice", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Students.Entities.StudentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StudentGroup", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("StudentGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentGroupId");

                    b.ToTable("Student", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Teachers.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Teacher", "Person");
                });

            modelBuilder.Entity("CodeLearn.Domain.TestingSessions.TestingSession", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FinishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TestingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TestingId");

                    b.ToTable("TestingSession", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.Testings.Testing", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Testing", "Test");
                });

            modelBuilder.Entity("Exercise2ExerciseTopic", b =>
                {
                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseTopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExerciseId", "ExerciseTopicId");

                    b.HasIndex("ExerciseTopicId");

                    b.ToTable("Exercise2ExerciseTopic", "Test");
                });

            modelBuilder.Entity("CodeLearn.Domain.ExerciseSubmissions.CodeExerciseSubmission", b =>
                {
                    b.HasBaseType("CodeLearn.Domain.ExerciseSubmissions.ExerciseSubmission");

                    b.Property<string>("StudentCode")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("TestingInformation")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasDiscriminator().HasValue("Code");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.ClassCodingExercise", b =>
                {
                    b.HasBaseType("CodeLearn.Domain.Exercises.Exercise");

                    b.Property<string>("ClassSolutionCode")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ClassTesterCode")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasDiscriminator().HasValue("ClassCoding");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.MethodCodingExercise", b =>
                {
                    b.HasBaseType("CodeLearn.Domain.Exercises.Exercise");

                    b.Property<Guid>("MethodReturnTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MethodSolutionCode")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("MethodToExecute")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasIndex("MethodReturnTypeId");

                    b.HasDiscriminator().HasValue("MethodCoding");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.QuestionExercise", b =>
                {
                    b.HasBaseType("CodeLearn.Domain.Exercises.Exercise");

                    b.Property<bool>("IsMultipleAnswers")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("CodeLearn.Domain.ExerciseSubmissions.ExerciseSubmission", b =>
                {
                    b.HasOne("CodeLearn.Domain.Exercises.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.Exercise", b =>
                {
                    b.HasOne("CodeLearn.Domain.Testings.Testing", null)
                        .WithMany()
                        .HasForeignKey("TestingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("CodeLearn.Domain.Exercises.Entities.ExerciseNote", "ExerciseNotes", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Decoration")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.Property<string>("Entry")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("ExerciseId");

                            b1.ToTable("ExerciseNote", "Test");

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.Navigation("ExerciseNotes");
                });

            modelBuilder.Entity("CodeLearn.Domain.QuestionChoices.QuestionChoice", b =>
                {
                    b.HasOne("CodeLearn.Domain.Exercises.QuestionExercise", null)
                        .WithMany("QuestionChoices")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeLearn.Domain.Students.Student", b =>
                {
                    b.HasOne("CodeLearn.Domain.Students.Entities.StudentGroup", "StudentGroup")
                        .WithMany()
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentGroup");
                });

            modelBuilder.Entity("CodeLearn.Domain.TestingSessions.TestingSession", b =>
                {
                    b.HasOne("CodeLearn.Domain.Students.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CodeLearn.Domain.Testings.Testing", null)
                        .WithMany()
                        .HasForeignKey("TestingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeLearn.Domain.Testings.Testing", b =>
                {
                    b.HasOne("CodeLearn.Domain.Teachers.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Exercise2ExerciseTopic", b =>
                {
                    b.HasOne("CodeLearn.Domain.Exercises.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeLearn.Domain.Exercises.Entities.ExerciseTopic", null)
                        .WithMany()
                        .HasForeignKey("ExerciseTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.MethodCodingExercise", b =>
                {
                    b.HasOne("CodeLearn.Domain.Exercises.Entities.DataType", "MethodReturnType")
                        .WithMany()
                        .HasForeignKey("MethodReturnTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("CodeLearn.Domain.Exercises.Entities.MethodParameter", "MethodParameters", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("DataTypeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Position")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("DataTypeId");

                            b1.HasIndex("ExerciseId");

                            b1.ToTable("MethodParameter", "Test");

                            b1.HasOne("CodeLearn.Domain.Exercises.Entities.DataType", "DataType")
                                .WithMany()
                                .HasForeignKey("DataTypeId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");

                            b1.Navigation("DataType");
                        });

                    b.OwnsMany("CodeLearn.Domain.Exercises.Entities.TestCase", "TestCases", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CorrectOutputValue")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)");

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("ExerciseId");

                            b1.ToTable("TestCase", "Test");

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");

                            b1.OwnsMany("CodeLearn.Domain.Exercises.Entities.TestCaseParameter", "TestCaseParameters", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Position")
                                        .HasColumnType("int");

                                    b2.Property<Guid>("TestCaseId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(250)
                                        .HasColumnType("nvarchar(250)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("TestCaseId");

                                    b2.ToTable("TestCaseParameter", "Test");

                                    b2.WithOwner()
                                        .HasForeignKey("TestCaseId");
                                });

                            b1.Navigation("TestCaseParameters");
                        });

                    b.Navigation("MethodParameters");

                    b.Navigation("MethodReturnType");

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("CodeLearn.Domain.Exercises.QuestionExercise", b =>
                {
                    b.Navigation("QuestionChoices");
                });
#pragma warning restore 612, 618
        }
    }
}
