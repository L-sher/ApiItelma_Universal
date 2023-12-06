using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiItelma_Universal.Services;
using ApiItelma_Universal.DBContext.CheckPoint_Packaging;
using ApiItelma_Universal.DBContext.SettingSenderMan;


namespace ApiItelma_Universal._Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPoint_PackagingController : ControllerBase
    {

        CheckPointPackagingContext db = new CheckPointPackagingContext();
        //CheckPointMetraContext db2 = new CheckPointMetraContext();
        CheckPoint_Packaging_Service packageService=new CheckPoint_Packaging_Service();
        #region //Методы Get получения данных из БД Upackage
        //Полностью таблицы данных
        #region//Получение GetAll по каждой таблице
        [HttpGet("GetAllFrom_CheckPointData_Packaging")]//_Packaging На конце, так как в метре такая же таблица есть. Проверено +
        public async Task<List<CheckPointDatum>> GetAll_CheckPointDatum() => await db.CheckPointData.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointDataPackages")]// Проверено +
        public async Task<List<CheckPointDataPackage>> GetAll_CheckPointDataPackage() => await db.CheckPointDataPackages.ToListAsync();

        [HttpGet("GetAllFrom_CheckPointDataSerials")]// Проверено +
        public async Task<List<CheckPointDataSerial>> GetAll_CheckPointDataSerial() => await db.CheckPointDataSerials.ToListAsync();
        /// <summary>
        /// Также пришлось назвать класс CheckPoint_Setting через _ так как без этого они смежаются с метрой и ломается отображение APi
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFrom_CheckPointSettings_Packaging")]//_Packaging На конце, так как в метре такая же таблица есть Проверено+
        public async Task<List<CheckPoint_Setting>> GetAll_CheckPointSetting() => await db.CheckPointSettings.ToListAsync();
        #endregion

        #region//Далее выборка по атрибуту из таблицы CheckPointData
        [HttpGet("GetFrom_CheckPointData_ByidAndBarCode/{idAndBarCode}")]//Проверено+
        public async Task<List<CheckPointDatum>> CheckPointData_GetByidAndBarCode(string idAndBarCode) => await packageService.Method_CheckPointData_GetByidAndBarCode(idAndBarCode);

        [HttpGet("GetFrom_CheckPointData_ByBarCodePackaging/{BarCode}")]//Проверено+
        public async Task<List<CheckPointDatum>> CheckPointData_GetByBarCode(string BarCode) => await packageService.Method_CheckPointData_GetByBarCode(BarCode);

        [HttpGet("GetFrom_CheckPointData_BySerial/{Serial}")]//Проверено+
        public async Task<List<CheckPointDatum>> CheckPointData_GetBySerial(string Serial) => await packageService.Method_CheckPointData_GetBySerial(Serial);

        //Далее запросы, который используется на Упаковке
        //Возвращает Список по ProductKey
        [HttpGet("GetFrom_CheckPointData_ByProductKey/{ProductKey}")]//Проверено+
        public async Task<List<CheckPointDatum>> GetFrom_CheckPointData_ByProductKey(string ProductKey) => await packageService.Method_CheckPointData_GetByProductKey(ProductKey);

        [HttpGet("GetFrom_CheckPointDataPackages_ByBarCode_ReturnPackageSerials/{BarCode}")]//Проверено+
        public async Task<List<CheckPointDataPackage>> GetFrom_CheckPointData_ByBarCode(string BarCode) => await packageService.Method_CheckPointDataPackage_GetByBarCode(BarCode);

        //[HttpGet("GetFrom_CheckPointData_BySerial/Consumer/{ProductId}")]
        //public async Task<List<CheckPointDatum>> CheckPointData_GetBySerial(string Serial) => await packageService.Method_CheckPointData_GetBySerial(Serial);

        //Нет таблицы в БД для этого метода.
        //[HttpGet("Consumer/{ProductId}")]
        //public async Task<string> GetConsumer(int ProductId)
        //{
        //    using (SettingsendermanContext cont = new SettingsendermanContext())
        //    {
        //        var consumer = cont.ProductIds
        //        .Where(w => w.ProdId == ProductId)
        //        .Select(d => d.Name)
        //        .FirstOrDefault();
        //        return consumer.ToString();
        //    }
        //}//Возвращает потребителя по ProductId


        #endregion

        #region//Далее выборка по атрибуту из таблицы CheckPointDataPackages
        [HttpGet("GetFrom_CheckPointDataPackages_ByBarCode/{BarCode}")]//Проверено+
        public async Task<List<CheckPointDataPackage>> CheckPointDataPackages_GetByBarCode(string BarCode) => await packageService.Method_CheckPointDataPackages_GetByBarcode(BarCode);


        [HttpGet("GetFrom_CheckPointDataPackages_ByPackageSerial/{PackageSerial}")]//Проверено+
        public async Task<List<CheckPointDataPackage>> CheckPointDataPackages_GetByPackageSerial(string PackageSerial) => await packageService.Method_CheckPointDataPackages_GetBySerial(PackageSerial);
        #endregion

        #region//Далее выборка по атрибуту из таблицы CheckPointDataSerials
        [HttpGet("GetFrom_CheckPointDataSerials_BySerial/{serial}")]//Проверено+
        public async Task<List<CheckPointDataSerial>> CheckPointDataSerials_GetBySerial(string serial) => await packageService.Method_CheckPointDataSerials_GetBySerial(serial);

        [HttpGet("GetFrom_CheckPointDataSerials_ByProductId/{ProductId}")]//Проверено+
        public async Task<List<CheckPointDataSerial>> CheckPointDataSerials_GetByProductId(string ProductId) => await packageService.Method_CheckPointDataSerials_GetByProductId(ProductId);
        #endregion

        #region// Методы для Сайта.
        //метод для связи таблиц CheckPointDataPackages и CheckPointDataSerials
        [HttpGet("GetFrom_CheckPointDataSerials_IncludeCheckPointDataPackages")]
        public async Task<string> CheckPointDataSerials_IncludeCheckPointDataPackages() => await packageService.Method_CheckPointDataSerials_IncludeCheckPointDataPackages();

        //метод для связи таблиц CheckPointDataPackages и CheckPointData
        [HttpGet("GetFrom_CheckPointDataPackages_IncludeCheckPointData")]
        public async Task<string> CheckPointDataPackages_IncludeCheckPointData() => await packageService.Method_CheckPointDataPackages_CheckPointData();

        #endregion
        //TODO: Добавить таблицу CheckPointSettings

        #endregion

        #region//Методы Put Редактирования данных из БД Upackage

        [HttpPost("Put_GeneratePackageSerial_ReturnNewSerial/{Json_productsPackageModel}")]//Проверено+
        public async Task<string> Put_GeneratePackageSerial_ReturnNewSerial(string Json_productsPackageModel)
        => await packageService.PutCheckPoint_DataSerials(Json_productsPackageModel);

        #endregion
        
        #region //Методы Post добавления данных в БД UPackage
        //получает Json строку и десериализует ее в CheckPointDataPackages
        [HttpPost("Post_CheckPointDataPackages/{JSon_CheckpointDataPackage}")]//Проверено+ (ограничение колонки Serial. Связано с CheckPointDataSerials)
        public async Task<string> CheckPointDataPackages_Post(string JSon_CheckpointDataPackage) => await packageService.Method_CheckPointDataPackages_Post(JSon_CheckpointDataPackage);

        //получает Json строку и десериализует ее в CheckPointDataSerial
        [HttpPost("Post_CheckPointDataSerial/{JSon_CheckPointDataSerial}")]//Проверено+
        public async Task<string> CheckPointDataSerial_Post(string JSon_CheckPointDataSerial) => await packageService.Method_CheckPointDataSerial_Post(JSon_CheckPointDataSerial);

        //получает Json строку и десериализует ее в CheckPointDatum
        [HttpPost("Post_CheckPointData/{JSon_CheckPointData}")]//Проверено+
        public async Task<string> CheckPointData_Post(string JSon_CheckPointData) => await packageService.Method_CheckPointData_Post(JSon_CheckPointData);

        //получает Json строку и десериализует ее в CheckPointSettings
        [HttpPost("Post_CheckPointSettings/{JSon_CheckPointSettings}")]//Проверено+
        public async Task<string> CheckPointSettings_Post(string JSon_CheckPointSettings) => await packageService.Method_CheckPointSettings_Post(JSon_CheckPointSettings);

        #endregion

        #region//Методы Delete Удаления данных из БД Upackage

        //Удаление по IdAndBarCode из таблицы CheckPointData
        [HttpDelete("DeleteString_CheckPointData/{IdAndBarCode}/{SerialNumber}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointData(string IdAndBarCode, string SerialNumber) => await packageService.Method_CheckPointData_delete(IdAndBarCode, SerialNumber);

        //Удаление по Barcode из таблицы CheckPointDataPackages
        [HttpDelete("DeleteString_CheckPointDataPackages/{Barcode}/{PackageSerial}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataPackages(string Barcode,string PackageSerial) => await packageService.Method_CheckPointDataPackages_delete(Barcode, PackageSerial);

        //Удаление по serial из таблицы CheckPointDataSerials
        [HttpDelete("DeleteString_CheckPointDataSerials/{serial}/{ID}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointDataSerials(string serial,string ID) => await packageService.Method_CheckPointDataSerials_delete(serial, ID);

        //Удаление по CheckPointId из таблицы CheckPointSettings
        [HttpDelete("DeleteString_CheckPointSettings/{CheckPointId}/{CheckPointName}")]//Проверено+
        public async Task<string> DeleteFrom_CheckPointSettings(string CheckPointId, string CheckPointName) => await packageService.Method_CheckPointSettings_delete(CheckPointId, CheckPointName);

        #endregion
    }
}
