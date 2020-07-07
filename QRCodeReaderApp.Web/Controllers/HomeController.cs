using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

            var content = await _qrCodeService.GetQrCodecontent(qrCodeUploaded);

            var apiResponse = await _qrCodeService.ReadQrCode(content);

            var qrCodeFileResponse = JsonConvert.DeserializeObject<List<QrCodeFileResponseModel>>(apiResponse);

            return View(qrCodeFileResponse);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
