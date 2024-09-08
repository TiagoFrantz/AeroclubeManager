using AeroclubeManager.Core.Entities.AiportDb;
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

            foreach(var fs in flightSchools)
            {
                if (fs != null && fs.IsApproved)
                {
                    var fsModel = new FlightSchoolViewModel();
                    fsModel.FlightSchool = fs;

                    fsModel.UserFlightSchool = fs.Users.FirstOrDefault(u => u.User.Id == user.Id);

                    if(fsModel.UserFlightSchool == null)
                    {
                        return BadRequest();
                    }

                    if (fsModel.FlightSchool.SchoolFlightAirport.ICAO.Length == 4) {
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

                return BadRequest(new {message = "Valores inválidos."});
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
                return BadRequest(new {message = "Não foi possível criar o Aeroclube. Referência nula."});
            }

            return Ok(new { message = "FlightSchool criado com sucesso." +  msgAirportDb});
        }

    }
    }
