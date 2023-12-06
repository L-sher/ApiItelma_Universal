using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.DBContext.CheckPoints;
using ApiItelma_Universal.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckPointsController : Controller
    {

        CheckPointsContext db= new CheckPointsContext();
        CheckPoints_Service checkPointsList= new CheckPoints_Service();

        #region//Методы GET
        // Получение всех данных из CheckPointsList
        [HttpGet("GetAllFrom_CheckPointsList")]//Проверено+
        public async Task<List<CheckPointsList>> CheckPointsList_GetByBarCode2() => await db.CheckPointsLists.ToListAsync();

        // Получение всех данных из PLCList
        [HttpGet("GetAllFrom_PLCList")]//Проверено+
        public async Task<List<Plclist>> PLCList_GetAll() => await db.Plclists.ToListAsync();

        // Получение всех данных из CameraList
        [HttpGet("GetAllFrom_CameraList")]//Проверено+
        public async Task<List<CameraList>> CameraList_GetAll() => await db.CameraLists.ToListAsync();


        // Получение строки данных из CheckPointsList по столбцу CheckPointName
        [HttpGet("GetFrom_CheckPointsList_ByCheckPointName/{CheckPointName}")]//Проверено+
        public async Task<List<CheckPointsList>> CheckPointsList_GetByCheckPointName(string CheckPointName) => await  checkPointsList.Method_CheckPointsList_GetByCheckPointName(CheckPointName);

        // Получение строки данных из CheckPointsList по столбцу CheckPointName
        [HttpGet("GetFrom_CheckPointsList_ByCheckPointLine/{CheckPointLine}")]//Проверено+
        public async Task<List<CheckPointsList>> CheckPointsList_GetByCheckPointLine(string CheckPointLine) => await checkPointsList.Method_CheckPointsList_GetByCheckPointLine(CheckPointLine);

        #endregion
        #region//Метод Put
        //добавление строки данных в CheckPointsList
        [HttpPut("Put_CheckPointsList/{JSON_CheckPointsList}")]//Проверено+
        public async Task<string> Put_CheckPointsList(string JSON_CheckPointsList) => await checkPointsList.Put_CheckPointsList(JSON_CheckPointsList);
        [HttpPut("Put_PlcList/{JSON_PlcList}")]//Проверено+
        public async Task<string> Put_PlcList(string JSON_PlcList) => await checkPointsList.Put_PLCList(JSON_PlcList);

        [HttpPut("Put_CameraList/{JSON_CameraList}")]//Проверено+
        public async Task<string> Put_CameraList(string JSON_CameraList) => await checkPointsList.Put_CameraList(JSON_CameraList);

        #endregion
        #region//Метод Post

        //добавление строки данных в CheckPointsList
        [HttpPost("Post_CheckPointsList/{JSON_CheckPointsList}")]//Проверено+
        public async Task<string> Post_CheckPointsList(string JSON_CheckPointsList) => await checkPointsList.Method_CheckPointsList_Post(JSON_CheckPointsList);

        //добавление строки данных в PLCList
        [HttpPost("Post_PLCList/{JSON_PLCList}")]//Проверено+
        public async Task<string> Post_PLCList(string JSON_PLCList) => await checkPointsList.Method_PLCList_Post(JSON_PLCList);

        //добавление строки данных в CameraList
        [HttpPost("Post_CameraList/{JSON_CameraList}")]//Проверено+
        public async Task<string> Post_CameraList(string JSON_CameraList) => await checkPointsList.Method_CameraList_Post(JSON_CameraList);

        #endregion

        #region//Метод Delete

        //Удаление строки данных из CheckPointsList по CheckPointName и CheckPointId
        [HttpDelete("DeleteString_CheckPointsList/{CheckPointName}/{CheckPointId}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointsList(string CheckPointName, string CheckPointId)
  => await checkPointsList.Method_CheckPointsList_delete(CheckPointName, CheckPointId);

        //Удаление строки данных из PLCList по Plcipaddress и Plcname
        [HttpDelete("DeleteString_PLCList/{Plcipaddress}/{Plcname}")]//Проверено+
        public async Task<string> DeleteFrom_PLCList(string Plcipaddress, string Plcname)
  => await checkPointsList.Method_PLCList_delete(Plcipaddress, Plcname);

        //Удаление строки данных из CameraList по CameraIpaddress и CameraName
        [HttpDelete("DeleteFrom_CameraList/{CameraIpaddress}/{CameraName}")]//Проверено+
        public async Task<string> DeleteFrom_CameraList(string CameraIpaddress, string CameraName)
  => await checkPointsList.Method_CameraList_delete(CameraIpaddress, CameraName);

        #endregion

    }
}
