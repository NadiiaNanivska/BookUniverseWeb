using System.Diagnostics;
using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookUniverseProject.Controllers
{
    public class AuthController : BaseController
    {
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
        
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}