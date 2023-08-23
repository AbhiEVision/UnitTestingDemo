using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace DemoPractical.API.Swagger
{
	public class SwaggerConfigurationOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public SwaggerConfigurationOptions(IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}


		/// <summary>
		/// Configure each API discovered for Swagger Documentation
		/// </summary>
		/// <param name="options"></param>
		public void Configure(SwaggerGenOptions options)
		{
			#region Create section to add JWT token
			options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please provide token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme()
					{
						Reference = new OpenApiReference
						{
							Type =ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
			#endregion Create section to add JWT token

			//add swagger document for every API version discovered
			foreach (var description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(
					description.GroupName,
					CreateVersionInfo(description));
			}

			//Enable from props => Documentation file(true) => leave path empty for default
			// Set the comments path for the Swagger JSON and UI .
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath);
		}

		/// <summary>
		/// Configure Swagger Options. Inherited from the Interface
		/// </summary>
		/// <param name="name"></param>
		/// <param name="options"></param>
		public void Configure(string name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		/// <summary>
		/// Create information about the version of the API
		/// </summary>
		/// <param name="desc"></param>
		/// <returns>Information about the API</returns>
		private OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
		{
			string description = (desc.ApiVersion.MajorVersion == 1) ? "Production API" : "Development API";
			var info = new OpenApiInfo()
			{
				Title = "Deixar.API",
				Version = desc.ApiVersion.ToString(),
				Description = description,
				Contact = new OpenApiContact { Email = "pabhi1504@gmail.com", Name = "Abhi Dashadiya" }
			};

			if (desc.IsDeprecated) info.Description += " (deprecated)";

			return info;
		}
	}
}
