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
        public string? LastError { get; private set; }

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
            var ok = _repo.DodajIntervenciju(IDOglasa, IDKorisnika, Opis);
            if (!ok) LastError = "Intervencija nije sačuvana.";
            return ok;
        }
    }
}
