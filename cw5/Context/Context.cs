using cw5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cw5.Context;

public partial class Context : DbContext
{
    public Context(){}
    public Context(DbContextOptions<Context> options): base(options) {}
    
    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<Country> Country { get; set; }
    public virtual DbSet<Country_Trip> Country_Trip { get; set; }
    public virtual DbSet<Trip> Trip { get; set; }
    public virtual DbSet<Client_Trip> Client_Trip { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSqlServer("Data Source=localhost;Initial Catalog=APBD;User ID=sa;Password=asd123POKo223;Encrypt=False")
        .LogTo(Console.WriteLine, LogLevel.Information);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK_Client");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("IdClient");
            
            entity.Property(e => e.FirstName)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("FirstName");
            
            entity.Property(e => e.LastName)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("LastName");
            
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Email");
            
            entity.Property(e => e.Telephone)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Telephone");
            
            entity.Property(e => e.Pesel)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Pesel");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("PK_Country");

            entity.ToTable("Country");

            entity.Property(e => e.IdCountry).HasColumnName("IdCountry");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Name");
        });

        modelBuilder.Entity<Country_Trip>(entity =>
        {
            entity.HasKey(e => new { e.IdCountry, e.IdTrip }).HasName("PK_Country_Trip");

            entity.ToTable("Country_Trip");

            entity.Property(e => e.IdCountry).HasColumnName("IdCountry");
            
            entity.Property(e => e.IdTrip).HasColumnName("IdTrip");
            
            entity.HasOne(d => d.Country)
                .WithMany(p => p.CountryTrips)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK_Country_Trip_Country");

            entity.HasOne(d => d.Trip)
                .WithMany(p => p.CountryTrips)
                .HasForeignKey(d => d.IdTrip)
                .HasConstraintName("FK_Country_Trip_Trip");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("PK_Trip");

            entity.ToTable("Trip");

            entity.Property(e => e.IdTrip).HasColumnName("IdTrip");
            
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Name");
            
            entity.Property(e => e.Description)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("Description");
            
            entity.Property(e => e.DateFrom).HasColumnName("DateFrom");
            entity.Property(e => e.DateTo).HasColumnName("DateTo");
            entity.Property(e => e.MaxPeople).HasColumnName("MaxPeople");
        });

        modelBuilder.Entity<Client_Trip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("PK_Client_Trip");

            entity.ToTable("Client_Trip");

            entity.Property(e => e.IdClient).HasColumnName("IdClient");
            entity.Property(e => e.IdTrip).HasColumnName("IdClient");
            
            entity.Property(e => e.RegisteredAt).HasColumnName("RegisteredAt");
            
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("PaymentDate");
            
            entity.HasOne(d => d.Client)
                .WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Client_Trip_Client");

            entity.HasOne(d => d.Trip)
                .WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .HasConstraintName("FK_Client_Trip_Trip");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}