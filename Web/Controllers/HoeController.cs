using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.NetworkInformation;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HoeController : Controller
    {
        public IConfiguration _configuration { get; }
        private readonly ILocalIPv4 _localIPv4;
        private readonly IPublicIP _publicIP;

        public HoeController(IConfiguration configuration, ILocalIPv4 localIPv4, IPublicIP publicIP)
        {
            _configuration = configuration;
            _localIPv4 = localIPv4;
            _publicIP = publicIP;
        }

        public IActionResult Index()
        {
            var viewModel = new HoeViewModel
            {
                EnvSql = $"LEXICON_SQL_CONNECTION={Environment.GetEnvironmentVariable("LEXICON_SQL_CONNECTION")}",
                EnvSubstituteLocalIp = $"SUBSTITUTE_LOCAL_IP={Environment.GetEnvironmentVariable("SUBSTITUTE_LOCAL_IP")}",
                EnvSubstitutePublicIp = $"SUBSTITUTE_PUBLIC_IP={Environment.GetEnvironmentVariable("SUBSTITUTE_PUBLIC_IP")}",
                ConfSql = $"ConnMsSQL={_configuration.GetConnectionString("ConnMsSQL")}",
                HostIp = $"HostIp={_localIPv4.GetLocalIPv4(NetworkInterfaceType.Ethernet)}",
                PublicIp = $"PublicIp={_publicIP.GetPublicIP()}",
                ActualConnectionString = $"ActualConnectionString={Environment.GetEnvironmentVariable("ActualConnectionString")}",
            };

            return View(viewModel);
        }
    }
}