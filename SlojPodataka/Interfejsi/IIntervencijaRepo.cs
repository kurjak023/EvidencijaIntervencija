using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IIntervencijaRepo
    {
        DataSet DajSveIntervencije();
        DataSet DajSveIntervencijePoKorisniku(int IDKorisnika);
        DataSet DajSveIntervencijePoZgradi(int zgradaId);
        bool DodajIntervenciju(int IDOglasa, int IDKorisnika, string Opis);
    }
}
