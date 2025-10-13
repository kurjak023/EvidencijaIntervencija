using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacioniSloj
{
    public class clsKorisnikServis
    {
        private IKorisnikRepo _repo;

        //Konstruktor
        public clsKorisnikServis(IKorisnikRepo repo)
        {
            _repo = repo;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveKorisnike();
        }

        public DataSet PrikaziPoPrezimenu(string prezime)
        {
            return _repo.DajKorisnikaPoPrezimenu(prezime);
        }

        public bool Dodaj(clsKorisnik objKorisnik)
        {
            return _repo.NoviKorisnik(objKorisnik);
        }

        public bool Obrisi(int IDKorisnika)
        {
            return _repo.ObrisiKorisnika(IDKorisnika);
        }

        public bool Izmeni(int StariID, clsKorisnik objNoviKorisnik)
        {
            return _repo.IzmeniKorisnika(StariID, objNoviKorisnik);
        }
        
        public clsKorisnik PrikaziPoKorisnickomImenu(string KorisnickoIme)
        {
            return _repo.DajKorisnikaPoKorisnickomImenu(KorisnickoIme);
        }
    }
}
