using ApiItelma_Universal.DataContext.Traceability;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.API._Services
{
    public class Traceability_Service
    {

        static TraceabilityContext db = new TraceabilityContext();
        private Serilog.Core.Logger Log_Traceability = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_Traceability.txt").CreateLogger();

        #region//Get Метод
        // Получение строки данных из Global по столбцу BarCode
        public async Task<Global> Method_Global_GetByBarCode(string BarCode)
        {
            try
            {
                return await db.Globals.FirstOrDefaultAsync(e => e.BarCode == BarCode);
            }
            catch (Exception e)
            {
                Log_Traceability.Error("Ошибка Method_Global_GetByBarCode В Контроллере Traceability " + e.Message.ToString());
                return null;
            }
        }
        #endregion

        #region//Post Метод
        //добавление строки данных в Global
        public async Task<string> Method_Global_Post(string JSON_Global)
        {
            try
            {
                Global jsonData = JsonConvert.DeserializeObject<Global>(JSON_Global);
                db.Globals.Add(jsonData);
                db.SaveChanges();
                Log_Traceability.Information("Method_Global_Post успешно " + jsonData.BarCode.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Traceability.Error("Ошибка Method_Global_Post В Контроллере Traceability" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion


        #region//Delete метод
        //Удаление строки данных из Global по Barcode и ProductId
        public async Task<string> Method_Global_delete(string Barcode, string ProductId)
        {
            try
            {
                db.Globals.Where(e => e.BarCode == Barcode & e.ProductId == int.Parse(ProductId)).ExecuteDelete();
                Log_Traceability.Information("Method_Global_delete успешно " + Barcode + " " + ProductId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Traceability.Error("Ошибка Method_Global_delete В Контроллере Traceability" + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion
    }
}
