using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Oglas - Predstavlja oglas koji može biti kreiran od strane korisnika.
    // Responsibility:
    // - Sadrži informacije o oglasu (naslov, opis, datum kreiranja).
    // - Omogućava kreiranje, izmenu i brisanje oglasa.
    // Collaboration:
    // - Sa klasom Korisnik (korisnik koji kreira oglas).
    // - Sa klasom Intervencija (oglas može biti povezan sa intervencijama).
    // - Sa klasom Zgrada (oglas može biti vezan za određenu zgradu).
    public class clsOglas
    {
        private int _IDOglasa;
        private string _naslov;
        private int _idKorisnika; // ID korisnika koji je kreirao oglas
        private int _idZgrade; // ID zgrade na koju se oglas odnosi
        private int _intervencijaID; // ID intervencije vezane za oglas
        private DateTime _vreme; // Datum i vreme kreiranja oglasa
        private string _Status; // Status oglasa ("Na cekanju", "Dodeljen", "Zavrsen", "Otkazan")

        public int IDOglasa
        {
            get { return _IDOglasa; }
            set { _IDOglasa = value; }
        }

        public string Naslov
        {
            get { return _naslov; }
            set { _naslov = value; }
        }

        public int IdKorisnika
        {
            get { return _idKorisnika; }
            set { _idKorisnika = value; }
        }

        public int IdZgrade
        {
            get { return _idZgrade; }
            set { _idZgrade = value; }
        }

        public int IntervencijaID
        {
            get { return _intervencijaID; }
            set { _intervencijaID = value; }
        }

        public DateTime Vreme
        {
            get { return _vreme; }
            set { _vreme = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
