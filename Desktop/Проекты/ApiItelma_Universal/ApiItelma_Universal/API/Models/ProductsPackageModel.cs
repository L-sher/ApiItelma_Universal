using ApiItelma_Universal.ModelsForCounter.BaseModelForSend;

namespace ApiItelma_Universal.API.Models
{
    /// <summary>
    /// Модель данных для генерации серийников коробки
    /// </summary>
    public class ProductsPackageModel
    {
        public List<CheckBarCode> BarCodes { get; set; }
        public string PackageSerial { get; set; }
        public DateTime Date { get; set; }
        public string Errors { get; set; }
    }
}
