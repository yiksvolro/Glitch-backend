using Amazon.S3;
using Amazon.S3.Model;
using Glitch.ApiModels;
using Glitch.Helpers.Enum;
using Glitch.Helpers.Models;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class StorageService : IStorageService
    {
        private string BucketName = "gower";
        private readonly IAmazonS3 _client;
        private readonly IFileService _fileService;
        public StorageService(IAmazonS3 client, IFileService fileService)
        {
            _client = client;
            _fileService = fileService;
        }
        public async Task<CreateUpdate<FileApiModel>> UploadFile(IFormFile file, FileType type, int placeId, string description)
        {
            string filePath = $"images/{placeId}/{type}/{file.FileName}";

            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = newMemoryStream,
                    BucketName = this.BucketName,
                    Key = filePath
                };

                PutObjectResponse response = await _client.PutObjectAsync(request);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new ApiException($"File '{file.FileName}' was not uploaded.");

                return await SetFilePath(filePath, type, placeId, description);
            }

        }
        private async Task<CreateUpdate<FileApiModel>> SetFilePath(string path, FileType type, int placeId, string description)
        {
            var fileToDb = new FileApiModel { PlaceId = placeId, FilePath = path, Description = description, Type = type};
            var result = await _fileService.CreateAsync(fileToDb);
            if (result.Success == true) return result;
            else throw new ApiException("Error with creating FilePath");
        }
    }
}
