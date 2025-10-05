using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    // Class: Zgrada - Predstavlja zgradu koja može biti povezana sa oglasom.
    // Responsibility:
    // - Sadrži informacije o zgradi (adresa, broj stanova).
    // - Omogućava praćenje zgrada u sistemu.
    // Collaboration:
    // - Sa klasom Oglas (oglas može biti vezan za određenu zgradu).
    
    public class clsZgrada
    {
        private int _zgradaID;
        private string _adresa;

        public int ZgradaID
        {
            get { return _zgradaID; }
            set { _zgradaID = value; }
        }

        public string Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

    }
}
