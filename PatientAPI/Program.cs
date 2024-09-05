using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientAPI.Database;
using System.Reflection;
using PatientAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using PatientAPI.Mapping;
using PatientAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "PatientRestAPI",
		Version = "v1",
		Description = "REST API for Patient",
	});

	c.IncludeXmlComments(
		Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
	c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfile));
builder.Services.AddTransient<IPatientService, PatientService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<PatientDto>, PatientValidator>();

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
	var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationContext>();
	context?.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
