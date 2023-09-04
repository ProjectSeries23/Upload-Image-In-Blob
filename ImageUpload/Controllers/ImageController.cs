using ImageUpload.Models;
using ImageUpload.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImageUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageUpload _fileUpload;
        public ImageController(IImageUpload imageUpload)
        {
            _fileUpload = imageUpload;
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlobs(IFormFile files)//([FromBody] UploadRequestModel uploadRequest)
        {
            try
            {
                var formValues = new List<string>();
                var formFile = HttpContext.Request.Form.Files.GetFile("file");
                var value = "";
                UploadRequestModel jsonObject =new UploadRequestModel() ;
                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    if (key == "fileBytes")
                    {
                        value = HttpContext.Request.Form[key];
                        formValues.Add(value);
                    }
                }
                int jsonEndIndex = value.IndexOf("}");
                if (jsonEndIndex >= 0)
                {
                    // Extract the JSON portion
                    string jsonPart = value.Substring(0, jsonEndIndex + 1);
                     jsonObject = JsonConvert.DeserializeObject<UploadRequestModel>(jsonPart);
                   

                }

                //new System.Collections.Generic.IDictionaryDebugView<string, Microsoft.Extensions.Primitives.StringValues>(((System.Collections.ICollection)HttpContext.Request.Form.Keys).SyncRoot).Items[0]
                // var formFile = HttpContext.Request.Form.Keys.SyncRoot.Items[0]; //new System.Collections.Generic.IDictionaryDebugView<string, Microsoft.Extensions.Primitives.StringValues>(((System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>.KeyCollection)((Microsoft.AspNetCore.Http.FormCollection)((Microsoft.AspNetCore.Http.DefaultHttpRequest)HttpContext.Request).Form.Keys._dictionary.Items[0]//.Files.GetFile("file"); //.Keys.ToDictionary();
                //var response = await _fileUpload.UploadFile(files);
                //return Ok(response);
                if (jsonObject == null || jsonObject.FileBytes == null || jsonObject.FileBytes.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                string fileName = jsonObject.FileName; // Use the provided filename

                var response = await _fileUpload.UploadFile(jsonObject.FileBytes, fileName);
                return Ok(response);
            }

            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
