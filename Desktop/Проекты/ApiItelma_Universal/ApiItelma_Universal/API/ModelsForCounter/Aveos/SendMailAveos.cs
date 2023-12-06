using ApiItelma_Universal.ModelsForCounter.BaseModelForSend;

namespace ApiItelma_Universal.ModelsForCounter.Aveos
{
    public class SendMailAveos : SendMailProduct
    {
        public SendMailAveos(ModelForAveos checkBarCodes)
        {
            _date = DateTime.Now;
            _checkBarCodes = checkBarCodes;
            fileName = @"D:\AveosSends\" + checkBarCodes.ProductName + "_" + _date.Day + "_" + _date.Month + "_" + _date.Year + "_" + _date.Hour + "_" + _date.Minute + ".txt";
            body = "Данное письмо содержит номера ICCID и Потребителей " + _checkBarCodes.ProductName + " за " + _date.ToString("dd.MM.yyyy").Replace(".", "_") +
                              " г. в количестве " + _checkBarCodes.AllCount +
                              " шт. \n\r С уважением, робот производственного сервера";
        }

        /// <summary>
        /// Для Aveos
        /// </summary>
        /// <returns></returns>
        public new bool SaveFile()
        {
            foreach (CheckBarCode checkBar in _checkBarCodes.LibsForSend)
            {
                File.AppendAllText(fileName, checkBar.ICCID + ";" + checkBar.Consumer + Environment.NewLine);
            }
            return true;
        }
    }
}
