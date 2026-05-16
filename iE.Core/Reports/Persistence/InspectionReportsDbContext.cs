using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports.Persistence;

public class InspectionReportsDbContext(DbContextOptions<InspectionReportsDbContext> options) : DbContext(options)
{
    public DbSet<ClientOrganization> ClientOrganizations => Set<ClientOrganization>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<ProcessUnit> ProcessUnits => Set<ProcessUnit>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<UserFacilityAccess> UserFacilityAccesses => Set<UserFacilityAccess>();
    public DbSet<ClientOrganizationUser> ClientOrganizationUsers => Set<ClientOrganizationUser>();
    public DbSet<ClientOrganizationUserSession> ClientOrganizationUserSessions => Set<ClientOrganizationUserSession>();
    public DbSet<ClientOrganizationUserDevice> ClientOrganizationUserDevices => Set<ClientOrganizationUserDevice>();
    public DbSet<InspectionReport> InspectionReports => Set<InspectionReport>();
    public DbSet<PhotoMarkup> PhotoMarkups => Set<PhotoMarkup>();
    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<ClientSubscription> ClientSubscriptions => Set<ClientSubscription>();
    public DbSet<SubscriptionEntitlement> SubscriptionEntitlements => Set<SubscriptionEntitlement>();
    public DbSet<ReportLogEntry> ReportLogEntries => Set<ReportLogEntry>();
    public DbSet<NdeRequest> NdeRequests => Set<NdeRequest>();
    public DbSet<NdeRequestTypeDefinition> NdeRequestTypeDefinitions => Set<NdeRequestTypeDefinition>();

    public override int SaveChanges()
    {
        EnforceAppendOnlyAuditEvents();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        EnforceAppendOnlyAuditEvents();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        EnforceAppendOnlyAuditEvents();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        EnforceAppendOnlyAuditEvents();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void EnforceAppendOnlyAuditEvents()
    {
        var invalidAudit = ChangeTracker.Entries<AuditEvent>().Any(e => e.State is EntityState.Modified or EntityState.Deleted);
        if (invalidAudit) throw new InvalidOperationException("AuditEvents are append-only.");
        var invalidReportLog = ChangeTracker.Entries<ReportLogEntry>().Any(e => e.State is EntityState.Modified or EntityState.Deleted);
        if (invalidReportLog) throw new InvalidOperationException("ReportLogEntries are append-only.");
    }

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

            builder.HasMany<ClientOrganizationUser>()
                .WithOne(cou => cou.ClientOrganization)
                .HasForeignKey(cou => cou.ClientOrganizationId)
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

        modelBuilder.Entity<ClientOrganizationUser>(builder =>
        {
            builder.ToTable("ClientOrganizationUsers");
            builder.HasKey(cou => cou.Id);
            builder.Property(cou => cou.Id).HasMaxLength(64);
            builder.Property(cou => cou.ClientOrganizationId).HasMaxLength(64);
            builder.Property(cou => cou.ExternalSubject).HasMaxLength(256);
            builder.Property(cou => cou.Email).HasMaxLength(256);
            builder.Property(cou => cou.NormalizedEmail).HasMaxLength(256);
            builder.Property(cou => cou.DisplayName).HasMaxLength(256);
            builder.Property(cou => cou.Status).HasMaxLength(32);

            builder.HasIndex(cou => cou.ClientOrganizationId);
            builder.HasIndex(cou => new { cou.ClientOrganizationId, cou.ExternalSubject }).IsUnique().HasFilter("\"ExternalSubject\" IS NOT NULL");
            builder.HasIndex(cou => new { cou.ClientOrganizationId, cou.NormalizedEmail });
            builder.HasIndex(cou => new { cou.ClientOrganizationId, cou.Status });
        });

        modelBuilder.Entity<ClientOrganizationUserSession>(builder =>
        {
            builder.ToTable("ClientOrganizationUserSessions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationId).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationUserId).HasMaxLength(64);
            builder.Property(x => x.ExternalSubject).HasMaxLength(256);
            builder.Property(x => x.SessionKeyHash).HasMaxLength(128);
            builder.Property(x => x.IpHash).HasMaxLength(128);
            builder.Property(x => x.UserAgentHash).HasMaxLength(128);
            builder.Property(x => x.DeviceId).HasMaxLength(64);
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ExternalSubject });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ClientOrganizationUserId });
            builder.HasIndex(x => x.LastSeenAtUtc);
            builder.HasIndex(x => x.IsRevoked);
        });

        modelBuilder.Entity<ClientOrganizationUserDevice>(builder =>
        {
            builder.ToTable("ClientOrganizationUserDevices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationId).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationUserId).HasMaxLength(64);
            builder.Property(x => x.ExternalSubject).HasMaxLength(256);
            builder.Property(x => x.DeviceFingerprintHash).HasMaxLength(128);
            builder.Property(x => x.LastIpHash).HasMaxLength(128);
            builder.Property(x => x.LastUserAgentHash).HasMaxLength(128);
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ExternalSubject });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ClientOrganizationUserId });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.DeviceFingerprintHash });
            builder.HasIndex(x => x.LastSeenAtUtc);
            builder.HasIndex(x => x.IsDisabled);
        });


        modelBuilder.Entity<AuditEvent>(builder =>
        {
            builder.ToTable("AuditEvents");
            builder.HasKey(a => a.AuditEventId);
            builder.Property(a => a.AuditEventId).HasMaxLength(64);
            builder.Property(a => a.TenantId).HasMaxLength(64);
            builder.Property(a => a.FacilityId).HasMaxLength(64);
            builder.Property(a => a.ActorUserId).HasMaxLength(128);
            builder.Property(a => a.Action).HasMaxLength(128);
            builder.Property(a => a.ResourceType).HasMaxLength(64);
            builder.Property(a => a.ResourceId).HasMaxLength(128);
            builder.Property(a => a.Result).HasMaxLength(32);
            builder.Property(a => a.CorrelationId).HasMaxLength(128);
            builder.Property(a => a.ClientIp).HasMaxLength(64);
            builder.Property(a => a.UserAgent).HasMaxLength(512);
            builder.Property(a => a.MetadataJson).HasMaxLength(4000);
            builder.HasIndex(a => a.TenantId);
            builder.HasIndex(a => new { a.ResourceType, a.ResourceId });
            builder.HasIndex(a => a.ActorUserId);
            builder.HasIndex(a => a.OccurredAtUtc);
            builder.HasIndex(a => new { a.TenantId, a.OccurredAtUtc });
            builder.HasIndex(a => new { a.TenantId, a.ResourceType, a.ResourceId });
            builder.HasIndex(a => new { a.TenantId, a.ActorUserId });
            builder.HasIndex(a => new { a.TenantId, a.Action });
            builder.HasIndex(a => new { a.TenantId, a.FacilityId });
        });



        modelBuilder.Entity<SubscriptionPlan>(builder =>
        {
            builder.ToTable("SubscriptionPlans");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasMaxLength(64);
            builder.Property(p => p.Name).HasMaxLength(128);
            builder.Property(p => p.PlanCode).HasMaxLength(64);
            builder.Property(p => p.MetadataJson).HasMaxLength(1000);
            builder.HasIndex(p => p.PlanCode).IsUnique();
        });

        modelBuilder.Entity<ClientSubscription>(builder =>
        {
            builder.ToTable("ClientSubscriptions");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasMaxLength(64);
            builder.Property(s => s.ClientOrganizationId).HasMaxLength(64);
            builder.Property(s => s.PlanId).HasMaxLength(64);
            builder.Property(s => s.Status).HasMaxLength(32);
            builder.HasIndex(s => s.ClientOrganizationId);
            builder.HasIndex(s => s.PlanId);
            builder.HasIndex(s => s.Status);

            builder.HasOne(s => s.Plan)
                .WithMany(p => p.ClientSubscriptions)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SubscriptionEntitlement>(builder =>
        {
            builder.ToTable("SubscriptionEntitlements");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(64);
            builder.Property(e => e.PlanId).HasMaxLength(64);
            builder.Property(e => e.EntitlementKey).HasMaxLength(128);
            builder.HasIndex(e => e.PlanId);
            builder.HasIndex(e => e.EntitlementKey);
            builder.HasIndex(e => new { e.PlanId, e.EntitlementKey }).IsUnique();

            builder.HasOne(e => e.Plan)
                .WithMany(p => p.Entitlements)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.Cascade);
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
                findingBuilder.Property(f => f.Location).HasMaxLength(256);
                findingBuilder.Property(f => f.ComponentType).HasMaxLength(256);
                findingBuilder.Property(f => f.FindingType).HasConversion<string>().HasMaxLength(64);
                findingBuilder.Property(f => f.AssociatedChecklistItem).HasMaxLength(256);
                findingBuilder.Property(f => f.Description).HasMaxLength(4000);
                findingBuilder.Property(f => f.Severity).HasConversion<string>().HasMaxLength(64);
                findingBuilder.Property(f => f.RepairRecommendation).HasMaxLength(4000);
                findingBuilder.Property(f => f.LineNumber).HasMaxLength(128);
                findingBuilder.Property(f => f.ApproximateFeetOfFindings);
                findingBuilder.Property(f => f.CorrosionAllowance);
                findingBuilder.PrimitiveCollection(f => f.PhotoIds);
            });


            builder.OwnsMany(r => r.Observations, observationBuilder =>
            {
                observationBuilder.ToTable("InspectionObservations");
                observationBuilder.WithOwner().HasForeignKey("InspectionReportId");
                observationBuilder.HasKey(o => o.Id);
                observationBuilder.Property(o => o.Id).HasMaxLength(64);
                observationBuilder.Property(o => o.Category).HasMaxLength(256);
                observationBuilder.Property(o => o.Status).HasConversion<string>().HasMaxLength(64);
                observationBuilder.Property(o => o.Notes).HasMaxLength(4000);
                observationBuilder.PrimitiveCollection(o => o.PhotoIds);
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

            builder.OwnsMany(r => r.Calculations, calcBuilder =>
            {
                calcBuilder.ToTable("InspectionCalculations");
                calcBuilder.WithOwner().HasForeignKey("InspectionReportId");
                calcBuilder.Property<int>("IdPk");
                calcBuilder.HasKey("IdPk");

                calcBuilder.Property(c => c.Id).HasMaxLength(128);
                calcBuilder.Property(c => c.CalculationType).HasMaxLength(128);
                calcBuilder.Property(c => c.DisplayName).HasMaxLength(256);
                calcBuilder.Property(c => c.FormulaVersion).HasMaxLength(128);
                calcBuilder.Property(c => c.Inputs).HasMaxLength(16000);
                calcBuilder.Property(c => c.Outputs).HasMaxLength(16000);
                calcBuilder.Property(c => c.InsertLabel).HasMaxLength(512);
                calcBuilder.Property(c => c.LinkedSectionId).HasMaxLength(128);
                calcBuilder.Property(c => c.LinkedFieldId).HasMaxLength(128);
                calcBuilder.Property(c => c.LinkedFindingId).HasMaxLength(128);

                calcBuilder.OwnsMany(c => c.StandardReferences, refs =>
                {
                    refs.ToTable("InspectionCalculationReferences");
                    refs.WithOwner().HasForeignKey("CalculationIdPk");
                    refs.Property<int>("IdPk");
                    refs.HasKey("IdPk");
                    refs.Property(r => r.Standard).HasMaxLength(128);
                    refs.Property(r => r.Edition).HasMaxLength(128);
                    refs.Property(r => r.Paragraph).HasMaxLength(128);
                    refs.Property(r => r.Note).HasMaxLength(1000);
                });

                calcBuilder.OwnsMany(c => c.Warnings, warnings =>
                {
                    warnings.ToTable("InspectionCalculationWarnings");
                    warnings.WithOwner().HasForeignKey("CalculationIdPk");
                    warnings.Property<int>("IdPk");
                    warnings.HasKey("IdPk");
                    warnings.Property(w => w.Code).HasMaxLength(128);
                    warnings.Property(w => w.Message).HasMaxLength(2000);
                    warnings.Property(w => w.Severity).HasMaxLength(32);
                });
            });

            builder.OwnsMany(r => r.ReviewHistory, reviewHistoryBuilder =>
            {
                reviewHistoryBuilder.ToTable("ReportReviewHistory");
                reviewHistoryBuilder.WithOwner().HasForeignKey("ReportId");
                reviewHistoryBuilder.HasKey(h => h.Id);
                reviewHistoryBuilder.Property(h => h.Id).HasMaxLength(64);
                reviewHistoryBuilder.Property(h => h.ReportId).HasMaxLength(64);
                reviewHistoryBuilder.Property(h => h.Action).HasConversion<string>().HasMaxLength(64);
                reviewHistoryBuilder.Property(h => h.Comments).HasMaxLength(4000);
                reviewHistoryBuilder.Property(h => h.PerformedByUserId).HasMaxLength(128);
                reviewHistoryBuilder.Property(h => h.PerformedAt);
                reviewHistoryBuilder.HasIndex("ReportId", nameof(ReportReviewHistory.PerformedAt));
            });

            builder.OwnsOne(r => r.PipingProfile, pipingBuilder =>
            {
                pipingBuilder.PrimitiveCollection(p => p.LineNumbers);
                pipingBuilder.Property(p => p.LineNumber).HasMaxLength(128);
                pipingBuilder.Property(p => p.ApproximateFeetOfFindings);
                pipingBuilder.Property(p => p.UpstreamEquipment).HasMaxLength(256);
                pipingBuilder.Property(p => p.DownstreamEquipment).HasMaxLength(256);
                pipingBuilder.Property(p => p.FromLocation).HasMaxLength(256);
                pipingBuilder.Property(p => p.ToLocation).HasMaxLength(256);
                pipingBuilder.Property(p => p.NominalPipeSize).HasMaxLength(64);
                pipingBuilder.Property(p => p.PipingClass).HasMaxLength(128);
                pipingBuilder.Property(p => p.InsulatedStatus).HasMaxLength(64);
            });
        });


        modelBuilder.Entity<ReportLogEntry>(builder =>
        {
            builder.ToTable("ReportLogEntries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.FacilityId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.ReportId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.EventType).HasMaxLength(64).IsRequired();
            builder.Property(x => x.EventStatus).HasMaxLength(32);
            builder.Property(x => x.Message).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ActorUserId).HasMaxLength(128);
            builder.Property(x => x.ActorExternalSubject).HasMaxLength(256);
            builder.Property(x => x.FromStatus).HasMaxLength(32);
            builder.Property(x => x.ToStatus).HasMaxLength(32);
            builder.Property(x => x.RelatedNdeRequestId).HasMaxLength(64);
            builder.Property(x => x.MetadataJson).HasMaxLength(1000);
            builder.HasIndex(x => new { x.ClientOrganizationId, x.FacilityId, x.ReportId, x.CreatedAtUtc });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ReportId });
            builder.HasIndex(x => x.EventType);
        });

        modelBuilder.Entity<NdeRequest>(builder =>
        {
            builder.ToTable("NdeRequests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.FacilityId).HasMaxLength(64).IsRequired();
            builder.Property(x => x.ProcessUnitId).HasMaxLength(64);
            builder.Property(x => x.AssetId).HasMaxLength(64);
            builder.Property(x => x.ReportId).HasMaxLength(64);
            builder.Property(x => x.RequestNumber).HasMaxLength(64);
            builder.Property(x => x.NdeType).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Priority).HasMaxLength(32).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(32).IsRequired();
            builder.Property(x => x.Reason).HasMaxLength(256).IsRequired();
            builder.Property(x => x.RequestedByUserId).HasMaxLength(128);
            builder.Property(x => x.RequestedByExternalSubject).HasMaxLength(256);
            builder.Property(x => x.AssignedToName).HasMaxLength(256);
            builder.Property(x => x.VendorName).HasMaxLength(256);
            builder.Property(x => x.ResultsSummary).HasMaxLength(1000);
            builder.Property(x => x.CancellationReason).HasMaxLength(500);
            builder.HasIndex(x => new { x.ClientOrganizationId, x.FacilityId, x.Status });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.ReportId });
            builder.HasIndex(x => new { x.ClientOrganizationId, x.AssetId });
            builder.HasIndex(x => x.DueDateUtc);
            builder.HasIndex(x => x.CreatedAtUtc);
        });

        modelBuilder.Entity<NdeRequestTypeDefinition>(builder =>
        {
            builder.ToTable("NdeRequestTypeDefinitions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64);
            builder.Property(x => x.ClientOrganizationId).HasMaxLength(64);
            builder.Property(x => x.Code).HasMaxLength(64).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(512);
            builder.HasIndex(x => new { x.ClientOrganizationId, x.Code });
            builder.HasIndex(x => x.IsBuiltIn);
            builder.HasIndex(x => x.IsActive);
        });

        modelBuilder.Entity<PhotoMarkup>(builder =>
        {
            builder.ToTable("PhotoMarkups");
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.Id).HasMaxLength(64);
            builder.Property(pm => pm.PhotoId).HasMaxLength(64);
            builder.Property(pm => pm.MarkupJson).HasMaxLength(16000);
            builder.Property(pm => pm.CreatedAt);
            builder.HasIndex(pm => pm.PhotoId);
            builder.HasIndex(pm => pm.CreatedAt);
        });
    }
}
