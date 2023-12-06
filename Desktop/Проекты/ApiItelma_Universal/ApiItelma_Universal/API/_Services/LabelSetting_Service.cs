using ApiItelma_Universal.DBContext.LabelSetting;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.API._Services
{
    public class LabelSetting_Service
    {
        static LabelSettingContext db = new LabelSettingContext();
        private Serilog.Core.Logger Log_LabelSetting = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_LabelSetting.txt").CreateLogger();

        #region//Get Методы
        // получаем строку данных по LabelName
        public async Task<PackageSetting> Method_PackageSetting_GetByLabelName(string LabelName)
        {
            try
            {
                return await db.PackageSettings.FirstOrDefaultAsync(e => e.LabelName == LabelName);
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка Method_PackageSetting_GetByBarCode В Контроллере LabelSetting " + e.Message.ToString());
                return null;
            }
        }

        public async Task<PassportSetting> Method_PassportSetting_GetByLabelName(string LabelName)
        {
            try
            {
                return await db.PassportSettings.FirstOrDefaultAsync(e => e.LabelName == LabelName);
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка Method_PassportSetting_GetByLabelName В Контроллере LabelSetting " + e.Message.ToString());
                return null;
            }
        }

        #endregion

        #region//Put методы
        public string Put_PassportSetting(string JsonData)
        {
            try
            {
                PassportSetting jsonPrinter = JsonConvert.DeserializeObject<PassportSetting>(JsonData);
                PassportSetting packageSerial = db.PassportSettings.Where(e => e.LabelName == jsonPrinter.LabelName).FirstOrDefault();

                packageSerial.LabelName = jsonPrinter.LabelName;
                packageSerial.LabelProduct = jsonPrinter.LabelProduct;
                packageSerial.LabelProductId = jsonPrinter.LabelProductId;
                packageSerial.LabelImage = jsonPrinter.LabelImage;
                packageSerial.ProductName = jsonPrinter.ProductName;
                db.PassportSettings.Update(packageSerial);
                db.SaveChanges();
                Log_LabelSetting.Information("Put_PassportSetting Успешно");
                return "OK";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Information("Ошибка Put_PassportSetting "+ e.Message.ToString());
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что
        public string Put_PackageSettings(string JsonData)
        {
            try
            {
                PackageSetting jsonPrinter = JsonConvert.DeserializeObject<PackageSetting>(JsonData);
                PackageSetting packageSerial = db.PackageSettings.Where(e => e.LabelName == jsonPrinter.LabelName).FirstOrDefault();

                packageSerial.LabelName = jsonPrinter.LabelName;
                packageSerial.VersionSettings = jsonPrinter.VersionSettings;
                packageSerial.ImageLabel = jsonPrinter.ImageLabel;
                packageSerial.Number1C = jsonPrinter.Number1C;
                packageSerial.ProductName = jsonPrinter.ProductName;
                packageSerial.CountPack = jsonPrinter.CountPack;
                packageSerial.PackSheet = jsonPrinter.PackSheet;
                packageSerial.Number = jsonPrinter.Number;
                packageSerial.Param1 = jsonPrinter.Param1;
                packageSerial.Param2 = jsonPrinter.Param2;
                packageSerial.Param3 = jsonPrinter.Param3;
                packageSerial.Param4 = jsonPrinter.Param4;
                packageSerial.Param5 = jsonPrinter.Param5;
                packageSerial.Param6 = jsonPrinter.Param6;
                packageSerial.Param7 = jsonPrinter.Param7;
                packageSerial.Param8 = jsonPrinter.Param8;
                packageSerial.Param9 = jsonPrinter.Param9;
                packageSerial.Param10 = jsonPrinter.Param10 ;
                packageSerial.Param11 = jsonPrinter.Param11 ;
                packageSerial.Param12 = jsonPrinter.Param12 ;
                packageSerial.Param13 = jsonPrinter.Param13 ;
                packageSerial.Param14 = jsonPrinter.Param14 ;
                packageSerial.LabelProductId = jsonPrinter.LabelProductId;
                db.PackageSettings.Update(packageSerial);
                db.SaveChanges();
                Log_LabelSetting.Information("метод Put_PackageSettings");
                return "OK";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Information("Ошибка Put_PackageSettings"+ e.Message.ToString());
                return e.Message.ToString();
            }
        }// меняем PoductId по Barcode Метод для галочки пока что
        #endregion

        #region//Post Методы
        //Добавляем строку данных в CheckPointData
        public string Post_PackageSetting(string JSON_for_CheckPointData)
        {
            try
            {
                PackageSetting jsonData = JsonConvert.DeserializeObject<PackageSetting>(JSON_for_CheckPointData);
                db.PackageSettings.Add(jsonData);
                db.SaveChanges();
                Log_LabelSetting.Information("Post_PackageSetting успешно " + jsonData.LabelName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка Post_PackageSetting В Контроллере LabelSetting" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public string Post_PassportSetting(string JSON_for_CheckPointData)
        {
            try
            {
                PassportSetting jsonData = JsonConvert.DeserializeObject<PassportSetting>(JSON_for_CheckPointData);
                db.PassportSettings.Add(jsonData);
                db.SaveChanges();
                Log_LabelSetting.Information("Post_PassportSettingуспешно " + jsonData.LabelName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка Post_PassportSetting В Контроллере LabelSetting" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion

        #region//Delete методы
        public async Task<string> PassportSetting_delete(string LabelName, string ProductName)
        {
            try
            {
                db.PassportSettings.Where(e => e.LabelName == LabelName & e.ProductName == ProductName).ExecuteDelete();
                Log_LabelSetting.Information("PassportSetting_delete успешно " + ProductName + " " + LabelName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка PassportSetting_delete В Контроллере LabelSetting" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> PackageSetting_delete(string LabelName, string ProductName)
        {
            try
            {
                db.PackageSettings.Where(e => e.LabelName == LabelName & e.ProductName == ProductName).ExecuteDelete();
                Log_LabelSetting.Information("PackageSetting_delete успешно " + ProductName + " " + LabelName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_LabelSetting.Error("Ошибка PackageSetting_delete В Контроллере LabelSetting" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion
    }
}
