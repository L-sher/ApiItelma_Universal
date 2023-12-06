using ApiItelma_Universal.DBContext.Permissions;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class Permissions_service
    {
        PermissionsContext db = new PermissionsContext();
        private Serilog.Core.Logger Log_Permissions = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_Permissions.txt").CreateLogger();

        #region//Get Метод
        public async Task<List<EmployeesList>> Method_EmployeesList_GetByEmployeeName(string EmployeeName)
        {
            try
            {
                return await db.EmployeesLists.Where(e => e.EmployeeName == EmployeeName).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка  Method_EmployeesList_GetByEmployeeName В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<EmployeesList>> Method_EmployeesList_GetByEmployeeLogin(string EmployeeLogin)
        {
            try
            {
                return await db.EmployeesLists.Where(e => e.EmployeeLogin == EmployeeLogin).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_EmployeesList_GetByEmployeeName В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<PermissionRule>> Method_PermissionRule_GetByRuleId(string RuleId)
        {
            try
            {
                return await db.PermissionRules.Where(e => e.RuleId == int.Parse(RuleId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionRule_GetByRuleId В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из PermissionRule по столбцу EmployeeId и WebSitePageId
        public async Task<List<PermissionRule>> Method_PermissionRule_GetByEmployeeIdAndWebSitePageId(int EmployeeId, int WebSitePageId)
        {
            try
            {
                return await db.PermissionRules.Where(e => e.EmployeeId == EmployeeId & e.WebSitePageId== WebSitePageId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionRule_GetByEmployeeIdAndWebSitePageId В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<PermissionsList>> Method_PermissionsList_GetByPermissionId(string PermissionId)
        {
            try
            {
                return await db.PermissionsLists.Where(e => e.PermissionId == int.Parse(PermissionId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionsList_GetByRuleId В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<WebSitePagesList>> Method_WebSitePagesList_GetByWebSitePageId(string WebSitePageId)
        {
            try
            {
                return await db.WebSitePagesLists.Where(e => e.WebSitePageId == int.Parse(WebSitePageId)).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_WebSitePagesList_GetByWebSitePageId В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        public async Task<List<WebSitePagesList>> Method_WebSitePagesList_GetByWebSitePageName(string WebSitePageName)
        {
            try
            {
                return await db.WebSitePagesLists.Where(e => e.WebSitePageName == WebSitePageName).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_WebSitePagesList_GetByWebSitePageId В Контроллере Permissions " + e.Message.ToString());
                return null;
            }
        }

        #endregion

        #region//Post Метод
        //Добавляем строку данных в CheckPointsList
        public async Task<string> Method_EmployeesList_Post(string JSON_EmployeesList)
        {
            try
            {
                EmployeesList jsonData = JsonConvert.DeserializeObject<EmployeesList>(JSON_EmployeesList);
                db.EmployeesLists.Add(jsonData);
                db.SaveChanges();
                Log_Permissions.Information("Method_EmployeesList_Postуспешно " + jsonData.EmployeeName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_EmployeesList_Post В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_PermissionRule_Post(string JSON_PermissionRule)
        {
            try
            {
                PermissionRule jsonData = JsonConvert.DeserializeObject<PermissionRule>(JSON_PermissionRule);
                db.PermissionRules.Add(jsonData);
                db.SaveChanges();
                Log_Permissions.Information("Method_PermissionRule_Postуспешно " + jsonData.RuleId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionRule_Post В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_PermissionsList_Post(string JSON_PermissionsList)
        {
            try
            {
                PermissionsList jsonData = JsonConvert.DeserializeObject<PermissionsList>(JSON_PermissionsList);
                db.PermissionsLists.Add(jsonData);
                db.SaveChanges();
                Log_Permissions.Information("Method_PermissionsList_Postуспешно " + jsonData.PermissionId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionsList_Post В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_WebSitePagesList_Post(string JSON_WebSitePagesList)
        {
            try
            {
                WebSitePagesList jsonData = JsonConvert.DeserializeObject<WebSitePagesList>(JSON_WebSitePagesList);
                db.WebSitePagesLists.Add(jsonData);
                db.SaveChanges();
                Log_Permissions.Information("Method_WebSitePagesList_Postуспешно " + jsonData.WebSitePageId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_WebSitePagesList_Post В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion


        #region//Delete метод
        public async Task<string> Method_EmployeesList_delete(string EmployeeName, string EmployeeId)
        {
            try
            {
                db.EmployeesLists.Where(e => e.EmployeeName == EmployeeName & e.EmployeeId == int.Parse(EmployeeId)).ExecuteDelete();
                Log_Permissions.Information("Method_EmployeesList_delete успешно " + EmployeeName + " " + EmployeeId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_EmployeesList_delete В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_PermissionRule_delete(string RuleId, string WebSitePageId)
        {
            try
            {
                db.PermissionRules.Where(e => e.RuleId == int.Parse(RuleId) & e.WebSitePageId == int.Parse(WebSitePageId)).ExecuteDelete();
                Log_Permissions.Information("Method_PermissionRule_delete успешно " + RuleId + " " + WebSitePageId + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionRule_delete В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_PermissionsList_delete(string PermissionId, string PermissionName)
        {
            try
            {
                db.PermissionsLists.Where(e => e.PermissionId == int.Parse(PermissionId) & e.PermissionName == PermissionName).ExecuteDelete();
                Log_Permissions.Information("Method_PermissionsList_delete успешно " + PermissionId + " " + PermissionName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка Method_PermissionsList_delete В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_WebSitePagesList_delete(string WebSitePageId, string WebSitePageName)
        {
            try
            {
                db.WebSitePagesLists.Where(e => e.WebSitePageId == int.Parse(WebSitePageId) & e.WebSitePageName == WebSitePageName).ExecuteDelete();
                Log_Permissions.Information("Method_WebSitePagesList_delete успешно " + WebSitePageId + " " + WebSitePageName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Permissions.Error("Ошибка  Method_WebSitePagesList_delete В Контроллере Permissions " + e.Message.ToString());
                return e.Message.ToString();
            }
        }
        #endregion
    }
}
