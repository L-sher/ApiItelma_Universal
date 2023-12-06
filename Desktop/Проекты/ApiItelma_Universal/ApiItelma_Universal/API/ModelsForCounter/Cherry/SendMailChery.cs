using ApiItelma_Universal.ModelsForCounter.BaseModelForSend;

namespace ApiItelma_Universal.ModelsForCounter.Cherry
{
    public class SendMailChery : SendMailProduct
    {
      
        public SendMailChery(ModelForChery checkBarCodes)
        {
            _date = DateTime.Now;
            _checkBarCodes = checkBarCodes;
            fileName = @"D:\CherySends\" + checkBarCodes.ProductName + "_" + _date.Day + "_" + _date.Month + "_" + _date.Year + "_" + _date.Hour + "_" + _date.Minute + ".txt";
            body = "Данное письмо содержит номера ICCID и IMEI " + _checkBarCodes.ProductName + " за " + _date.ToString("dd.MM.yyyy").Replace(".", "_") +
                              " г. в количестве " + _checkBarCodes.AllCount +
                              " шт. \n\r С уважением, робот производственного сервера";
        }
       
    }
}
