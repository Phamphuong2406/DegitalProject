using Microsoft.EntityFrameworkCore;

namespace DigitalProject.Entitys
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
       : base(options)
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Role> roles { get; set; }
        public virtual DbSet<UserRole> userRoles { get; set; }
        public virtual DbSet<Project> projects { get; set; }
        public virtual DbSet<Gallery> galleries { get; set; }
        public virtual DbSet<ContactRequest> contactRequests { get; set; }
        public virtual DbSet<Setting> settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)// Fluent API (Application Programming Interface)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.HashedPassword)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.PhoneNumber)
                      .HasMaxLength(20);

                entity.Property(u => u.RefreshToken)
                      .HasMaxLength(500);

                entity.Property(u => u.RefreshTokenExpired)
                      .HasMaxLength(100);

                entity.Property(u => u.IsActive)
                      .HasDefaultValue(true);

                entity.Property(u => u.note)
                      .HasMaxLength(500);

                // Quan hệ 1-nhiều: User → UserRole
                entity.HasMany(u => u.userRoles)
                      .WithOne(ur => ur.users)
                      .HasForeignKey(ur => ur.UserId)// khóa n
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.projects)
                .WithOne(ur => ur.users)
                .HasForeignKey(u => u.IdPoster)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.galleries)
                    .WithOne(ur => ur.users)
                    .HasForeignKey(ur => ur.PosterId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.contactRequests)
                .WithOne(ur => ur.users)
                .HasForeignKey(u => u.RespondentId)
                .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(r => r.userRoles)// quan hệ 1-n với bảng userrole
                      .WithOne(ur => ur.roles)
                      .HasForeignKey(ur => ur.RoleId)// với khóa ngoại là RoleId
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");

                entity.HasKey(ur => new { ur.UserId, ur.RoleId }); 
                entity.HasOne(ur => ur.users)
                      .WithMany(u => u.userRoles)
                      .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.roles)// quan hệ n-1 với bảng roles với khóa ngoại lấy từ bảng role là RoleId
                      .WithMany(r => r.userRoles)
                      .HasForeignKey(ur => ur.RoleId);//UserRole trỏ về Role qua khóa ngoại RoleId.
            });
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(r => r.ProjectId);


                entity.Property(u => u.ProjectName)
                .IsRequired()
                .HasMaxLength(255);
                entity.Property(u => u.ProjectType)
                .HasMaxLength(255);
                entity.Property(u => u.ImageUrl)
                .HasMaxLength(255);
                entity.Property(u => u.Shortdescription)
                .HasMaxLength(500);
                entity.Property(u => u.DetailedDescription)
                .IsRequired();
                entity.Property(u => u.architect)
                .HasMaxLength(100);
                entity.Property(u => u.structuralEngineer)
                .HasMaxLength(100);


            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("Gallerys");
                entity.HasKey(r => r.GalleryId);

                entity.Property(u => u.GalleryName)
               .IsRequired()
               .HasMaxLength(255);
                entity.Property(u => u.Address)
                .HasMaxLength(255);
            });

            modelBuilder.Entity<ContactRequest>(entity =>
            {
                entity.ToTable("ContactRequests");
                entity.HasKey(u => u.RequestId);

                entity.Property(u => u.CustommerName)
              .IsRequired()
              .HasMaxLength(100);
                entity.Property(u => u.CustomerPhoneNumber)
              .IsRequired()
              .HasMaxLength(20);
                entity.Property(u => u.CustomerEmail)
              .IsRequired()
              .HasMaxLength(50);
                entity.Property(u => u.CustomerMessage)
              .HasMaxLength(255);
                entity.Property(u => u.Note)
              .HasMaxLength(255);
            });
            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Settings");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Key)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(u => u.Value)
                .HasMaxLength(255);

            } );
        }
    }
}