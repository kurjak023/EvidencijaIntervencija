using SlojPodataka;
using SlojPodataka.Interfejsi;
using System;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IIntervencijaRepo _repoIntervencija;
        private IOglasRepo _repoOglas;
        private IKorisnikRepo _repoKorisnik;

        //Konstruktor
        //Dobija se string konekcije pri pozivanju
        public clsPoslovnaPravila(IIntervencijaRepo repoIntervencija, IOglasRepo repoOglas, IKorisnikRepo repoKorisnik)
        {
            _repoIntervencija = repoIntervencija;
            _repoOglas = repoOglas;
            _repoKorisnik = repoKorisnik;
        }

        ///Pravila:
        // 1) Admin NE preuzima oglase
         public bool PreuzmiOglas(int oglasId, int korisnikId)
        {
            var ds = _repoKorisnik.DajKorisnikaPoID(korisnikId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return false;

            var tip = ds.Tables[0].Rows[0]["TipKorisnika"]?.ToString();
            return tip == "obican_korisnik"; // samo običan korisnik preuzima
        }

        // 2) Novi oglas kreira SAMO admin; adresa i naziv su obavezni
        public bool KreirajOglas(int korisnikId, string adresa, string naziv)
        {
            if (string.IsNullOrWhiteSpace(adresa)) return false;
            if (string.IsNullOrWhiteSpace(naziv)) return false;

            var ds = _repoKorisnik.DajKorisnikaPoID(korisnikId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return false;

            var tip = ds.Tables[0].Rows[0]["TipKorisnika"]?.ToString();
            return tip == "admin";
        }

        // 3) Vraćanje na čekanju
        public bool VratiOglasNaCekanju(int oglasId, int korisnikId)
        {
            return _repoOglas.VratiOglasNaCekanju(oglasId, korisnikId);
        }

        // 4) Završavanje oglasa - opis intervencije je obavezan, min 5, max 100 karaktera
        public bool ZavrsiOglas(int IDOglasa, int IDKorisnika, string opisIntervencije)
        {
            if (string.IsNullOrWhiteSpace(opisIntervencije))
            {
                LastError = "Opis intervencije ne može biti prazan.";
                return false;
            }

            int duzina = opisIntervencije.Trim().Length;

            if (duzina < 5)
            {
                LastError = "Opis intervencije mora imati najmanje 5 karaktera.";
                return false;
            }

            if (duzina > 100)
            {
                LastError = "Opis intervencije može imati najviše 100 karaktera.";
                return false;
            }

            LastError = "";
            return true;
        }
        public string LastError { get; private set; } = "";
    }
}
