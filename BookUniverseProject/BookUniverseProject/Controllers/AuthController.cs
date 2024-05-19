using System.Diagnostics;
using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookUniverse.Domain.Entities;
using BookUniverse.Web.Extensions;
using BookUniverse.Infrastructure.Services.EmailSender;
using BookUniverse.Application.MediatR.Authentication.Commands.SignUp;
using BookUniverse.Application.DTOs.UserDTOs;
using System.Text.Encodings.Web;

namespace BookUniverseProject.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult ForgotPassPage()
        {
            return View();
        }

        public IActionResult ConfirmEmailPage()
        {
            return View();
        }

        public IActionResult ForgotPasswordConfirmationPage()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegistrationDto model, string returnUrl = null)
        {
            // Show errors
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, Request.Scheme);
                    HandleResult(await Mediator.Send(new SendEmailCommand(user.Email, callbackUrl, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>")));
                    return RedirectToLocal(returnUrl);
                }
            }
            return View("Registration");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginDto model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    // Show errors
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View("LogIn");
                }
            }
            return View("LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn), "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(ConfirmEmailPage), "Auth");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            return RedirectToAction(nameof(ForgotPasswordConfirmationPage));
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmationPage));
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id.ToString(), code, Request.Scheme);
                HandleResult(await Mediator.Send(new SendEmailCommand(user.Email, callbackUrl, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>")));
                return RedirectToAction(nameof(ForgotPasswordConfirmationPage));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordDto { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(LogIn));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LogIn));
            }
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BookController.MainPage), "Book");
            }
        }
    }
}