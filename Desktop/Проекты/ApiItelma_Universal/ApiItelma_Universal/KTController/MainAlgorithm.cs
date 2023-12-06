using ApiItelma_Universal.API._Services;
using ApiItelma_Universal.DataContext.Traceability;
using ApiItelma_Universal.DBContext.ProductionLines;
using ApiItelma_Universal.DBContext.Products;
using ApiItelma_Universal.DataContext.Traceability;
using ApiItelma_Universal.SeriLog;
using ApiItelma_Universal.Services;
using Newtonsoft.Json;
using Serilog;
using System.Net.Sockets;
using System.Text;

namespace ApiItelma_Universal.KTController
{

    public class MainAlgorithm
    {
        static TraceabilityContext dbTrace = new TraceabilityContext();

        //Использую Логгер только для Системы мониторинга производства
        public static Serilog.Core.Logger Log_AlgorithmBarcodeCheck = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_AlgorithmBarcodeCheck.txt").CreateLogger();

        static ProductionLinesContext productionLineDBContext = new ProductionLinesContext();
        public static List<DeviceOnTheLineOrder> DevicesOnTheLineOrder { get; set; } = new();

        //Булева определяющая есть ли баркод в базе
        static bool ThisBarcodeAlreadyInDB = false;
        //для определения ProductId на текущей линии
        static int? LineProductId = 0;

        static Global DBBarcode=new Global();
        static int KTOrderNumber=-1;
        //Id Текущей КТ
        static int CurrentCheckPointID = 0;

        //Последняя ли это КТ?
        static bool IsThisTheLastCheckpointInTheRoute=false;

        //Производственный процесс на нашей линии
        static ProcessesAndSetting currentProcess = new ProcessesAndSetting();

        static bool errorWasDetected = false;
        //Главный метод по которому движемся
        public static async Task MainRouteTranzitions(string ReceivedBarcode, string ActualKT, string CurrentLine)
        {
            try
            {
                errorWasDetected = false;
                CollectData(ReceivedBarcode, ActualKT, CurrentLine);

                if (errorWasDetected){ return;}

                if (KTOrderNumber == -1) { ErrorWrongKT("0x0005"); DisposeParameters(); return; }

                //Проходим через КТ-0
                //Записываем новый баркод в БД,
                if (KTOrderNumber == 0){ KT_V2(ReceivedBarcode); }

                if (errorWasDetected) { return; }

                //Проверка маршрута баркода
                if (KTOrderNumber != 0)
                {
                    if (!ThisBarcodeAlreadyInDB){ ErrorBarcodeIsNotInDB("0x00010"); return; }
                    CheckBarcodeRoute(ReceivedBarcode, DBBarcode.LastCheckPointId);
                }

                if (errorWasDetected) { return; }

                Global DataToSend = new Global() { BarCode = ReceivedBarcode, ProductId = LineProductId, Result = IsThisTheLastCheckpointInTheRoute, LastCheckPointId = CurrentCheckPointID, CheckTime = DateTime.Now };
                SendUpdatedDataToTraceabilityTable(DataToSend);
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information(ex.Message + "MainRouteTranzitions");
            }
        }
     
        private static void ErrorWrongKT(string inf)
        {
            ErrorToDatabase("Неверный маршрут оборудования");
            Log_AlgorithmBarcodeCheck.Information($"ErrorWrongKT {inf}");
        }
        private static void ErrorTheDeviceIsNotInTheRoute(string inf)
        {
            ErrorToDatabase("Оборудования нет в маршруте");
            Log_AlgorithmBarcodeCheck.Information($"ErrorTheDeviceIsNotInTheRoute {inf}");
        }
        private static void ErrorBarcodeIsAlreadyFinishedRoute(string inf)
        {
            ErrorToDatabase("Баркод уже закончил маршрут");
            Log_AlgorithmBarcodeCheck.Information($"ErrorBarcodeIsAlreadyFinishedRoute {inf}");
        }
        private static void ErrorBarcodeIsNotInDB(string inf)
        {
            ErrorToDatabase("Баркода нет базе, однако он прошел дальше КТ-0");
            Log_AlgorithmBarcodeCheck.Information($"ErrorBarcodeIsNotInDB {inf}");
        }
        private static void ErrorToDatabase(string ErrorMessage)
        {
            errorWasDetected = true;
            if (DBBarcode != null)
            {
                SaveErrorToDB(ErrorMessage);
                SendUpdatedDataToTraceabilityTable(DBBarcode);
            }
        }

        public static void CheckBarcodeRoute(string ReceivedBarcode, int LastCheckPointPassed)
        {
            try
            {
                if (KTOrderNumber == DevicesOnTheLineOrder.FirstOrDefault(e=>e.CheckPointId == LastCheckPointPassed).Order+1)
                {
                    SaveToDB(ReceivedBarcode);
                    
                }
                else
                {
                    ErrorWrongKT("0x00001");
                    //Сообщение об ошибке
                }
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information(ex.Message + "CheckBarcodeRoute");
            }
        }

        //Записываем новый баркод в БД, либо выводим сообщение об ошибке
        public static void KT_V2(string ReceivedBarcode)
        {
            try
            {
                if (!ThisBarcodeAlreadyInDB)
                {
                    SaveToDB(ReceivedBarcode);
                }
                else
                {
                    ErrorWrongKT("0x00001");
                }
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information(ex.Message + "KT_V2");
            }
        }

        private static void SaveToDB(string ReceivedBarcode)
        {
            try
            {
                DBBarcode = dbTrace.Globals.FirstOrDefault(e => e.BarCode == ReceivedBarcode);
                if (DBBarcode == null)
                {
                    SaveNewBarcode(ReceivedBarcode);
                }
                else
                {
                    UpdateExistingBarcode(ReceivedBarcode);
                }
                
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information("Ошибка во время работы с БД"+ ex.Message);
            }
        }

        private static void SaveNewBarcode(string ReceivedBarcode)
        {
            try
            {
                dbTrace.Globals.Add(new Global() { BarCode = ReceivedBarcode, CheckTime = DateTime.Now, ProductId = LineProductId, Result = false, LastCheckPointId = CurrentCheckPointID, LastCheckPointErrors = "" });
                dbTrace.SaveChanges();
                dbTrace.ChangeTracker.Clear();
                Log_AlgorithmBarcodeCheck.Information($"Баркод {ReceivedBarcode} успешно добавлен в БД");
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"SaveNewBarcode"+ex.Message.ToString());
            }
        }

        private static void UpdateExistingBarcode(string ReceivedBarcode)
        {
            try
            {
                DBBarcode.LastCheckPointId = CurrentCheckPointID;
                DBBarcode.Result = IsThisTheLastCheckpointInTheRoute;
                DBBarcode.ProductId = LineProductId;
                DBBarcode.LastCheckPointErrors = "";
                Log_AlgorithmBarcodeCheck.Information($"Баркод {ReceivedBarcode} обновлен успешно");
                dbTrace.SaveChanges();
                dbTrace.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"UpdateExistingBarcode"+ ex.Message.ToString() );
            }
            
        }
        private static void SaveErrorToDB(string error)
        {
            try
            {
                DBBarcode.LastCheckPointErrors = error;
                dbTrace.SaveChanges();
                dbTrace.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"SaveErrorToDB" + ex.Message.ToString());
            }
        }


        //Определяем необходимые переменные
        private static void CollectData(string ReceivedBarcode, string ActualKT, string CurrentLine)
        {
            try
            {
                DBBarcode = dbTrace.Globals.FirstOrDefault(e => e.BarCode == ReceivedBarcode);
                if (DBBarcode != null && DBBarcode.Result)
                { ErrorBarcodeIsAlreadyFinishedRoute("0x00004"); DisposeParameters(); return; }

                DefineActualKTOrderNumber(ActualKT, CurrentLine);
                if (KTOrderNumber == -1) { DisposeParameters(); return; }

                ThisBarcodeAlreadyInDB = DBBarcode != null;

                LineProductId = int.Parse(currentProcess.ProductId);
                //Определяем последняя ли это КТ
                IsThisTheLastCheckpointInTheRoute = KTOrderNumber == DevicesOnTheLineOrder.Count - 1 ? true : false;
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"SaveErrorToDB" + ex.Message.ToString());
            }
        }

        //Определяем порядок текущего КТ в Маршруте изделия на Линии, получаем ProductId Изделия, Result (если 1 то баркод уже завершил свой маршрут)
        private static void DefineActualKTOrderNumber(string ActualKT, string CurrentLine)
        {
            try
            {
                //Находим какой сейчас производственный процесс на линии
                currentProcess = productionLineDBContext.ProcessesAndSettings.FirstOrDefault(e => e.ProcessName == productionLineDBContext.LineSettings.FirstOrDefault(e => e.LineName == CurrentLine).LineProcess);

                //Получаем список всех точек которые включены(IsEnabled=true)
                DevicesOnTheLineOrder = JsonConvert.DeserializeObject<List<DeviceOnTheLineOrder>>(productionLineDBContext.ProcessesAndSettings.FirstOrDefault(e => e.ProcessName == currentProcess.ProcessName).ProductRoute).Where(e=>e.IsEnabled==true).ToList();

                if(DevicesOnTheLineOrder.FirstOrDefault(e => e.Name == ActualKT) != null)
                {
                    KTOrderNumber = DevicesOnTheLineOrder.FirstOrDefault(e => e.Name == ActualKT).Order;
                    CurrentCheckPointID = DevicesOnTheLineOrder.FirstOrDefault(e => e.Name == ActualKT).CheckPointId;
                }
                else
                {
                    ErrorTheDeviceIsNotInTheRoute("0x00003");
                    DisposeParameters();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorTheDeviceIsNotInTheRoute("0x00007");
                DisposeParameters();
                Log_AlgorithmBarcodeCheck.Information($"SaveErrorToDB" + ex.Message.ToString());
                return;
            }
        }

        //Отправка данных в таблицу Traceability на сайте
        private static async void SendUpdatedDataToTraceabilityTable(Global DataToSend)
        {
            try
            {
                using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await tcpClient.ConnectAsync("192.168.96.139", 8882);

                var message = JsonConvert.SerializeObject(DataToSend);
                SocketFlags flags = new SocketFlags();

                // считыванием строку в массив байт
                byte[] requestData = Encoding.UTF8.GetBytes(message);

                // отправляем данные
                await tcpClient.SendAsync(requestData, flags);
                DisposeParameters();
                Log_AlgorithmBarcodeCheck.Information($"Баркод отправлен Result= {IsThisTheLastCheckpointInTheRoute},CurrentCheckPointID= {CurrentCheckPointID} ");
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"Ошибка SendUpdatedDataToTraceabilityTable {ex.Message}");
            }
        }


        private static void DisposeParameters()
        {
            try
            {
                KTOrderNumber = -1;
                IsThisTheLastCheckpointInTheRoute = false;
            }
            catch (Exception ex)
            {
                Log_AlgorithmBarcodeCheck.Information($"Ошибка DisposeParameters {ex.Message}");
            }
        } 

        //
        //public static void KT0Stage(string ReceivedBarcode, int ActualKT, int LineNumber)
        //{
        //    if (ActualKT != 1) { return; }

        //    //Получаем настройки линии
        //    //LineSetting ThisLineSettings = AsyncHelper.RunSync(() => ProductionLines_Service.Method_LineSettings_GetByLineNumber(LineNumber));

        //    //Проверка есть ли такой баркод в базе
        //    Global ThisBarcode = AsyncHelper.RunSync(() => Traceability_Service.Method_Global_GetByBarCode(ReceivedBarcode));

        //    //Получаем Маршрут для изделия на линии, записываем в массив Steps.
        //    //ProductSetting ThisProductToGetRoute = AsyncHelper.RunSync(() => Products_Service.Method_ProductLabels_GetByProductId(ThisLineSettings.LineProductId));

        //    //string[] Steps = ThisProductToGetRoute.ProductRoute.Split(";");

        //    //если нет такого баркода
        //    if (ThisBarcode == null)
        //    {
        //        //проверяем что следующая контрольная точка не пуста или не равна null
        //        //if (Steps[Array.IndexOf(Steps, ActualKT.ToString()) + 1].ToString() != null | Steps[Array.IndexOf(Steps, ActualKT.ToString()) + 1].ToString() != "")
        //        {
        //            //Если это не конец маршрута
        //            ThisBarcode = new Global()
        //            {
        //                LastCheckPointId = ActualKT,
        //               // NextCheckPoint = int.Parse(Steps[Array.IndexOf(Steps, ActualKT.ToString()) + 1]),
        //                BarCode = ReceivedBarcode.Substring(0, ReceivedBarcode.Length),
        //               // ProductId = ThisLineSettings.LineProductId,
        //                CheckTime = DateTime.Now
        //            };
        //        }
        //        else
        //        {
        //            //если конец маршрута
        //            ThisBarcode = new Global()
        //            {
        //                LastCheckPointId = ActualKT,
        //                Result = true,
        //                NextCheckPoint = ActualKT,
        //                BarCode = ReceivedBarcode.Substring(0, ReceivedBarcode.Length),
        //               // ProductId = ThisLineSettings.LineProductId,
        //                CheckTime = DateTime.Now
        //            };

        //        }
        //        try
        //        {

        //            dbTrace.Globals.Add(ThisBarcode);

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        // Проверка Шага Баркода
        //        // Если проверка успешна то записываем текущую точку как ту, что мы прошли
        //        //CheckBarCodeSteps(ThisBarcode, ActualKT, Steps);
        //    }
        //    //Сохранение в БД
        //    dbTrace.SaveChanges();
        //    dbTrace.ChangeTracker.Clear();
        //}


        ////Проверка что Изделие идет по заданному маршруту.
        //private static void CheckBarCodeSteps(Global ThisBarcode, int actualKT, string[] Steps)
        //{
        //    try
        //    {

        //        for (int i = 0; i <= Steps.Length - 1; i++)
        //        {
        //            if (Steps[i] == ThisBarcode.LastCheckPointId.ToString())
        //            {
        //                if (Steps[i + 1] == ThisBarcode.NextCheckPoint.ToString() & ThisBarcode.NextCheckPoint == actualKT)
        //                {
        //                    //Если мы на последней точке маршрута исходя из таблицы ProductCheckPointSettings то result=true
        //                    if (i + 1 == Steps.Length - 1)
        //                    {
        //                        ThisBarcode.Result = true;
        //                    }
        //                    else
        //                    {
        //                        ThisBarcode.NextCheckPoint = int.Parse(Steps[i + 2]);
        //                    }
        //                    //Если проверка успешна то записываем текущую точку как ту, что мы прошли
        //                    ThisBarcode.LastCheckPointId = actualKT;
        //                    break;

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_AlgorithmBarcodeCheck.Information("Ошибка Проверки Баркода " + ex.Message.ToString());
        //    }

        //}




        public static class AsyncHelper
        {
            private static readonly TaskFactory _taskFactory = new
                TaskFactory(CancellationToken.None,
                            TaskCreationOptions.None,
                            TaskContinuationOptions.None,
                            TaskScheduler.Default);

            public static TResult RunSync<TResult>(Func<Task<TResult>> func)
                => _taskFactory
                    .StartNew(func)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();

            public static void RunSync(Func<Task> func)
                => _taskFactory
                    .StartNew(func)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();
        }

    }
    public class DeviceOnTheLineOrder
    {
        public int Order { get; set; }
        public int CheckPointId { get; set; }
        public string Name { get; set; } = "";
        public bool IsDragOver { get; set; }
        public bool IsEnabled { get; set; }
    }
}
