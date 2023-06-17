using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Models;

public partial class Lab1Context : DbContext
{
    public Lab1Context()
    {
    }

    public Lab1Context(DbContextOptions<Lab1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<KodyPocztowe> KodyPocztowes { get; set; }

    public virtual DbSet<KodyPocztoweEf> KodyPocztoweEfs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LOCALHOST\\LOCALDATABASE;User ID=ADMIN;Password=cisco123;Encrypt=False;TrustServerCertificate=True;Database=lab1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KodyPocztowe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Kody_Pocztowe");

            entity.Property(e => e.Adres).HasColumnType("text");
            entity.Property(e => e.KodPocztowy)
                .HasColumnType("text")
                .HasColumnName("Kod_pocztowy");
            entity.Property(e => e.Miejscowosc).HasColumnType("text");
            entity.Property(e => e.Powiat).HasColumnType("text");
            entity.Property(e => e.Wojewodztwo).HasColumnType("text");
        });

        modelBuilder.Entity<KodyPocztoweEf>(entity =>
        {
            entity.ToTable("Kody_PocztoweEF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Adres).HasColumnType("text");
            entity.Property(e => e.KodPocztowy)
                .HasColumnType("text")
                .HasColumnName("Kod_pocztowy");
            entity.Property(e => e.Miejscowosc).HasColumnType("text");
            entity.Property(e => e.Powiat).HasColumnType("text");
            entity.Property(e => e.Wojewodztwo).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
