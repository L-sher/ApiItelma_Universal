using ApiItelma_Universal.API._Services;
using ApiItelma_Universal.DBContext.CheckPoint_Packaging;
using ApiItelma_Universal.DBContext.LabelSetting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal._Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelSetting_Controller : ControllerBase
    {
        LabelSettingContext db = new LabelSettingContext();
        LabelSetting_Service LabelService = new LabelSetting_Service();

        #region //Методы Get получения данных из CheckPoint_Metra
        #region//Методы GetAll по каждой таблице

        //Получение полностью всех данных из таблиц
        [HttpGet("GetAllFrom_Labels")]//Проверено+
        public async Task<List<Label>> GetAll_Labels() => await db.Labels.ToListAsync();
        [HttpGet("GetAllFrom_PackageSettings")]//Проверено+
        public async Task<List<PackageSetting>> GetAll_PackageSettings() => await db.PackageSettings.ToListAsync();
        [HttpGet("GetAllFrom_PassportSettings")]
        public async Task<List<PassportSetting>> GetAll_PassportSettings() => await db.PassportSettings.ToListAsync();

        #endregion

        ////Далее Выборка по конкретному атрибуту
        [HttpGet("GetFrom_PackageSetting_GetByLabelName/{LabelName}")]//Проверено+ (CheckPointData_GetByBarCode2) 2 на конце так как смежается с другим методом CheckpointData из Packaging
        public async Task<PackageSetting> GetFrom_PackageSetting_GetByProductName(string LabelName) => await LabelService.Method_PackageSetting_GetByLabelName(LabelName);

        ////Далее Выборка по конкретному атрибуту
        [HttpGet("GetFrom_PassportSetting_GetByLabelName/{LabelName}")]//Проверено+ (CheckPointData_GetByBarCode2) 2 на конце так как смежается с другим методом CheckpointData из Packaging
        public async Task<PassportSetting> GetFrom_PassportSetting_GetByLabelName(string LabelName) => await LabelService.Method_PassportSetting_GetByLabelName(LabelName);


       

        #endregion

        #region//Методы Put Изменения данных на CheckPoint_Metra

        [HttpPut("Put_PackageSettings/{Json_LabelName}")]
        public string Put_PackageSettings(string Json_LabelName) => LabelService.Put_PackageSettings(Json_LabelName);

        [HttpPut("Put_PassportSetting/{Json_LabelName}")]
        public string Put_PassportSetting(string Json_LabelName) => LabelService.Put_PassportSetting(Json_LabelName);

        #endregion

        #region //Методы Post добавления данных в CheckPoint_Metra
        //Добавление записи в таблицу CheckPointData
        [HttpPost("Post_PassportSetting/{JSON_for_PassportSetting}")]//Проверено+ (время TimeCheck должно быть указано)
        public string Post_PassportSetting(string JSON_for_PassportSetting) => LabelService.Post_PassportSetting(JSON_for_PassportSetting);

        [HttpPost("Post_PackageSettings/{JSON_for_PackageSettings}")]//Проверено+ (время TimeCheck должно быть указано)
        public string Post_PackageSettings(string JSON_for_PackageSettings) => LabelService.Post_PackageSetting(JSON_for_PackageSettings);

        #endregion

        #region//Методы Delete Удаления данных из Checkpoint_Metra

        //Удаление по LocalBarCode из таблицы CheckPointData
        [HttpDelete("PassportSetting_delete/{LabelName}/{ProductName}")]//Проверено+
        public async Task<string> PassportSetting_delete(string LabelName, string ProductName)
  => await LabelService.PassportSetting_delete(LabelName, ProductName);

        //Удаление по LocalBarCode из таблицы CheckPointDatas
        [HttpDelete("PackageSetting_delete/{LabelName}/{ProductName}")]//Проверено+
        public async Task<string> PackageSetting_delete(string LabelName, string ProductName)
  => await LabelService.PackageSetting_delete(LabelName, ProductName);

        #endregion
    }
}
