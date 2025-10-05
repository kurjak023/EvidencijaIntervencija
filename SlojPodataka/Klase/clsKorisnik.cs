using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Korisnik - Upisuje podatke i ima mogućnost registracije, prijave i izmene pdataka.

    // Responsibility:
    // - Upisuje podatke korisnika (ime, prezime, lozinka).
    // - Prati tip korisnika (da li je admin ili običan korisnik).
    // - Omogućava registraciju i prijavu.
    // - Omogućava promenu podataka o korisniku.

    // Collaboration:
    // - Sa klasom Oglas (kreiranje, provera statusa oglasa za odrzavanje zgrade).
    // - Sa klasom Intervencija (praćenje intervencija pri odrzavanju).
    public class clsKorisnik
    {
        private int _IDKorisnika;
        private string _ime;
        private string _prezime;
        private string _korisnickoIme;
        private string _lozinka;
        private string _tipKorisnika; // "admin" ili "obican"

        public int IDKorisnika
        {
            get { return _IDKorisnika; }
            set { _IDKorisnika = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; }
        }

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        public string TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }



    }
}
