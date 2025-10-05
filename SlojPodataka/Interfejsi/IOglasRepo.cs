using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    internal interface IOglasRepo
    {
        DataSet DajSveOglase();
        DataSet DajOglasPoStatusu(string Status);
        DataSet DajSveIntervencije();
        DataSet DajSveIntervencijePoKorisniku(int IDKorisnika);
        bool DodeliOglas(int IDOglasa);
        bool OtkaziOglas(int IDOglasa);
        bool ZavrsiOglas(int IDOglasa);
        
    }
}
