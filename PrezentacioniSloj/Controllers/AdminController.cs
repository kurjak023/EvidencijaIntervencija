using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SlojPodataka.Klase; // clsKorisnik
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;
            var tip = http.Session.GetString("TipKorisnika");

            if (!string.Equals(tip, "admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("RedirectNaPocetnu", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }

    public class AdminController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsOglasServis _oglasServis;
        private readonly clsIntervencijaServis _intervencijaServis;

        public AdminController(clsKorisnikServis korisnikServis, clsOglasServis oglasServis,clsIntervencijaServis intervencijaServis)
        {
            _korisnikServis = korisnikServis;
            _oglasServis = oglasServis;
            _intervencijaServis = intervencijaServis;
        }

        public IActionResult AdminPocetna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledKorisnika(string prezime)
        {
            DataSet rezultat;

            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _korisnikServis.PrikaziPoPrezimenu(prezime);
            }
            else
            {
                rezultat = _korisnikServis.Prikazi();
            }

            return View(rezultat);
        }
        public IActionResult AdminPregledKorisnikaDetalji()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IzmeniKorisnika(string? korisnickoime, string? action, int idkorisnika)
        {
            if (!string.IsNullOrEmpty(korisnickoime))
            {
                if (action == "izmeni")
                {
                    clsKorisnik korisnik = _korisnikServis.PrikaziPoKorisnickomImenu(korisnickoime);
                    return View("AdminPregledKorisnikaDetalji", korisnik);
                }
                if (action == "obrisi")
                {
                    _korisnikServis.Obrisi(idkorisnika);
                }
            }

            return RedirectToAction("AdminPregledKorisnika");
        }

        [HttpGet]
        public IActionResult AdminPregledOglasa(string? status, int? korisnikId)
        {
            DataSet ds;

            if (korisnikId.HasValue)
                ds = _oglasServis.PrikaziMoje(korisnikId.Value);
            else if (!string.IsNullOrWhiteSpace(status) && status.Equals("aktivni", StringComparison.OrdinalIgnoreCase))
                ds = _oglasServis.PrikaziAktivne();
            else
                ds = _oglasServis.Prikazi();

            return View("AdminPregledOglasa", ds);
        }

        [HttpGet]
        public IActionResult AdminOglasKreiraj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DodajOglas(string naziv, string adresa)
        {
            bool ok;

            if (string.IsNullOrWhiteSpace(naziv) || string.IsNullOrWhiteSpace(adresa))
            {
                TempData["Err"] = "Naziv i adresa su obavezni.";
                return RedirectToAction("AdminOglasKreiraj");
            }

            int adminId = 0;
            if (HttpContext.Session.GetInt32("KorisnikID").HasValue)
            {
                adminId = HttpContext.Session.GetInt32("KorisnikID").Value;
            }

            ok = _oglasServis.Dodaj(adminId, adresa, naziv);

            if (ok)
            {
                TempData["SuccessMessage"] = "Oglas dodat.";
                return RedirectToAction("AdminPregledOglasa");
            }
            else
            {
                TempData["Err"] = "Dodavanje nije uspelo.";
                return RedirectToAction("AdminOglasKreiraj");
            }
        }


        [HttpGet]
        public IActionResult AdminPregledIntervencija(int? korisnikId, int? zgradaId)
        {
            DataSet dataSet;

            // Preuzmi sve korisnike iz baze podataka
            if (korisnikId.HasValue)
            {
                dataSet = _intervencijaServis.DajSveIntervencijePoKorisniku(korisnikId.Value);
            }
            else
            {
                if (zgradaId.HasValue)
                {
                    dataSet = _intervencijaServis.DajSveIntervencijePoZgradi(zgradaId.Value);
                }
                else
                {
                    dataSet = _intervencijaServis.DajSveIntervencije();
                }
            }

            return View("AdminPregledIntervencija", dataSet);
        }
    }
}
