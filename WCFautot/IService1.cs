using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using WCFautot.model;

namespace WCFautot
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool TestDatabaseConnection();
        [OperationContract]
        bool saveAuto(Auto newAuto);
        [OperationContract]
        List<Autonmerkki> getAutoMakers();
        [OperationContract]
        List<Autonmallit> GetAutonmallits(int merkkiID);
        [OperationContract]
        List<Varit> GetVarits();
        [OperationContract]
        List<Polttoaine> GetPolttoaines();
        [OperationContract]
        bool SaveAuto(Auto pirssi);
        [OperationContract]
        Auto NextCars(int ID);
        [OperationContract]
        Auto PreviousCars(int ID);
    }
    // TODO: Add your service operations here

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Auto
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public decimal Hinta { get; set; }
        [DataMember]
        public DateTime Rekisteri_Paivamaara { get; set; }
        [DataMember]
        public decimal Moottorin_tilavuus { get; set; }
        [DataMember]
        public int Mittarilukema { get; set; }
        [DataMember]
        public int AutonmerkkiID { get; set; }
        [DataMember]
        public int AutonmalliID { get; set; }
        [DataMember]
        public int VaritID { get; set; }
        [DataMember]
        public int PolttoaineID { get; set; }
    }
    public class Autonmerkki
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string merkkinimi { get; set; }
    }
    public class Autonmallit
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string mallinimi { get; set; }
        [DataMember]
        public int merkkiID { get; set; }
    }
    public class Polttoaine
    {
            [DataMember]
            public int ID { get; set; }
            [DataMember]
            public string Polttoaineen_nimi { get; set; }
    }
    public class Varit
    {
            [DataMember]
            public int ID { get; set; }
            [DataMember]
            public string Varin_nimi { get; set; }
    }
    
}





