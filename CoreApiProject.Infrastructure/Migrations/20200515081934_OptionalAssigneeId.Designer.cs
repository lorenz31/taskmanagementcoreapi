﻿// <auto-generated />
using System;
using CoreApiProject.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreApiProject.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200515081934_OptionalAssigneeId")]
    partial class OptionalAssigneeId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreApiProject.Core.Models.Attachments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttachmentLocation")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated");

                    b.Property<Guid>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Comments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated");

                    b.Property<Guid>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.MemberProjects", b =>
                {
                    b.Property<Guid>("MemberId");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("MemberId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("MemberProjects");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Progress");

                    b.Property<DateTime>("StartDate");

                    b.Property<bool>("Status");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Tasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssigneeId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("ProjectId");

                    b.Property<bool>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Attachments", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.Tasks", "Tasks")
                        .WithMany("Attachments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Comments", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.Tasks", "Tasks")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Member", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.User", "User")
                        .WithMany("Members")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.MemberProjects", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.Member", "Member")
                        .WithMany("MembersProjects")
                        .HasForeignKey("MemberId");

                    b.HasOne("CoreApiProject.Core.Models.Project", "Project")
                        .WithMany("MembersProjects")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Project", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApiProject.Core.Models.Tasks", b =>
                {
                    b.HasOne("CoreApiProject.Core.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
