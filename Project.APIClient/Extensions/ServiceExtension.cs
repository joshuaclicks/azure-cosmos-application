﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using NLog.Extensions.Logging;
using Project.Application.Configurations;
using Project.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using System.Net;
using Project.Application.Contracts;
using Project.Services.BusinessLogic;

namespace Project.APIClient.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config, string allowedSpecificOrigins)
        {
            services.AddWebCoreServices(config, allowedSpecificOrigins);
            services.AddSharedInfrastructure(config);
            services.AddSwaggerExtension();
        }

        private static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Register Application Services

            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICandidateService, CandidateService>();
            #endregion

            services.Configure<DatabaseConfiguration>(options => config.GetSection(nameof(DatabaseConfiguration)));

            return services;
        }

        private static void AddWebCoreServices(this IServiceCollection services, IConfiguration config, string allowedSpecificOrigins)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.AllowTrailingCommas = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddCors(options =>
            {
                options.AddPolicy(allowedSpecificOrigins, builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

            });

            services.AddDbContext<EmployerApplicationContext>(options =>
                    options.UseCosmos(config["DatabaseConfiguration:Endpoint"], config["DatabaseConfiguration:AccountKey"], config["DatabaseConfiguration:DatabaseName"], options =>
                    {
                        options.ConnectionMode(ConnectionMode.Direct);
                        options.WebProxy(new WebProxy());
                        options.LimitToEndpoint();
                        options.RequestTimeout(TimeSpan.FromSeconds(Convert.ToInt32(config["DatabaseConfiguration:RequestTimeoutInSeconds"])));
                    }));


            services.AddAntiforgery();

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddNLog();
            });


        }

        private static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Project Explorer Service - API",
                    Description = "This Api will be responsible for the management of the employer's programmes.",
                    Contact = new OpenApiContact
                    {
                        Name = "Project Explorer Service",
                        Email = "joshua.akinyoade@gmail.com",
                        Url = new Uri("http://joshuaclicks.com"),
                    }
                });
            });
        }
    }
}
