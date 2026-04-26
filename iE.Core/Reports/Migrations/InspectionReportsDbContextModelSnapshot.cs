using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    [DbContext(typeof(InspectionReportsDbContext))]
    partial class InspectionReportsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("iE.Core.Reports.Asset", b =>
            {
                b.Property<string>("Id").HasMaxLength(64);
                b.Property<string>("EquipmentTag").HasMaxLength(128);
                b.Property<string>("EquipmentType").HasMaxLength(128);
                b.Property<string>("FacilityId").HasMaxLength(64);
                b.Property<bool>("IsActive");
                b.Property<string>("ProcessUnitId").HasMaxLength(64);
                b.Property<string>("Service").HasMaxLength(256);
                b.HasKey("Id");
                b.HasIndex("FacilityId");
                b.HasIndex("ProcessUnitId");
                b.ToTable("Assets", (string)null);
            });

            modelBuilder.Entity("iE.Core.Reports.ClientOrganization", b =>
            {
                b.Property<string>("Id").HasMaxLength(64);
                b.Property<DateTime>("CreatedAt");
                b.Property<bool>("IsActive");
                b.Property<string>("Name").HasMaxLength(256);
                b.HasKey("Id");
                b.ToTable("ClientOrganizations", (string)null);
            });

            modelBuilder.Entity("iE.Core.Reports.Facility", b =>
            {
                b.Property<string>("Id").HasMaxLength(64);
                b.Property<string>("ClientOrganizationId").HasMaxLength(64);
                b.Property<DateTime>("CreatedAt");
                b.Property<bool>("IsActive");
                b.Property<string>("Location").HasMaxLength(512);
                b.Property<string>("Name").HasMaxLength(256);
                b.HasKey("Id");
                b.HasIndex("ClientOrganizationId");
                b.ToTable("Facilities", (string)null);
            });

            modelBuilder.Entity("iE.Core.Reports.InspectionReport", b =>
            {
                b.Property<string>("Id").HasMaxLength(64);
                b.Property<string>("AssetId").HasMaxLength(64);
                b.Property<string>("CircuitId").HasMaxLength(128);
                b.Property<string>("ClientOrganizationId").HasMaxLength(64);
                b.Property<DateTime>("CreatedAt");
                b.Property<string>("CreatedByUserId").HasMaxLength(128);
                b.Property<string>("EquipmentTag").HasMaxLength(128);
                b.Property<string>("FacilityId").HasMaxLength(64);
                b.Property<string>("ProcessUnitId").HasMaxLength(64);
                b.Property<string>("ReportNumber").HasMaxLength(128);
                b.Property<string>("Service").HasMaxLength(256);
                b.Property<string>("Status").HasMaxLength(32);
                b.Property<string>("SystemId").HasMaxLength(128);
                b.Property<string>("TemplateId").HasMaxLength(128);
                b.Property<string>("Unit").HasMaxLength(128);
                b.Property<DateTime?>("UpdatedAt");
                b.Property<string>("UpdatedByUserId").HasMaxLength(128);
                b.HasKey("Id");
                b.HasIndex("AssetId");
                b.HasIndex("ClientOrganizationId");
                b.HasIndex("FacilityId");
                b.HasIndex("ProcessUnitId");
                b.HasIndex("Status");
                b.HasIndex("TemplateId");
                b.ToTable("InspectionReports", (string)null);
            });
#pragma warning restore 612, 618
        }
    }
}
