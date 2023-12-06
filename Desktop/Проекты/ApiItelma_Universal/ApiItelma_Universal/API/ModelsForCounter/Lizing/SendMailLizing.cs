using ApiItelma_Universal.ModelsForCounter.BaseModelForSend;

namespace ApiItelma_Universal.ModelsForCounter.Lizing
{
    public class SendMailLizing : SendMailProduct
    {
        public SendMailLizing(ModelForLizing checkBarCodes)
        {
            _date = DateTime.Now;
            _checkBarCodes = checkBarCodes;
            fileName = @"D:\LizingSends\" + checkBarCodes.ProductName + "_" + _date.Day + "_" + _date.Month + "_" + _date.Year + "_" + _date.Hour + "_" + _date.Minute + ".txt";
            body = "Данное письмо содержит номера VIN, IMEI, ICCID и ICCID_VIM" + _checkBarCodes.ProductName + " за " + _date.ToString("dd.MM.yyyy").Replace(".", "_") +
                              " г. в количестве " + _checkBarCodes.AllCount +
                              " шт. \n\r С уважением, робот производственного сервера";
        }

        /// <summary>
        /// Для Lizing
        /// </summary>
        /// <returns></returns>
        public new bool SaveFile()
        {
            foreach (CheckBarCode checkBar in _checkBarCodes.LibsForSend)
            {
                File.AppendAllText(fileName, checkBar.VIN + ";" + checkBar.IMEI + ";" + checkBar.ICCID + ";" + checkBar.ICCID_VIM + Environment.NewLine);
            }
            return true;
        }
    }
}
