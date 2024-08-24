using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<string> UploadImage(byte[] image, string fileName)
        {
            using(var content = new MultipartFormDataContent())
            {
                var byteArrayContent = new ByteArrayContent(image);
                content.Add(byteArrayContent, "image", fileName);

                var apiKey = "*******";

                var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key={apiKey}", content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return ExtractImageUrl(jsonResponse);

            }
        }

        private string ExtractImageUrl(string jsonResponse)
        {
            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            return jsonObject.data.url;
        }
    }
}
