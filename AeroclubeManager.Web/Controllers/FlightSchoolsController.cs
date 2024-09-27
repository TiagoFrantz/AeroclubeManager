using AeroclubeManager.Core.Entities.AiportDb;
using AeroclubeManager.Core.Entities.Controllers.FlightSchools;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.Services.Image;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Services.Airport;
using AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices;
using AeroclubeManager.Core.Interfaces.Services.Image;
using AeroclubeManager.Core.Interfaces.Services.User;
using AeroclubeManager.Core.Interfaces.Services.Weather;
using AeroclubeManager.Core.Services.Image;
using AeroclubeManager.Web.Attributes;
using AeroclubeManager.Web.Models.FlightSchools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AeroclubeManager.Web.Controllers
{
    public class FlightSchoolsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFlightSchoolService _flightSchoolService;
        private readonly IImageService _imageService;
        private readonly IAirportDbService _airportDbService;
        private readonly IWeatherService _weatherService;

        const string urlNameController = "flightschools";
        public FlightSchoolsController(UserManager<ApplicationUser> userManager,
            IFlightSchoolService flightSchoolService,
            IImageService imageService,
            IAirportDbService airportDbService,
            IWeatherService weatherService
            )
        {
            _userManager = userManager;
            _flightSchoolService = flightSchoolService;
            _imageService = imageService;
            _airportDbService = airportDbService;
            _weatherService = weatherService;
        }

        [ConfirmedAccount]
        [HttpGet]
        [Route(urlNameController)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            List<FlightSchool> flightSchools = await _flightSchoolService.GetFlightSchoolsByUserId(user.Id);

            var flightSchoolsModel = new List<FlightSchoolViewModel>();

            foreach (var fs in flightSchools)
            {
                if (fs != null && fs.IsApproved)
                {
                    var fsModel = new FlightSchoolViewModel();
                    fsModel.FlightSchool = fs;

                    fsModel.UserFlightSchool = fs.Users.FirstOrDefault(u => u.User.Id == user.Id);

                    if (fsModel.UserFlightSchool == null)
                    {
                        return BadRequest();
                    }

                    if (fsModel.FlightSchool.SchoolFlightAirport.ICAO.Length == 4)
                    {
                        var weatherInFs = await _weatherService.GetWeatherUsingCacheAsync(fsModel.FlightSchool.SchoolFlightAirport.ICAO, fsModel.FlightSchool.SchoolFlightAirport.Latitude, fsModel.FlightSchool.SchoolFlightAirport.Longitude);
                        if (weatherInFs != null)
                        {
                            fsModel.LocationName = weatherInFs.City;
                            fsModel.LastUpdated = weatherInFs.LastUpdated;
                            fsModel.ConditionText = weatherInFs.ConditionText;
                            fsModel.IconUrl = weatherInFs.IconUrl;
                            fsModel.TemperatureC = weatherInFs.TemperatureC;
                        }
                    }
                    flightSchoolsModel.Add(fsModel);

                }

            }

            var model = new FlightSchoolIndexViewModel()
            {
                User = user,
                FlightSchools = flightSchoolsModel,

            };
            return View(model);
        }


        [ConfirmedAccount]
        [HttpGet]
        [Route(urlNameController + "/create")]
        public async Task<IActionResult> CreateNew()
        {
            return View();
        }


        [ConfirmedAccount]
        [HttpPost]
        [Route(urlNameController + "/create")]
        public async Task<IActionResult> CreateNew(string Nome, string Descricao, bool ImagemSelecionada, IFormFile ImagemLogo, string Estado, string Cidade, string NomeAeroporto, string Icao, string Cnpj, string Email, string Ciac)
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(Nome) ||
    string.IsNullOrWhiteSpace(Descricao) ||
    string.IsNullOrWhiteSpace(Estado) ||
    string.IsNullOrWhiteSpace(Cidade) ||
    string.IsNullOrWhiteSpace(NomeAeroporto) ||
    string.IsNullOrWhiteSpace(Icao) ||
    Icao.Length != 4 ||
    string.IsNullOrWhiteSpace(Cnpj) ||
    string.IsNullOrWhiteSpace(Email) ||
    string.IsNullOrWhiteSpace(Ciac) ||
    user == null
    )
            {

                return BadRequest(new { message = "Valores inválidos." });
            }

            var airportDb = await _airportDbService.GetAirportDb(Icao);

            string msgAirportDb = string.Empty;

            if (airportDb == null)
            {
                msgAirportDb = $"Nenhum aeroporto com este ICAO ({Icao.ToUpper()}) foi encontrado. Algumas funcionalidades estarão indisponíveis.";
                airportDb = new AirportDbDto();
            }




            var newFlightSchool = new FlightSchool
            {
                Name = Nome,
                IsApproved = true,
                LicenseNumber = Ciac,
                Description = Descricao,
                Document = Cnpj,
                Email = Email,
                SchoolFlightAirport = new Airport
                {
                    Name = NomeAeroporto,
                    ICAO = Icao,
                    Location = Cidade,
                    State = Estado,
                    Longitude = airportDb.Longitude,
                    Latitude = airportDb.Latitude
                }
            };


            if (ImagemSelecionada)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImagemLogo.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var nameFile = string.Empty;
                    if (Nome.Length <= 200)
                    {
                        nameFile = Nome + Guid.NewGuid().ToString();
                    }
                    else
                    {
                        nameFile = Guid.NewGuid().ToString();
                    }

                    var imageUrl = await _imageService.UploadImage(imageBytes, nameFile);
                    newFlightSchool.LogoUrl = imageUrl.ImageUrl;
                }
            }

            newFlightSchool.SchoolFlightAirport.FlightSchool = newFlightSchool;

            var flightSchoolUser = new UserFlightSchool
            {
                User = user,
                UserId = user.Id,
                FlightSchool = newFlightSchool
            };

            var userRole = new UserFlightSchoolRole
            {
                Role = FlightSchoolRoleEnum.Admin,
                User = flightSchoolUser,
                UserId = flightSchoolUser.Id
            };

            flightSchoolUser.FlightSchoolRoles.Add(userRole);

            newFlightSchool.Users.Add(flightSchoolUser);

            user.FlightSchools.Add(flightSchoolUser);

            var result = await _flightSchoolService.CreateFlightSchool(newFlightSchool);


            if (result == null)
            {
                return BadRequest(new { message = "Não foi possível criar o Aeroclube. Referência nula." });
            }

            return Ok(new { message = "FlightSchool criado com sucesso." + msgAirportDb });
        }

        [HttpPost]
        [Route("flightschool/edit")]
        public async Task<IActionResult> EditFlightSchool(string flightSchoolId, EditFlightSchool model, IFormFile? logoImage = null)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Dados inválidos."});
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var requiredRoles = new List<FlightSchoolRoleEnum>();
            requiredRoles.Add(FlightSchoolRoleEnum.Admin);

            var userApproved = await _flightSchoolService.UserApprovedInFlightSchool(Guid.Parse(flightSchoolId), user.Id, requiredRoles);
            
            if(userApproved.Status == UserInFlightSchoolStatus.Rejected)
            {
                return Forbid(userApproved.Message);
            }

            List<EditFlightSchoolLink> Links = JsonConvert.DeserializeObject<List<EditFlightSchoolLink>>(model.LinksJson);

            if(Links.Count > 3)
            {
                return BadRequest(new {message = "Quantidade de links ultrapassou o limite."});
            }
            var flightSchoolUpdated = await _flightSchoolService.GetFlightSchoolById(flightSchoolId);

            ICollection<FlightSchoolLink> flightSchoolLinks = Links.Select(link => new FlightSchoolLink { 
            Text = link.Text,
            Url = link.Url,
                FlightSchool = flightSchoolUpdated,
            FlightSchoolId = flightSchoolUpdated.Id
            }).ToList();


            flightSchoolUpdated.Links = flightSchoolLinks;
            flightSchoolUpdated.Name = model.Name;
            flightSchoolUpdated.Description = model.Description;
            flightSchoolUpdated.LicenseNumber = model.FlightSchoolLicenseNumber;
            flightSchoolUpdated.Document = model.FlightSchoolDocument;
            flightSchoolUpdated.SchoolFlightAirport.Name = model.AirportName;
            flightSchoolUpdated.SchoolFlightAirport.Location = model.AirportCity;
            flightSchoolUpdated.SchoolFlightAirport.State = model.AirportState;

            if(flightSchoolUpdated.SchoolFlightAirport.ICAO.Replace(" ", "").ToLower() != model.ICAO.Replace(" ", "").ToLower())
            {
                var airportDb = await _airportDbService.GetAirportDb(model.ICAO);

                if (airportDb != null) {
                    flightSchoolUpdated.SchoolFlightAirport.ICAO = model.ICAO.Replace(" ", "");

                    flightSchoolUpdated.SchoolFlightAirport.Latitude = airportDb.Latitude;
                    flightSchoolUpdated.SchoolFlightAirport.Longitude = airportDb.Longitude;
                }
            }

            if (model.UpdatedImage && logoImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await logoImage.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var nameFile = Guid.NewGuid().ToString();
                    var imageUrl = await _imageService.UploadImage(imageBytes, nameFile);
                    flightSchoolUpdated.LogoUrl = imageUrl.ImageUrl;
                }
            }

            var result = await _flightSchoolService.UpdateFlightSchool(flightSchoolUpdated);

            if(result == null)
            {
                return BadRequest(new { message = "Erro ao atualizar o aeroclube." });
            }


            return Ok();
        }



    }
}
