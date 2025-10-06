using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IOglasRepo
    {
        DataSet DajSveOglase();
        DataSet DajAktivneOglase();
        DataSet DajMojeOglase(int idKorisnika);
        bool NoviOglas(string Adresa, string Naziv);
        bool DodeliOglas(int IDOglasa, int IDKorisnika);
        bool VratiOglasNaCekanju(int idOglasa, int idKorisnika);
    }
}
