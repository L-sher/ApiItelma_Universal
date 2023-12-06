using ApiItelma_Universal.DBContext.CheckPoint_Metra;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class CheckPoint_Metra_Service
    {
        static CheckPointMetraContext db = new CheckPointMetraContext();
        private Serilog.Core.Logger Log_CheckPoint_Metra = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_CheckPoint_Metra.txt").CreateLogger();


        #region//Get Методы
        // получаем строку данных по Barcode
        public async Task<List<CheckPointData>> Method_CheckPointData_GetByBarCode(string barcode)
        {
            try
            {
                return await db.CheckPointData.Where(e => e.BarCode == barcode).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка метода Method_CheckPointData_GetByBarCode В Контроллере метры " + e.Message.ToString());
                return null;
            }
           
        }

        // получаем строку данных по id
        public async Task<List<CheckPointDataProgrammer>> Method_CheckPointDataProgrammers_SelectinId(string id)
        {
            try
            {
                return await db.CheckPointDataProgrammers.Where(e => e.Id == int.Parse(id)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataProgrammers_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }

        }
        // получаем строку данных по id
        public async Task<List<CheckPointDataPrinter>> Method_CheckPointDataPrinters_GetById(string id)
        {
            try
            {
                return await db.CheckPointDataPrinters.Where(e => e.Id == int.Parse(id)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataPrinters_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }
        // получаем строку данных по id
        public async Task<List<CheckPointDataUfk1>> Method_CheckPointDataUfk1s_GetById(string id)
        {
            try
            {
                return await db.CheckPointDataUfk1s.Where(e => e.Id == int.Parse(id)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataUfk1s_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        // получаем строку данных по id
        public async Task<List<CheckPointDataUfk3>> Method_CheckPointDataUfk3_GetById(string id)
        {
            try
            {
                return await db.CheckPointDataUfk3s.Where(e => e.Id == int.Parse(id)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_UFK3Data_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        // получаем строку данных по id из CheckPointSetting
        public async Task<List<CheckPointSetting>> Method_CheckPointSetting_GetById(string ProductId)
        {
            try
            {
                return await db.CheckPointSettings.Where(e => e.ProductId == int.Parse(ProductId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSetting_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<CheckPointSettingsUfk1>> Method_CheckPointSettingsUfk1_GetById(string ProductId)
        {
            try
            {
                return await db.CheckPointSettingsUfk1s.Where(e => e.ProductId == int.Parse(ProductId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUfk1_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<CheckPointSettingsUfk3>> Method_CheckPointSettingsUfk3_GetById(string ProductId)
        {
            try
            {
                return await db.CheckPointSettingsUfk3s.Where(e => e.ProductId == int.Parse(ProductId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUfk3_SelectinId В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<CheckPointData>> Method_Get_CheckPointData_ByBarcode_IncludeAllTables(string barCode)
        {
            try
            {
                //Сначала проверяем что баркод есть во всех таблицах
                var check = await db.CheckPointData
                  .Where(e => e.BarCode == barCode && e.ResultTest == true)
                  .Include(e => e.CheckPointDataUfk1s)
                  .Include(e => e.CheckPointDataUfk3s)
                  .Include(e => e.CheckPointDataPrinters)
                  .Include(e => e.CheckPointDataProgrammers)
                  .ToListAsync();
                db.ChangeTracker.Clear();

                List<CheckPointData> gjfgj = null;

                //Теперь если баркод везде присутствует возвращаем только CheckPointdata.
                if (check != null)
                {
                    gjfgj = await db.CheckPointData
           .Where(e => e.BarCode == barCode && e.ResultTest == true).ToListAsync();
                }

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(gjfgj, Formatting.Indented, settings);
                //string str= JsonConvert.SerializeObject(gjfgj);

                Log_CheckPoint_Metra.Information("Успешно Get_CheckPointData_ByBarcode_IncludeAllTables ");
                return gjfgj;
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка метода Get_CheckPointData_ByBarcode_IncludeAllTables В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }

        public async Task<string> Method_GetVIN_CheckPointDataUfk3_ByBarcode(string barCode)
        {
            try
            {
                var JSONTest = db.CheckPointDataUfk3s
                .Where(w => w.LocalBarCode == barCode)
                .Select(d => d.Jsontest)
                .FirstOrDefault();
                JObject VIN = JObject.Parse(JSONTest);
                return (string)VIN["5.02"];
            }
            catch (Exception ex)
            {
                Log_CheckPoint_Metra.Information("Method_GetVIN_CheckPointDataUfk3_ByBarcode "+ ex.Message.ToString());
                return "null";
            }
        }

        #region//метод ATTENTION. не применяется на упаковке. не понятно где применяется.
        public async Task<List<CheckPointDataUfk3>> Method_Get_CheckPointDataUfk3_ByDateAndProductId_IncludeCheckPointData(DateTime DateStart, DateTime DateEnd, int productId)
        {
            try
            {
                var dateEnd = DateEnd;
                if (DateStart == DateEnd)
                    dateEnd = DateEnd.AddDays(1);

                var req = db.CheckPointDataUfk3s
                    .Where(e => e.LocalBarCodeNavigation.TimeCheck >= DateStart && e.LocalBarCodeNavigation.TimeCheck <= dateEnd)
                    .Where(e => e.LocalBarCodeNavigation.ProductId == productId)
                    .Where(e => e.LocalBarCodeNavigation.ResultTest == true)
                    .Where(u => u.Result == true).ToList();
                Log.Logger.Error("Успешно выполнен метод Method_Get_CheckPointDataUfk3_ByDateAndProductId_IncludeCheckPointData ");
                return req;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Ошибка Method_Get_CheckPointDataUfk3_ByDateAndProductId_IncludeCheckPointData В Контроллере метры " + e.Message.ToString());
                return null;
            }
        }
        #endregion


        #endregion

        #region//Post Методы
        //Добавляем строку данных в CheckPointData
        public async Task<string> Method_CheckPointData_Post(string JSON_for_CheckPointData)
        {
            try
            {
                CheckPointData jsonData = JsonConvert.DeserializeObject<CheckPointData>(JSON_for_CheckPointData);
                db.CheckPointData.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointData_Post успешно " + jsonData.BarCode.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointData_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в CheckPointDataPrinter
        public async Task<string> Method_CheckPointDataPrinter_Post(string JSON_for_CheckPointDataPrinter)
        {
            try
            {
                CheckPointDataPrinter jsonPrinter = JsonConvert.DeserializeObject<CheckPointDataPrinter>(JSON_for_CheckPointDataPrinter);
                if (jsonPrinter.Serial == null) { return "Обязательное поле Serial"; }
                db.CheckPointDataPrinters.Add(jsonPrinter);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataPrinter_Postуспешно " + jsonPrinter.Serial.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataPrinter_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        //Добавляем строку данных в CheckPointDataProgrammer
        public async Task<string> Method_CheckPointDataProgrammer_Post(string JSON_for_CheckPointDataProgrammer)
        {
            try
            {
                CheckPointDataProgrammer jsonData = JsonConvert.DeserializeObject<CheckPointDataProgrammer>(JSON_for_CheckPointDataProgrammer);
                if (jsonData.Ku == null) { return "Обязательное поле Ku"; }
                db.CheckPointDataProgrammers.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataProgrammer_Postуспешно " + jsonData.Ku.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataProgrammer_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        //Добавляем строку данных в CheckPointDataUFK1
        public async Task<string> Method_CheckPointDataUFK1_Post(string JSON_for_CheckPointDataUFK1)
        {
            try
            {
                CheckPointDataUfk1 jsonData = JsonConvert.DeserializeObject<CheckPointDataUfk1>(JSON_for_CheckPointDataUFK1);
                if (jsonData.Serial == null) { return "Обязательное поле Serial"; }
                db.CheckPointDataUfk1s.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataUFK1_Post успешно " + jsonData.Serial.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataUFK1_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в CheckPointDataUFK3
        public async Task<string> Method_CheckPointDataUFK3_Post(string JSON_for_CheckPointDataUFK3)
        {
            try
            {
                CheckPointDataUfk3 jsonData = JsonConvert.DeserializeObject<CheckPointDataUfk3>(JSON_for_CheckPointDataUFK3);
                if (jsonData.Serial == null) { return "Обязательное поле Serial"; }
                db.CheckPointDataUfk3s.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataUFK3_Postуспешно " + jsonData.Serial.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataUFK3_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в CheckPointSetting
        public async Task<string> Method_CheckPointSettings_Post(string JSON_for_CheckPointSettings)
        {
            try
            {
                CheckPointSetting jsonData = JsonConvert.DeserializeObject<CheckPointSetting>(JSON_for_CheckPointSettings);
                db.CheckPointSettings.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettings_Postуспешно " + jsonData.CheckPointId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettings_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в CheckPointSettingsUFK1
        public async Task<string> Method_CheckPointSettingsUFK1_Post(string JSON_for_CheckPointSettingsUFK1)
        {
            try
            {
                CheckPointSettingsUfk1 jsonData = JsonConvert.DeserializeObject<CheckPointSettingsUfk1>(JSON_for_CheckPointSettingsUFK1);
                db.CheckPointSettingsUfk1s.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettingsUFK1_Post успешно " + jsonData.ProductId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUFK1_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в CheckPointSettingsUFK3
        public async Task<string> Method_CheckPointSettingsUFK3_Post(string JSON_for_CheckPointSettingsUFK3)
        {
            try
            {
                CheckPointSettingsUfk3 jsonData = JsonConvert.DeserializeObject<CheckPointSettingsUfk3>(JSON_for_CheckPointSettingsUFK3);
                db.CheckPointSettingsUfk3s.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettingsUFK3_Post успешно " + jsonData.ProductId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUFK3_Post В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion

        #region//Post методы
        public string Method_CheckPointData_Change(string JsonData)
        {
            try
            {
                CheckPointData jsonPrinter = JsonConvert.DeserializeObject<CheckPointData>(JsonData);
                CheckPointData packageSerial = db.CheckPointData.Where(e => e.BarCode == jsonPrinter.BarCode).FirstOrDefault();
                packageSerial.ProductId = jsonPrinter.ProductId;
                db.CheckPointData.Update(packageSerial);
                db.SaveChanges();
                Log_CheckPoint_Metra.Information("Method_CheckPointData_Change успешно");
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Information("Method_CheckPointData_Change ошибка "+ e.Message.ToString());
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что
        #endregion

        #region//Delete методы
        public async Task<string> Method_CheckPointData_delete(string Barcode,string ProductId)
        {
            try
            {
                db.CheckPointData.Where(e => e.BarCode == Barcode & e.ProductId== int.Parse(ProductId)).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointData_delete успешно " + Barcode + " " + ProductId+ " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointData_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        public async Task<string> Method_CheckPointDataPrinter_delete(string LocalBarCode, string Serial)
        {
            try
            {
                db.CheckPointDataPrinters.Where(e => e.LocalBarCode == LocalBarCode & e.Serial== Serial).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataPrinter_delete успешно " + LocalBarCode + " " + Serial + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataPrinter_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointDataProgrammer_delete(string LocalBarCode, string Ku)
        {
            try
            {
                db.CheckPointDataProgrammers.Where(e => e.LocalBarCode == LocalBarCode & e.Ku==int.Parse(Ku)).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataProgrammer_delete успешно " + LocalBarCode + " "+ Ku + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataProgrammer_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointDataUfk1_delete(string LocalBarCode, string Serial)
        {
            try
            {
                db.CheckPointDataUfk1s.Where(e => e.LocalBarCode == LocalBarCode & e.Serial==Serial).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataUfk1_delete успешно " + LocalBarCode +" "+ Serial + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataUfk1_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        public async Task<string> Method_CheckPointDataUfk3_delete(string LocalBarCode, string Serial)
        {
            try
            {
                db.CheckPointDataUfk3s.Where(e => e.LocalBarCode == LocalBarCode & e.Serial == Serial).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointDataUfk3_delete успешно " + LocalBarCode + " " + Serial + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointDataUfk3_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointSettings_delete(string CheckPointId, string ProductId)
        {
            try
            {
                db.CheckPointSettings.Where(e => e.CheckPointId == int.Parse(CheckPointId) & e.ProductId == int.Parse(ProductId)).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettings_delete успешно " + CheckPointId + " " + ProductId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettings_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointSettingsUfk1_delete(string ProductId, string MaxVal)
        {
            try
            {
                db.CheckPointSettingsUfk1s.Where(e => e.ProductId == int.Parse(ProductId) & e.MaxVal== int.Parse(MaxVal)).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettingsUfk1_deleteуспешно " + ProductId + " " + MaxVal + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUfk1_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointSettingsUfk3_delete(string ProductId, string MaxVal)
        {
            try
            {
                db.CheckPointSettingsUfk3s.Where(e => e.ProductId == int.Parse(ProductId) & e.MaxVal == int.Parse(MaxVal)).ExecuteDelete();
                Log_CheckPoint_Metra.Information("Method_CheckPointSettingsUfk3_delete успешно " + ProductId + " " + MaxVal + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_Metra.Error("Ошибка Method_CheckPointSettingsUfk3_delete В Контроллере метры" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion
    }
}

