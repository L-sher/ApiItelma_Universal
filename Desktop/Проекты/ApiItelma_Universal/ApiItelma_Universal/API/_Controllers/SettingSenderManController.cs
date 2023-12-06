using ApiItelma_Universal.DBContext.SettingSenderMan;
using ApiItelma_Universal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingSenderManController : Controller
    {
        SettingSenderManContext db=new SettingSenderManContext();
        SettingSenderMan_Service senderMan_Service = new SettingSenderMan_Service();


        [HttpGet("Get_ProductIds_Consumer/{ProductId}")]
        public async Task<string> Get_ProductIds_Consumer(string ProductId) => await senderMan_Service.Method_ProductIds_GetConsumerByProductId(ProductId);




        //Метод для сайта прослеживаемости
        [HttpGet("Get_SettingSenderManMails_includingAllTables")]
        public async Task<string> Get_SettingSenderManMails_includingAllTables()
        {
            try
            {
                List<SettingSenderManMail> s = await db.SettingSenderManMails.Include(c => c.ClientMails).Include(u => u.CopyMails).Include(r => r.ProductIds).ToListAsync();
                string json = JsonConvert.SerializeObject(s, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                return json;
            }
            catch(Exception e) { return ""; }
           
          
        } 

    }
}
