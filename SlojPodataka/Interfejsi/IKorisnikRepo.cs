using SlojPodataka.Klase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IKorisnikRepo
    {
        DataSet DajSveKorisnike();
        DataSet DajKorisnikaPoPrezimenu(string Prezime);
        DataSet DajKorisnikaPoID(int IDKorisnika);
        DataSet DajKorisnikaPoKorisnickomImenu(string KorisnickoIme);
        bool NoviKorisnik(clsKorisnik objNoviKorisnik);
        bool ObrisiKorisnika(int IDKorisnika);
        bool IzmeniKorisnika(int StariID, clsKorisnik objNoviKorisnik);

    }
}
