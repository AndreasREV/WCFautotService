    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFautot.model;
using System.Data;

namespace WCFautot
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

            DatabaseHallinta dbController = new DatabaseHallinta();


            public bool TestDatabaseConnection()
            {
                bool doesItWork = dbController.connectDatabase();
                dbController.disconnectDatabase();
                return doesItWork;
            }

            public bool saveAuto(Auto newAuto)
            {
                bool didItGoIntoDatabase = dbController.saveAutoIntoDatabase(newAuto);
                return didItGoIntoDatabase;
            }



            public List<Autonmerkki> getAutoMakers()
            {
                List<Autonmerkki> autolista = dbController.getAllAutoMakers();
                return autolista;
                //return dbController.getAutoModelsByMakerId(makerId);
            }
            public List<Autonmallit> GetAutonmallits(int merkkiID)
            {
                List<Autonmallit> mallilista = dbController.GetAutonmallits(merkkiID);
                return mallilista;
            }
            public List<Varit> GetVarits()
            {
                List<Varit> varits = dbController.CarColors();
                return varits;
            }
            public List<Polttoaine> GetPolttoaines()
            {
                return dbController.CarFuel();
            }
            public bool SaveAuto(Auto pirssi)
            {
                bool kekeke = dbController.SafeCar(pirssi);
                return kekeke;
                //kekwSQUAD
            }
            public Auto NextCars(int ID)
            {
                if (ID == 0)
                {
                    return dbController.FirstCar(ID);

                }
                else
                {
                    Auto haettuAuto = dbController.NextCar(ID);
                    if (haettuAuto.ID == 0)
                    {
                        return dbController.FirstCar(ID);

                    }
                    return haettuAuto;
                }
            }
            public Auto PreviousCars(int ID)
            {
                if (ID == 0)
                {
                    return dbController.LastCar();

                }
                else
                {
                    Auto haettuAuto = dbController.PreviousCar(ID);
                    if (haettuAuto.ID == 0)
                    {
                        return dbController.LastCar();

                    }
                    return haettuAuto;
                }
            }
        }
}

