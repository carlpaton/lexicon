using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HoeController : Controller
    {
        public IConfiguration _configuration { get; }
        private readonly ILocalIPv4 _localIPv4;

        public HoeController(IConfiguration configuration, ILocalIPv4 localIPv4)
        {
            _configuration = configuration;
            _localIPv4 = localIPv4;
        }

        public IActionResult Index()
        {
            var viewModel = new HoeViewModel
            {
                EnvSql = $"LEXICON_SQL_CONNECTION={Environment.GetEnvironmentVariable("LEXICON_SQL_CONNECTION")}",
                ConfSql = $"ConnMsSQL={_configuration.GetConnectionString("ConnMsSQL")}",
                HostIp = $"HostIp={_localIPv4.GetLocalIPv4(NetworkInterfaceType.Ethernet)}",
                ActualConnectionString = $"ActualConnectionString={Environment.GetEnvironmentVariable("ActualConnectionString")}",
            };

            return View(viewModel);
        }
    }
}