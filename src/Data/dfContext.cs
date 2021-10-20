using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data
{
  public partial class dfContext : DbContext
  {
    public dfContext()
    {
    }

    public dfContext(DbContextOptions<dfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Client> Clients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(DH.CS);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<Models.Client>(entity =>
      {
        entity.ToTable("client");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Birthdate)
                  .HasColumnType("datetime")
                  .HasColumnName("birthdate");

        entity.Property(e => e.Cellphone)
                  .HasMaxLength(20)
                  .IsUnicode(false)
                  .HasColumnName("cellphone")
                  .HasDefaultValueSql("('')");

        entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(80)
                  .IsUnicode(false)
                  .HasColumnName("email")
                  .HasDefaultValueSql("('-')");

        entity.Property(e => e.Firstname)
                  .IsRequired()
                  .HasMaxLength(80)
                  .IsUnicode(false)
                  .HasColumnName("firstname")
                  .HasDefaultValueSql("('new name')");

        entity.Property(e => e.Lastname)
                  .IsRequired()
                  .HasMaxLength(80)
                  .IsUnicode(false)
                  .HasColumnName("lastname")
                  .HasDefaultValueSql("('last name')");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
