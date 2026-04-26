using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports;

public class InspectionReportsDbContext(DbContextOptions<InspectionReportsDbContext> options) : DbContext(options)
{
    public DbSet<ClientOrganization> ClientOrganizations => Set<ClientOrganization>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<ProcessUnit> ProcessUnits => Set<ProcessUnit>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<UserFacilityAccess> UserFacilityAccesses => Set<UserFacilityAccess>();
    public DbSet<InspectionReport> InspectionReports => Set<InspectionReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientOrganization>(builder =>
        {
            builder.ToTable("ClientOrganizations");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(64);
            builder.Property(c => c.Name).HasMaxLength(256);

            builder.HasMany(c => c.Facilities)
                .WithOne(f => f.ClientOrganization)
                .HasForeignKey(f => f.ClientOrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.UserFacilityAccesses)
                .WithOne(ufa => ufa.ClientOrganization)
                .HasForeignKey(ufa => ufa.ClientOrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.InspectionReports)
                .WithOne()
                .HasForeignKey(r => r.ClientOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new ClientOrganization
            {
                Id = "client-demo-refining",
                Name = "Demo Refining Client",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        });

        modelBuilder.Entity<Facility>(builder =>
        {
            builder.ToTable("Facilities");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).HasMaxLength(64);
            builder.Property(f => f.ClientOrganizationId).HasMaxLength(64);
            builder.Property(f => f.Name).HasMaxLength(256);
            builder.Property(f => f.Location).HasMaxLength(512);
            builder.HasIndex(f => f.ClientOrganizationId);

            builder.HasMany(f => f.ProcessUnits)
                .WithOne(pu => pu.Facility)
                .HasForeignKey(pu => pu.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.Assets)
                .WithOne(a => a.Facility)
                .HasForeignKey(a => a.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.InspectionReports)
                .WithOne()
                .HasForeignKey(r => r.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Facility
            {
                Id = "facility-demo-gulf-coast",
                ClientOrganizationId = "client-demo-refining",
                Name = "Demo Gulf Coast Refinery",
                Location = null,
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        });

        modelBuilder.Entity<ProcessUnit>(builder =>
        {
            builder.ToTable("ProcessUnits");
            builder.HasKey(pu => pu.Id);
            builder.Property(pu => pu.Id).HasMaxLength(64);
            builder.Property(pu => pu.FacilityId).HasMaxLength(64);
            builder.Property(pu => pu.Name).HasMaxLength(256);
            builder.Property(pu => pu.UnitCode).HasMaxLength(64);

            builder.HasMany(pu => pu.Assets)
                .WithOne(a => a.ProcessUnit)
                .HasForeignKey(a => a.ProcessUnitId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(new ProcessUnit
            {
                Id = "unit-demo-crude",
                FacilityId = "facility-demo-gulf-coast",
                Name = "Crude Unit",
                UnitCode = "001",
                IsActive = true
            });
        });

        modelBuilder.Entity<Asset>(builder =>
        {
            builder.ToTable("Assets");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasMaxLength(64);
            builder.Property(a => a.FacilityId).HasMaxLength(64);
            builder.Property(a => a.ProcessUnitId).HasMaxLength(64);
            builder.Property(a => a.EquipmentTag).HasMaxLength(128);
            builder.Property(a => a.EquipmentType).HasMaxLength(128);
            builder.Property(a => a.Service).HasMaxLength(256);
            builder.HasIndex(a => a.FacilityId);

            builder.HasData(new Asset
            {
                Id = "asset-demo-piping-system",
                FacilityId = "facility-demo-gulf-coast",
                ProcessUnitId = "unit-demo-crude",
                EquipmentTag = "EDR-010-H21",
                EquipmentType = "Piping",
                Service = "Demo Service",
                IsActive = true
            });
        });

        modelBuilder.Entity<UserFacilityAccess>(builder =>
        {
            builder.ToTable("UserFacilityAccesses");
            builder.HasKey(ufa => ufa.Id);
            builder.Property(ufa => ufa.Id).HasMaxLength(64);
            builder.Property(ufa => ufa.UserId).HasMaxLength(128);
            builder.Property(ufa => ufa.ClientOrganizationId).HasMaxLength(64);
            builder.Property(ufa => ufa.FacilityId).HasMaxLength(64);
            builder.Property(ufa => ufa.Role).HasMaxLength(64);

            builder.HasOne(ufa => ufa.Facility)
                .WithMany()
                .HasForeignKey(ufa => ufa.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ufa => ufa.UserId);
            builder.HasIndex(ufa => ufa.ClientOrganizationId);
            builder.HasIndex(ufa => ufa.FacilityId);
        });

        modelBuilder.Entity<InspectionReport>(builder =>
        {
            builder.ToTable("InspectionReports");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasMaxLength(64);
            builder.Property(r => r.ClientOrganizationId).HasMaxLength(64);
            builder.Property(r => r.FacilityId).HasMaxLength(64);
            builder.Property(r => r.ProcessUnitId).HasMaxLength(64);
            builder.Property(r => r.AssetId).HasMaxLength(64);
            builder.Property(r => r.CreatedByUserId).HasMaxLength(128);
            builder.Property(r => r.UpdatedByUserId).HasMaxLength(128);
            builder.Property(r => r.TemplateId).HasMaxLength(128);
            builder.Property(r => r.ReportNumber).HasMaxLength(128);
            builder.Property(r => r.EquipmentTag).HasMaxLength(128);
            builder.Property(r => r.Unit).HasMaxLength(128);
            builder.Property(r => r.SystemId).HasMaxLength(128);
            builder.Property(r => r.CircuitId).HasMaxLength(128);
            builder.Property(r => r.Service).HasMaxLength(256);
            builder.Property(r => r.Status).HasMaxLength(32);
            builder.HasIndex(r => r.ClientOrganizationId);
            builder.HasIndex(r => r.FacilityId);
            builder.HasIndex(r => r.TemplateId);
            builder.HasIndex(r => r.Status);

            builder.HasOne<ProcessUnit>()
                .WithMany()
                .HasForeignKey(r => r.ProcessUnitId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Asset>()
                .WithMany()
                .HasForeignKey(r => r.AssetId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.OwnsMany(r => r.Sections, sectionBuilder =>
            {
                sectionBuilder.ToTable("InspectionReportSections");
                sectionBuilder.WithOwner().HasForeignKey("InspectionReportId");
                sectionBuilder.Property<int>("Id");
                sectionBuilder.HasKey("Id");

                sectionBuilder.Property(s => s.SectionId).HasMaxLength(128);
                sectionBuilder.Property(s => s.SectionTitle).HasMaxLength(256);

                sectionBuilder.OwnsMany(s => s.Answers, answerBuilder =>
                {
                    answerBuilder.ToTable("InspectionReportAnswers");
                    answerBuilder.WithOwner().HasForeignKey("SectionId");
                    answerBuilder.Property<int>("Id");
                    answerBuilder.HasKey("Id");

                    answerBuilder.Property(a => a.FieldId).HasMaxLength(128);
                    answerBuilder.Property(a => a.Label).HasMaxLength(256);
                    answerBuilder.Property(a => a.DataType).HasMaxLength(64);
                    answerBuilder.Property(a => a.Value).HasMaxLength(4000);
                    answerBuilder.Property(a => a.Comment).HasMaxLength(4000);
                    answerBuilder.PrimitiveCollection(a => a.Values);
                });
            });

            builder.OwnsMany(r => r.Findings, findingBuilder =>
            {
                findingBuilder.ToTable("InspectionFindings");
                findingBuilder.WithOwner().HasForeignKey("InspectionReportId");
                findingBuilder.HasKey(f => f.Id);
                findingBuilder.Property(f => f.Id).HasMaxLength(64);
                findingBuilder.Property(f => f.ComponentLocation).HasMaxLength(256);
                findingBuilder.Property(f => f.ComponentType).HasMaxLength(256);
                findingBuilder.Property(f => f.FindingType).HasMaxLength(256);
                findingBuilder.Property(f => f.AssociatedChecklistItem).HasMaxLength(256);
                findingBuilder.Property(f => f.DetailedDescription).HasMaxLength(4000);
                findingBuilder.Property(f => f.Severity).HasMaxLength(64);
                findingBuilder.Property(f => f.RecommendationText).HasMaxLength(4000);
                findingBuilder.PrimitiveCollection(f => f.PhotoIds);
            });

            builder.OwnsMany(r => r.Photos, photoBuilder =>
            {
                photoBuilder.ToTable("InspectionPhotos");
                photoBuilder.WithOwner().HasForeignKey("InspectionReportId");
                photoBuilder.HasKey(p => p.Id);
                photoBuilder.Property(p => p.Id).HasMaxLength(64);
                photoBuilder.Property(p => p.PhotoNumber).HasMaxLength(64);
                photoBuilder.Property(p => p.Description).HasMaxLength(2000);
                photoBuilder.Property(p => p.RelatedComponent).HasMaxLength(256);
                photoBuilder.Property(p => p.RelatedChecklistItem).HasMaxLength(256);
                photoBuilder.Property(p => p.FileName).HasMaxLength(512);
                photoBuilder.Property(p => p.FileUrl).HasMaxLength(2000);
            });
        });
    }
}
