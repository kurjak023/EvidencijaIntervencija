using SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacioniSloj
{
    public class clsIntervencijaServis
    {
        private IIntervencijaRepo _repo;

        //Konstruktor
        public clsIntervencijaServis(IIntervencijaRepo repo)
        {
            _repo = repo;
        }

        public DataSet DajSveIntervencije()
        {
            return _repo.DajSveIntervencije();
        }

        public DataSet DajSveIntervencijePoKorisniku(int IDKorisnika)
        {
            return _repo.DajSveIntervencijePoKorisniku(IDKorisnika);
        }

        public DataSet DajSveIntervencijePoZgradi(int zgradaId)
        {
            return _repo.DajSveIntervencijePoZgradi(zgradaId);
        }

        public bool DodajIntervenciju(int IDOglasa, int IDKorisnika, string Opis)
        {
            return _repo.DodajIntervenciju(IDOglasa, IDKorisnika, Opis);
        }
    }
}
