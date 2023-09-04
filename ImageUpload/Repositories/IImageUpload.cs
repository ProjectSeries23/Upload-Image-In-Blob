using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUpload.Repositories
{
    public interface IImageUpload
    {
        Task<List<Azure.Response<BlobContentInfo>>> UploadFile(byte[] fileBytes, string fileName);// IFormFile files);
    }
}
