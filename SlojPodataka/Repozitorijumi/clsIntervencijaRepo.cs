using SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijumi
{
    public class clsIntervencijaRepo : IIntervencijaRepo
    {
        //Polje za konekciju
        private string _stringKonekcije;

        //Konstruktor
        //Dobije se string konekcije pri pozivanju
        public clsIntervencijaRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveIntervencije()
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveIntervencije", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }

        public DataSet DajSveIntervencijePoKorisniku(int IDKorisnika)
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("DajSveIntervencijePoKorisniku", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = IDKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }
        public DataSet DajSveIntervencijePoZgradi(int ZgradaID)
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("DajSveIntervencijePoZgradi", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@ZgradaID", SqlDbType.Int).Value = ZgradaID;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }

        public bool DodajIntervenciju(int IDOglasa, int IDKorisnika, string Opis)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("DodajIntervenciju", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDOglasa", SqlDbType.Int).Value = IDOglasa;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = IDKorisnika;
            Komanda.Parameters.Add("@Opis", SqlDbType.NVarChar, 100).Value = Opis;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}
    