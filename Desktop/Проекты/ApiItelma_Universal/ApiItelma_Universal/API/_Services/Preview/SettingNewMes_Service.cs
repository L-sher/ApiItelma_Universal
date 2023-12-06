using ApiItelma_Universal.DBContext.Preview.SettingNewMes;
using Newtonsoft.Json;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ApiItelma_Universal.SeriLog;

namespace ApiItelma_Universal.API._Services.Preview
{
    public class SettingNewMes_Service
    {
        static SettingNewMesContext db = new SettingNewMesContext();

        private Serilog.Core.Logger Log_SettingNewMes = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_SettingNewMes.txt").CreateLogger();

        #region//POST

        public async Task<string> Method_Setting_POSTSetting(string selectedProduct)
        {
            try
            {
                Setting jsonPrinter = JsonConvert.DeserializeObject<Setting>(selectedProduct);

                if (db.Settings.FirstOrDefault(e => e.IdProduct == jsonPrinter.IdProduct) == null)
                {

                    db.Settings.Add(jsonPrinter);
                    db.SaveChanges(true);
                    db.ChangeTracker.Clear();

                }
                Log_SettingNewMes.Information("Method_Setting_POSTSetting успешно " + jsonPrinter.IdProduct.ToString());
                return "OK";

            }
            catch (Exception e)
            {

                Log_SettingNewMes.Error("Ошибка метода Method_Setting_POSTSetting В Контроллере SettingNewMes " + e.Message.ToString());
                return null;

            }
        }

        #endregion


        #region//Put
        public async Task<string> Method_Setting_PUTSetting(string selectedProduct)
        {
            try
            {

                Setting jsonPrinter = JsonConvert.DeserializeObject<Setting>(selectedProduct);
                Setting packageSerial = db.Settings.FirstOrDefault(e => e.IdProduct == jsonPrinter.IdProduct);
                if (packageSerial!=null)
                {
                    //packageSerial.CheckStepsUfk1s = jsonPrinter.CheckStepsUfk1s;
                    //packageSerial.CheckStepsUfk3s = jsonPrinter.CheckStepsUfk3s;
                    packageSerial.KodUfk3Mts = jsonPrinter.KodUfk3Mts;
                    packageSerial.KodUfk1 = jsonPrinter.KodUfk1;
                    packageSerial.KodUfk3Nit = jsonPrinter.KodUfk3Nit;
                    packageSerial.Number1C = jsonPrinter.Number1C;
                    packageSerial.TechRomStruct = jsonPrinter.TechRomStruct;
                    packageSerial.LabelStruct = jsonPrinter.LabelStruct;
                    packageSerial.Param1 = jsonPrinter.Param1;
                    packageSerial.Param2 = jsonPrinter.Param2;
                    packageSerial.FileAdress = jsonPrinter.FileAdress;
                    packageSerial.Vch = jsonPrinter.Vch;
                    packageSerial.IsEasy = jsonPrinter.IsEasy;
                    packageSerial.LabelImage = jsonPrinter.LabelImage;
                    packageSerial.Name = jsonPrinter.Name;
                    packageSerial.Param3 = jsonPrinter.Param3;
                    packageSerial.Param4 = jsonPrinter.Param4;
                    packageSerial.Param5 = jsonPrinter.Param5;
                    packageSerial.Version = jsonPrinter.Version;

                    packageSerial = jsonPrinter;

                    //db.Settings.Update(packageSerial);
                    db.SaveChanges(true);
                    db.ChangeTracker.Clear();

                    Log_SettingNewMes.Information("Method_Setting_PUTSettingуспешно " + packageSerial.IdProduct.ToString());
                    return "OK";
                }
                else
                {

                    return "NOK";
                }

            }
            catch (Exception ex)
            {

                Log_SettingNewMes.Error("Ошибка метода Method_Setting_PUTSetting В Контроллере SettingNewMes " + ex.Message.ToString());
                return null;

            }
        }

        //
        public async Task Method_CheckPointSettingsUFK1_PUT_WhereIdProduct(string Idproduct, string json_CheckPointSettings)
        {
            try
            {
                List<CheckStepsUfk1> jsonCheckStepsUfk1 = JsonConvert.DeserializeObject<List<CheckStepsUfk1>>(json_CheckPointSettings);

                db.CheckStepsUfk1s.Where(e => e.SettingIdProduct == Idproduct).ExecuteDelete();
                foreach (var item in jsonCheckStepsUfk1)
                {
                    db.CheckStepsUfk1s.Add(new() 
                    { 
                        MaxVal=item.MaxVal,
                        MinVal=item.MinVal,
                        SettingIdProduct = Idproduct,
                        Step=item.Step
                    });
                }
                db.SaveChanges(true);
                db.ChangeTracker.Clear();

                Log_SettingNewMes.Information("Method_CheckPointSettingsUFK1_PUT_WhereIdProduct успешно " + Idproduct.ToString());
         
            }
            catch (Exception ex)
            {
                Log_SettingNewMes.Error("Ошибка метода Method_CheckPointSettingsUFK1_PUT_WhereIdProduct В Контроллере SettingNewMes " + ex.Message.ToString());
        
            }
        }
        public async Task Method_CheckPointSettingsUFK3_PUT_WhereIdProduct(string Idproduct, string json_CheckPointSettings)
        {
            try
            {
                List<CheckStepsUfk3> jsonCheckStepsUfk3 = JsonConvert.DeserializeObject<List<CheckStepsUfk3>>(json_CheckPointSettings);

                db.CheckStepsUfk3s.Where(e => e.SettingIdProduct == Idproduct).ExecuteDelete();
                foreach(var item in jsonCheckStepsUfk3)
                {
                    db.CheckStepsUfk3s.Add(new()
                    {
                        MaxVal = item.MaxVal,
                        MinVal = item.MinVal,
                        SettingIdProduct = Idproduct,
                        Step = item.Step
                    });
                }
                db.SaveChanges(true);
                db.ChangeTracker.Clear();

                Log_SettingNewMes.Information("Method_CheckPointSettingsUFK3_PUT_WhereIdProduct успешно " + Idproduct.ToString());

            }
            catch (Exception ex)
            {
                Log_SettingNewMes.Error("Ошибка метода Method_CheckPointSettingsUFK3_PUT_WhereIdProduct В Контроллере SettingNewMes " + ex.Message.ToString());

            }
        }


        #endregion



        #region//delete
        //Удаление строки из таблицы CheckStepsUFK1
        public async Task<string> Method_Settings_delete_CheckStepsUFK1(string Setting_IdProduct, string Step)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(Step);
                db.CheckStepsUfk1s.Where(y => y.SettingIdProduct == Setting_IdProduct & y.Step == bytes).ExecuteDelete();
                db.SaveChanges();
                Log_SettingNewMes.Information("Method_Settings_delete_CheckStepsUFK1успешно шаг и " + Setting_IdProduct + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_SettingNewMes.Error("Ошибка метода Method_Settings_delete_CheckStepsUFK1 В Контроллере SettingNewMes" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки из таблицы CheckStepsUFK3
        public async Task<string> Method_Settings_delete_CheckStepsUFK3(string Setting_IdProduct, string Step)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(Step);
                db.CheckStepsUfk3s.Where(y => y.SettingIdProduct == Setting_IdProduct & y.Step == bytes).ExecuteDelete();
                db.SaveChanges();
                Log_SettingNewMes.Information("Method_Settings_delete_CheckStepsUFK3успешно шаг и " + Setting_IdProduct + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_SettingNewMes.Error("Ошибка Method_Settings_delete_CheckStepsUFK3 В Контроллере SettingNewMes" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_Setting_Delete(string IdProduct, string Number1C)
        {
            try
            {
                db.Settings.Where(e => e.IdProduct == IdProduct & e.Number1C == Number1C).ExecuteDelete();
                db.SaveChanges();
                db.ChangeTracker.Clear();

                Log_SettingNewMes.Information("Method_Setting_POSTSettingуспешно " + IdProduct.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_SettingNewMes.Error("Ошибка метода В Контроллере SettingNewMes " + e.Message.ToString());
                return null;
            }
        }
        #endregion
    }

}
