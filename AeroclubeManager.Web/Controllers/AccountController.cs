using System.Text.RegularExpressions;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using RestSharp;
using AeroclubeManager.Core.Interfaces.Services.Email;

namespace AeroclubeManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModelView> _loggerRegister;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModelView> loggerRegister,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _loggerRegister = loggerRegister;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
        }

        public IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
                throw new NotSupportedException("The default ui rquires a user store with email support");

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }


        public IActionResult Index()
        {
            return View();
        }


        [Route("/login")]
        public IActionResult Login(string? returnUrl)
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToLocal(model.ReturnUrl);

            ModelState.AddModelError(string.Empty, "Não foi possível fazer o login");
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Register(string? returnUrl)
        {
            var model = new RegisterModelView { ReturnUrl = returnUrl };
            return View();
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterModelView model)
        {
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel validar o formulário.");
                return View(model);
            }

            model.Document = model.Document.Trim();
            model.Document = model.Document.Replace(".", "").Replace("-", "");

            var user = CreateUser();

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Houve um erro ao criar o usuário");
                return View(model);
            }

            try
            {
                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                user.Document = model.Document;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
            }
            catch (Exception ex)

            {
                _loggerRegister.LogError(ex, "Error seting email-user");
                ModelState.AddModelError(string.Empty, "Erro ao criar usuário");
                return View(model);
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _loggerRegister.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmationAccount",null, new { userId = userId, userCode = code }, Request.Scheme);

                try
                {
                    await _emailSender.SendEmailAsync(
                        model.Email,
                        "Confirmação de E-mail",
                        $"<div style='font-family: Arial, sans-serif; color: #333;'>" +
                        $"<h2 style='color: #2c3e50;'>Olá {model.FirstName},</h2>" +
                        "<p style='font-size: 16px;'>Obrigado por se registrar. Para concluir o processo de criação de sua conta, por favor, confirme seu e-mail " +
                        $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='color: #1e90ff; text-decoration: none;'>clicando aqui</a>.</p>" +
                        "<p style='font-size: 14px; color: #777;'>Se você não solicitou esta conta, por favor, ignore este e-mail.</p>" +
                        "<br/>" +
                        "<p style='font-size: 16px;'>Atenciosamente,<br/><span style='font-weight: bold;'>Equipe Aeroclube Manager</span></p>" +
                        "</div>"
                    );

                    InvitationLinkSentModelView invitationModel = new InvitationLinkSentModelView
                    {
                        UserName = model.FirstName,
                        Email = model.Email
                    };


                    return RedirectToAction(nameof(InvitationLinkSent), invitationModel);


                }
                catch (Exception ex) {
                    Console.WriteLine("Occcoreu um erro: " + ex.ToString());
                }
                System.Console.WriteLine("Sent");

            }
            return View(model);


        }


        [Route("/confirmationinvited")]
        public IActionResult InvitationLinkSent(InvitationLinkSentModelView model)
        {
            return View(model);
        }


        [Route("/confirmationaccount")]
        public async Task<IActionResult> ConfirmationAccount(string userId, string userCode)
        {
            if (userId == null || userCode == null)
            {
                return BadRequest("/Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("/Error");
            }

            userCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(userCode));
            var result = await _userManager.ConfirmEmailAsync(user, userCode);
            if (result.Succeeded)
            {
                var model = new ConfirmationAccountModelView(user.FirstName, user.Email, result.Succeeded);
                return View(model);
            }
            else
            {
                var model = new ConfirmationAccountModelView(string.Empty, string.Empty, result.Succeeded);
                return View(model);
            }
        }


        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
