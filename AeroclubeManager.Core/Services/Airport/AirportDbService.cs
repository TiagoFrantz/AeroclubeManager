using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.AiportDb;
using AeroclubeManager.Core.Interfaces.Services.Airport;
using Newtonsoft.Json;

namespace AeroclubeManager.Core.Services.Airport
{
    public class AirportDbService : IAirportDbService
    {
        private readonly HttpClient _httpClient;
        private string apiKey = string.Empty;

        //response from api example

/*{25 items
"ident":string"SSSC"
"type":string"small_airport"
"name":string"Santa Cruz do Sul Airport"
"latitude_deg":float-29.684099197387695
"longitude_deg":float-52.412200927734375
"elevation_ft":string"646"
"continent":string"SA"
"iso_country":string"BR"
"iso_region":string"BR-RS"
"municipality":string"Santa Cruz Do Sul"
"scheduled_service":string"no"
"gps_code":string"SSSC"
"iata_code":string"CSU"
"local_code":string"SSSC"
"home_link":string""
"wikipedia_link":string""
"keywords":string""
"icao_code":string"SSSC"
"runways":[1 item
0:{20 items
"id":string"234098"
"airport_ref":string"504"
"airport_ident":string"SSSC"
"length_ft":string"3871"
"width_ft":string"59"
"surface":string"ASP"
"lighted":string"0"
"closed":string"0"
"le_ident":string"8"
"le_latitude_deg":string""
"le_longitude_deg":string""
"le_elevation_ft":string""
"le_heading_degT":string""
"le_displaced_threshold_ft":string""
"he_ident":string"26"
"he_latitude_deg":string""
"he_longitude_deg":string""
"he_elevation_ft":string""
"he_heading_degT":string""
"he_displaced_threshold_ft":string""
}
]
"country":{6 items
"id":string"302791"
"code":string"BR"
"name":string"Brazil"
"continent":string"SA"
"wikipedia_link":string"https://en.wikipedia.org/wiki/Brazil"
"keywords":string"Brasil, Brasilian"
}
"region":{8 items
"id":string"303221"
"code":string"BR-RS"
"local_code":string"RS"
"name":string"Rio Grande do Sul"
"continent":string"SA"
"iso_country":string"BR"
"wikipedia_link":string"https://en.wikipedia.org/wiki/Rio_Grande_do_Sul"
"keywords":string""
}
"updatedAt":string"2022-05-06T03:36:43.351Z"
"freqs":NULL
"navaids":NULL
"station":{2 items
"icao_code":string"SBMQ"
"distance":float1788.8730513621645
}
}*/

public AirportDbService(HttpClient httpClient)
{
    _httpClient = httpClient;
    apiKey = Environment.GetEnvironmentVariable("APIKEY_AIRPORTDB");
}
public async Task<AirportDbDto?> GetAirportDb(string icao)
{
    var url = $"https://airportdb.io/api/v1/airport/{icao}?apiToken={apiKey}";

    var response = await _httpClient.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
        var json = await response.Content.ReadAsStringAsync();
        var airportInfo = JsonConvert.DeserializeObject<dynamic>(json);

        var airportDto = new AirportDbDto();
                airportDto.Name = airportInfo.name;
                airportDto.Longitude = (double)airportInfo.longitude_deg;
                airportDto.Latitude = (double)airportInfo.latitude_deg;
                airportDto.Location = airportInfo.municipality;
                return airportDto;
    }

    return null;
}

}
}
