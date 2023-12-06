using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.DBContext.Permissions;
using ApiItelma_Universal.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : Controller
    {
        PermissionsContext db = new PermissionsContext();
        Permissions_service permissions_service = new Permissions_service();

        #region//Методы GET

        #region//GetAll методы

        //Получение всех данных из таблицы EmployeesList
        [HttpGet("GetAllFrom_EmployeesList")]//Проверено+
        public async Task<List<EmployeesList>> GetAll_CheckPointSetting() => await db.EmployeesLists.ToListAsync();

        //Получение всех данных из таблицы PermissionRule
        [HttpGet("GetAllFrom_PermissionRule")]//Проверено+
        public async Task<List<PermissionRule>> GetAll_PermissionRule() => await db.PermissionRules.ToListAsync();

        //Получение всех данных из таблицы PermissionsList
        [HttpGet("GetAllFrom_PermissionsList")]//Проверено+
        public async Task<List<PermissionsList>> GetAll_PermissionsList() => await db.PermissionsLists.ToListAsync();

        //Получение всех данных из таблицы WebSitePagesList
        [HttpGet("GetAllFrom_WebSitePagesList")]//Проверено+
        public async Task<List<WebSitePagesList>> GetAll_WebSitePagesList() => await db.WebSitePagesLists.ToListAsync();

        #endregion

        //Получение строки данных из EmployeesList по столбцу EmployeeName
        [HttpGet("GetFrom_EmployeesList_ByEmployeeName/{EmployeeName}")]//Проверено+ (Даже с "\" работает. Просто заменяем на "%5C")
        public async Task<List<EmployeesList>> EmployeesList_GetByEmployeeName(string EmployeeName) => await permissions_service.Method_EmployeesList_GetByEmployeeName(EmployeeName);

        //Получение строки данных из EmployeesList по столбцу EmployeeLogin
        [HttpGet("GetFrom_EmployeesList_ByEmployeeLogin/{EmployeeLogin}")]//Проверено+ (Даже с "\" работает. Просто заменяем на "%5C")
        public async Task<List<EmployeesList>> EmployeesList_GetByEmployeeLogin(string EmployeeLogin) => await permissions_service.Method_EmployeesList_GetByEmployeeLogin(EmployeeLogin);



        //Получение строки данных из PermissionRule по столбцу RuleId
        [HttpGet("GetFrom_PermissionRule_ByRuleId/{RuleId}")]//Проверено+
        public async Task<List<PermissionRule>> PermissionRule_GetByRuleId(string RuleId) => await permissions_service.Method_PermissionRule_GetByRuleId(RuleId);

        //Получение строки данных из PermissionRule по столбцу EmployeeId и WebSitePageId
        [HttpGet("GetFrom_PermissionRule_ByEmployeeIdAndWebSitePageId/{EmployeeId}/{WebSitePageId}")]
        public async Task<List<PermissionRule>> PermissionRule_GetByEmployeeIdAndWebSitePageId(int EmployeeId, int WebSitePageId) => await permissions_service.Method_PermissionRule_GetByEmployeeIdAndWebSitePageId(EmployeeId, WebSitePageId);


        //Получение строки данных из PermissionsList по столбцу PermissionId
        [HttpGet("GetFrom_PermissionsList_ByPermissionId/{PermissionId}")]//Проверено+
        public async Task<List<PermissionsList>> PermissionsList_GetByPermissionId(string PermissionId) => await permissions_service.Method_PermissionsList_GetByPermissionId(PermissionId);

        //Получение строки данных из WebSitePagesList по столбцу WebSitePageId
        [HttpGet("GetFrom_WebSitePagesList_ByWebSitePageId/{WebSitePageId}")]//Проверено+
        public async Task<List<WebSitePagesList>> WebSitePagesList_GetByWebSitePageId(string WebSitePageId) => await permissions_service.Method_WebSitePagesList_GetByWebSitePageId(WebSitePageId);

        //Получение строки данных из WebSitePagesList по столбцу WebSitePageName
        [HttpGet("GetFrom_WebSitePagesList_ByWebSitePageName/{WebSitePageName}")]
        public async Task<List<WebSitePagesList>> WebSitePagesList_GetByWebSitePageName(string WebSitePageName) => await permissions_service.Method_WebSitePagesList_GetByWebSitePageName(WebSitePageName);

        #endregion

        #region//Методы Post

        //добавление строки данных в EmployeesList
        [HttpPost("Post_EmployeesList/{JSON_EmployeesList}")]//Проверено+
        public async Task<string> Post_EmployeesList(string JSON_EmployeesList) => await permissions_service.Method_EmployeesList_Post(JSON_EmployeesList);

        //добавление строки данных в PermissionRule
        [HttpPost("Post_PermissionRule/{JSON_PermissionRule}")]//Проверено+ (ограничения по столбцам EmployeeID и PermissionId. В Ruleid нельзя добавлять значения)
        public async Task<string> Post_PermissionRule(string JSON_PermissionRule) => await permissions_service.Method_PermissionRule_Post(JSON_PermissionRule);

        //добавление строки данных в PermissionsList
        [HttpPost("Post_PermissionsList/{JSON_PermissionsList}")]//Проверено+ 
        public async Task<string> Post_PermissionsList(string JSON_PermissionsList) => await permissions_service.Method_PermissionsList_Post(JSON_PermissionsList);

        //добавление строки данных в WebSitePagesList
        [HttpPost("Post_WebSitePagesList/{JSON_WebSitePagesList}")]//Проверено+
        public async Task<string> Post_WebSitePagesList(string JSON_WebSitePagesList) => await permissions_service.Method_WebSitePagesList_Post(JSON_WebSitePagesList);

        #endregion

        #region//Методы Delete

        //Удаление строки данных из EmployeesList по EmployeeName и EmployeeId
        [HttpDelete("DeleteString_EmployeesList/{EmployeeName}/{EmployeeId}")]//Проверено+
        public async Task<string> DeleteFrom_EmployeesList(string EmployeeName, string EmployeeId)
  => await permissions_service.Method_EmployeesList_delete(EmployeeName, EmployeeId);

        //Удаление строки данных из PermissionRule по RuleId и WebSitePageId
        [HttpDelete("DeleteString_PermissionRule/{RuleId}/{WebSitePageId}")]//Проверено+
        public async Task<string> DeleteFrom_PermissionRule(string RuleId, string WebSitePageId)
  => await permissions_service.Method_PermissionRule_delete(RuleId, WebSitePageId);

        //Удаление строки данных из PermissionsList по PermissionId и PermissionName
        [HttpDelete("DeleteString_PermissionsList/{PermissionId}/{PermissionName}")]//Проверено+
        public async Task<string> DeleteFrom_PermissionsList(string PermissionId, string PermissionName)
  => await permissions_service.Method_PermissionsList_delete(PermissionId, PermissionName);

        //Удаление строки данных из WebSitePagesList по WebSitePageId и WebSitePageName
        [HttpDelete("DeleteString_WebSitePagesList/{WebSitePageId}/{WebSitePageName}")]//Проверено+
        public async Task<string> DeleteFrom_WebSitePagesList(string WebSitePageId, string WebSitePageName)
  => await permissions_service.Method_WebSitePagesList_delete(WebSitePageId, WebSitePageName);

        #endregion
    }
}
