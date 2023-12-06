using ApiItelma_Universal.API.Models;
using ApiItelma_Universal.DBContext.CheckPoint_Packaging;
using Newtonsoft.Json;


namespace ApiItelma_Universal.Services.Additional_Classes
{

    public class SerialGeneratorClass
    {
        string SymbolCollection = ("0123456789ABCDEFGHJKLMNPQRSTUVWXYZ");

        ProductsPackageModel packageModel { get; set; }
        public string Result { get; set; }
        public SerialGeneratorClass(ProductsPackageModel box)
        {
            packageModel = box;
            CreateBox();
        }

        private void CreateBox()
        {
            try
            {
                using (CheckPointPackagingContext Db = new CheckPointPackagingContext())
                {
                    // Запрос на получение последней записи ProductsPackage  
                    var Box = Db.CheckPointDataSerials.ToList();
                    var req = Box.Last();
                    // Генерация серийного номера для новой коробки
                    string newSerial = GenerateSerial(req.Serial);
                    packageModel.PackageSerial = newSerial;

                    // Запись данных о новой коробке в БД
                    CheckPointDataSerial newBox = new CheckPointDataSerial();
                    newBox.Serial = newSerial;
                    newBox.ProductId = packageModel.BarCodes.FirstOrDefault().ProductId;
                    newBox.Date = packageModel.Date;
                    Db.CheckPointDataSerials.Add(newBox);

                    // Запись баркодов изделий упакованных в коробку с сгенерированным серийным номером в БД
                    foreach (var checkBarCode in packageModel.BarCodes)
                    {
                        CheckPointDataPackage barCode = new CheckPointDataPackage();
                        barCode.BarCode = checkBarCode.BarCode;
                        barCode.PackageSerial = newBox.Serial;
                        Db.CheckPointDataPackages.Add(barCode);
                    }
                    Db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                packageModel.Errors = ex.ToString();
            }

            Result = JsonConvert.SerializeObject(packageModel);
        }

        /// <summary>
        /// Генерация нового серийного номера
        /// </summary>
        /// <param name="NowString">Последний серийный номер в таблице</param>
        /// <returns></returns>
        private string GenerateSerial(string NowString)
        {
            var resultStr = NowString;
            var chars = new List<char>();
            bool AddingTrue = false;
            for (int i = NowString.Length - 1; i >= 0; i--)
            {
                var ch = resultStr[i];
                if (!AddingTrue)
                {
                    if (SymbolCollection.IndexOf(ch) >= SymbolCollection.Length - 1)
                    {
                        chars.Add('0');
                    }
                    else
                    {
                        chars.Add(SymbolCollection[SymbolCollection.IndexOf(ch) + 1]);
                        AddingTrue = true;
                    }
                }
                else
                {
                    chars.Add(NowString[i]);
                }
            }
            resultStr = "";
            foreach (var ch in chars)
            {
                resultStr = ch + resultStr;
            }
            return resultStr;
        }
    }
}
