using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCodeReaderApp.Application.Extensions;
using QRCodeReaderApp.Application.Interfaces;
using QRCodeReaderApp.Web.Models;

namespace QRCodeReaderApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQrCodeService _qrCodeService;

        public HomeController(ILogger<HomeController> logger, IQrCodeService qrCodeService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormFile file)
        {
            var qrCodeUploaded = await _qrCodeService.UploadQrCode(file);

            var qrCodeUrl = StringExtensions.MapUrl(qrCodeUploaded);

            var encodedQrCodeUrl = WebUtility.UrlEncode(qrCodeUrl);

            var qrCodeFileResponse = new List<QrCodeFileResponseModel>();
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync("http://api.qrserver.com/v1/read-qr-code/" + encodedQrCodeUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();
                qrCodeFileResponse = JsonConvert.DeserializeObject<List<QrCodeFileResponseModel>>(apiResponse);
            }
            return View(qrCodeFileResponse);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
