using ApiItelma_Universal.API._Services;
using ApiItelma_Universal.DataContext.Traceability;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiItelma_Universal.API._Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceabilityController : Controller
    {
        TraceabilityContext db = new TraceabilityContext();

        Traceability_Service tracebility_Service = new Traceability_Service();

        #region//Методы GET

        // Получение всех данных из Global
        [HttpGet("GetAllFrom_Global")]
        public async Task<List<Global>> Global_GetAll() => await db.Globals.ToListAsync();

        // Получение строки данных из Global по столбцу BarCode
        [HttpGet("GetFrom_Global_ByBarCode/{BarCode}")]
        public async Task<Global> Global_GetByBarCode(string BarCode) => await tracebility_Service.Method_Global_GetByBarCode(BarCode);

        #endregion

        #region//Метод Post

        //добавление строки данных в Global
        [HttpPost("Post_Global/{JSON_Global}")]
        public async Task<string> Post_Global(string JSON_Global) => await tracebility_Service.Method_Global_Post(JSON_Global);

        #endregion

        #region//Метод Delete

        //Удаление строки данных из Global по Barcode и ProductId
        [HttpDelete("DeleteString_Global/{Barcode}/{ProductId}")]
        public async Task<string> DeleteFrom_Global(string Barcode, string ProductId)
  => await tracebility_Service.Method_Global_delete(Barcode, ProductId);

        #endregion
    }
}
