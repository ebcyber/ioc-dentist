using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using ioc_dentist.Configurations;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
            {
                var options = new OpenApiConfigurationOptions()
                {
                    Info = new OpenApiInfo()
                    {
                        Version = DefaultOpenApiConfigurationOptions.GetOpenApiDocVersion(),
                        Title = DefaultOpenApiConfigurationOptions.GetOpenApiDocTitle(),
                        Description = DefaultOpenApiConfigurationOptions.GetOpenApiDocDescription(),
                        TermsOfService = new Uri("https://www.ebcyber.net/dentist-tos.html"),
                        Contact = new OpenApiContact()
                        {
                            Name = "Support",
                            Email = "support@ebcyber.net",
                            Url = new Uri("https://www.ebcyber.net/contact"),
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "CC-BY-SA",
                            Url = new Uri("https://www.creativecommons.org"),
                        }
                    },
                    Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                    OpenApiVersion = DefaultOpenApiConfigurationOptions.GetOpenApiVersion(),
                    IncludeRequestingHostName = true,
                    ForceHttps = IocDentistOpenApiConfigurationOptions.IsHttpsForced(),
                    ForceHttp = IocDentistOpenApiConfigurationOptions.IsHttpForced(),
                };
                return options;
            });
    })
    .Build();

host.Run();
