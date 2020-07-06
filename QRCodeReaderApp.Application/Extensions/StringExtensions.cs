using Microsoft.AspNetCore.Http;
using QRCodeReaderApp.Application.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QRCodeReaderApp.Application.Extensions
{
    public static class StringExtensions
    {
        public static string MapUrl(this string path)
        {
            var locationPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);  
            if (File.Exists(locationPath))
            {
                //DirectoryInfo dInfo = new DirectoryInfo(locationPath);
                //var files = dInfo.GetFilesByExtensions(QrCodeFormat.gif.ToString(), QrCodeFormat.jpeg.ToString(), QrCodeFormat.png.ToString()).ToArray();
                //if (files.Length > 0)
                //{
                //    string filenameRelative = locationPath + Path.GetFileName(files[0].ToString());
                    //return new Uri(filenameRelative).ToString();
                //}
                return new Uri(locationPath).ToString();
            }

            return null;
        }
    }
}
