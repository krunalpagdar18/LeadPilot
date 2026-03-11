using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LeadPilot.Models;

public partial class LeadPilotDbContext : DbContext
{
    public LeadPilotDbContext()
    {
    }

    public LeadPilotDbContext(DbContextOptions<LeadPilotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<EmailType> EmailTypes { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<LeadEmailLog> LeadEmailLogs { get; set; }

    public virtual DbSet<LeadSource> LeadSources { get; set; }

    public virtual DbSet<LeadStatus> LeadStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DBEntities");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Email_Template_pkey");

            entity.ToTable("Email_Template");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.EmailTypeId).HasColumnName("EmailTypeID");
            entity.Property(e => e.SouceId).HasColumnName("SouceID");

            entity.HasOne(d => d.EmailType).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.EmailTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Email_Type_Email_Template");

            entity.HasOne(d => d.Souce).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.SouceId)
                .HasConstraintName("LeadSource_EmailTemplate");
        });

        modelBuilder.Entity<EmailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Email_Type_pkey");

            entity.ToTable("Email_Type");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Primary_Key_Lead");

            entity.ToTable("Lead");

            entity.HasIndex(e => e.Inactive, "IDX_Inactive").HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.HasIndex(e => e.StatusId, "IDX_Status").HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.HasIndex(e => e.AddedOn, "Idx_Addedon")
                .IsDescending()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.HasIndex(e => e.Website, "Uni_Website").IsUnique();

            entity.HasIndex(e => e.SourceId, "idx_Source").HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Inactive).HasDefaultValue(false);
            entity.Property(e => e.InactiveDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Source).WithMany(p => p.Leads)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("Lead_source");

            entity.HasOne(d => d.Status).WithMany(p => p.Leads)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("Lead_status");
        });

        modelBuilder.Entity<LeadEmailLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LeadEmailLog_pkey");

            entity.ToTable("LeadEmailLog");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.EmailTemplateId).HasColumnName("EmailTemplateID");
            entity.Property(e => e.LeadId).HasColumnName("LeadID");

            entity.HasOne(d => d.EmailTemplate).WithMany(p => p.LeadEmailLogs)
                .HasForeignKey(d => d.EmailTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lead_TemplateID");

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadEmailLogs)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lead_EmailLog");
        });

        modelBuilder.Entity<LeadSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lead_Source_pkey");

            entity.ToTable("Lead_Source");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<LeadStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lead_Status_pkey");

            entity.ToTable("Lead_Status");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });
       
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
