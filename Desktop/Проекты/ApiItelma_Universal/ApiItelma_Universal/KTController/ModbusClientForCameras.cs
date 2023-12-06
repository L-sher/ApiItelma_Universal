using FluentModbus;
using Serilog;
using System.Net.Sockets;
using System.Text;
using ApiItelma_Universal.SeriLog;
using ApiItelma_Universal.DBContext.CheckPoints;
using System.Collections;
using System;
using System.Diagnostics;

namespace ApiItelma_Universal.KTController
{
    public class ClientForCameras
    {

        //Использую Логгер только для Системы мониторинга производства
        private static Serilog.Core.Logger Log_ModbusClientForCameras = new LoggerConfiguration().WriteTo.File(new MyOwnCompactJsonFormatter(), "SMP_Log.txt").CreateLogger();
        //Модбас клиент для по
        //private static ModbusTcpClient CameraWatchDogKT0 = new ModbusTcpClient();

        //Список Камер
        //public static List<ModbusTcpClient> clients = new List<ModbusTcpClient>() { CameraWatchDogKT0 };


        //Словарь экземпляр камеры + ip адрес+ порт + Назначение
        //public static Dictionary<ModbusTcpClient, string[]> Camera_Client = new Dictionary<ModbusTcpClient, string[]>()
        //{
        //    { CameraWatchDogKT0, new string[]{ "10.68.25.2", "Камера Кт0" } }
        //};

        //Подключаемся ко всем камерам
        static bool AllConnected = false;

        //Список всех подключаемых камер к системе прослеживаемости
        static List<Camera_Client> Camera_Clients = new List<Camera_Client>(){};
        
        //Подключение к базе данных CheckPoints
        static CheckPointsContext dbCheckPoints = new CheckPointsContext();

        //Подключение ко всем камерам в отдельных потоках
        public static void ConnectingToAllCameras()
        {
            while (AllConnected == false)
            {
                Thread.Sleep(200);
                try
                {
                    LoadAllCameraDataFromDataBase();
                    foreach(var item in Camera_Clients)
                    {
                        try
                        {
                            new Thread(()=> ReceivingBarcode(item.CameraIpAddress, 2112, item.Camera, item.CheckPointName, item.CheckPointLine)).Start();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    AllConnected = true;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                    Log_ModbusClientForCameras.Error("Ошибка Подключения к Камере " + ex.Message.ToString());
                }
            }
           
        }


        static object locker = new object();
        static Stopwatch ForCountingBarcodes = new Stopwatch();
        //Прочитываю данные с камеры, после тригера на ПЛК, передаю дальше полученный баркод
        public static void ReceivingBarcode(string IpAddress, int port, ModbusTcpClient CameraModbus, string CheckPointName, string CheckPointLine)
        {

            string lastBarcode = "";
            TcpClient sickTCPClient = new TcpClient();
            string c = "";
            //на случай если Камера отключится, чтобы переподключить
            while (true)
            {
                try
                {
                    //подключаемся к камере
                    sickTCPClient.Connect(IpAddress, 2112);

                    //подключаемся по TCP для получения данных
                    NetworkStream net = sickTCPClient.GetStream();
                    int numberOfReadBarcodes = 0;

                    while (true)
                    {

                        var builder = new StringBuilder();

                        // CameraModbus.Connect(IpAddress);
                        //TCP Соединение
                        lock (locker)
                        {
                            try
                            {

                                byte[] responseBytes = new byte[100];
                                int bytes = 0;
                                net.Read(responseBytes, 0, responseBytes.Length);
                                
                                string Barcode = Encoding.UTF8.GetString(responseBytes);
                                string convertBarcode = Barcode.Replace("\u0000", "");

                                if (convertBarcode == "" || convertBarcode.Contains("NoRead"))
                                {
                                    //Для просмотра через логах
                                    Log_ModbusClientForCameras.Error($"Результат чтения : {convertBarcode}");
                                    CameraModbus.Dispose(); Thread.Sleep(200); continue;
                                }
                                if (convertBarcode == lastBarcode) { continue; }
                                lastBarcode = convertBarcode;
                                if (ForCountingBarcodes.ElapsedMilliseconds != 0)
                                {
                                    if (ForCountingBarcodes.ElapsedMilliseconds < 2000)
                                    {
                                        Log_ModbusClientForCameras.Error($"Новый Баркод получен: {convertBarcode}");
                                        numberOfReadBarcodes++;
                                    }
                                    else
                                    {
                                        if (numberOfReadBarcodes < 3)
                                        {
                                            Log_ModbusClientForCameras.Information($"Получено меньше трех баркодов!!! = {numberOfReadBarcodes}");
                                            Log_ModbusClientForCameras.Error($"Новый Баркод получен: {convertBarcode}");
                                            numberOfReadBarcodes = 0;
                                            numberOfReadBarcodes++;
                                        }
                                        else
                                        {
                                            Log_ModbusClientForCameras.Information("Получено три баркода");
                                            Log_ModbusClientForCameras.Error($"Новый Баркод получен: {convertBarcode}");
                                            numberOfReadBarcodes = 0;
                                            numberOfReadBarcodes++;
                                        }
                                    }
                                }
                                else
                                {
                                    Log_ModbusClientForCameras.Error($"Новый Баркод получен: {convertBarcode}");
                                    numberOfReadBarcodes++;
                                }

                                if (convertBarcode != "")
                                {
                                    MainAlgorithm.MainRouteTranzitions(convertBarcode, CheckPointName, CheckPointLine);
                                    ForCountingBarcodes.Restart();
                                }
                            }
                            catch (Exception e)
                            {
                                sickTCPClient.Dispose();
                                break;
                            }
                        }

                        //Socket Соединение
                        //Socket socket = ConnectSocketAsync(IpAddress, port);
                        //if (socket is null) { continue; }

                        //// получаем данные
                        //SocketFlags flags = SocketFlags.None;
                        //int bytes;

                        //// буфер для получения данных
                        //var responseBytes = new byte[512];

                        ////Команда на прочитывание Баркода отправляется на Камеру
                        ////CameraModbus.WriteSingleRegister(3, 0, new byte[] { 0x32, 0x31 });

                        //bytes = socket.Receive(responseBytes, flags);

                        //string responsePart = Encoding.UTF8.GetString(responseBytes, 0, bytes);
                        //builder.Append(responsePart);

                        //if (responsePart == "" || responsePart.Contains("NoRead")) 
                        //{
                        //    //Для просмотра в логах
                        //    Log_ModbusClientForCameras.Error($"Результат чтения : {responsePart}");
                        //    socket.Dispose(); CameraModbus.Dispose(); Thread.Sleep(1000); continue; 
                        //}
                        //if(responsePart == lastBarcode){ continue; }
                        //lastBarcode = responsePart;
                        //Log_ModbusClientForCameras.Error($"Новый Баркод получен: {responsePart}");
                        //if (responsePart != "")
                        //{
                        //    MainAlgorithm.MainRouteTranzitions(responsePart, CheckPointName, CheckPointLine);
                        //}
                        //socket.Dispose();
                        //CameraModbus.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
           
            


        }//Получение данных по Socket

        //Подключение по Socket
        private static Socket ConnectSocketAsync(string url, int port)
        {
            Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                tempSocket.Connect(url, port);
                return tempSocket;
            }
            catch (SocketException ex)
            {
                Log_ModbusClientForCameras.Error("Ошибка подключения к Socket  " + ex.Message.ToString());
                tempSocket.Close();
            }
            return null;
        }
        //Загружаем данные из БД и консолидируем в одном списке
        private static void LoadAllCameraDataFromDataBase()
        {
            try
            {
                //List<Plclist> PLCListFromDB = dbCheckPoints.Plclists.ToList();
                List<CameraList> cameraListsFromDb = dbCheckPoints.CameraLists.ToList();
                List<CheckPointsList> checkPointsListsFromDb = dbCheckPoints.CheckPointsLists.ToList();
                //Заполняем данные по всем ПЛК.
                foreach (var item in cameraListsFromDb)
                {
                    Camera_Clients.Add(new Camera_Client()
                    {
                        Comments = item.CameraName,
                        //IPAddress = item.Plcipaddress,
                        Camera = new ModbusTcpClient(),
                        CheckPointName = item.CheckPointName,
                        //PLC_Client_EEIPProtocol = new EEIPClient(),
                        CameraIpAddress = cameraListsFromDb.FirstOrDefault(e => e.CheckPointName == item.CheckPointName).CameraIpaddress,
                        CheckPointLine = checkPointsListsFromDb.FirstOrDefault(e => e.CheckPointName == item.CheckPointName).CheckPointLine
                    });
                }
            }
            catch (Exception ex)
            {
                Log_ModbusClientForCameras.Error("Ошибка Получения данных из БД " + ex.Message.ToString());
                throw;
            }
        }
    }
}

