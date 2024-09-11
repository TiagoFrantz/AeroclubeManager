using System.Diagnostics;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices;
using AeroclubeManager.Web.Models;
using AeroclubeManager.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PagedList.Mvc;
using PagedList;
namespace AeroclubeManager.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFlightSchoolService _flightSchoolService;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IFlightSchoolService flightSchoolService)
        {
            _logger = logger;

            _userManager = userManager;
            _flightSchoolService = flightSchoolService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexModelView();
            bool isAuthenticated = User.Identity.IsAuthenticated;
            ApplicationUser user = null;
            if (isAuthenticated)
                user = await _userManager.GetUserAsync(User);

            model.UserAuthenticated = isAuthenticated;
            model.User = user;


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [Route("credits")]
        public IActionResult Credits()
        {
            return View();
        }

        [Route("aeroclubes/p={page}")]
        public async Task<IActionResult> Aeroclubes(int page, string? searchString = null)
        {
            var aeroclubes = new List<FlightSchool>();
            HomeAeroclubesModelView aeroclubesFiltrados = new HomeAeroclubesModelView();

            if(searchString != null && searchString.IsNullOrEmpty() == false)
            {
                aeroclubes = await _flightSchoolService.GetFlightSchoolsContaining(searchString);
                aeroclubesFiltrados.SearchString = searchString;
            } else
            {
                aeroclubes = await _flightSchoolService.GetAllFlightSchools();
                aeroclubesFiltrados.SearchString = string.Empty;
            }

            int pageSize = 10;

            if(aeroclubes?.Count > 0 && aeroclubes != null)
            {
                aeroclubesFiltrados.FlightSchools = aeroclubes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                aeroclubesFiltrados.PageCount = (int)Math.Ceiling((double)aeroclubes.Count / pageSize);
                aeroclubesFiltrados.PageNumber = page;
            } else
            {            
            aeroclubesFiltrados.PageCount = 1;
            aeroclubesFiltrados.PageNumber = 1;

            }


            return View(aeroclubesFiltrados);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
