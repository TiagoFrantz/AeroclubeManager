using Microsoft.AspNetCore.Mvc;

namespace AeroclubeManager.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
