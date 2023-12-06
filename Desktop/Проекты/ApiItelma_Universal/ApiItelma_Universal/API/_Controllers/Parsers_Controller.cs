using ApiItelma_Universal.DBContext.TimeOffline;
using ApiItelma_Universal.DeviceWatcher;
using Microsoft.AspNetCore.Mvc;


namespace ApiItelma_Universal.API._Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Parsers_Controller : Controller
    {
        static TimeOfflineContext db = new TimeOfflineContext();
        DeviceQueryMethods DeviceQuery = new DeviceQueryMethods();

        #region

        [HttpGet("GetAllFrom_AllDevices")]
        public List<AllDevice> GetAllFrom_AllDevices() => db.AllDevices.ToList();

        [HttpGet("GetAllFrom_Parser/{ipAddress}")]
        public async Task<List<MrmLog>> GetAllFrom_Parser(string ipAddress) => await DeviceQuery.Method_FullPeriod(ipAddress);

        [HttpGet("GetDailyData_Parser/{ipAddress}/{Day}")]
        public async Task<List<MrmLog>> GetDailyData_Parser(string ipAddress, string Day) => await DeviceQuery.Method_DayPeriod(ipAddress, Day);
        
        [HttpGet("GetIntervalData_Parser/{ipAddress}/{Day1}/{Day2}")]
        public async Task<List<MrmLog>> GetIntervalData_Parser(string ipAddress, string Day1, string Day2) => await DeviceQuery.Method_IntervalPeriod(ipAddress, Day1, Day2);

        [HttpGet("GetDeviceStatus_Parser/{DeviceName}")]
        public async Task<string> GetOfflineStatus_Parser(string DeviceName) => await DeviceQuery.Method_GetDeviceStatus_Parser(DeviceName);



        #endregion


     

        #region//Put
        [HttpPut("PutDeviceStatus_Parser/{DeviceName}/{OfflineStatusTime}")]
        public async Task<string> PutDeviceStatus(string DeviceName, string OfflineStatusTime) => await DeviceQuery.Method_PutDeviceStatus_Parser(DeviceName, OfflineStatusTime);

        #endregion

        #region//Post
        [HttpPost("PostDeviceStatus_Parser/{Json_DeviceStatus}")]
        public async Task<string> PostDeviceStatus(string Json_DeviceStatus) => await DeviceQuery.Method_PostNewDevice_Parser(Json_DeviceStatus);
        #endregion

        #region//Delete
        [HttpDelete("DeleteDevice_Parser/{DeviceName}/{IpAddress}")]
        public async Task<string> DeleteDevice_Parser(string DeviceName, string IpAddress) => await DeviceQuery.Method_DeleteDevice(DeviceName, IpAddress);

        #endregion



    }

}
