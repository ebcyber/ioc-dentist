using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography.X509Certificates;

namespace IOCDentist
{
    public class Defang
    {
        private readonly ILogger<Defang> _logger;

        public class IpList
        {
            [Required, NotNull, Range(1,100)]
            public IEnumerable<string>Ips { get; set; }
        }
        public Defang(ILogger<Defang> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Defang.DefangIp))]
        [OpenApiOperation(operationId: "defangIp", Summary = "Defang IP address IOCs.", Description = "This is a basic IOC defanging tool.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<string>), Required = true, Description = "IOC object that needs to be defanged")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "The response", Description = "This returns the defanged IOCs.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid input supplied", Description = "Invalid input supplied")]
        public IActionResult DefangIp([HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "v1/defang/ip")] HttpRequest req, [FromBody] List<string> ipList)
        {
            _logger.LogInformation("IP Defang request received");
            for (int i = 0; i < ipList.Count; i++){
                IPAddress address;
                if (IPAddress.TryParse(ipList[i], out address)){
                    switch (address.AddressFamily){
                        case System.Net.Sockets.AddressFamily.InterNetwork:
                            ipList[i] = address.ToString();
                            _logger.LogInformation($"Item {ipList[i]} identified as an IPv4 address. Defanging...");
                            int lastDecimal = ipList[i].LastIndexOf('.');
                            ipList[i] = ipList[i].Insert(lastDecimal, "[");
                            ipList[i] = ipList[i].Insert(lastDecimal + 2, "]");
                            _logger.LogInformation($"Defanged: {ipList[i]}");
                            break;
                        case System.Net.Sockets.AddressFamily.InterNetworkV6:
                            ipList[i] = address.ToString();
                            _logger.LogInformation($"Item {ipList[i]} identified as an IPv6 address. Defanging...");
                            int lastColon = ipList[i].LastIndexOf(':');
                            ipList[i] = ipList[i].Insert(lastColon -1, "[");
                            ipList[i] = ipList[i].Insert(lastColon + 2, "]");
                            _logger.LogInformation($"Defanged: {ipList[i]}");
                            break;
                        default:
                            _logger.LogError($"Unable to identify {ipList[i]} as an IP Address");
                            ipList.Remove(ipList[i]);
                            break;
                    }
                } else {
                    _logger.LogError($"Unable to identify {ipList[i]} as an IP Address");
                    ipList.Remove(ipList[i]);
                    i--;
                };
            }

            return new OkObjectResult(ipList);
        }
    }
}
