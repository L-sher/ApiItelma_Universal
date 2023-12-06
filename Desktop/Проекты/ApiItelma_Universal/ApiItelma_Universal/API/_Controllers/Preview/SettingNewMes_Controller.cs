using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.API._Services.Preview;
using ApiItelma_Universal.DBContext.Preview.SettingNewMes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiItelma_Universal.API._Controllers.Preview
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingNewMes_Controller
    {

        SettingNewMesContext db = new DBContext.Preview.SettingNewMes.SettingNewMesContext();

        SettingNewMes_Service SetNewMesService = new SettingNewMes_Service();

        #region //Методы Get получения данных из SettingNewMes

        #region //Методы GetAll по каждой таблице

        //Получение полностью всех данных из таблиц
        [HttpGet("GetAllFrom_Settings")]//Проверено+
        public async Task<string> GetAll_Setting() 
        {
            try
            {

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                List<Setting> test = db.Settings.Include(u => u.CheckStepsUfk1s).Include(u => u.CheckStepsUfk3s).ToList();

                var json = JsonConvert.SerializeObject(db.Settings.Include(u => u.CheckStepsUfk1s).Include(u => u.CheckStepsUfk3s).ToList(), Formatting.Indented, settings);

                return json;

            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
        
        [HttpGet("GetAllFrom_CheckStepsUfk1")]//Проверено+
        public async Task<List<CheckStepsUfk1>> GetAll_CheckStepsUfk1() => await db.CheckStepsUfk1s.ToListAsync();

        [HttpGet("GetAllFrom_CheckStepsUfk3")]//Проверено+
        public async Task<List<CheckStepsUfk3>> GetAll_CheckStepsUfk3() => await db.CheckStepsUfk3s.ToListAsync();



        #endregion

        #endregion

        #region//Post
        [HttpPost("Post_Setting/{selectedProduct}")]//Проверено+
        public async Task<IResult> Post_Setting(string selectedProduct)
        {

            await SetNewMesService.Method_Setting_POSTSetting(selectedProduct);

            return Results.NotFound(new { message = "Love, save the world" });

        }
        #endregion

        #region//Put
        [HttpPut("Put_Setting/{selectedProduct}")]//Проверено+
        public async Task<IResult> Put_Setting(string selectedProduct)
        {

            await SetNewMesService.Method_Setting_PUTSetting(selectedProduct);

            return Results.NotFound(new {message= "Love, save the world" });

        }

        [HttpPut("Put_CheckPointSettingsUFK1_WhereIdProduct/{idProduct}/{Json_CheckPointUfk1}")]//Проверено+
        public async Task Put_Setting_WhereIdProduct(string idProduct, string Json_CheckPointUfk1) => SetNewMesService.Method_CheckPointSettingsUFK1_PUT_WhereIdProduct(idProduct, Json_CheckPointUfk1);

        [HttpPut("Put_CheckPointSettingsUFK3_WhereIdProduct/{idProduct}/{Json_CheckPointUfk3}")]//Проверено+
        public async Task Put_CheckPointSettingsUFK3_WhereIdProduct(string idProduct, string Json_CheckPointUfk3) => SetNewMesService.Method_CheckPointSettingsUFK3_PUT_WhereIdProduct(idProduct, Json_CheckPointUfk3);

        #endregion

        #region

        //Удаление по Step из таблицы CheckStepsUFK1
        [HttpDelete("DeleteString_CheckStepsUFK1/{Setting_IdProduct}/{Step}")]
        public async Task<string> DeleteFrom_CheckStepsUFK1(string Setting_IdProduct, string Step)
  => await SetNewMesService.Method_Settings_delete_CheckStepsUFK1(Setting_IdProduct, Step);


        //Удаление по Step из таблицы CheckStepsUFK3
        [HttpDelete("DeleteString_CheckStepsUFK3/{Setting_IdProduct}/{Step}")]
        public async Task<string> DeleteFrom_CheckStepsUFK3(string Setting_IdProduct, string Step)
  => await SetNewMesService.Method_Settings_delete_CheckStepsUFK3(Setting_IdProduct, Step);

        //Удаление по Step из таблицы CheckStepsUFK3
        [HttpDelete("DeleteString_DeleteSetting/{IdProduct}/{Number1C}")]
        public async Task<string> DeleteFrom_Setting(string IdProduct,string Number1C)
  => await SetNewMesService.Method_Setting_Delete(IdProduct, Number1C);

        #endregion
    }
}


