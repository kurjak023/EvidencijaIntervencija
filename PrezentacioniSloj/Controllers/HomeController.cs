using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.Models;
using System.Diagnostics;

namespace PrezentacioniSloj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Pocetna()
        {
            return View();
        }

        public IActionResult OServisu()
        {
            return View();
        }

        public IActionResult PrijavaIliRegistracija()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RedirectNaPocetnu()
        {
            int? idKorisnika = HttpContext.Session.GetInt32("IDKorisnika");

            if (idKorisnika == null)
            {
                TempData["LoginMsg"] = "Molimo prijavite se da biste nastavili.";
                return RedirectToAction("Prijava", "Nalog");
            }

            var tip = HttpContext.Session.GetString("TipKorisnika") ?? "obican_korisnik";

            if (string.Equals(tip, "admin", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("AdminPocetna", "Admin");

            return RedirectToAction("KorisnikPocetna", "Korisnik");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear(); // briše sve podatke iz sesije
            return RedirectToAction("Pocetna", "Home");
        }
    }
}
