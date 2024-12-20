﻿// <auto-generated />
using System;
using BudgetManagementAPI.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    [DbContext(typeof(UnitOfWork))]
    [Migration("20241212211834_addCurrency")]
    partial class addCurrency
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Percent")
                        .HasColumnType("real");

                    b.Property<float>("TotalPercentageSpent")
                        .HasColumnType("real");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Lesson", b =>
                {
                    b.Property<Guid>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CoverImage")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FileText")
                        .HasColumnType("bytea");

                    b.Property<string>("LessonContentName")
                        .HasColumnType("text");

                    b.Property<Guid>("LevelLessonId")
                        .HasColumnType("uuid");

                    b.HasKey("LessonId");

                    b.HasIndex("LevelLessonId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.LevelLesson", b =>
                {
                    b.Property<Guid>("LevelLessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("MaxValue")
                        .HasColumnType("double precision");

                    b.Property<double>("MinValue")
                        .HasColumnType("double precision");

                    b.HasKey("LevelLessonId");

                    b.ToTable("LevelLessons");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descripiton")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("TransactionProcessingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TransactionId");

                    b.HasIndex("BudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<string>("CodeResetPassword")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("Income")
                        .HasColumnType("double precision");

                    b.Property<string>("LevelFinancialEducation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TimeCodeExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.Property<Guid>("LessonsLessonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("LessonsLessonId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LessonUser");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Budget", b =>
                {
                    b.HasOne("BudgetManagementAPI.Entities.Entities.User", "User")
                        .WithMany("UserBudgetCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Lesson", b =>
                {
                    b.HasOne("BudgetManagementAPI.Entities.Entities.LevelLesson", "Level")
                        .WithMany("Lessons")
                        .HasForeignKey("LevelLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Transaction", b =>
                {
                    b.HasOne("BudgetManagementAPI.Entities.Entities.Budget", "Budget")
                        .WithMany("Transactions")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetManagementAPI.Entities.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.HasOne("BudgetManagementAPI.Entities.Entities.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetManagementAPI.Entities.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.Budget", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.LevelLesson", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("BudgetManagementAPI.Entities.Entities.User", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("UserBudgetCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
