using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.Services.Image
{
    public class ImageUploadData
    {
        public ImageUploadData(string imageUrl, string deleteImageUrl) {
        ImageUrl = imageUrl;
            DeleteImageUrl = deleteImageUrl;

        }

        public string ImageUrl { get; set; } = string.Empty;
        public string DeleteImageUrl { get; set; } = string.Empty;
    }
}
