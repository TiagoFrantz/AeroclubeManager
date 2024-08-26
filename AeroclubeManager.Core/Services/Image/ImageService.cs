using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Services.Image;
using AeroclubeManager.Core.Interfaces.Services.Image;

namespace AeroclubeManager.Core.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ImageUploadData> UploadImage(byte[] image, string fileName)
        {
            using(var content = new MultipartFormDataContent())
            {
                var byteArrayContent = new ByteArrayContent(image);
                content.Add(byteArrayContent, "image", fileName);

                var apiKey = Environment.GetEnvironmentVariable("APIKEY_IMGBB");

                var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key={apiKey}", content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                string imageUrl = ExtractImageUrl(jsonResponse);
                string deleteUrl = DeleteImageUrl(jsonResponse);

                return new ImageUploadData(imageUrl, deleteUrl);

            }
        }

        public async Task<bool> DeleteImage(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return !response.IsSuccessStatusCode;
            }

            return true;
        }

        private string ExtractImageUrl(string jsonResponse)
        {
            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            return jsonObject.data.url;
        }

        private string DeleteImageUrl(string jsonResponse)
        {
            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            return jsonObject.data.delete_url;
        }
    }
}
