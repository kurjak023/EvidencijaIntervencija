using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
 
    public class clsIntervencija
    {
        private int _intervencijaID;
        private string _opis;
        private int _oglasID;
        private int _idkorisnika;
        private int _zgradaID;

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

        public int OglasID
        {
            get { return _oglasID; }
            set { _oglasID = value; }
        }

        public int IDKorisnika
        {
            get { return _idkorisnika; }
            set { _idkorisnika = value; }
        }

        public int ZgradaID
        {
            get { return _zgradaID; }
            set { _zgradaID = value; }
        }

    }
}
