using System.Text.Json.Serialization;
using iE.Core.Reports;
using iE.Core.Reports.Services;
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
builder.Services.AddScoped<InspectionReportFactory>();
builder.Services.AddScoped<InspectionReportDocxExportService>();
builder.Services.AddScoped<PhotoAppendixExportService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
