using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;

        public KorisnikController(clsKorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
        }

        // /Korisnik/KorisnikPocetna
        public IActionResult KorisnikPocetna() => View();

        // /Korisnik/KorisnikProfil
        // Čita podatke iz sesije i puni formu (RegistracijaModel) za prikaz/izmenu
        public IActionResult KorisnikProfil()
        {
            var model = new RegistracijaModel
            {
                Ime = HttpContext.Session.GetString("Ime") ?? "",
                Prezime = HttpContext.Session.GetString("Prezime") ?? "",
                KorisnickoIme = HttpContext.Session.GetString("KorisnickoIme") ?? ""
                // Lozinku NE prikazujemo niti čuvamo u sesiji
            };
            ViewBag.TipKorisnika = HttpContext.Session.GetString("TipKorisnika") ?? "obican_korisnik";
            return View(model);
        }


    }
}
