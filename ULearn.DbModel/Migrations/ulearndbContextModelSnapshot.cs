﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ULearn.DbModel.Models;

namespace ULearn.DbModel.Migrations
{
    [DbContext(typeof(ulearndbContext))]
    partial class ulearndbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique();

                    b.HasIndex(new[] { "TeacherId" }, "courseId_teacherId_idx");

                    b.ToTable("course");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("UpdatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE1");

                    b.HasIndex(new[] { "CourseId" }, "lesson_courseId_idx");

                    b.ToTable("lesson");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id", "Name")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE2");

                    b.ToTable("module");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE3");

                    b.HasIndex(new[] { "ModuleId" }, "moduleId_permissionModuleId_idx");

                    b.ToTable("permission");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE4");

                    b.ToTable("role");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE5");

                    b.HasIndex(new[] { "PermissionId" }, "permissionId_userPermissionId_idx");

                    b.HasIndex(new[] { "RoleId" }, "roleId_rolePermission_idx");

                    b.ToTable("rolepermission");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.UserPermissionView", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(255)")
                        .UseCollation("utf8_general_ci");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int(11)");

                    b.Property<string>("ModuleKey")
                        .HasColumnType("varchar(255)")
                        .UseCollation("utf8_general_ci");

                    b.Property<int>("RoleId")
                        .HasColumnType("int(11)");

                    b.Property<string>("RoleName")
                        .HasColumnType("varchar(255)")
                        .UseCollation("utf8_general_ci");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(255)")
                        .UseCollation("utf8_general_ci");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)");

                    b.ToView("userpermissionview");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint")
                        .HasDefaultValueSql("'0'");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE6");

                    b.HasIndex(new[] { "UserId" }, "roleId_userId_idx");

                    b.HasIndex(new[] { "RoleId" }, "roleId_userRole_idx");

                    b.ToTable("userrole");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.StudentCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasIndex(new[] { "CourseId" }, "studentCourse_courseId_idx");

                    b.HasIndex(new[] { "StudentId" }, "studentCourse_studentId_idx");

                    b.ToTable("studentcourse");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConfirmationLink")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("CreatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsEmailConfirmed")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsSuperAdmin")
                        .HasColumnType("tinyint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("UpdatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "Email_UNIQUE")
                        .IsUnique();

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE7");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<byte>("IsArchived")
                        .HasColumnType("tinyint");

                    b.Property<int>("LessonId")
                        .HasColumnType("int")
                        .HasColumnName("lessonId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("UpdatedDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValueSql("''");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("Id_UNIQUE8");

                    b.HasIndex(new[] { "LessonId" }, "video_lessonId_idx");

                    b.ToTable("video");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Course", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.User", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("courseId_teacherId")
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Lesson", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("lesson_courseId")
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Permission", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.RoleModels.Module", "Module")
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("moduleId_permissionModuleId")
                        .HasPrincipalKey("Id")
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.RolePermission", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.RoleModels.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("permissionId_userPermissionId")
                        .IsRequired();

                    b.HasOne("ULearn.DbModel.Models.DB.RoleModels.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("roleId_rolePermission")
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.UserRole", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.RoleModels.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("roleId_userRole")
                        .IsRequired();

                    b.HasOne("ULearn.DbModel.Models.DB.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("roleId_userId")
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.StudentCourse", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("studentCourse_courseId")
                        .IsRequired();

                    b.HasOne("ULearn.DbModel.Models.DB.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("studentCourse_studentId")
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Video", b =>
                {
                    b.HasOne("ULearn.DbModel.Models.DB.Lesson", "Lesson")
                        .WithMany("Videos")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("video_lessonId")
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Course", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.Lesson", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Module", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.RoleModels.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ULearn.DbModel.Models.DB.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
