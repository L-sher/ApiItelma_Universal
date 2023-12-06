using ApiItelma_Universal.ModelsForCounter.BaseModelForSend;

namespace ApiItelma_Universal.ModelsForCounter.Crafter
{
    public class SendMailCrafter : SendMailProduct
    {
        public SendMailCrafter(ModelForCrafter checkBarCodes)
        {
            _date = DateTime.Now;
            _checkBarCodes = checkBarCodes;
            fileName = @"D:\CrafterSends\" + checkBarCodes.ProductName + "_" + _date.Day + "_" + _date.Month + "_" + _date.Year + "_" + _date.Hour + "_" + _date.Minute + ".txt";
            body = "Данное письмо содержит номера ICCID" + _checkBarCodes.ProductName + " за " + _date.ToString("dd.MM.yyyy").Replace(".", "_") +
                              " г. в количестве " + _checkBarCodes.AllCount +
                              " шт. \n\r С уважением, робот производственного сервера";
        }

        /// <summary>
        /// Для Crafter
        /// </summary>
        /// <returns></returns>
        public new bool SaveFile()
        {
            foreach (CheckBarCode checkBar in _checkBarCodes.LibsForSend)
            {
                File.AppendAllText(fileName, checkBar.ICCID + Environment.NewLine);
            }
            return true;
        }
    }
}
