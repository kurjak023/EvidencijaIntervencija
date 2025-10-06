using SlojPodataka.Interfejsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SlojPodataka.Repozitorijumi
{
    public class clsOglasRepo : IOglasRepo
    {
        //Polje za konekciju
        private string _stringKonekcije;

        //Konstruktor
        //Dobije se string konekcije pri pozivanju
        public clsOglasRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveOglase()
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveOglase", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }

        public DataSet DajAktivneOglase()
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajAktivneOglase", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }

        public DataSet DajMojeOglase(int idKorisnika)
        {
            DataSet ds = new DataSet();
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajMojeOglase", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = idKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(ds);
            Veza.Close();
            Veza.Dispose();

            return ds;
        }

        public bool NoviOglas(string Adresa, string Naziv)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("NoviOglas", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Adresa", SqlDbType.NVarChar).Value = Adresa;
            Komanda.Parameters.Add("@Naziv", SqlDbType.NVarChar).Value = Naziv;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool DodeliOglas(int IDOglasa, int IDKorisnika)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("DodeliOglas", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDOglasa", SqlDbType.Int).Value = IDOglasa;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = IDKorisnika;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public bool VratiOglasNaCekanju(int idOglasa, int idKorisnika)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("VratiOglasNaCekanju", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDOglasa", SqlDbType.Int).Value = idOglasa;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = idKorisnika;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

    }
}
