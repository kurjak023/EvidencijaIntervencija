using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsOglasServis _oglasServis;

        public KorisnikController(clsKorisnikServis korisnikServis, clsOglasServis oglasServis)
        {
            _korisnikServis = korisnikServis;
            _oglasServis = oglasServis;
        }

        // /Korisnik/KorisnikPocetna
        public IActionResult KorisnikPocetna()
        {
            return View();
        }

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

        // ---------- Slobodni nalozi (PENDING) ----------
        // /Korisnik/KorisnikSlobodniOglasi (GET)
        public IActionResult KorisnikSlobodniOglasi()
        {
            // mapirano na tvoj servis: PrikaziAktivne()
            DataSet ds = _oglasServis.PrikaziAktivne();
            return View(ds);
        }

        // /Korisnik/KorisnikDodeli (POST)
        [HttpPost]
        public IActionResult KorisnikDodeli(int id)
        {
            var idKorisnika = HttpContext.Session.GetInt32("IDKorisnika");
            if (idKorisnika == null) return RedirectToAction(nameof(KorisnikPocetna));

            _oglasServis.Dodeli(id, idKorisnika.Value);
            return RedirectToAction(nameof(KorisnikMojiNalozi));
        }

        // ---------- Moji nalozi ----------
        // /Korisnik/KorisnikMojiNalozi (GET)
        public IActionResult KorisnikMojiNalozi()
        {
            var idKorisnika = HttpContext.Session.GetInt32("IDKorisnika");
            if (idKorisnika == null) return RedirectToAction(nameof(KorisnikPocetna));

            DataSet ds = _oglasServis.PrikaziMoje(idKorisnika.Value);
            return View(ds);
        }

        // ---------- Završi nalog ----------
        // /Korisnik/KorisnikZavrsi/{id} (GET)
        public IActionResult KorisnikZavrsi(int id)
        {
            ViewBag.IDOglasa = id;
            return View();
        }

        // /Korisnik/KorisnikPotvrdiZavrsetak (POST)
        [HttpPost]
        public IActionResult KorisnikPotvrdiZavrsetak(int id, string OpisIntervencije)
        {
            var idKorisnika = HttpContext.Session.GetInt32("IDKorisnika");
            if (idKorisnika == null) return RedirectToAction(nameof(KorisnikPocetna));

            bool uspesno = _oglasServis.Zavrsi(id, idKorisnika.Value, OpisIntervencije);

            if (!uspesno)
            {
                TempData["ErrorMessage"] = "Greška prilikom završavanja naloga.";
                return RedirectToAction(nameof(KorisnikZavrsi), new { id });
            }

            TempData["SuccessMessage"] = "Intervencija uspešno završena.";
            return RedirectToAction(nameof(KorisnikSlobodniOglasi));
        }
    }
}

