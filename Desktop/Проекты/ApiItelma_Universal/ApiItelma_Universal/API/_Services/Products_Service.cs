using ApiItelma_Universal.DBContext.Products;
using ApiItelma_Universal.SeriLog;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ApiItelma_Universal.Services
{
    public class Products_Service
    {
        static ProductsContext db = new ProductsContext();
        private Serilog.Core.Logger Log_Products = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_Products.txt").CreateLogger();
        #region//Get Метод
        //Получение строки данных из ProductLabels по столбцу LabelId
        public async Task<List<ProductLabel>> Method_ProductLabels_GetByLabelId(int LabelId)
        {
            try
            {
                return await db.ProductLabels.Where(e => e.LabelId == LabelId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductLabels_GetByLabelId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из ProductPackingLists по столбцу PackingListId
        public async Task<List<ProductPackingList>> Method_ProductPackingLists_GetByPackingListId(int PackingListId)
        {
            try
            {
                return await db.ProductPackingLists.Where(e => e.PackingListId == PackingListId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductPackingLists_GetByPackingListId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из ProductPassports по столбцу PassportId
        public async Task<List<ProductPassport>> Method_ProductPassports_GetByPassportId(int PassportId)
        {
            try
            {
                return await db.ProductPassports.Where(e => e.PassportId == PassportId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductPassports_GetByPassportId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }


        //Получение строки данных из ProductSettings по столбцу Product1C
        public async Task<List<ProductSetting>> Method_ProductSettings_GetByProduct1C(string Product1C)
        {
            try
            {
                return await db.ProductSettings.Where(e => e.Product1C == Product1C).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductSettings_GetByProduct1C В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из ProductTagParameters по столбцу ParametersId
        public async Task<List<ProductTagParameter>> Method_ProductTagParameters_GetByParametersId(int ParametersId)
        {
            try
            {
                return await db.ProductTagParameters.Where(e => e.ParametersId == ParametersId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductTagParameters_GetByParametersId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из ProductTags по столбцу TagId
        public async Task<List<ProductTag>> Method_ProductTags_GetByTagId(int TagId)
        {
            try
            {
                return await db.ProductTags.Where(e => e.TagId == TagId).ToListAsync();
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductTags_GetByTagId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }

        //Получение строки данных из ProductSettings по столбцу ProductId
        public async Task<ProductSetting> Method_ProductLabels_GetByProductId(int? ProductId)
        {
            try
            {
                return await db.ProductSettings.FirstOrDefaultAsync(e => e.ProductId == ProductId);
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка выполнения метода Method_ProductLabels_GetByLabelId В Контроллере Products " + e.Message.ToString());
                return null;
            }
        }
        #endregion

        #region//Post Метод
        //Добавляем строку данных в ProductLabels
        public async Task<string> Method_ProductLabels_Post(string JSON_ProductLabels)
        {
            try
            {
                ProductLabel jsonData = JsonConvert.DeserializeObject<ProductLabel>(JSON_ProductLabels);
                db.ProductLabels.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductLabels_Post успешно " + jsonData.LabelId.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductLabels_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProductPackingLists
        public async Task<string> Method_ProductPackingLists_Post(string JSON_ProductPackingLists)
        {
            try
            {
                ProductPackingList jsonData = JsonConvert.DeserializeObject<ProductPackingList>(JSON_ProductPackingLists);
                db.ProductPackingLists.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductPackingLists_Post успешно " + jsonData.PackingListName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductPackingLists_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProductPassports
        public async Task<string> Method_ProductPassports_Post(string JSON_ProductPassports)
        {
            try
            {
                ProductPassport jsonData = JsonConvert.DeserializeObject<ProductPassport>(JSON_ProductPassports);
                db.ProductPassports.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductPassports_Post успешно " + jsonData.PassportName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductPassports_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProductSettings
        public async Task<string> Method_ProductSettings_Post(string JSON_ProductSettings)
        {
            try
            {
                ProductSetting jsonData = JsonConvert.DeserializeObject<ProductSetting>(JSON_ProductSettings);
                db.ProductSettings.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductSettings_Post успешно " + jsonData.Product1C.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductSettings_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProductTagParameters
        public async Task<string> Method_ProductTagParameters_Post(string JSON_ProductTags)
        {
            try
            {
                ProductTagParameter jsonData = JsonConvert.DeserializeObject<ProductTagParameter>(JSON_ProductTags);
                db.ProductTagParameters.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductTagParameters_Post успешно " + jsonData.TagParametersName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductTagParameters_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Добавляем строку данных в ProductTags
        public async Task<string> Method_ProductTags_Post(string JSON_ProductTags)
        {
            try
            {
                ProductTag jsonData = JsonConvert.DeserializeObject<ProductTag>(JSON_ProductTags);
                db.ProductTags.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductTags_Post успешно " + jsonData.TagName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductTags_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        public async Task<string> Method_ProductTags_Post1(string JSON_ProductTags)
        {


            try
            {
                ProductTag jsonData = JsonConvert.DeserializeObject<ProductTag>(JSON_ProductTags);
                db.ProductTags.Add(jsonData);
                db.SaveChanges();
                Log_Products.Information("Method_ProductTags_Post успешно " + jsonData.TagName.ToString());
                return "OK";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductTags_Post В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion


        #region//Delete метод

        //Удаление строки данных из ProductLabels по LabelId и LabelName
        public async Task<string> Method_ProductLabels_delete(int LabelId, string LabelName)
        {
            try
            {
                db.ProductLabels.Where(e => e.LabelId == LabelId & e.LabelName == LabelName).ExecuteDelete();
                Log_Products.Information("Method_ProductLabels_delete успешно " + LabelId + " " + LabelName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductLabels_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProductPackingLists по PackingListId и PackingListName
        public async Task<string> Method_ProductPackingLists_delete(int PackingListId, string PassportName)
        {
            try
            {
                db.ProductPackingLists.Where(e => e.PackingListId == PackingListId & e.PackingListName == PassportName).ExecuteDelete();
                Log_Products.Information("Method_ProductPackingLists_delete успешно " + PackingListId + " " + PassportName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductPackingLists_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProductPassports по PassportId и PassportName
        public async Task<string> Method_ProductPassports_delete(int PassportId, string PassportName)
        {
            try
            {
                db.ProductPassports.Where(e => e.PassportId == PassportId & e.PassportName == PassportName).ExecuteDelete();
                Log_Products.Information("Method_ProductPassports_delete успешно " + PassportId + " " + PassportName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductPassports_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProductSetting по ProductId и Product1C
        public async Task<string> Method_ProductSetting_delete(int ParametersId, string TagParametersName)
        {
            try
            {
                db.ProductSettings.Where(e => e.ProductId == ParametersId & e.Product1C == TagParametersName).ExecuteDelete();
                Log_Products.Information("Method_ProductSetting_delete успешно " + ParametersId + " " + TagParametersName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductSetting_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProductTagParameters по ParametersId и TagParametersName
        public async Task<string> Method_ProductTagParameters_delete(int ParametersId, string TagParametersName)
        {
            try
            {
                db.ProductTagParameters.Where(e => e.ParametersId == ParametersId & e.TagParametersName == TagParametersName).ExecuteDelete();
                Log_Products.Information("Method_ProductTagParameters_delete успешно " + ParametersId + " " + TagParametersName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductTagParameters_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        //Удаление строки данных из ProductTags по TagId и TagName
        public async Task<string> Method_ProductTags_delete(int TagId, string TagName)
        {
            try
            {
                db.ProductTags.Where(e => e.TagId == TagId & e.TagName == TagName).ExecuteDelete();
                Log_Products.Information("Method_ProductTags_delete успешно " + TagId + " " + TagName + " Удалено");
                return "Deleted Successfully";
            }
            catch (Exception e)
            {
                Log_Products.Error("Ошибка Method_ProductTags_delete В Контроллере Products" + e.Message.ToString());
                return e.Message.ToString();
            }
        }

        #endregion
    }
}
