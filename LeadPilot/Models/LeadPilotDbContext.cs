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

        #region Seed Data
        modelBuilder.Entity<EmailType>().HasData(
            new EmailType() { Id=1,Name= "Initial",Active=true},
            new EmailType() { Id=2,Name= "Followup", Active=true}
           );

        modelBuilder.Entity<LeadSource>().HasData(
            new LeadSource() {Id=1,Name= "Google Map",Active=true },
            new LeadSource() {Id=2,Name= "HotFrog", Active=true }
            );

        modelBuilder.Entity<LeadStatus>().HasData(
            new LeadStatus() { Id=6,Name= "New" ,Active=true},
            new LeadStatus() { Id=7,Name= "InitialSent", Active=true},
            new LeadStatus() { Id=8,Name= "FollowUpSent", Active=true},
            new LeadStatus() { Id=9,Name= "Replied", Active=true},
            new LeadStatus() { Id=10,Name= "Closed", Active=true},
            new LeadStatus() { Id=11,Name= "DoNotContact", Active=true}
            );

        modelBuilder.Entity<EmailTemplate>().HasData(
            new EmailTemplate() { Id=1,Name= "Initial",
                EmailTypeId=1,Subject= "Quick question about your internal workflows",
                    Body= "<p>Hi@ContactName,</p><p> I came across @FirmName while researching businesses in @City and wanted to reach out.</p><p> Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>",
                Active=true,SouceId=null
                },
            new EmailTemplate()
            {
                Id = 3,
                Name = "FollowUp",
                EmailTypeId = 2,
                Subject = "Following up",
                Body = "<p>Hi@ContactName,</p>\r\n\r\n<p>Just following up on my earlier note.</p>\r\n\r\n<p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>",
                Active = true,
                SouceId = null
            }
            );
        #endregion


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
