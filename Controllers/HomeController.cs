using lista7_zad1.Context;
using lista7_zad1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace lista7_zad1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentsDbContext _context;

        public HomeController(ILogger<HomeController> logger, StudentsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String login, String password)
        {
            var adm = _context.Admins.FirstOrDefault(p => p.Login == login);
            var prof = _context.Professors.FirstOrDefault(p => p.AlbumNr == login);

            if(adm != null && adm.Password == password)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, adm.Login),
                    new Claim(ClaimTypes.Role, "Admin") // Set the role for professor
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Grades");
            }

            else if(prof != null && prof.Password == password)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, prof.AlbumNr),
                    new Claim(ClaimTypes.Role, "Professor") // Set the role for professor
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Grades");
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}