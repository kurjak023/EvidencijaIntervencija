using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IIntervencijaRepo _repoIntervencija;
        private IOglasRepo _repoOglas;
        private IKorisnikRepo _repoKorisnik;

        //Konstruktor
        //Dobija se string konekcije pri pozivanju
        public clsPoslovnaPravila(IIntervencijaRepo repoIntervencija, IOglasRepo repoOglas, IKorisnikRepo repoKorisnik)
        {
            _repoIntervencija = repoIntervencija;
            _repoOglas = repoOglas;
            _repoKorisnik = repoKorisnik;
        }

        //Pravila:
        
    }
}
