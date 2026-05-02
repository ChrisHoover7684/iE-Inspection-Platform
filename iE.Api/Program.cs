using System.Text.Json.Serialization;
using iE.Core.Reports.Checklists;
using iE.Core.Reports.Drafting;
using iE.Core.Reports.Exports;
using iE.Core.Reports.Persistence;
using iE.Core.Reports.Photos;
using iE.Core.Reports.Rules;
using iE.Core.Reports.Review;
using iE.Core.Reports.Services;
using iE.Core.Reports.Templates;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<InspectionReportsDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("InspectionReports")
        ?? "Host=localhost;Port=5432;Database=inspection_reports;Username=postgres;Password=postgres"));

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InspectionReportsDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalDevelopmentCors");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
