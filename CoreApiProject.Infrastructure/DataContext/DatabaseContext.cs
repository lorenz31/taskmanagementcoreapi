using CoreApiProject.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace CoreApiProject.DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<MemberProjects> MemberProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region User Configuration
            builder
                .Entity<User>()
                .ToTable("Users")
                .HasKey(p => p.Id);

            builder
                .Entity<User>()
                .Property(p => p.Username)
                .IsRequired();

            builder
                .Entity<User>()
                .Property(p => p.Password)
                .IsRequired();

            builder
                .Entity<User>()
                .Property(p => p.Salt)
                .IsRequired();
            #endregion

            #region Client Configuration
            //builder
            //    .Entity<Client>()
            //    .ToTable("Clients")
            //    .HasKey(p => p.Id);

            //builder
            //    .Entity<Client>()
            //    .Property(p => p.Email)
            //    .IsRequired();

            //builder
            //    .Entity<Client>()
            //    .Property(p => p.ApiKey)
            //    .IsRequired();
            #endregion

            #region Project Configuration
            builder
                .Entity<Project>()
                .ToTable("Projects")
                .HasKey(p => p.Id);

            builder
                .Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Project>()
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Entity<Project>()
                .Property(p => p.DateCreated)
                .IsRequired();

            builder
                .Entity<Project>()
                .Property(p => p.Status)
                .IsRequired();

            builder
                .Entity<Project>()
                .Property(p => p.Progress)
                .IsRequired();
            #endregion

            #region Tasks Configuration
            builder
                .Entity<Tasks>()
                .ToTable("Tasks")
                .HasKey(p => p.Id);

            builder
                .Entity<Tasks>()
                .HasOne(p => p.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Tasks>()
                .Property(p => p.Title)
                .IsRequired();

            builder
                .Entity<Tasks>()
                .Property(p => p.Description)
                .IsRequired();

            builder
                .Entity<Tasks>()
                .Property(p => p.AssigneeId);

            builder
                .Entity<Tasks>()
                .Property(p => p.Status)
                .IsRequired();

            builder
                .Entity<Tasks>()
                .Property(p => p.DateCreated)
                .IsRequired();

            builder
                .Entity<Tasks>()
                .Property(p => p.IsPriority)
                .IsRequired();
            #endregion

            #region Member Configuration
            builder
                .Entity<Member>()
                .ToTable("Members")
                .HasKey(p => p.Id);

            builder
                .Entity<Member>()
                .HasOne(p => p.User)
                .WithMany(p => p.Members)
                .HasForeignKey(p => p.UserId);

            builder
                .Entity<Member>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion

            #region Comments Configuration
            builder
                .Entity<Comments>()
                .ToTable("Comments")
                .HasKey(p => p.Id);

            builder
                .Entity<Comments>()
                .HasOne(p => p.Tasks)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.TaskId);

            builder
                .Entity<Comments>()
                .Property(p => p.Comment)
                .IsRequired();

            builder
                .Entity<Comments>()
                .Property(p => p.DateCreated)
                .IsRequired();

            builder
                .Entity<Comments>()
                .Property(p => p.MemberId)
                .IsRequired();
            #endregion

            #region Attachments Configuration
            builder
                .Entity<Attachments>()
                .ToTable("Attachments")
                .HasKey(p => p.Id);

            builder
                .Entity<Attachments>()
                .HasOne(p => p.Tasks)
                .WithMany(p => p.Attachments)
                .HasForeignKey(p => p.TaskId);

            builder
                .Entity<Attachments>()
                .Property(p => p.AttachmentLocation)
                .IsRequired();

            builder
                .Entity<Attachments>()
                .Property(p => p.DateCreated)
                .IsRequired();
            #endregion

            #region Member Projects Configuration
            builder
                .Entity<MemberProjects>()
                .HasKey(p => new { p.MemberId, p.ProjectId });

            builder
                .Entity<MemberProjects>()
                .HasOne(m => m.Member)
                .WithMany(mp => mp.MembersProjects)
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Entity<MemberProjects>()
                .HasOne(p => p.Project)
                .WithMany(mp => mp.MembersProjects)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion
        }
    }
}
