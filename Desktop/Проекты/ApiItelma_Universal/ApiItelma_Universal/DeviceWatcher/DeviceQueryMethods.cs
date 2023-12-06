using ApiItelma_Universal.DBContext.TimeOffline;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace ApiItelma_Universal.DeviceWatcher
{
    public class DeviceQueryMethods
    {
        static HttpClient httpClient = new HttpClient();
        static TimeOfflineContext db = new TimeOfflineContext();
        private Serilog.Core.Logger Log_DeviceQueryMethods = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_DeviceQueryMethods.txt").CreateLogger();

        public async Task<List<MrmLog>> Method_FullPeriod(string ipAddress)
        {
            try
            { 
                var response = await httpClient.GetAsync("http://" + ipAddress + ":8886/FullPeriod/");//Посылаем запрос на получение строки

                var content = await response.Content.ReadAsByteArrayAsync();//Получаем ответ

                string cont = Encoding.Unicode.GetString(content);

                List<MrmLog> ReceivedData = JsonConvert.DeserializeObject<List<MrmLog>>(cont);

                return ReceivedData;
            }
            catch (Exception ex)
            {
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_FullPeriod");
                return null;
            }
        }
     
        public async Task<List<MrmLog>> Method_DayPeriod(string ipAddress,string day)
        {
            try
            {
                StringContent content = new StringContent(day);

                using var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAddress + ":8886/Day/");

                request.Content = content;

                using var response = await httpClient.SendAsync(request);

                //var response = await httpClient.GetAsync("http://" + ipAddress + ":8886/Interval/");//Посылаем запрос на получение строки

                var content1 = await response.Content.ReadAsByteArrayAsync();//Получаем ответ

                string cont = Encoding.Unicode.GetString(content1);
                
                List<MrmLog> ReceivedData = JsonConvert.DeserializeObject<List<MrmLog>>(cont);

                return ReceivedData;

            }
            catch (Exception ex)
            {
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_IntervalPeriod");
                return null;
            }
        }
        //Получение данных за заданный период времени
        public async Task<List<MrmLog>> Method_IntervalPeriod(string ipAddress, string Day1, string Day2)
        {
            try
            {
                StringContent content = new StringContent(Day1 + "=>" + Day2);

                using var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAddress + ":8886/Day/");

                request.Content = content;

                using var response = await httpClient.SendAsync(request);

                //var response = await httpClient.GetAsync("http://" + ipAddress + ":8886/Interval/");//Посылаем запрос на получение строки

                var content1 = await response.Content.ReadAsByteArrayAsync();//Получаем ответ

                string cont = Encoding.Unicode.GetString(content1);

                List<MrmLog> ReceivedData = JsonConvert.DeserializeObject<List<MrmLog>>(cont);

                return ReceivedData;

            }
            catch (Exception ex)
            {
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_IntervalPeriod");
                return null;
            }
        }

        public async Task<string> Method_GetDeviceStatus_Parser(string DeviceName)
        {
            return db.AllDevices.FirstOrDefault(e => e.DeviceName == DeviceName).OfflineStatusDate;
        }

        #region//Post
        public async Task<string> Method_PostNewDevice_Parser(string Json_DeviceStatus)
        {
            try
            {
                AllDevice jsonData = JsonConvert.DeserializeObject<AllDevice>(Json_DeviceStatus);
                db.AllDevices.Add(jsonData);
                db.SaveChanges();
                Log.Logger.Information("Post_PackageSettingуспешно " + jsonData.DeviceName.ToString());
                return "OK";
            }
            catch (Exception ex)
            {
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_PostNewDevice_Parser");
                return "NOK";
            }
        }
        #endregion

        #region//Put
        public async Task<string> Method_PutDeviceStatus_Parser(string DeviceName, string OfflineStatusTime)
        {
            try
            {
                if (OfflineStatusTime == "OnLine")
                {
                    db.AllDevices.FirstOrDefault(e => e.DeviceName == DeviceName).OfflineStatusDate = "OnLine";
                    db.SaveChanges();
                    return "";
                }
                else
                {
                    db.AllDevices.FirstOrDefault(e => e.DeviceName == DeviceName).OfflineStatusDate = DateTime.Now.ToString("s");
                    db.SaveChanges();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_PutDeviceStatus_Parser");
            }
        }
        #endregion

        #region//Delete
        public async Task<string> Method_DeleteDevice(string DeviceName, string IpAddress)
        {
            try
            {
                if (db.AllDevices.FirstOrDefault(e => e.DeviceName == DeviceName) != null)
                {
                    db.ChangeTracker.Clear();
                    db.AllDevices.Where(e=>e.DeviceName==DeviceName & e.IpAddress==IpAddress).ExecuteDelete();
                    db.SaveChanges();
                }
                return "OK";
            }
            catch (Exception ex)
            {
                Log_DeviceQueryMethods.Information(ex.Message.ToString() + "Method_DeleteDevice");
                return "NOK";
            }
        }
        #endregion
    }
}
