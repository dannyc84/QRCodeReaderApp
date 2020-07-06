using Newtonsoft.Json;

namespace QRCodeReaderApp.Web.Models
{
    public class Symbol
    {
        [JsonProperty(PropertyName ="seq")]
        public int Seq { get; set; } 

        [JsonProperty(PropertyName ="data")]
        public string Data { get; set; }
        
        [JsonProperty(PropertyName ="error")]
        public string Error { get; set; } 
    }
}