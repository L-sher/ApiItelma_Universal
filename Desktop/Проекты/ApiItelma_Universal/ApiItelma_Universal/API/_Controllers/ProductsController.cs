using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.DBContext.Products;
using Microsoft.EntityFrameworkCore;
using ApiItelma_Universal.Services;


namespace ApiItelma_Universal._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        ProductsContext db = new ProductsContext();
        Products_Service products_Service = new Products_Service();

        #region//Методы GET

        #region//Методы GETALL  Получение всех данных из таблиц
        //Получение всех данных из таблицы ProductLabels
        [HttpGet("GetAllFrom_ProductLabels")]
        public async Task<List<ProductLabel>> GetAll_ProductLabels() 
        {
            try
            {
                db.ChangeTracker.Clear();
                return await db.ProductLabels.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
           
        }


        //Получение всех данных из таблицы ProductPackingLists
        [HttpGet("GetAllFrom_ProductPackingLists")]
        public async Task<List<ProductPackingList>> GetAll_ProductPackingLists()
        {
            db.ChangeTracker.Clear(); 
            return await db.ProductPackingLists.ToListAsync();
        }

        //Получение всех данных из таблицы ProductPassports
        [HttpGet("GetAllFrom_ProductPassports")]
        public async Task<List<ProductPassport>> GetAll_ProductPassports() => await db.ProductPassports.ToListAsync();

        //Получение всех данных из таблицы ProductSettings
        [HttpGet("GetAllFrom_ProductSettings")]
        public async Task<List<ProductSetting>> GetAll_ProductSettings() => await db.ProductSettings.ToListAsync();

        //Получение всех данных из таблицы ProductTagParameters
        [HttpGet("GetAllFrom_ProductTagParameters")]
        public async Task<List<ProductTagParameter>> GetAll_ProductTagParameters() => await db.ProductTagParameters.ToListAsync();

        //Получение всех данных из таблицы ProductTags
        [HttpGet("GetAllFrom_ProductTags")]
        public async Task<List<ProductTag>> GetAll_ProductTags() 
        {

            db.ChangeTracker.Clear();
            return await db.ProductTags.ToListAsync();

        }

        #endregion

        //Получение строки данных из ProductLabels по столбцу LabelId
        [HttpGet("GetFrom_ProductLabels_ByLabelId/{LabelId}")]
        public async Task<List<ProductLabel>> ProductLabels_GetByLabelId(int LabelId) => await products_Service.Method_ProductLabels_GetByLabelId(LabelId);

        //Получение строки данных из ProductPackingLists по столбцу PackingListId
        [HttpGet("GetFrom_ProductPackingLists_ByPackingListId/{PackingListId}")]
        public async Task<List<ProductPackingList>> ProductPackingLists_GetByPackingListId(int PackingListId) => await products_Service.Method_ProductPackingLists_GetByPackingListId(PackingListId);

        //Получение строки данных из ProductPassports по столбцу PassportId
        [HttpGet("GetFrom_ProductPassports_ByPassportId/{PassportId}")]
        public async Task<List<ProductPassport>> ProductPassports_GetByPassportId(int PassportId) => await products_Service.Method_ProductPassports_GetByPassportId(PassportId);

        //Получение строки данных из ProductSettings по столбцу Product1C
        [HttpGet("GetFrom_ProductSettings_ByProduct1C/{Product1C}")]
        public async Task<List<ProductSetting>> ProductSettings_GetByProduct1C(string Product1C) => await products_Service.Method_ProductSettings_GetByProduct1C(Product1C);

        //Получение строки данных из ProductTagParameters по столбцу ParametersId
        [HttpGet("GetFrom_ProductTagParameters_ByParametersId/{ParametersId}")]
        public async Task<List<ProductTagParameter>> ProductTagParameters_GetByParametersId(int ParametersId) => await products_Service.Method_ProductTagParameters_GetByParametersId(ParametersId);

        //Получение строки данных из ProductTags по столбцу TagId
        [HttpGet("GetFrom_ProductTags_ByTagId/{TagId}")]
        public async Task<List<ProductTag>> ProductTags_GetByTagId(int TagId) => await products_Service.Method_ProductTags_GetByTagId(TagId);

        #endregion

        #region//Методы Post

        //добавление строки данных в ProductLabels
        [HttpPost("Post_ProductLabels/{JSON_ProductLabels}")]
        public async Task<string> Post_ProductLabels(string JSON_ProductLabels) => await products_Service.Method_ProductLabels_Post(JSON_ProductLabels);

        //добавление строки данных в ProductPackingLists
        [HttpPost("Post_ProductPackingLists/{JSON_ProductPackingLists}")]
        public async Task<string> Post_ProductPackingLists(string JSON_ProductPackingLists) => await products_Service.Method_ProductPackingLists_Post(JSON_ProductPackingLists);

        //добавление строки данных в ProductPassports
        [HttpPost("Post_ProductPassports/{JSON_ProductPassports}")]
        public async Task<string> Post_ProductPassports(string JSON_ProductPassports) => await products_Service.Method_ProductPassports_Post(JSON_ProductPassports);

        //добавление строки данных в ProductSettings
        [HttpPost("Post_ProductSettings/{JSON_ProductSettings}")]
        public async Task<string> Post_ProductSettings(string JSON_ProductSettings) => await products_Service.Method_ProductSettings_Post(JSON_ProductSettings);

        //добавление строки данных в ProductTagParameters
        [HttpPost("Post_ProductTagParameters/{JSON_ProductTagParameters}")]
        public async Task<string> Post_ProductTagParameters(string JSON_ProductTagParameters) => await products_Service.Method_ProductTagParameters_Post(JSON_ProductTagParameters);

        //добавление строки данных в ProductTags
        [HttpPost("Post_ProductTags/{JSON_ProductTags}")]
        public async Task<string> Post_ProductTags(string JSON_ProductTags) => await products_Service.Method_ProductTags_Post(JSON_ProductTags);

        //[HttpPost("Post_ProductTags")]
        //public async Task<string> Post_ProductTags1(string JSON_ProductTags) => await products_Service.Method_ProductTags_Post1(JSON_ProductTags);

        #endregion

        #region//Методы Delete

        //Удаление строки данных из ProductLabels по LabelId и LabelName
        [HttpDelete("DeleteString_ProductLabels/{LabelId}/{LabelName}")]
        public async Task<string> DeleteFrom_ProductLabels(int LabelId, string LabelName)
  => await products_Service.Method_ProductLabels_delete(LabelId, LabelName);

        //Удаление строки данных из ProductPackingLists по PackingListId и PackingListName
        [HttpDelete("DeleteString_ProductPackingLists/{PackingListId}/{LabelName}")]
        public async Task<string> DeleteFrom_ProductPackingLists(int PackingListId, string PackingListName)
  => await products_Service.Method_ProductPackingLists_delete(PackingListId, PackingListName);

        //Удаление строки данных из ProductPassports по PassportId и PassportName
        [HttpDelete("DeleteString_ProductPassports/{PassportId}/{PassportName}")]
        public async Task<string> DeleteFrom_ProductPassports(int PassportId, string PassportName)
  => await products_Service.Method_ProductPassports_delete(PassportId, PassportName);

        //Удаление строки данных из ProductSetting по ProductId и Product1C
        [HttpDelete("DeleteString_ProductSetting/{ProductId}/{Product1C}")]
        public async Task<string> DeleteFrom_ProductSetting(int ProductId, string Product1C)
  => await products_Service.Method_ProductSetting_delete(ProductId, Product1C);

        //Удаление строки данных из ProductTagParameters по ParametersId и TagParametersName
        [HttpDelete("DeleteString_ProductTagParameters/{ParametersId}/{TagParametersName}")]
        public async Task<string> DeleteFrom_ProductTagParameters(int ParametersId, string TagParametersName)
  => await products_Service.Method_ProductTagParameters_delete(ParametersId, TagParametersName);

        //Удаление строки данных из ProductTags по TagId и TagName
        [HttpDelete("DeleteString_ProductTags/{TagId}/{TagName}")]
        public async Task<string> DeleteFrom_ProductTags(int TagId, string TagName)
  => await products_Service.Method_ProductTags_delete(TagId, TagName);

        #endregion
    }
}
