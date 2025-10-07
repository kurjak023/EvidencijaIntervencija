using AplikacioniSloj;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Klase;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class NalogController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;

        public NalogController(clsKorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
        }

        // GET: /Nalog/Registracija
        [HttpGet]
        public IActionResult Registracija()
        {
            return View();
        }

        // POST: /Nalog/Registracija
        [HttpPost]
        public IActionResult Registracija(RegistracijaModel model)
        {
            if (ModelState.IsValid)
            {
                bool uspesnaRegistracija = _korisnikServis.Dodaj(new clsKorisnik
                {
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    KorisnickoIme = model.KorisnickoIme,
                    Lozinka = model.Lozinka
                });

                if (uspesnaRegistracija)
                {
                    // Ukoliko je registracija uspešna, preusmeri korisnika na odgovarajući view ili akciju
                    return RedirectToAction("Prijava");
                }
                else
                {
                    // Ukoliko registracija nije uspela, može se dodati odgovarajuća logika ili poruka
                    ModelState.AddModelError(string.Empty, "Registracija nije uspešna. Pokušajte ponovo.");
                }
            }

            // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
            return View(model);
        }
        [HttpGet]
        public ActionResult Prijava()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Prijava(PrijavaModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // 1) Učitaj korisnika po korisničkom imenu
            DataSet ds = _korisnikServis.PrikaziPoKorisnickomImenu(model.KorisnickoIme);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Nema korisnika sa navedenim korisničkim imenom!");
                return View(model);
            }

            // 2) Uzmi prvi red i proveri lozinku
            var row = ds.Tables[0].Rows[0];

            string lozinkaDb = row["Lozinka"]?.ToString() ?? "";
            string tipKorisnika = row["TipKorisnika"]?.ToString() ?? "obican_korisnik";
            string ime = row["Ime"]?.ToString() ?? "";
            string prezime = row["Prezime"]?.ToString() ?? "";
            string korisnickoIme = row["KorisnickoIme"]?.ToString() ?? "";
            int idKorisnika = int.TryParse(row["IDKorisnika"]?.ToString(), out var id) ? id : 0;

            if (lozinkaDb != model.Lozinka)
            {
                ModelState.AddModelError(string.Empty, "Pogrešna lozinka");
                return View(model);
            }

            // 3) Sesija
            HttpContext.Session.SetInt32("KorisnikID", idKorisnika);
            HttpContext.Session.SetString("KorisnickoIme", korisnickoIme);
            HttpContext.Session.SetString("Ime", ime);
            HttpContext.Session.SetString("Prezime", prezime);
            HttpContext.Session.SetString("TipKorisnika", tipKorisnika);

            // 4) Redirect po ulozi
            if (tipKorisnika == "admin")
                return RedirectToAction("AdminPocetna", "Admin");

            return RedirectToAction("KorisnikPocetna", "Korisnik");
        }
    }
}
