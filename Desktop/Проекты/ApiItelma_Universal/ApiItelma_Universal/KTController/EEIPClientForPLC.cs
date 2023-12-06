using ApiItelma_Universal.DBContext.CheckPoints;
using ApiItelma_Universal.SeriLog;
using FluentModbus;
using Newtonsoft.Json;
using Serilog;
using Sres.Net.EEIP;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace ApiItelma_Universal.KTController
{
    public class EEIPClientForPLC
    {
        
        
        private static Serilog.Core.Logger Log_EEIPClientForPLC = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "Log_EEIPClientForPLC.txt").CreateLogger();

        

        bool AllConnected = false;
        public EEIPClientForPLC()
        {
            ConnectToPlC();
        }

        public void ConnectToPlC()
        {
            while (AllConnected == false)
            {
                try
                {
                    //LoadAllPLCDataFromDataBase();
                    ConnectToAllPLC();

                    AllConnected = true;

                    //Распараллеливаем чтение со всех ПЛК
                    //foreach (Camera_Client client in PLC_Clients)
                    //    new Thread(() => { ReadingInputsFromPLC(client); }).Start(); 
                }
                catch (Exception ex)
                {
                    Log_EEIPClientForPLC.Error("Ошибка Подключения к ПЛК " + ex.Message.ToString());
                }
            }
        }

        //Чтение регистров из ПЛК 
        private async Task ReadingInputsFromPLC(Camera_Client client, int InputNumber = 5)
        {
            while (true)
            {
                try
                {
                    //var data = client.PLC_Client_EEIPProtocol.GetAttributeAll(0x72, 1);
                    //if (data[28] != 0)
                    //{
                    //    //получаем Баркод
                    //    //string Barcode = PLCTriggered(client.PLC_Client_EEIPProtocol, client.CameraIpAddress, client.Camera);
                    //    //if (Barcode != "")
                    //    //{
                    //    //    MainAlgorithm.MainRouteTranzitions(Barcode, client.CheckPointName, client.CheckPointLine);
                    //    //}
                    //}
                    Task.Delay(300);
                }
                catch (Exception ex)
                {
                    AllConnected = false;
                   // await PLCReconnect(client);
                    Log_EEIPClientForPLC.Error("Ошибка чтения регистров " + ex.Message.ToString());
                }
            }
        }
        

        //Считываем баркод с камеры
        //private string PLCTriggered(EEIPClient eEIPClient ,string Camera_Ip_Address, ModbusTcpClient cameraModbus)
        //{
        //    try
        //    {
        //        bool SuccessFullyReadMarker = false;

        //        //остановка Конвеера
        //        SetOutputsToPLC(eEIPClient, 0, 5, 0);

        //        //считывание баркода
        //        //string Barcode = ClientForCameras.ReceivingBarcode(Camera_Ip_Address, 2112, cameraModbus); //10.68.25.6:2112 

        //        //Запуск Конвеера
        //        SetOutputsToPLC(eEIPClient, 0, 5, 1);
        //        //return Barcode;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_EEIPClientForPLC.Error("Ошибка передачи команд ПЛК " + ex.Message.ToString());
        //        return "";
        //    }
        //}

        //Полключаемся ко всем ПЛК
        private void ConnectToAllPLC()
        {
            try
            {
                //foreach (var item in PLC_Clients)
                //{
                //    try
                //    {
                //        item.PLC_Client_EEIPProtocol.RegisterSession(item.IPAddress);
                //    }
                //    catch (Exception ex)
                //    {
                //        Log_EEIPClientForPLC.Error($"Ошибка подключения к {item.IPAddress} " + ex.Message.ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log_EEIPClientForPLC.Error("Ошибка подключения к одному из ПЛК команд ПЛК " + ex.Message.ToString());
            }
        }

        //private void ConnectToThisPLC(Camera_Client item)
        //{
        //    item.PLC_Client_EEIPProtocol.RegisterSession(item.IPAddress);
        //}

        private int byte_id { get; set; }
        public void SetOutputsToPLC(EEIPClient PLC_EEip, int Byte_id, int Dataset, int OutputNumber)
        {
            try
            {
                byte[] data = new byte[255];
                byte_id = Byte_id;// Выбираем строку на ПЛК
                data[byte_id] = (byte)OutputNumber;
                PLC_EEip.SetAttributeSingle(0x72, 1, Dataset, data);
            }
            catch (Exception ex)
            {
                Log_EEIPClientForPLC.Error("Ошибка записи регистров в ПЛК " + ex.Message.ToString());
            }
        }

        //Переподключение к PLC
        //private async Task PLCReconnect(Camera_Client PLCToConnect)
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            PLCToConnect.PLC_Client_EEIPProtocol.RegisterSession(PLCToConnect.IPAddress);
        //            break;
        //        }
        //        catch (Exception ex)
        //        {
        //            Log_EEIPClientForPLC.Error("Ошибка Переподключения к ПЛК " + ex.Message.ToString());
        //        }
        //    }
        //}

        //Эмулятор
        static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8881);
        static Socket TCPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
     
        public static void EmulatorRequests()
        {
            try
            {
                TCPSocket.Bind(ipPoint);
                // начинаем прослушивать входящие подключения
                TCPSocket.Listen();

                //Запускаем "общение" с приложением
                HttpServer();
            }
            catch (Exception ex)
            {
            }
        }

        static Stopwatch stopwatch = new Stopwatch();
        static object locker = new object();
        
        static async Task HttpServer()
        {
            while (true)
            {
                try
                {
                    // получаем подключение в виде TcpClient
                    using var tcpClient = await TCPSocket.AcceptAsync();
                    // определяем буфер для получения данных
                    byte[] responseData = new byte[512];
                    int bytes = 0; // количество считанных байтов
                    var response = new StringBuilder(); // для склеивания данных в строку
                    SocketFlags flags = new SocketFlags();

                    // считываем данные 
                    bytes = await tcpClient.ReceiveAsync(responseData, flags);
                    response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                    lock (locker)
                    {
                        MainData ReceivedData = JsonConvert.DeserializeObject<MainData>(response.ToString());
                    // выводим отправленные клиентом данные

                        stopwatch.Restart();
                        MainAlgorithm.MainRouteTranzitions(ReceivedData.Barcode, ReceivedData.CurrentCheckPoint, ReceivedData.LineName);
                        MainAlgorithm.Log_AlgorithmBarcodeCheck.Information(stopwatch.ElapsedMilliseconds.ToString());
                        stopwatch.Stop();
                    }
                }
                catch (Exception ex)
                {
                    MainAlgorithm.Log_AlgorithmBarcodeCheck.Information("Получены неверные данные");
                }
            }
        }

   
    }

    class CameraAndPLCCheckPoint
    {
        public Camera_Client PLC { get; set; }
        public ModbusTcpClient Camera { get; set; }

    }

    class Camera_Client
    {
        //public EEIPClient PLC_Client_EEIPProtocol { get; set; }
        //public string IPAddress { get; set; }
        public ModbusTcpClient Camera { get; set; }
        public string CameraIpAddress { get; set; }
        public string CheckPointName { get; set; }
        public string CheckPointLine { get; set; }
        
        public string Comments { get; set; }
    }

    class MainData
    {
        public string Barcode { get; set; }
        public string CurrentCheckPoint { get; set; }
        public string LineName { get; set; }
    }
}
