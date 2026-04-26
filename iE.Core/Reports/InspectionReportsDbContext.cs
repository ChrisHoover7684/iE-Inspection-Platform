using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports;

public class InspectionReportsDbContext(DbContextOptions<InspectionReportsDbContext> options) : DbContext(options)
{
    public DbSet<InspectionReport> InspectionReports => Set<InspectionReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InspectionReport>(builder =>
        {
            builder.ToTable("InspectionReports");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasMaxLength(64);
            builder.Property(r => r.TemplateId).HasMaxLength(128);
            builder.Property(r => r.ReportNumber).HasMaxLength(128);
            builder.Property(r => r.EquipmentTag).HasMaxLength(128);
            builder.Property(r => r.Unit).HasMaxLength(128);
            builder.Property(r => r.SystemId).HasMaxLength(128);
            builder.Property(r => r.CircuitId).HasMaxLength(128);
            builder.Property(r => r.Service).HasMaxLength(256);
            builder.Property(r => r.Status).HasMaxLength(32);

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
