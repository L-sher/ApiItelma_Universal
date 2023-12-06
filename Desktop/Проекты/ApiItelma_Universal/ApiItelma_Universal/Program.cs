using ApiItelma_Universal.DBContext.CheckPoint_Packaging;
using ApiItelma_Universal.DBContext.CheckPoint_Metra;
using Serilog;
using ApiItelma_Universal.SeriLog;
using ApiItelma_Universal.KTController;
using ApiItelma_Universal.API._Services;
using ApiItelma_Universal.Services;
using ApiItelma_Universal.DBContext.LabelSetting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
Log.Logger = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "API_Log.txt").CreateLogger();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<CheckPointMetraContext>();
builder.Services.AddSingleton<CheckPointPackagingContext>();
builder.Services.AddSingleton<LabelSettingContext>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    //для того чтобы открывался сайт сразу на swager UI
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

Log.Logger.Information("API Работает http://localhost:5139/swagger/index.html");

new Thread(ClientForCameras.ConnectingToAllCameras).Start();

//Подключение к ПЛК
//new Thread(EEIPClientForPLC.ConnectToPlC).Start();

//Эмулятор ПЛК
//new Thread(EEIPClientForPLC.EmulatorRequests).Start();

app.Run();


//Для ускорения операций с бд предварительно загрузим данные
//void DBContextsLoad()
//{

//    Parallel.Invoke(
//        () =>
//        {
//            ProductionLines_Service.ProductionLinesContextLoad();
//        },
//        () =>
//        {
//            Products_Service.ProductsContextLoad();
//        },
//        () =>
//        {
//            Traceability_Service.TraceabilityContextLoad();
//        },
//        () =>
//        {
           
//        },
//        () =>
//        {
//            CheckPoints_Service.CheckPoints_ServiceLoad();
//        },
//        () =>
//        {
//            CheckPoint_Metra_Service.CheckPoint_Metra_ServiceLoad();
//        },
//        () =>
//        {
//            CheckPoint_AutoSend_Service.CheckPoint_AutoSend_ServiceLoad();
//        }
//        );

//}