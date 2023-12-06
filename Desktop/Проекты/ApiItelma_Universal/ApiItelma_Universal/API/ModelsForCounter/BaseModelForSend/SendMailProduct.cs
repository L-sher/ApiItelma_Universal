using System.Net;
using System.Net.Mail;

namespace ApiItelma_Universal.ModelsForCounter.BaseModelForSend
{
    public class SendMailProduct
    {
        internal ModelForProduct _checkBarCodes;
        internal string fileName { get; set; }
        internal DateTime _date { get; set; }
        internal string body { get; set; } 
        
        public bool saveForFile()//Сохранение данных в файл
        {
            if (_checkBarCodes == null)
            {
                return false;
            }
            if (_checkBarCodes.LibsForSend == null)
            {
                return false;
            }
            return SaveFile();
        }

        /// <summary>
        /// Для Cherry
        /// </summary>
        /// <returns></returns>
        public bool SaveFile()
        {
            foreach (CheckBarCode checkBar in _checkBarCodes.LibsForSend)
            {
                File.AppendAllText(fileName, checkBar.ICCID + ";" + checkBar.IMEI + Environment.NewLine);
            }
            return true;
        }

        public string SendMail()//Отправка данных писмом
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("kt.pacs.beg@itelma.su", _checkBarCodes.ProductName);
                    message.Subject = _checkBarCodes.ProductName + "_" + _date.ToString("dd.MM.yyyy").Replace(".", "_");
                    message.Body = body;
                    message.To.Add(new MailAddress("y.belosorochko@itelma.su", "Белосорочко Юрий"));
                    message.CC.Add(new MailAddress("s.tsurkov@itelma.su", "Цурков Сергей "));
                    message.CC.Add(new MailAddress("a.ilichev@itelma.su", "Ильичев Алексей"));
                    message.Attachments.Add(new Attachment(fileName));
                    SmtpClient client = new SmtpClient("post.itelma.su", 25);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("kt.pacs.beg@itelma.su", "YYwsn9255");
                    client.Send(message);
                }
                return true.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
