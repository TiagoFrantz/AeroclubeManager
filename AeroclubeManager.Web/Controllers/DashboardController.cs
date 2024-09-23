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
using AeroclubeManager.Web.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AeroclubeManager.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFlightSchoolService _flightSchoolService;
        const string dashboardUrl = "d/";

        public DashboardController(UserManager<ApplicationUser> userManager, IFlightSchoolService flightSchoolService)
        {
            _userManager = userManager;
            _flightSchoolService = flightSchoolService;
        }


        [Route("d/NA")]

        public IActionResult UserNotAuthorized(string? flightSchoolId = null)
        {
            if (flightSchoolId == null)
            {
                ViewBag.FlightSchoolId = string.Empty;
            } else
            {
                ViewBag.FlightSchoolId = flightSchoolId;
            }
            return View();
        }



        [HttpGet]
        [Route("d/{flightSchoolId}/home")]
        public async Task<IActionResult> Index(string flightSchoolId)
        {

            var user = await _userManager.GetUserAsync(User);

            UserInFlightSchoolApprovation userApproved = await _flightSchoolService.UserApprovedInFlightSchool(Guid.Parse(flightSchoolId), user.Id);

            if (userApproved.Status == UserInFlightSchoolStatus.Rejected)
            {
                return RedirectToAction(nameof(UserNotAuthorized));
            }

            DashboardIndexModelView modelView = new DashboardIndexModelView();
            FlightSchool flightSchool = await _flightSchoolService.GetFlightSchoolById(flightSchoolId);
            modelView.FlightSchool = flightSchool;
            modelView.UserFlightSchool = userApproved.User;

            return View(modelView);
        }

        
        [Route("d/{flightSchoolId}/aeroclube")]
        public async Task<IActionResult> FlightSchool(string flightSchoolId)
        {
            var user = await _userManager.GetUserAsync(User);

            List<FlightSchoolRoleEnum> requiredRoles = new List<FlightSchoolRoleEnum>();
            requiredRoles.Add(FlightSchoolRoleEnum.Admin);

            UserInFlightSchoolApprovation userApproved = await _flightSchoolService.UserApprovedInFlightSchool(Guid.Parse(flightSchoolId), user.Id, requiredRoles);

            if (userApproved.Status == UserInFlightSchoolStatus.Rejected)
            {
                return RedirectToAction(nameof(UserNotAuthorized), new {flightSchoolId = flightSchoolId});
            }

            var model = new DashboardFlightSchoolModelView();
            model.UserFlightSchool = userApproved.User;
                model.FlightSchool = await _flightSchoolService.GetFlightSchoolById(flightSchoolId);

            return View(model);
        }
    }
}
