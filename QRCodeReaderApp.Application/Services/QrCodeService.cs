using Microsoft.AspNetCore.Http;
using QRCodeReaderApp.Application.Helpers;
using QRCodeReaderApp.Application.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRCodeReaderApp.Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        private const string ReadQrCodeApiUrl = "http://api.qrserver.com/v1/read-qr-code/";

        public static MultipartFormDataContent content;

        public QrCodeService()
        {
            content = new MultipartFormDataContent();
        }

        public async Task<string> UploadQrCode(IFormFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (await CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            throw new InvalidDataException("Invalid image file");
        }

        public async Task<MultipartFormDataContent> GetQrCodecontent(string filePath)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var content = new MultipartFormDataContent();
            var fs = await File.ReadAllBytesAsync(filePath);
            content.Add(new ByteArrayContent(fs), "file", "file" + Path.GetExtension(filePath));
            return content;
        }

        public async Task<string> ReadQrCode(HttpContent fileContent)
        {
            if (fileContent is null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            using var response = await new HttpClient().PostAsync(ReadQrCodeApiUrl, fileContent);

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<bool> CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            return QrCodeHelper.GetImageFormat(fileBytes) != QrCodeFormat.unknown;
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string path;
            try
            {
                var format = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + format;
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//qrCodes", fileName);

                using var bits = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(bits);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return path;
        }
    }
}
