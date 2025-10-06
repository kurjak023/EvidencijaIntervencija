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
    }
}
