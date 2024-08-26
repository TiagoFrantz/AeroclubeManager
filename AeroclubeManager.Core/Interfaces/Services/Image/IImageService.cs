using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Services.Image;

namespace AeroclubeManager.Core.Interfaces.Services.Image
{
    public interface IImageService
    {
        public Task<ImageUploadData> UploadImage(byte[] image, string fileName);
        public Task<bool> DeleteImage(string url);
    }
}
