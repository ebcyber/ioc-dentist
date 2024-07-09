using System;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace ioc_dentist.Configurations
{
    public class IocDentistOpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = GetOpenApiDocVersion(),
            Title = GetOpenApiDocTitle(),
            Description = GetOpenApiDocDescription(),
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
        };
        public override List<OpenApiServer> Servers { get; set; } = new List<OpenApiServer>()
        {
            new OpenApiServer() { Url = "https://dentist.ebcyber.net/api/"}
        };
        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
        public override bool ForceHttps { get; set; } = true;
        public override bool ForceHttp { get; set; } = true;
    }
}