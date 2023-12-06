using ApiItelma_Universal.DBContext.ProductionLines;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.API._Services
{
    public class ProductionLines_Service
    {

         ProductionLinesContext db = new ProductionLinesContext();
        private Serilog.Core.Logger Log_ProductionLines = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_ProductionLines.txt").CreateLogger();

        #region//Get Метод
        public  async Task<LineSetting> Method_LineSettings_GetByLineName(string LineName)
        {

            try
            {
                return await db.LineSettings.FirstOrDefaultAsync(e => e.LineName ==LineName);
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_LineSettings_GetByLineName В Контроллере ProductionLines " + e.Message.ToString());
                return null;
            }

        }

        public  async Task<LineSetting> Method_LineSettings_GetByLineNumber(int LineNumber)
        {

            try
            {
                
                return await db.LineSettings.FirstOrDefaultAsync(e => e.LineId == LineNumber);

            }
            catch (Exception e)
            {

                Log_ProductionLines.Error("Ошибка Method_LineSettings_GetByLineName В Контроллере ProductionLines " + e.Message.ToString());
                return null;

            }

        }

        public  async Task<ProcessesAndSetting> Method_ProcessesAndSetting_ByProcessName(string ProcessName)
        {
            try
            {
                return await db.ProcessesAndSettings.FirstOrDefaultAsync(e => e.ProcessName == ProcessName);
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_ProcessesAndSetting_ByProcessName В Контроллере ProductionLines " + e.Message.ToString());
                return null;
            }
        }

        #endregion

        #region//Post Метод
        //Добавляем строку данных в LineSettings
        public async Task<string> Method_LineSettings_Post(string JSON_LineSettings)
        {
            try
            {
                LineSetting jsonData = JsonConvert.DeserializeObject<LineSetting>(JSON_LineSettings);
                db.LineSettings.Add(jsonData);
                db.SaveChanges();
                Log_ProductionLines.Information("Method_LineSettings_Post успешно " + jsonData.LineName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_LineSettings_Post В Контроллере LineSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProcessesAndSettings
        public async Task<string> Method_JSON_ProcessesAndSettings_Post(string JSON_ProcessesAndSettings)
        {
            try
            {
                db.ChangeTracker.Clear();
                ProcessesAndSetting jsonData = JsonConvert.DeserializeObject<ProcessesAndSetting>(JSON_ProcessesAndSettings);
                db.ProcessesAndSettings.Add(jsonData);
                db.SaveChanges();
                Log_ProductionLines.Information("Method_JSON_ProcessesAndSettings_Post успешно " + jsonData.ProcessName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_LineSettings_Post В Контроллере LineSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion

        #region//Put Метод
        //Изменение строки данных в LineSettings
        public async Task<string> Method_LineSettings_Put(string JSON_LineSettings)
        {
            try
            {
                db.ChangeTracker.Clear();
                if (JSON_LineSettings == "null") { return "OK"; }
                LineSetting? jsonData = JsonConvert.DeserializeObject<LineSetting>(JSON_LineSettings);
                LineSetting? setting = db.LineSettings.FirstOrDefault(e => e.LineId == jsonData.LineId);
                setting.LineProductId = jsonData.LineProductId;
                setting.LineName = jsonData.LineName;
                setting.LineCheckPointsJson = jsonData.LineCheckPointsJson;
                setting.LineProcess = jsonData.LineProcess;
                db.SaveChanges();
                db.ChangeTracker.Clear();
                Log_ProductionLines.Information("Method_LineSettings_Put успешно " + jsonData.LineName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_LineSettings_Put В Контроллере LineSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Изменение строки данных в ProcessesAndSettings
        public async Task<string> Put_ProcessesAndSettings(string JSON_ProcessesAndSettings)
        {
            try
            {
                db.ChangeTracker.Clear();
                ProcessesAndSetting? jsonData = JsonConvert.DeserializeObject<ProcessesAndSetting>(JSON_ProcessesAndSettings);
                ProcessesAndSetting? setting = db.ProcessesAndSettings.FirstOrDefault(e => e.ProcessName == jsonData.ProcessName);
                setting.ProductId = jsonData.ProductId;
                setting.ProductRoute = jsonData.ProductRoute;
                setting.IsThereKtzero = jsonData.IsThereKtzero;
                db.SaveChanges();
                db.ChangeTracker.Clear();
                Log_ProductionLines.Information("Method_ProcessesAndSettings_Put успешно " + jsonData.ProcessName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_ProcessesAndSettings_Put В Контроллере ProcessesAndSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Изменение строки данных в ProcessesAndSettings
        //прищлось так сделать так как processroute это уже серилизованная строка, если ее серилизовать еще раз и сделать нормальный метод put, то она не отправится по http.
        public async Task<string> Method_Put_ProcessesAndSettings_WhereProcessName(string ProcessName,string ProcessRoute)
        {
            try
            {
                db.ChangeTracker.Clear();
                db.ProcessesAndSettings.FirstOrDefault(e => e.ProcessName == ProcessName).ProductRoute = ProcessRoute;
                db.SaveChanges();
                db.ChangeTracker.Clear();
                Log_ProductionLines.Information("Method_Put_ProcessesAndSettings_WhereProcessName успешно " + ProcessName);
                return "OK";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_Put_ProcessesAndSettings_WhereProcessName В Контроллере ProcessesAndSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion

        #region//Delete метод

        //Удаление строки данных из LineSettings по LineId и LineName
        public async Task<string> Method_LineSettings_delete(string LineId, string LineName)
        {
            try
            {
                db.LineSettings.Where(e => e.LineId == int.Parse(LineId) & e.LineName == LineName).ExecuteDelete();
                Log_ProductionLines.Information("Method_LineSettings_delete успешно " + LineId + " " + LineName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_LineSettings_delete В Таблице LineSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProcessesAndSettings по ProcessName и ProductId
        public async Task<string> Method_ProcessesAndSettings_delete(string ProcessName, string ProductId)
        {
            try
            {
                db.ChangeTracker.Clear();
                db.ProcessesAndSettings.Where(e => e.ProcessName == ProcessName & e.ProductId == ProductId).ExecuteDelete();
                db.SaveChanges();
                db.ChangeTracker.Clear();
                Log_ProductionLines.Information("Method_ProcessesAndSettings_delete успешно " + ProcessName + " " + ProductId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_ProductionLines.Error("Ошибка Method_ProcessesAndSettings_delete В Таблице ProcessesAndSettings" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion
    }
}
