using ApiItelma_Universal.API._Services;
using ApiItelma_Universal.DBContext.ProductionLines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.API._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionLinesController : Controller
    {

        public static ProductionLinesContext db = new ProductionLinesContext();
        ProductionLines_Service productionLines_Service = new ProductionLines_Service();

        #region//Методы GET
        //Получение всех данных из таблицы LineSettings
        [HttpGet("GetAllFrom_LineSettings")]//Проверено +
        public async Task<List<LineSetting>> GetAll_LineSettings() 
        { 
            db.ChangeTracker.Clear();
            return await db.LineSettings.ToListAsync();
        }
        //Получение всех данных из таблицы ProcessesAndSettings
        [HttpGet("GetAllFrom_ProcessesAndSettings")]//Проверено +
        public async Task<List<ProcessesAndSetting>> GetAll_ProcessesAndSettings()
        {
            db.ChangeTracker.Clear();
            return await db.ProcessesAndSettings.ToListAsync();
        }

        //Получение строки данных из LineSettings по столбцу LineName
        [HttpGet("GetFrom_LineSetting_ByLineName/{LineName}")]
        public async Task<LineSetting> LineSettings_GetByLineName(string LineName) => await productionLines_Service.Method_LineSettings_GetByLineName(LineName);

        //Получение строки данных из ProcessesAndSettings по столбцу ProcessName
        [HttpGet("GetFrom_ProcessesAndSetting_ByProcessName/{ProcessName}")]
        public async Task<ProcessesAndSetting> ProcessesAndSetting_GetByProcessName(string ProcessName) => await productionLines_Service.Method_ProcessesAndSetting_ByProcessName(ProcessName);

        #endregion

        #region//Метод Post

        //добавление строки данных в LineSettings
        [HttpPost("Post_LineSettings/{JSON_LineSettings}")]
        public async Task<string> Post_LineSettings(string JSON_LineSettings) => await productionLines_Service.Method_LineSettings_Post(JSON_LineSettings);

        //добавление строки данных в ProcessesAndSettings
        [HttpPost("Post_ProcessesAndSettings/{JSON_ProcessesAndSettings}")]
        public async Task<string> Post_JSON_ProcessesAndSettings(string JSON_ProcessesAndSettings) => await productionLines_Service.Method_JSON_ProcessesAndSettings_Post(JSON_ProcessesAndSettings);

        #endregion

        #region// Метод Put 

        //добавление строки данных в LineSettings
        [HttpPut("Put_LineSettings/{JSON_LineSettings}")]
        public async Task<string> Put_LineSettings(string JSON_LineSettings) => await productionLines_Service.Method_LineSettings_Put(JSON_LineSettings);

        //добавление строки данных в ProcessesAndSettings
        [HttpPut("Put_ProcessesAndSettings/{JSON_ProcessesAndSettings}")]
        public async Task<string> Put_ProcessesAndSettings(string JSON_ProcessesAndSettings) => await productionLines_Service.Put_ProcessesAndSettings(JSON_ProcessesAndSettings);

        //добавление строки данных в ProcessesAndSettings
        [HttpPut("Put_ProcessesAndSettings_WhereProcessName/{ProcessName}/{CurrentProcessRouteConverted}")]
        public async Task<string> Put_ProcessesAndSettings_WhereProcessName(string ProcessName, string CurrentProcessRouteConverted) => await productionLines_Service.Method_Put_ProcessesAndSettings_WhereProcessName(ProcessName, CurrentProcessRouteConverted);


        #endregion

        #region//Метод Delete

        //Удаление строки данных из LineSettings по LineId и LineName
        [HttpDelete("DeleteString_LineSettings/{LineId}/{LineName}")]
        public async Task<string> DeleteFrom_LineSettings(string LineId, string LineName)
  => await productionLines_Service.Method_LineSettings_delete(LineId, LineName);

        //Удаление строки данных из ProcessesAndSettings по ProcessName и ProductId
        [HttpDelete("DeleteString_ProcessesAndSettings/{ProcessName}/{ProductId}")]
        public async Task<string> DeleteFrom_ProcessesAndSettings(string ProcessName, string ProductId)
  => await productionLines_Service.Method_ProcessesAndSettings_delete(ProcessName, ProductId);

        #endregion
    }
}
