namespace ApiItelma_Universal.ModelsForCounter.BaseModelForSend
{
    public class ModelForProduct
    {
        public int? AllCount { get; set; }
        public string ProductName { get; set; }
        public List<CheckBarCode> LibsForSend { get; set; }
    }   
    public class CheckBarCode
    {
        public string Server { get; set; }
        public string BarCode { get; set; }
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public string Serial { get; set; }
        public string ICCID { get; set; }
        public string ICCID_VIM { get; set; }
        public string IMEI { get; set; }
        public string VIN { get; set; } //Для БЭГ Лизинг
        public string Consumer { get; set; } //Для БЭГ Увэос
    }
}
