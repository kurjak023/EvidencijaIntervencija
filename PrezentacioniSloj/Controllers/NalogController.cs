using AplikacioniSloj;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Klase;
using System.Data;
using System.Data.SqlClient;

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
            if (!ModelState.IsValid)
                return View(model);

            // Provera lozinke
            if (!string.Equals(model.Lozinka, model.PotvrdaLozinke))
            {
                ModelState.AddModelError(nameof(model.PotvrdaLozinke), "Lozinke se ne poklapaju.");
                return View(model);
            }

            // Provera Korisnickog imena
            var postoji = _korisnikServis.PrikaziPoKorisnickomImenu(model.KorisnickoIme);
            if (postoji != null)
            {
                ModelState.AddModelError(nameof(model.KorisnickoIme), "Korisničko ime je već zauzeto.");
                return View(model);
            }

            try
            {
                var ok = _korisnikServis.Dodaj(new clsKorisnik
                {
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    KorisnickoIme = model.KorisnickoIme,
                    Lozinka = model.Lozinka,
                    TipKorisnika = "obican_korisnik"
                });
                if (!ok)
                {
                    ModelState.AddModelError("", "Registracija nije uspela. Pokušajte ponovo.");
                    return View(model);
                }
                TempData["Success"] = "Nalog je kreiran. Prijavite se.";
                return RedirectToAction("Prijava");
            }
            catch (InvalidOperationException ex) when (ex.Message == "USERNAME_TAKEN")
            {
                ModelState.AddModelError(nameof(model.KorisnickoIme), "Korisničko ime je već zauzeto.");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(PrijavaModel model)
        {
            if (ModelState.IsValid)
            {
                // Pozovi metodu iz servisa koja proverava korisničke podatke
                var prijavljeniKorisnik = _korisnikServis.PrikaziPoKorisnickomImenu(model.KorisnickoIme);

                if (prijavljeniKorisnik != null)
                {
                    // Ako je pronađen korisnik sa datom e-poštom, proveri lozinku
                    if (prijavljeniKorisnik.Lozinka == model.Lozinka)
                    {
                        // Lozinka je ispravna, postavi korisničke podatke u sesiju
                        HttpContext.Session.SetInt32("KorisnikID", prijavljeniKorisnik.IDKorisnika); 
                        HttpContext.Session.SetString("Ime", prijavljeniKorisnik.Ime);
                        HttpContext.Session.SetString("Prezime", prijavljeniKorisnik.Prezime);
                        HttpContext.Session.SetString("Email", prijavljeniKorisnik.KorisnickoIme);
                        HttpContext.Session.SetString("Lozinka", prijavljeniKorisnik.Lozinka);
                        HttpContext.Session.SetString("TipKorisnika", prijavljeniKorisnik.TipKorisnika);

                        // Redirekcija na odgovarajući view u zavisnosti od toga kog tipa je korisnik
                        if (prijavljeniKorisnik.TipKorisnika == "admin")
                        {
                            return RedirectToAction("AdminPocetna", "Admin");
                        }
                        else if (prijavljeniKorisnik.TipKorisnika == "obican_korisnik")
                        {
                            return RedirectToAction("KorisnikPocetna", "Korisnik");
                        }
                    }
                    else
                    {
                        // Pogrešna lozinka
                        ModelState.AddModelError(string.Empty, "Pogrešna lozinka");
                    }
                }
                else
                {
                    // Nema korisnika sa datom e-mailom
                    ModelState.AddModelError(string.Empty, "Nema korisnika u bazi podataka sa navedenim e-mailom!");
                }
            }

            // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
            return View(model);
        }
    }
}
