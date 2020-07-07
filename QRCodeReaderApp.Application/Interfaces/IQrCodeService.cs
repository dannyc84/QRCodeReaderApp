using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRCodeReaderApp.Application.Interfaces
{
    public interface IQrCodeService
    {
        Task<string> UploadQrCode(IFormFile file);

        Task<MultipartFormDataContent> GetQrCodecontent(string filePath);

        Task<string> ReadQrCode(HttpContent fileContent);
    }
}
