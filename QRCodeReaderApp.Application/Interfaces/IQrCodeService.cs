using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace QRCodeReaderApp.Application.Interfaces
{
    public interface IQrCodeService
    {
        Task<string> UploadQrCode(IFormFile file);
    }
}
