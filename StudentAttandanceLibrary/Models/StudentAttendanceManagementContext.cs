using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentAttandanceLibrary.Models
{
    public partial class StudentAttendanceManagementContext : DbContext
    {
        public StudentAttendanceManagementContext()
        {
        }

        public StudentAttendanceManagementContext(DbContextOptions<StudentAttendanceManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("server=localhost;database=StudentAttendanceManagement;user=sa;password=123456");
                optionsBuilder.UseSqlServer("server=localhost;database=StudentAttendanceManagement;user=sa;password=tien261001");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasMaxLength(256);

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__RoleID__2E1BDC42");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(256);

                entity.Property(e => e.Image).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.AdminNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Admin__AdminId__31EC6D26");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.StudentId).HasMaxLength(256);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Sessi__44FF419A");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Stude__45F365D3");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseCode).HasMaxLength(7);

                entity.Property(e => e.CourseName).HasMaxLength(256);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.GroupName).HasMaxLength(256);

                entity.Property(e => e.TeacherId).HasMaxLength(256);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Group__CourseId__3B75D760");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Group__TeacherId__3A81B327");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Group__TermId__3C69FB99");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).HasMaxLength(256);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomName).HasMaxLength(8);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TeacherId).HasMaxLength(256);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__GroupId__3F466844");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__RoomId__412EB0B6");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Teacher__4222D4EF");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.TimeSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__TimeSlo__403A8C7D");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasMaxLength(256);

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FullName).HasMaxLength(256);

                entity.Property(e => e.Image).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__Student__37A5467C");

                entity.HasMany(d => d.Groups)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentGroup",
                        l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_G__Group__49C3F6B7"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_G__Stude__48CFD27E"),
                        j =>
                        {
                            j.HasKey("StudentId", "GroupId").HasName("PK__Student___838C84AF910C5D35");

                            j.ToTable("Student_Group");

                            j.IndexerProperty<string>("StudentId").HasMaxLength(256);
                        });
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId).HasMaxLength(256);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FullName).HasMaxLength(256);

                entity.Property(e => e.Image).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.TeacherNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher__Teacher__34C8D9D1");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term");

                entity.Property(e => e.TermName).HasMaxLength(10);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.ToTable("TimeSlot");

                entity.Property(e => e.Description).HasMaxLength(13);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
