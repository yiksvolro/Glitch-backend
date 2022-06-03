using System.IO;

namespace Glitch.ApiModels.ResponseModel
{
    public class FileResponseModel
    {
        public byte[] File { get; set; }
        public string ContentType { get; set; }
    }
}
