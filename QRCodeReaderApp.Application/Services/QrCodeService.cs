using Microsoft.AspNetCore.Http;
using QRCodeReaderApp.Application.Helpers;
using QRCodeReaderApp.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QRCodeReaderApp.Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        public async Task<string> UploadQrCode(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return QrCodeHelper.GetImageFormat(fileBytes) != QrCodeFormat.unknown;
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            string path;
            try
            {
                var format = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + format; 
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return path;
        }
    }
}
