using ApiItelma_Universal.API.Models;
using ApiItelma_Universal.DBContext.CheckPoint_Packaging;
using ApiItelma_Universal.SeriLog;
using ApiItelma_Universal.Services.Additional_Classes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class CheckPoint_Packaging_Service
    {
        static CheckPointPackagingContext db = new CheckPointPackagingContext();
        private Serilog.Core.Logger Log_CheckPoint_Packaging = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_CheckPoint_Packaging.txt").CreateLogger();

        #region //Get методы
        #region//CheckPointData
        // получаем строку данных по Serial
        public async Task<List<CheckPointDatum>> Method_CheckPointData_GetBySerial(string serial)
        {
            try
            {
                return await db.CheckPointData.Where(e => e.SerialNumber == serial).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(serial + " Не найден" + ex.Message.ToString());
                return null;
            }
        }

        // получаем строку данных по idAndBarCod
        public async Task<List<CheckPointDatum>> Method_CheckPointData_GetByidAndBarCode(string IdAndBarCode)
        {
            try
            {
                return await db.CheckPointData.Where(e => e.IdAndBarCode == IdAndBarCode).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(IdAndBarCode + " Не найден" + ex.Message.ToString());
                return null;
            }
        }

        // получаем строку данных по BarCode
        public async Task<List<CheckPointDatum>> Method_CheckPointData_GetByBarCode(string BarCode)
        {
            try
            {
                return await db.CheckPointData.Where(e => e.BarCode == BarCode).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(BarCode + " Не найден" + ex.Message.ToString());
                return null;
            }
        }

        //метод для Упаковки
        public async Task<List<CheckPointDatum>> Method_CheckPointData_GetByProductKey(string ProductKey)
        {
            try
            {
                var packDataBase = await db.CheckPointData.Where(s => s.ProductKey == ProductKey).ToListAsync();
                return packDataBase;
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(ProductKey + " Не найден" + ex.Message.ToString());
                return null;
            }
        }

        //метод для Упаковки
        public async Task<List<CheckPointDataPackage>> Method_CheckPointDataPackage_GetByBarCode(string barCode)
        {
            try
            {
                var boxPackageSerial = db.CheckPointDataPackages.Where(e => e.BarCode == barCode).Select(e => e.PackageSerial).FirstOrDefault();
                if (String.IsNullOrEmpty(boxPackageSerial))
                {
                    Log_CheckPoint_Packaging.Information(barCode + " Не найден");
                    return null;
                }
                var productsInPackage = await db.CheckPointDataPackages.Where(e => e.PackageSerial == boxPackageSerial).ToListAsync();
                return productsInPackage;
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(barCode + " Не найден" + ex.Message.ToString());
                return null;
            }
        }

        #endregion

        #region //CheckPointDataPackages
        public async Task<List<CheckPointDataPackage>> Method_CheckPointDataPackages_GetByBarcode(string Barcode)
        {
            try
            {
                return await db.CheckPointDataPackages.Where(e => e.BarCode == Barcode).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(Barcode + " Не найден" + ex.Message.ToString());
                return null;
            }
        }// получаем строку данных по Barcode

        public async Task<List<CheckPointDataPackage>> Method_CheckPointDataPackages_GetBySerial(string Serial)
        {
            try
            {
                return await db.CheckPointDataPackages.Where(e => e.PackageSerial == Serial).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(Serial + " Не найден" + ex.Message.ToString());
                return null;
            }
        }// получаем строку данных по serial
        #endregion

        #region // CheckPointDataSerials
        public async Task<List<CheckPointDataSerial>> Method_CheckPointDataSerials_GetBySerial(string serial)
        {
            try
            {
                return await db.CheckPointDataSerials.Where(e => e.Serial == serial).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(serial + " Не найден"+ ex.Message.ToString());
                return null;
            }
        }// получаем строку данных по Barcode

        public async Task<List<CheckPointDataSerial>> Method_CheckPointDataSerials_GetByProductId(string ProductId)
        {
            try
            {
                return await db.CheckPointDataSerials.Where(e => e.ProductId == int.Parse(ProductId)).ToListAsync();
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information(ProductId + " Не найден"+ ex.Message.ToString());
                return null;
            }
        }// получаем строку данных по Barcode
        #endregion

        #region// методы для Сайта.
        public async Task<string> Method_CheckPointDataSerials_IncludeCheckPointDataPackages()
        {
            try
            {
                List <CheckPointDataSerial> ch = await db.CheckPointDataSerials.Include(e => e.CheckPointDataPackages).ToListAsync();
                string json = JsonConvert.SerializeObject(ch,Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                return json;
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information("Ошибка Method_CheckPointDataSerials_IncludeCheckPointDataPackages ");
                return null;
            }
        }
        //метод для связи таблиц CheckPointDataPackages и CheckPointData
        public async Task<string> Method_CheckPointDataPackages_CheckPointData()
        {
            try
            {
                List<CheckPointDataPackage> ch = await db.CheckPointDataPackages.Include(e => e.CheckPointData).ToListAsync();
                string json = JsonConvert.SerializeObject(ch, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                return json;
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information("Ошибка Method_CheckPointDataPackages_IncludeCheckPointData ");
                return null;
            }
        }
        #endregion
        #endregion

        #region //Put методы 
        //Возвращает новый Серийник .(Используется на упаковке)
        public async Task<string> PutCheckPoint_DataSerials(string Json_productsPackageModel)
        {
            try
            {
                ProductsPackageModel productsPackageModel = JsonConvert.DeserializeObject<ProductsPackageModel>(Json_productsPackageModel);
                SerialGeneratorClass boxSerial = new SerialGeneratorClass(productsPackageModel);
                return boxSerial.Result;
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Information("Ошибка PutCheckPoint_DataSerials " + ex.Message.ToString());
                return ex.ToString();
            }
        }

        #endregion

        #region //Post методы

        //таблица CheckPointDataPackages
        public async Task<string> Method_CheckPointDataPackages_Post(string JSon_CheckpointDataPackage)
        {
            try
            {
                CheckPointDataPackage jsonPrinter = JsonConvert.DeserializeObject<CheckPointDataPackage>(JSon_CheckpointDataPackage);
                db.CheckPointDataPackages.Add(jsonPrinter);
                db.SaveChanges();
                Log_CheckPoint_Packaging.Information("Method_CheckPointDataPackages_Post успешно "+jsonPrinter.BarCode.ToString());
                return "OK";
            }
            catch(Exception ex) 
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointDataPackages_Post " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        //таблица CheckPointDataSerial
        public async Task<string> Method_CheckPointDataSerial_Post(string JSon_CheckPointDataSerial)
        {
            try
            { 
                CheckPointDataSerial jsonPrinter = JsonConvert.DeserializeObject<CheckPointDataSerial>(JSon_CheckPointDataSerial);
                db.CheckPointDataSerials.Add(jsonPrinter);
                db.SaveChanges();
                Log_CheckPoint_Packaging.Information("Method_CheckPointDataSerial_Post успешно "+jsonPrinter.Serial.ToString());
                return "OK";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointDataSerial_Post "+ ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
        //таблица CheckPointData
        public async Task<string> Method_CheckPointData_Post(string JSon_CheckPointData)
        {
            try
            {
                CheckPointDatum jsonPrinter = JsonConvert.DeserializeObject<CheckPointDatum>(JSon_CheckPointData);
                db.CheckPointData.Add(jsonPrinter);
                db.SaveChanges();
                Log_CheckPoint_Packaging.Information("Method_CheckPointData_Post успешно " + jsonPrinter.IdAndBarCode?.ToString());
                return "OK";
            }
            catch(Exception ex) 
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointData_Post " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
        //таблица CheckPointSettings
        public async Task<string> Method_CheckPointSettings_Post(string JSon_CheckPointSettings)
        {
            try
            {
                CheckPoint_Setting jsonPrinter = JsonConvert.DeserializeObject<CheckPoint_Setting>(JSon_CheckPointSettings);
                db.CheckPointSettings.Add(jsonPrinter);
                db.SaveChanges();
                Log_CheckPoint_Packaging.Information("Method_CheckPointData_Post успешно " + jsonPrinter.ProductId.ToString());
                return "OK";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointData_Post " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
        #endregion

        #region//Delete методы
        public async Task<string> Method_CheckPointData_delete(string IdAndBarCode, string SerialNumber)
        {
            try
            {
                db.CheckPointData.Where(e => e.IdAndBarCode == IdAndBarCode & e.SerialNumber == SerialNumber).ExecuteDelete();
                Log_CheckPoint_Packaging.Information("Method_CheckPointData_delete успешно " + IdAndBarCode + " Удалено");
                return "Deleted Successfully";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointData_delete " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
        public async Task<string> Method_CheckPointDataPackages_delete(string Barcode, string PackageSerial)
        {
            try
            {
                db.CheckPointDataPackages.Where(e => e.BarCode == Barcode & e.PackageSerial== PackageSerial).ExecuteDelete();
                Log_CheckPoint_Packaging.Information("Method_CheckPointDataPackages_delete успешно " + Barcode + " Удалено");
                return "Deleted Successfully";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointDataPackages_delete " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        public async Task<string> Method_CheckPointDataSerials_delete(string serial,string ID)
        {
            try
            {
                db.CheckPointDataSerials.Where(e => e.Serial == serial & e.Id == int.Parse(ID)).ExecuteDelete();
                Log_CheckPoint_Packaging.Information("Method_CheckPointDataSerials_delete успешно" + serial + " Удалено");
                return "Deleted Successfully";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointDataSerials_delete " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        

        public async Task<string> Method_CheckPointSettings_delete(string CheckPointId, string CheckPointName)
        {
            try
            {
                db.CheckPointSettings.Where(e => e.CheckPointId == int.Parse(CheckPointId) & e.CheckPointName== CheckPointName).ExecuteDelete();
                Log_CheckPoint_Packaging.Information("Method_CheckPointSettings_delete успешно " + CheckPointId + " Удалено");
                return "Deleted Successfully";
            }
            catch(Exception ex)
            {
                Log_CheckPoint_Packaging.Error("Ошибка Method_CheckPointSettings_delete " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        #endregion
    }
}
