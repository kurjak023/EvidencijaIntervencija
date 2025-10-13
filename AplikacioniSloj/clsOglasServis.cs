using DomenskiSloj;
using SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacioniSloj
{
    public class clsOglasServis
    {
        private IOglasRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        public string? LastError { get; private set; }


        //Konstruktor
        public clsOglasServis(IOglasRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
            LastError = null;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveOglase();
        }

        public DataSet PrikaziAktivne()
        {
            return _repo.DajAktivneOglase();
        }

        public DataSet PrikaziMoje(int idKorisnika)
        {
            return _repo.DajMojeOglase(idKorisnika);
        }

        public bool Dodaj(int korisnikId, string adresa, string naziv)
        {
            _poslovnaPravila.KreirajOglas(korisnikId, adresa, naziv);
            return _repo.NoviOglas(adresa, naziv);
        }

        public bool Dodeli(int IDOglasa, int IDKorisnika)
        {
            if (!_poslovnaPravila.PreuzmiOglas(IDOglasa, IDKorisnika))
            {
                LastError = _poslovnaPravila.LastError; // propagacija
                return false;
            }

            var ok = _repo.DodeliOglas(IDOglasa, IDKorisnika);
            if (!ok) LastError = "Oglas nije moguće dodeliti (možda je već preuzet ili status nije 'Na čekanju').";
            return ok;
        }


        public bool VratiNaCekanju(int IDOglasa, int IDKorisnika)
        {
            return _poslovnaPravila.VratiOglasNaCekanju(IDOglasa, IDKorisnika);
        }

        public bool Zavrsi(int IDOglasa, int IDKorisnika, string opisIntervencije)
        {
            if (!_poslovnaPravila.ZavrsiOglas(IDOglasa, IDKorisnika, opisIntervencije))
            {
                LastError = _poslovnaPravila.LastError;
                return false;
            }

            return true;
        }
    }
}