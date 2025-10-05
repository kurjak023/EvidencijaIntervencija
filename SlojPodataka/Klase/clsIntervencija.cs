using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Intervencija - Predstavlja intervenciju koja može biti povezana sa oglasom.
    // Responsibility:
    // - Sadrži informacije o intervenciji (opis, datum, status).
    // - Omogućava praćenje i ažuriranje statusa intervencije.
    // Collaboration:
    // - Sa klasom Oglas (intervencija može biti povezana sa oglasom).
    // - Sa klasom Korisnik (korisnik koji je zadužen za intervenciju).

    public class clsIntervencija
    {
        private int _intervencijaID;
        private string _opis;

        public int IntervencijaID
        {
            get { return _intervencijaID; }
            set { _intervencijaID = value; }
        }

        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }

    }
}
