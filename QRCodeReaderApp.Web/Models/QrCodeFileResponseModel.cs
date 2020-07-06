using Newtonsoft.Json;
using System.Collections.Generic;

namespace QRCodeReaderApp.Web.Models
{
    public class QrCodeFileResponseModel
    {
        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName ="symbol")]
        public List<Symbol> Symbol { get; set; }
    }
}
