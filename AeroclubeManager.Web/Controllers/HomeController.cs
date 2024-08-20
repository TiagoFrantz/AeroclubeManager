using System.Diagnostics;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Web.Models;
using AeroclubeManager.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AeroclubeManager.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;

            _userManager = userManager;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
