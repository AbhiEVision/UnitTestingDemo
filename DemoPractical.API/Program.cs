using DemoPractical._Domain.Interface;
using DemoPractical.API.Middleware;
using DemoPractical.API.Services;
using DemoPractical.API.Swagger;
using DemoPractical.DataAccessLayer.Data;
using DemoPractical.DataAccessLayer.Repositories;
using DemoPractical.DataAccessLayer.ValidationClass;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using DemoPractical.Models.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Services Register


builder.Services.AddDbContext<ApplicationDataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DemoApp"));
	//options.UseSqlServer(builder.Configuration.GetConnectionString("Laptop"));
});

// Adding Authorization and Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(auth =>
{
	auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
	option.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		RequireExpirationTime = true,
		ValidateLifetime = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
		ValidateIssuerSigningKey = true,
		ClockSkew = TimeSpan.Zero,
	};
	option.SaveToken = true;
});


// Register various inbuilt services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Fluent Validation 
builder.Services.AddFluentValidationAutoValidation();

// Serilog Registration
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Register class for fluent validations 
builder.Services.AddScoped<IValidator<ConractBaseEmployee>, ContractBaseEmployeeValidate>();
builder.Services.AddScoped<IValidator<Department>, DepartmentValidation>();
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidation>();
builder.Services.AddScoped<IValidator<PermentEmployee>, PermanentEmployeeValidation>();
builder.Services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidation>();
builder.Services.AddScoped<IValidator<EmailModel>, EmailModelValidation>();
builder.Services.AddScoped<IValidator<Role>, RoleValidation>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginValidation>();

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Register Services
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IJwtServices, JwtServices>();

// Error handling class for middleware
builder.Services.AddTransient<ErrorHandalingMiddleware>();

// Object of Email configuration
builder.Services.AddSingleton<EmailConfigurations>(builder.Configuration.GetSection("EmailConfigurations").Get<EmailConfigurations>());

// Swagger options registrations
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();

// Sentry Registration
builder.WebHost.UseSentry();

// API Versing
builder.Services.AddApiVersioning(opt =>
{
	opt.AssumeDefaultVersionWhenUnspecified = true;
	opt.DefaultApiVersion = new ApiVersion(2, 1);
	opt.ReportApiVersions = true;
});

//Setup API explorer that is API version aware
builder.Services.AddVersionedApiExplorer(setup =>
{
	setup.GroupNameFormat = "'v'VVV";
	setup.SubstituteApiVersionInUrl = true;
});

// Register Health Checks 
builder.Services.AddHealthChecks()
	.AddSqlServer(builder.Configuration.GetConnectionString("DemoApp"));


#endregion

var app = builder.Build();

#region Adding Middleware

app.UseSwaggerUI(options =>
{
	var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
	foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
	{
		options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
			description.GroupName.ToUpperInvariant());
	}
});

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandalingMiddleware>();

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

#endregion