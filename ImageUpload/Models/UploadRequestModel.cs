using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUpload.Models
{
    public class UploadRequestModel
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
    }
}
