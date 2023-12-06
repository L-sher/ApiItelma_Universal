using ApiItelma_Universal.DBContext.SettingSenderMan;
using ApiItelma_Universal.SeriLog;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class SettingSenderMan_Service
    {
        private Serilog.Core.Logger Log_SettingSenderMan = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_SettingSenderMan.txt").CreateLogger();
        public async Task<string> Method_ProductIds_GetConsumerByProductId(string ProductId)
        {
            try
            {
                using (SettingSenderManContext cont = new SettingSenderManContext())
                {
                    var consumer = cont.ProductIds
                    .Where(w => w.ProdId == int.Parse(ProductId))
                    .Select(d => d.Name)
                    .FirstOrDefault();
                    return consumer.ToString();
                }
            }
            catch (Exception e)
            {
                Log_SettingSenderMan.Error("Ошибка Method_ProductIds_GetConsumerByProductId В Контроллере SettingSenderMan " + e.Message.ToString());
                return null;
            }
        }

       

    }
}
