using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Vehicle_Config_DotNet_.Models;

namespace Vehicle_Config_DotNet_.Repositories;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }


    public virtual DbSet<AltComponent> AltComponents { get; set; }

    public virtual DbSet<CompanyInfo> CompanyInfos { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }
    public  DbSet<LogEntry> LogEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        if(!optionsBuilder.IsConfigured)
        optionsBuilder.UseMySql("server=localhost;port=3307;database=project;user=root;password=root", ServerVersion.Parse("8.0.40-mysql"));

        optionsBuilder.UseLazyLoadingProxies(false);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AltComponent>(entity =>
        {
            entity.HasKey(e => e.AltId).HasName("PRIMARY");

            entity.HasOne(d => d.AltComp).WithMany(p => p.AltComponentAltComps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKgvcwehmbldv5wtks58m5pxipg");

            entity.HasOne(d => d.Comp).WithMany(p => p.AltComponentComps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKh6wwkiru12oixcdy55to8y3xp");

            entity.HasOne(d => d.Model).WithMany(p => p.AltComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKiaxybepfrjxkrov9xnwl6fbtv");
        });

        modelBuilder.Entity<CompanyInfo>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.CompId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Seg).WithMany(p => p.Manufacturers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKvt86n4h6jurg9ofnq26txaw7");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.SafetyRating).HasDefaultValueSql("'5'");

            entity.HasOne(d => d.Manu).WithMany(p => p.Models).HasConstraintName("FKr7t3perk8n5abjcuk8j3kn9ko");

            entity.HasOne(d => d.Seg).WithMany(p => p.Models).HasConstraintName("FKeoehxbf066gnexos8p4o9fn5e");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<VehicleDetail>(entity =>
        {
            entity.HasKey(e => e.ConfiId).HasName("PRIMARY");

            entity.Property(e => e.CompType).IsFixedLength();
            entity.Property(e => e.IsConfigurable).IsFixedLength();

            entity.HasOne(d => d.Comp).WithMany(p => p.VehicleDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKq35ocm7yckn0ak8jrsk06l0c3");

            entity.HasOne(d => d.Model).WithMany(p => p.VehicleDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK4xnsh93sp191ava4jva94peds");  
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
