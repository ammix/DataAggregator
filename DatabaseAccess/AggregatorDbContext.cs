using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess;

public partial class AggregatorDbContext : DbContext
{
    public AggregatorDbContext()
    {
    }

    public AggregatorDbContext(DbContextOptions<AggregatorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer101> Customer101s { get; set; }

    public virtual DbSet<Customer145> Customer145s { get; set; }

    public virtual DbSet<Customer2> Customer2s { get; set; }

    public virtual DbSet<EventTypes2> EventTypes2s { get; set; }

    public virtual DbSet<Events101> Events101s { get; set; }

    public virtual DbSet<Events145> Events145s { get; set; }

    public virtual DbSet<Events2> Events2s { get; set; }

    public virtual DbSet<NotificationsBroker> NotificationsBrokers { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=test;User ID=sa;Password=1;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer101>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0787FF8003");

            entity.ToTable("Customer_101");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.Salutation).HasMaxLength(10);
        });

        modelBuilder.Entity<Customer145>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Customer_145");

            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);
        });

        modelBuilder.Entity<Customer2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC076F4849D4");

            entity.ToTable("Customer_2");

            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.JobPosition).HasMaxLength(128);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
        });

        modelBuilder.Entity<EventTypes2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventTyp__3214EC074B60C0B0");

            entity.ToTable("EventTypes_2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Events101>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_1__3214EC0769C97846");

            entity.ToTable("Events_101");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Events145>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_1__3214EC0702757525");

            entity.ToTable("Events_145");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CustomerId).HasMaxLength(128);
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Events2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_2__3214EC0702B0BE5F");

            entity.ToTable("Events_2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NotificationsBroker>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.FinHash });

            entity.ToTable("NotificationsBroker");

            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.FinHash).HasMaxLength(128);
            entity.Property(e => e.FirstName).HasMaxLength(128);
            entity.Property(e => e.LastName).HasMaxLength(128);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tenants__3214EC0758D1086D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.OrganisationName).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
