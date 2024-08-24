using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Interfaces.Services.Image
{
    public interface IImageService
    {
        public Task<string> UploadImage(byte[] image, string fileName);
    }
}
