using GetSensorDataServiсe;
using GetSensorDataServiсe.DB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

File.AppendAllText("logs.txt", $"\n{DateTime.Now} Старт службы GetSensorDataServise"); //логируем запуск приложения

while (true) //приложение выполняет 1 бесконечный цикл
{
    string json = File.ReadAllText("appconfig.json"); // достаём настройки из appconfig.json
    Settings settings = JsonSerializer.Deserialize<Settings>(json);
    DbContextOptionsBuilder<GetSensorDataServiсe.DB.MSSQLContext> dbContextOptionsBuilder = new(); // создаём настройки подключения к бд
    dbContextOptionsBuilder.UseSqlServer(settings.ConnectionString); 
    int Interval = settings.Interval;

    using (MSSQLContext db = new(dbContextOptionsBuilder.Options)) // подключаемся к бд
    {
        foreach (var item in db.SensorRooms) // для каждого датчика из комнаты выполняем запрос
        {
            try
            {
                SensorData sensorData = new SensorData();
                sensorData.RoomId = item.Id;
                sensorData.RoomName = item.Name;
                sensorData.Temperature = GetData.GetTemperature(item.Ip);
                sensorData.Humidity = GetData.GetHumidity(item.Ip);
                sensorData.Date = DateTime.Now;
                db.Add(sensorData);
                db.SaveChanges(); // сохраняем данные
            }
            catch (Exception ex)
            {
                File.AppendAllText("logs.txt", ex.Message);
            }
        }
        File.AppendAllText("logs.txt", $"\n{DateTime.Now} Данные успешно сохранены"); //логируем успешное завершение
    }
    Thread.Sleep(Interval); // делаем паузу на время указанное в appconfig.json
}



