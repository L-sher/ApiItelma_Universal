using ApiItelma_Universal.DBContext.CheckPoint_Metra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiItelma_Universal.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckPoint_MetraController : Controller
    {

        CheckPointMetraContext db = new CheckPointMetraContext();

        CheckPoint_Metra_Service metraService = new CheckPoint_Metra_Service();

        #region //Методы Get получения данных из CheckPoint_Metra

        #region//Методы GetAll по каждой таблице
        //Получение полностью всех данных из таблиц
        [HttpGet("GetAllFrom_CheckPointData")]//Проверено+
        public async Task<List<CheckPointData>> GetAll_CheckPointData() => await db.CheckPointData.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointDataPrinter")]//Проверено+
        public async Task<List<CheckPointDataPrinter>> GetAll_CheckPointDataPrinter() => await db.CheckPointDataPrinters.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointDataProgrammer")]//Проверено+
        public async Task<List<CheckPointDataProgrammer>> GetAll_CheckPointDataProgrammer() => await db.CheckPointDataProgrammers.ToListAsync();
        
        [HttpGet("GetAllFrom_CheckPointDataUfk1")]//Проверено+
        public async Task<List<CheckPointDataUfk1>> GetAll_CheckPointDataUfk1() => await db.CheckPointDataUfk1s.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointDataUfk3")]//Проверено+
        public async Task<List<CheckPointDataUfk3>> GetAll_CheckPointDataUfk3() => await db.CheckPointDataUfk3s.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointSettings")]//Проверено+
        public async Task<List<CheckPointSetting>> GetAll_CheckPointSettings() => await db.CheckPointSettings.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointSettingsUfk1")]//Проверено+
        public async Task<List<CheckPointSettingsUfk1>> GetAll_CheckPointSettingsUfk1() => await db.CheckPointSettingsUfk1s.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointSettingsUfk3")]//Проверено+
        public async Task<List<CheckPointSettingsUfk3>> GetAll_CheckPointSettingsUfk3() => await db.CheckPointSettingsUfk3s.ToListAsync();
        #endregion

        //Далее Выборка по конкретному атрибуту
        [HttpGet("GetFrom_CheckPointData_ByBarcode/{barcode}")]//Проверено+ (CheckPointData_GetByBarCode2) 2 на конце так как смежается с другим методом CheckpointData из Packaging
        public async Task<List<CheckPointData>> CheckPointData_GetByBarCode2(string BarCode) => await metraService.Method_CheckPointData_GetByBarCode(BarCode);

        [HttpGet("GetFrom_CheckPointDataPrinter_ById/{id}")]//Проверено+
        public async Task<List<CheckPointDataPrinter>> CheckPointDataPrinter_GetById(string Id) => await metraService.Method_CheckPointDataPrinters_GetById(Id);

        [HttpGet("GetFrom_CheckPointDataProgrammer_ById/{id}")]//Проверено+
        public async Task<List<CheckPointDataProgrammer>> CheckPointDataProgrammer_GetById(string Id) => await metraService.Method_CheckPointDataProgrammers_SelectinId(Id);

        [HttpGet("GetFrom_CheckPointDataUfk1_ById/{id}")]//Проверено+
        public async Task<List<CheckPointDataUfk1>> CheckPointDataUfk1_GetById(string Id) => await metraService.Method_CheckPointDataUfk1s_GetById(Id);

        [HttpGet("GetFrom_CheckPointDataUfk3_ById/{id}")]//Проверено+
        public async Task<List<CheckPointDataUfk3>> CheckPointDataUfk3_GetById(string Id) => await metraService.Method_CheckPointDataUfk3_GetById(Id);

        [HttpGet("GetFrom_CheckPointSetting_ByProductId/{ProductId}")]//Проверено+
        public async Task<List<CheckPointSetting>> CheckPointSetting_GetById(string ProductId) => await metraService.Method_CheckPointSetting_GetById(ProductId);

        [HttpGet("GetFrom_CheckPointSettingsUfk1_ByProductId/{ProductId}")]//Проверено+
        public async Task<List<CheckPointSettingsUfk1>> CheckPointSettingsUfk1_GetById(string ProductId) => await metraService.Method_CheckPointSettingsUfk1_GetById(ProductId);

        [HttpGet("GetFrom_CheckPointSettingsUfk3_ByProductId/{ProductId}")]//Проверено+
        public async Task<List<CheckPointSettingsUfk3>> CheckPointSettingsUfk3_GetById(string ProductId) => await metraService.Method_CheckPointSettingsUfk3_GetById(ProductId);

        //Обработка запроса с Упаковки Присоединяем все остальные таблицы.
        [HttpGet("Get_CheckPointData_ByBarcode_IncludeAllTables/{barCode}")]//Проверено+
        public async Task<List<CheckPointData>> Get_CheckPointData_ByBarcode_IncludeAllTables(string barCode) => await metraService.Method_Get_CheckPointData_ByBarcode_IncludeAllTables(barCode);

        [HttpGet("GetVIN_CheckPointDataUfk3_ByBarcode/{barCode}")]//Проверено+ но бесполлезен метод. Хз Зачем
        public async Task<string> Get_CheckPointDataUfk3_ByBarcode_IncludeAllTables(string barCode) => await metraService.Method_GetVIN_CheckPointDataUfk3_ByBarcode(barCode);

        #region//метод ATTENTION. не применяется на упаковке.не понятно где применяется.
        //Запрос ХЗ откуда, Отправляем список Баркодов
        [HttpGet("Get_CheckPointDataUfk3_ByDateAndProductId_IncludeAllTables/{DateStart};{DateEnd};{productId}")]//!!!Не известно где используется
        public async Task<List<CheckPointDataUfk3>> Get_CheckPointDataUfk3_ByDateAndProductId(DateTime DateStart, DateTime DateEnd, int productId) => await metraService.Method_Get_CheckPointDataUfk3_ByDateAndProductId_IncludeCheckPointData(DateStart, DateEnd, productId);
        #endregion


        #endregion

        #region//Методы Put Изменения данных на CheckPoint_Metra

        [HttpPost("Put_CheckPointData/{Json_Barcode_ProductId}")]
        public string CheckPointData_Change(string Json_Barcode_ProductId) =>  metraService.Method_CheckPointData_Change(Json_Barcode_ProductId);


        #endregion

        #region //Методы Post добавления данных в CheckPoint_Metra
        //Добавление записи в таблицу CheckPointData
        [HttpPost("Post_CheckPointData/{JSON_for_CheckPointData}")]//Проверено+ (время TimeCheck должно быть указано)
        public async Task<string> Post_CheckPointData(string JSON_for_CheckPointData) => await metraService.Method_CheckPointData_Post(JSON_for_CheckPointData);

        //Добавление записи в таблицу CheckPointDataPrinter
        [HttpPost("Post_CheckPointDataPrinter/{JSON_for_CheckPointDataPrinter}")]//Проверено+ ( Дата Time обязательно)
        public async Task<string> Post_CheckPointDataPrinter(string JSON_for_CheckPointDataPrinter) => await metraService.Method_CheckPointDataPrinter_Post(JSON_for_CheckPointDataPrinter);

        //Добавление записи в таблицу CheckPointDataProgrammer
        [HttpPost("Post_CheckPointDataProgrammer/{JSON_for_CheckPointDataProgrammer}")]//Проверено+ ( TimeStart и TimeStop обязательно)
        public async Task<string> Post_CheckPointDataProgrammer(string JSON_for_CheckPointDataProgrammer) => await metraService.Method_CheckPointDataProgrammer_Post(JSON_for_CheckPointDataProgrammer);

        //Добавление записи в таблицу CheckPointDataUFK1
        [HttpPost("Post_CheckPointDataUFK1/{JSON_for_CheckPointDataUFK1}")]//Проверено+
        public async Task<string> Post_CheckPointDataUFK1(string JSON_for_CheckPointDataUFK1) => await metraService.Method_CheckPointDataUFK1_Post(JSON_for_CheckPointDataUFK1);

        //Добавление записи в таблицу CheckPointDataUFK3
        [HttpPost("Post_CheckPointDataUFK3/{JSON_for_CheckPointDataUFK3}")]//Проверено+
        public async Task<string> Post_CheckPointDataUFK3(string JSON_for_CheckPointDataUFK3) => await metraService.Method_CheckPointDataUFK3_Post(JSON_for_CheckPointDataUFK3);

        //Добавление записи в таблицу CheckPointSettings
        [HttpPost("Post_CheckPointSettings/{JSON_for_CheckPointSettings}")]//Проверено+
        public async Task<string> Post_CheckPointSettings(string JSON_for_CheckPointSettings) => await metraService.Method_CheckPointSettings_Post(JSON_for_CheckPointSettings);

        //Добавление записи в таблицу CheckPointSettingsUFK1
        [HttpPost("Post_CheckPointSettingsUFK1/{JSON_for_CheckPointSettingsUFK1}")]//Проверено+
        public async Task<string> Post_CheckPointSettingsUFK1(string JSON_for_CheckPointSettingsUFK1) => await metraService.Method_CheckPointSettingsUFK1_Post(JSON_for_CheckPointSettingsUFK1);

        //Добавление записи в таблицу CheckPointSettingsUFK3
        [HttpPost("Post_CheckPointSettingsUFK3/{JSON_for_CheckPointSettingsUFK3}")]//Проверено+
        public async Task<string> Post_CheckPointSettingsUFK3(string JSON_for_CheckPointSettingsUFK3) => await metraService.Method_CheckPointSettingsUFK3_Post(JSON_for_CheckPointSettingsUFK3);

        #endregion

        #region//Методы Delete Удаления данных из Checkpoint_Metra

        //Удаление по LocalBarCode из таблицы CheckPointData
        [HttpDelete("DeleteString_CheckPointData/{Barcode}/{ProductId}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointData(string Barcode, string ProductId)
  => await metraService.Method_CheckPointData_delete(Barcode, ProductId);

        //Удаление по LocalBarCode из таблицы CheckPointDataPrinter
        [HttpDelete("DeleteString_CheckPointDataPrinter/{LocalBarCode}/{Serial}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataPrinter(string LocalBarCode, string Serial)
  => await metraService.Method_CheckPointDataPrinter_delete(LocalBarCode, Serial);

        //Удаление по LocalBarCode из таблицы CheckPointDataProgrammer
        [HttpDelete("DeleteString_CheckPointDataProgrammer/{LocalBarCode}/{Ku}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataProgrammer(string LocalBarCode, string Ku)
  => await metraService.Method_CheckPointDataProgrammer_delete(LocalBarCode, Ku);

        //Удаление по LocalBarCode из таблицы CheckPointDataUfk1
        [HttpDelete("DeleteString_CheckPointDataUfk1/{LocalBarCode}/{Serial}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataUfk1(string LocalBarCode,string Serial)
  => await metraService.Method_CheckPointDataUfk1_delete(LocalBarCode, Serial);

        //Удаление по LocalBarCode из таблицы CheckPointDataUfk3
        [HttpDelete("DeleteString_CheckPointDataUfk3/{LocalBarCode}/{Serial}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataUfk3(string LocalBarCode, string Serial)
  => await metraService.Method_CheckPointDataUfk3_delete(LocalBarCode, Serial);

        //Удаление по ProductId из таблицы CheckPointSettings
        [HttpDelete("DeleteString_CheckPointSettings/{CheckPointId}/{ProductId}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointSettings(string CheckPointId, string ProductId)
  => await metraService.Method_CheckPointSettings_delete(CheckPointId, ProductId);

        //Удаление по ProductId из таблицы CheckPointSettingsUfk1
        [HttpDelete("DeleteString_CheckPointSettingsUfk1/{ProductId}/{MaxVal}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointSettingsUfk1(string ProductId, string MaxVal)
  => await metraService.Method_CheckPointSettingsUfk1_delete(ProductId, MaxVal);

        //Удаление по ProductId из таблицы CheckPointSettingsUfk3
        [HttpDelete("DeleteString_CheckPointSettingsUfk3/{ProductId}/{MaxVal}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointSettingsUfk3(string ProductId, string MaxVal)
  => await metraService.Method_CheckPointSettingsUfk3_delete(ProductId, MaxVal);
        #endregion

    }
}
