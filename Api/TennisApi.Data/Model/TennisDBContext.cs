using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TennisApi.Data.Model
{
    public partial class TennisDBContext : DbContext
    {
        public TennisDBContext()
        {
        }

        public TennisDBContext(DbContextOptions<TennisDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountAddress> AccountAddress { get; set; }
        public virtual DbSet<AccountEmail> AccountEmail { get; set; }
        public virtual DbSet<AccountPhone> AccountPhone { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Court> Court { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonAddress> PersonAddress { get; set; }
        public virtual DbSet<PersonEmail> PersonEmail { get; set; }
        public virtual DbSet<PersonPhone> PersonPhone { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=innoTennisDB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AccountAddress>(entity =>
            {
                entity.HasKey(e => new { e.AccountID, e.AddressID });
            });

            modelBuilder.Entity<AccountEmail>(entity =>
            {
                entity.HasKey(e => new { e.AccountID, e.EmailID });
            });

            modelBuilder.Entity<AccountPhone>(entity =>
            {
                entity.HasKey(e => new { e.AccountID, e.PhoneID });
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("Address")
                    .HasMaxLength(250);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Court>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Ilumination).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Email1)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(250);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<PersonAddress>(entity =>
            {
                entity.HasKey(e => new { e.PersonID, e.AddressID });
            });

            modelBuilder.Entity<PersonEmail>(entity =>
            {
                entity.HasKey(e => new { e.PersonID, e.EmailID });
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                entity.HasKey(e => new { e.PersonID, e.PhoneID });
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.PermissionID });

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
