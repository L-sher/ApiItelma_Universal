using ApiItelma_Universal.DBContext.CheckPoint_AutoSend;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class CheckPoint_AutoSend_Service
    {
        static CheckPointAutoSendContext db = new CheckPointAutoSendContext();
        private Serilog.Core.Logger Log_CheckPoint_AutoSend = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_CheckPoint_AutoSend.txt").CreateLogger();

        #region//Get Метод
        public async Task<List<CheckPointSetting_AutoSend>> Method_CheckPointSetting_GetByProgramId(string ProgramId)
        {
            try
            {
                return await db.CheckPointSettings.Where(e => e.ProgramId == int.Parse(ProgramId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_CheckPoint_AutoSend.Error("Ошибка метода Method_CheckPointsList_GetByCheckPointName В Контроллере CheckPoint_AutoSend " + e.Message.ToString());
                return null;
            }
        }
        #endregion

        #region//Post Метод
        //Добавляем строку данных в CheckPointSettings
        public async Task<string> Method_CheckPointSetting_Post(string JSON_CheckPointSetting)
        {
            try
            {
                CheckPointSetting_AutoSend jsonData = JsonConvert.DeserializeObject<CheckPointSetting_AutoSend>(JSON_CheckPointSetting);
                db.CheckPointSettings.Add(jsonData);
                db.SaveChanges();
                Log_CheckPoint_AutoSend.Information("Method_CheckPointSetting_Postуспешно " + jsonData.ProgramId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_CheckPoint_AutoSend.Error("Ошибка метода Method_CheckPointSetting_Post В Контроллере CheckPoint_AutoSend" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion


        #region//Delete метод
        public async Task<string> Method_CheckPointSetting_delete(string ProgramId, string NumberSend)
        {
            try
            {
                db.CheckPointSettings.Where(e => e.ProgramId == int.Parse(ProgramId) & e.NumberSend == int.Parse(NumberSend)).ExecuteDelete();
                Log_CheckPoint_AutoSend.Information("Method_CheckPointSetting_deleteуспешно " + ProgramId + " " + NumberSend + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_CheckPoint_AutoSend.Error("Ошибка метода Method_CheckPointSetting_delete В Контроллере CheckPoint_AutoSend" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion
    }
}
