
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace WCFautot.model
{
    public class DatabaseHallinta
    {

        SqlConnection dbYhteys = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Autokauppa;Integrated Security=True;Connect Timeout=30" +
        ";Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public DatabaseHallinta()
        {

        }

        public bool connectDatabase()
        {
            try
            {
                dbYhteys.Open();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheilmoitukset:" + e);
                dbYhteys.Close();
                return false;
            }
        }
        public void disconnectDatabase()
        {
            dbYhteys.Close();
        }
        public bool saveAutoIntoDatabase(Auto newAuto)
        {
            bool palaute = false;
            return palaute;
        }
        /// <summary>
        /// Returns all auto makers
        /// </summary>
        /// <returns>List of makers</returns>
        public List<Autonmerkki> getAllAutoMakers()
        {
            dbYhteys.Open();
            List<Autonmerkki> autoMakers = new List<Autonmerkki>();
            SqlCommand jes1 = new SqlCommand("SELECT * FROM AutonMerkki", dbYhteys);
            SqlDataReader dasd = jes1.ExecuteReader();
            while (dasd.Read())
            {
                Autonmerkki jepulis = new Autonmerkki();
                jepulis.id = (int)dasd["ID"];
                jepulis.merkkinimi = (string)dasd["Merkki"];
                autoMakers.Add(jepulis);
            }
            dbYhteys.Close();
            return autoMakers;

        }
        /// <summary>
        /// Returns all chosen maker models
        /// </summary>
        /// <param name="merkkiID"></param>
        /// <returns></returns>
        public List<Autonmallit> GetAutonmallits(int merkkiID)
        {
            dbYhteys.Open();
            List<Autonmallit> autoModels = new List<Autonmallit>();
            SqlCommand jes2 = new SqlCommand("SELECT * FROM AutonMallit WHERE AutonMerkkiID=" + merkkiID, dbYhteys);
            SqlDataReader fasd = jes2.ExecuteReader();
            while (fasd.Read())
            {
                Autonmallit jeejee = new Autonmallit();
                jeejee.id = (int)fasd["ID"];
                jeejee.mallinimi = fasd["Auton_mallin_nimi"].ToString();
                jeejee.merkkiID = (int)fasd["AutonMerkkiID"];
                autoModels.Add(jeejee);
            }
            dbYhteys.Close();
            return autoModels;
        }
        public List<Varit> CarColors()
        {
            dbYhteys.Open();
            List<Varit> autoColors = new List<Varit>();
            SqlCommand jes3 = new SqlCommand("SELECT * FROM Varit", dbYhteys);
            SqlDataReader asdf = jes3.ExecuteReader();
            while (asdf.Read())
            {
                Varit colorsor = new Varit();
                colorsor.ID = (int)asdf["ID"];
                colorsor.Varin_nimi = (string)asdf["Varin_nimi"];
                autoColors.Add(colorsor);
            }
            dbYhteys.Close();
            return autoColors;
        }
        public List<Polttoaine> CarFuel()
        {
            dbYhteys.Open();
            List<Polttoaine> autoFuel = new List<Polttoaine>();
            SqlCommand jes4 = new SqlCommand("SELECT * FROM Polttoaine", dbYhteys);
            SqlDataReader qwerty = jes4.ExecuteReader();
            while (qwerty.Read())
            {
                Polttoaine fuellii = new Polttoaine();
                fuellii.ID = (int)qwerty["ID"];
                fuellii.Polttoaineen_nimi = (string)qwerty["Polttoaineen_nimi"];
                autoFuel.Add(fuellii);
            }
            dbYhteys.Close();
            return autoFuel;
        }
        public bool SafeCar(Auto pirssi)
        {
            dbYhteys.Open();
            SqlCommand komento1 = new SqlCommand("INSERT INTO Auto *"+
            "VALUES(@hinta,@rekpvm,@mottlv,@mitlkv,@automerkki,@automalli,@autovari,@polttoaine)",dbYhteys);
            //SqlParameter autoadd = new SqlParameter("@Safecarpara",dbYhteys);
            //komento1.Parameters.Add(autoadd);

            SqlParameter hinta = new SqlParameter("@hinta", pirssi.Hinta);
            komento1.Parameters.Add(hinta);
            SqlParameter rekisteri_paivamaara = new SqlParameter("@rekpvm", pirssi.Rekisteri_Paivamaara);
            komento1.Parameters.Add(rekisteri_paivamaara);
            SqlParameter moottoritilavuus = new SqlParameter("@mottlv", pirssi.Moottorin_tilavuus);
            komento1.Parameters.Add(moottoritilavuus);
            SqlParameter mittarilukema = new SqlParameter("@mitlkv", pirssi.Mittarilukema);
            komento1.Parameters.Add(mittarilukema);
            SqlParameter merkki = new SqlParameter("@automerkki", pirssi.AutonmerkkiID);
            komento1.Parameters.Add(merkki);
            SqlParameter malli = new SqlParameter("@automalli", pirssi.AutonmalliID);
            komento1.Parameters.Add(malli);
            SqlParameter vari = new SqlParameter("@autovari", pirssi.VaritID);
            komento1.Parameters.Add(vari);
            SqlParameter polttoaine = new SqlParameter("@polttoaine", pirssi.PolttoaineID);
            komento1.Parameters.Add(polttoaine);
            int jep = komento1.ExecuteNonQuery();
            dbYhteys.Close();
            return true;
        }
        public Auto NextCar(int ID)
        {
            Auto seuraavaauto = new Auto();
            dbYhteys.Open();
            SqlCommand seuraava = new SqlCommand("SELECT TOP 1 * FROM AUTO WHERE ID>"+ID+" ORDER BY ID ASC", dbYhteys);
            SqlDataReader srvauto = seuraava.ExecuteReader();
            while(srvauto.Read())
            {              
                seuraavaauto.ID = (int)srvauto["ID"];
                seuraavaauto.Hinta = (decimal)srvauto["Hinta"];
                seuraavaauto.Rekisteri_Paivamaara = (DateTime)srvauto["Rekisteri_paivamaara"];
                seuraavaauto.Moottorin_tilavuus = (decimal)srvauto["Moottorin_tilavuus"];
                seuraavaauto.Mittarilukema = (int)srvauto["Mittarilukema"];
                seuraavaauto.AutonmerkkiID = (int)srvauto["AutonMerkkiID"];
                seuraavaauto.AutonmalliID = (int)srvauto["AutonMalliID"];
                seuraavaauto.VaritID = (int)srvauto["VaritID"];
                seuraavaauto.PolttoaineID = (int)srvauto["PolttoaineID"];
            }
            dbYhteys.Close();
            return seuraavaauto;
        }
        public Auto PreviousCar(int ID)
        {
            Auto edellinenauto = new Auto();
            dbYhteys.Open();
            SqlCommand edellinen = new SqlCommand("SELECT TOP 1 * FROM AUTO WHERE ID<"+ID+" ORDER BY ID DESC", dbYhteys);
            SqlDataReader prvauto = edellinen.ExecuteReader();
            while(prvauto.Read())
            {
                edellinenauto.ID = (int)prvauto["ID"];
                edellinenauto.Hinta = (decimal)prvauto["Hinta"];
                edellinenauto.Rekisteri_Paivamaara = (DateTime)prvauto["Rekisteri_paivamaara"];
                edellinenauto.Moottorin_tilavuus = (decimal)prvauto["Moottorin_tilavuus"];
                edellinenauto.Mittarilukema = (int)prvauto["Mittarilukema"];
                edellinenauto.AutonmerkkiID = (int)prvauto["AutonMerkkiID"];
                edellinenauto.AutonmalliID = (int)prvauto["AutonMalliID"];
                edellinenauto.VaritID = (int)prvauto["VaritID"];
                edellinenauto.PolttoaineID = (int)prvauto["PolttoaineID"];
            }
            dbYhteys.Close();
            return edellinenauto;
        }
        public Auto LastCar()
        {
            Auto viimeinenAuto = new Auto();
            dbYhteys.Open();
            SqlCommand viimeinen = new SqlCommand("SELECT TOP 1 * FROM AUTO ORDER BY ID DESC", dbYhteys);
            SqlDataReader lstcar = viimeinen.ExecuteReader();
            while(lstcar.Read())
            {
                viimeinenAuto.ID = (int)lstcar["ID"];
                viimeinenAuto.Hinta = (decimal)lstcar["Hinta"];
                viimeinenAuto.Rekisteri_Paivamaara = (DateTime)lstcar["Rekisteri_paivamaara"];
                viimeinenAuto.Moottorin_tilavuus = (decimal)lstcar["Moottorin_tilavuus"];
                viimeinenAuto.Mittarilukema = (int)lstcar["Mittarilukema"];
                viimeinenAuto.AutonmerkkiID = (int)lstcar["AutonMerkkiID"];
                viimeinenAuto.AutonmalliID = (int)lstcar["AutonMalliID"];
                viimeinenAuto.VaritID = (int)lstcar["VaritID"];
                viimeinenAuto.PolttoaineID = (int)lstcar["PolttoaineID"];
            }
            dbYhteys.Close();
            return viimeinenAuto;
        }
        public Auto FirstCar(int ID)
        {
            Auto ekaPirssi = new Auto();
            dbYhteys.Open();
            SqlCommand edellinen1 = new SqlCommand("SELECT TOP 1 * FROM AUTO  ORDER BY ID ASC", dbYhteys);
            SqlDataReader prvauto1 = edellinen1.ExecuteReader();
            while (prvauto1.Read())
            {
                ekaPirssi.ID = (int)prvauto1["ID"];
                ekaPirssi.Hinta = (decimal)prvauto1["Hinta"];
                ekaPirssi.Rekisteri_Paivamaara = (DateTime)prvauto1["Rekisteri_paivamaara"];
                ekaPirssi.Moottorin_tilavuus = (decimal)prvauto1["Moottorin_tilavuus"];
                ekaPirssi.Mittarilukema = (int)prvauto1["Mittarilukema"];
                ekaPirssi.AutonmerkkiID = (int)prvauto1["AutonMerkkiID"];
                ekaPirssi.AutonmalliID = (int)prvauto1["AutonMalliID"];
                ekaPirssi.VaritID = (int)prvauto1["VaritID"];
                ekaPirssi.PolttoaineID = (int)prvauto1["PolttoaineID"];
            }
            dbYhteys.Close();
            return ekaPirssi;
        }
    }
}
