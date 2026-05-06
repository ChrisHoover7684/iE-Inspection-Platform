using iE.Api;
using iE.Api.Auth;
using iE.Api.Middleware;
using iE.Api.Tenancy;
using iE.Core.MaterialStress.Services;
using iE.Core.Mechanical.PressureVessels;
using iE.Core.Reports.Checklists;
using iE.Core.Reports.Drafting;
using iE.Core.Reports.Exports;
using iE.Core.Reports.Persistence;
using iE.Core.Reports.Photos;
using iE.Core.Reports.Review;
using iE.Core.Reports.Rules;
using iE.Core.Reports.Services;
using iE.Core.Reports.Templates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var inspectionReportsConnectionString = StartupConfiguration.GetRequiredConnectionString(builder.Configuration);
var authenticationEnabled = StartupConfiguration.IsAuthenticationEnabled(builder.Configuration);
var authOptions = StartupConfiguration.GetAuthOptions(builder.Configuration);
AuthOptions.ValidateWhenEnabled(authenticationEnabled, authOptions);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<InspectionReportsDbContext>(options =>
    options.UseNpgsql(inspectionReportsConnectionString));

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
    .AddNpgSql(
        inspectionReportsConnectionString,
        name: "postgres",
        tags: new[] { "ready" });

builder.Services.AddScoped<InspectionReportRepository>();
builder.Services.AddScoped<PhotoMarkupRepository>();
builder.Services.AddScoped<InspectionReportFactory>();
builder.Services.AddScoped<InspectionSummaryService>();
builder.Services.AddScoped<SummaryBuilder>();
builder.Services.AddScoped<IInspectionTagRuleEngine, InspectionTagRuleEngine>();
builder.Services.AddScoped<InspectionAlertMapper>();
builder.Services.AddScoped<IInspectionAssistantService, InspectionAssistantService>();
builder.Services.AddScoped<IReportNarrativeGenerator, ReportNarrativeGenerator>();
builder.Services.AddScoped<ReportValidationService>();
builder.Services.AddScoped<RepairRecommendationBuilder>();
builder.Services.AddScoped<ReportDraftBuilder>();
builder.Services.AddScoped<NoFindingObservationBuilder>();
builder.Services.AddScoped<ObservationChecklistService>();
builder.Services.AddScoped<ChecklistMergeService>();
builder.Services.AddScoped<ReportWorkflowService>();
builder.Services.AddScoped<InspectionReportDocxExportService>();
builder.Services.AddScoped<PhotoAppendixExportService>();
builder.Services.AddScoped<IPhotoMarkupRenderer, PlaceholderPhotoMarkupRenderer>();
builder.Services.AddScoped<IPhotoDetectionService, PhotoDetectionService>();
builder.Services.AddScoped<AnnotatedPhotoExportService>();
builder.Services.AddSingleton<IReportTemplateRegistry, InMemoryReportTemplateRegistry>();
builder.Services.AddSingleton(_ => MaterialStressServiceFactory.CreateInitialized());
builder.Services.AddScoped<PressureVesselAllowableStressResolver>();
builder.Services.AddScoped<ITenantContextAccessor, TenantContextAccessor>();
builder.Services.AddScoped<ITenantContextBuilder>(_ => new TenantContextBuilder(authOptions));
builder.Services.AddScoped<ReportAccessGuard>();

if (authenticationEnabled)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = authOptions.Authority;
            options.Audience = authOptions.Audience;
            options.RequireHttpsMetadata = authOptions.RequireHttpsMetadata;
        });

    builder.Services.AddTransient<Microsoft.AspNetCore.Authentication.IClaimsTransformation, CapabilityClaimsTransformation>();
}

builder.Services.AddAuthorization(options =>
{
    foreach (var capability in AuthCapabilities.All)
    {
        options.AddPolicy(capability, policy => policy.RequireClaim("capability", capability));
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevelopmentCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

var applyMigrationsOnStartup = StartupConfiguration.ShouldApplyMigrationsOnStartup(builder.Configuration);
if (applyMigrationsOnStartup)
{
    app.Logger.LogInformation("Applying database migrations on startup because Database:ApplyMigrationsOnStartup=true.");

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<InspectionReportsDbContext>();
    try
    {
        dbContext.Database.Migrate();
        app.Logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Database migration failed during startup.");
        throw;
    }
}
else
{
    app.Logger.LogInformation("Skipping database migrations on startup because Database:ApplyMigrationsOnStartup is not enabled.");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalDevelopmentCors");
}

app.UseHttpsRedirection();

if (authenticationEnabled)
{
    app.UseAuthentication();
}

app.UseMiddleware<TenantContextMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();
