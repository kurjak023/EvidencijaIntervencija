using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SlojPodataka.Repozitorijumi
{
    public class clsKorisnikRepo : IKorisnikRepo
    {
        //Polje za konekciju
        private string _stringKonekcije;

        //Konstruktor
        //Dobije se string konekcije pri pozivanju
        public clsKorisnikRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveKorisnike()
        {
            DataSet dsPodaci = new DataSet();
            
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveKorisnike", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }
        public DataSet DajKorisnikaPoPrezimenu(string Prezime)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajKorisnikaPoPrezimenu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = Prezime;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }
        public DataSet DajKorisnikaPoID(int IDKorisnika)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajKorisnikaPoID", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = IDKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }
        
        public bool NoviKorisnik(clsKorisnik objNoviKorisnik)
        { 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviKorisnik", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviKorisnik.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviKorisnik.Prezime;
            Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = objNoviKorisnik.KorisnickoIme;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviKorisnik.Lozinka;
            Komanda.Parameters.Add("@TipKorisnika", SqlDbType.NVarChar).Value = objNoviKorisnik.TipKorisnika;
            
            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
        public bool ObrisiKorisnika(int IDKorisnika)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("ObrisiKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = IDKorisnika;
            
            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
        public bool IzmeniKorisnika(int StariID, clsKorisnik objNoviKorisnik) 
        {
            //Promenljiva koja služi za proveru uspesnosti unosa
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("IzmeniKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@StariID", SqlDbType.Int).Value = StariID;
            Komanda.Parameters.Add("@IDKorisnika", SqlDbType.Int).Value = objNoviKorisnik.IDKorisnika;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviKorisnik.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviKorisnik.Prezime;
            Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = objNoviKorisnik.KorisnickoIme;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviKorisnik.Lozinka;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //Vraća se true ako je uspesno
            return (proveraUnosa > 0);
        }
        public clsKorisnik DajKorisnikaPoKorisnickomImenu(string KorisnickoIme)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {

                Veza.Open();
                SqlCommand Komanda = new SqlCommand("DajKorisnikaPoKorisnickomImenu", Veza);
                Komanda.CommandType = CommandType.StoredProcedure;
                Komanda.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = KorisnickoIme;

                using (SqlDataReader Reader = Komanda.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        return MapirajRedUObjekat(Reader);
                    }
                    else
                    {
                        return null; // Nema pronađenog korisnika sa datim email-om
                    }
                }
            }


        }
        private clsKorisnik MapirajRedUObjekat(SqlDataReader reader)
        {
            return new clsKorisnik
            {
                IDKorisnika = Convert.ToInt32(reader["IDKorisnika"]),
                Ime = reader["Ime"].ToString(),
                Prezime = reader["Prezime"].ToString(),
                KorisnickoIme = reader["KorisnickoIme"]?.ToString(),
                Lozinka = reader["Lozinka"].ToString(),
                TipKorisnika = reader["TipKorisnika"].ToString(),
            };
        }
    }
}
