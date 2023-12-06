using ApiItelma_Universal.DBContext.CheckPoints;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class CheckPoints_Service
    {
        static CheckPointsContext db = new CheckPointsContext();

        private Serilog.Core.Logger Log_CheckPoints = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_CheckPoints.txt").CreateLogger();

        #region//Get Метод
        public async Task<List<CheckPointsList>> Method_CheckPointsList_GetByCheckPointName(string CheckPointName)
        {
            try
            {
                return await db.CheckPointsLists.Where(e => e.CheckPointName == CheckPointName).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CheckPointsList_GetByCheckPointName В Контроллере CheckPoints " + e.Message.ToString());
                return null;
            }
        }
        public async Task<List<CheckPointsList>> Method_CheckPointsList_GetByCheckPointLine(string CheckPointLine)
        {
            try
            {
                return await db.CheckPointsLists.Where(e => e.CheckPointLine == CheckPointLine).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CheckPointsList_GetByCheckPointLine В Контроллере CheckPoints " + e.Message.ToString());
                return null;
            }
        }
        #endregion

        #region//Put Метод

        public async Task<string> Put_CheckPointsList(string JsonData)
        {
            try
            {
                CheckPointsList jsonPrinter = JsonConvert.DeserializeObject<CheckPointsList>(JsonData);
                CheckPointsList packageSerial = db.CheckPointsLists.Where(e => e.CheckPointName == jsonPrinter.CheckPointName).FirstOrDefault();

                packageSerial.CheckPointIp = jsonPrinter.CheckPointIp;
                packageSerial.CheckPointLine = jsonPrinter.CheckPointLine;
               
                db.CheckPointsLists.Update(packageSerial);
                db.SaveChanges();
                Log_CheckPoints.Information("метод Put_CheckPointsList успешно");
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Information("метод Put_CheckPointsList произошла ошибка");
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что

        public async Task<string> Put_PLCList(string JsonData)
        {
            try
            {
                Plclist jsonPrinter = JsonConvert.DeserializeObject<Plclist>(JsonData);
                Plclist packageSerial = db.Plclists.Where(e => e.Plcname == jsonPrinter.Plcname).FirstOrDefault();

                packageSerial.Plcipaddress = jsonPrinter.Plcipaddress;
                packageSerial.CheckPointName = jsonPrinter.CheckPointName;

                db.Plclists.Update(packageSerial);
                db.SaveChanges();
                Log_CheckPoints.Information("метод Put_PLCList успешно");
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Information("метод Put_PLCList произошла ошибка");
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что

        public async Task<string> Put_CameraList(string JsonData)
        {
            try
            {
                CameraList jsonPrinter = JsonConvert.DeserializeObject<CameraList>(JsonData);
                CameraList packageSerial = db.CameraLists.Where(e => e.CameraName == jsonPrinter.CameraName).FirstOrDefault();

                packageSerial.CheckPointName = jsonPrinter.CheckPointName;
                packageSerial.CameraIpaddress = jsonPrinter.CameraIpaddress;

                db.CameraLists.Update(packageSerial);
                db.SaveChanges();
                Log_CheckPoints.Information("метод Put_CameraList успешно");
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Information("метод Put_CameraList произошла ошибка");
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что


        #endregion

        #region//Post Метод
        //Добавляем строку данных в CheckPointsList
        public async Task<string> Method_CheckPointsList_Post(string JSON_CheckPointsList)
        {
            try
            {
                CheckPointsList jsonData = JsonConvert.DeserializeObject<CheckPointsList>(JSON_CheckPointsList);
                db.CheckPointsLists.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoints.Information("Method_CheckPointsList_Post успешно " + jsonData.CheckPointName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CheckPointsList_Post В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        //Добавляем строку данных в PLCList
        public async Task<string> Method_PLCList_Post(string JSON_PLCList)
        {
            try
            {
                Plclist jsonData = JsonConvert.DeserializeObject<Plclist>(JSON_PLCList);
                db.Plclists.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoints.Information("Method_PLCList_Post успешно " + jsonData.CheckPointName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_PLCList_Post В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        //Добавляем строку данных в CameraList
        public async Task<string> Method_CameraList_Post(string JSON_CameraList)
        {
            try
            {
                CameraList jsonData = JsonConvert.DeserializeObject<CameraList>(JSON_CameraList);
                db.CameraLists.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoints.Information("Method_CameraList_Post успешно " + jsonData.CheckPointName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CheckPointsList_Post В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion


        #region//Delete метод
        public async Task<string> Method_CheckPointsList_delete(string CheckPointName, string CheckPointId)
        {
            try
            {
                db.CheckPointsLists.Where(e => e.CheckPointName == CheckPointName & e.CheckPointId == int.Parse(CheckPointId)).ExecuteDelete();
                Log_CheckPoints.Information("Method_CheckPointsList_delete успешно " + CheckPointName + " " + CheckPointId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CheckPointsList_delete В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        public async Task<string> Method_PLCList_delete(string Plcipaddress, string Plcname)
        {
            try
            {
                db.Plclists.Where(e => e.Plcipaddress == Plcipaddress & e.Plcname == Plcname).ExecuteDelete();
                Log_CheckPoints.Information("Method_PLCList_delete успешно " + Plcipaddress + " " + Plcname + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_PLCList_delete В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        public async Task<string> Method_CameraList_delete(string CameraIpaddress, string CameraName)
        {
            try
            {
                db.CameraLists.Where(e => e.CameraIpaddress == CameraIpaddress & e.CameraName == CameraName).ExecuteDelete();
                Log_CheckPoints.Information("Method_CameraList_delete успешно " + CameraIpaddress + " " + CameraName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoints.Error("Ошибка Method_CameraList_delete В Контроллере CheckPoints" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion
    }
}
