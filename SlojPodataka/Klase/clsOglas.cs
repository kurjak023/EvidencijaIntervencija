using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase
{
    
    public class clsOglas
    {
        private int _idOglasa;
        private string _naziv;
        private int _idKorisnika;
        private int _zgradaid;
        private DateTime _vreme;
        private string _status; // Status oglasa ("Na čekanju", "Dodeljen", "Završen")

        public int IDOglasa
        {
            get { return _idOglasa; }
            set { _idOglasa = value; }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        public int IDKorisnika
        {
            get { return _idKorisnika; }
            set { _idKorisnika = value; }
        }

        public int ZgradaID
        {
            get { return _zgradaid; }
            set { _zgradaid = value; }
        }

        public DateTime Vreme
        {
            get { return _vreme; }
            set { _vreme = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
