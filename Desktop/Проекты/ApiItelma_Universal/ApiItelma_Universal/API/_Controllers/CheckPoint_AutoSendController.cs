using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.DBContext.CheckPoint_AutoSend;
using Microsoft.EntityFrameworkCore;
using ApiItelma_Universal.Services;

namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckPoint_AutoSendController : Controller
    {
        CheckPointAutoSendContext db =  new CheckPointAutoSendContext();
        CheckPoint_AutoSend_Service checkPoint_AutoSend_Service = new CheckPoint_AutoSend_Service();

        #region//Методы GET
        //Получение всех данных из таблицы CheckPointSetting
        [HttpGet("GetAllFrom_CheckPointSetting")]//Проверено+
        public async Task<List<CheckPointSetting_AutoSend>> GetAll_CheckPointSetting() => await db.CheckPointSettings.ToListAsync();


        //Получение строки данных из CheckPointSetting по столбцу ProgramId
       [HttpGet("GetFrom_CheckPointSetting_ByProgramId/{ProgramId}")]//Проверено+
        public async Task<List<CheckPointSetting_AutoSend>> CheckPointSetting_GetByProgramId(string ProgramId) => await checkPoint_AutoSend_Service.Method_CheckPointSetting_GetByProgramId(ProgramId);

        #endregion

        #region//Метод Post

        //добавление строки данных в CheckPointSetting
        [HttpPost("Post_CheckPointSetting/{JSON_CheckPointSetting}")]//Проверено+
        public async Task<string> Post_CheckPointSetting2(string JSON_CheckPointSetting) => await checkPoint_AutoSend_Service.Method_CheckPointSetting_Post(JSON_CheckPointSetting);

        #endregion

        #region//Метод Delete

        //Удаление строки данных из CheckPointSetting по ProgramId и NumberSend
              [HttpDelete("DeleteString_CheckPointsList/{ProgramId}/{NumberSend}")]//Проверено+
              public async Task<string> DeleteFrom_CheckPointSetting2(string ProgramId, string NumberSend)
        => await checkPoint_AutoSend_Service.Method_CheckPointSetting_delete(ProgramId, NumberSend);

        #endregion

    }

}
