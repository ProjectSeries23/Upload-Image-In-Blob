using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUpload.Repositories
{
    public class ImageUploadService : IImageUpload
    {
        BlobServiceClient _blobClient;
        BlobContainerClient _containerClient;
        string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=fx1storageprod;AccountKey=vVygzt4X7KJmU2xsI20UMFwoFIxH4ZALtto8GClooyrSe7/tFNBCuiEA/juSczpwDWbuQhhxqq9y+ASt2ecrug==;EndpointSuffix=core.windows.net";

        public ImageUploadService()
        {
            _blobClient = new BlobServiceClient(azureConnectionString);
            _containerClient = _blobClient.GetBlobContainerClient(@"8541\\GuestImages"); 
        }
        public async Task<List<Azure.Response<BlobContentInfo>>> UploadFile(byte[] fileBytes, string fileName)
        {
            var azureResponse = new List<Azure.Response<BlobContentInfo>>();
            using (var memoryStream = new MemoryStream(fileBytes))
            {
                memoryStream.Position = 0;

                var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
                azureResponse.Add(client);
            }
            //string fileName = file.FileName;
            //using (var memoryStream = new MemoryStream())
            //{
            //    file.CopyTo(memoryStream);
            //    memoryStream.Position = 0;
            //    var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
            //    azureResponse.Add(client);
            //}
            return azureResponse;
        }
    }
}
