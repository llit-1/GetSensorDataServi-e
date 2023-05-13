using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSensorDataServiсe.DB
{
    internal class MSSQLContext : DbContext
    {
        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }


        public DbSet<SensorRooms> SensorRooms { get; set; } // таблица с данными заполненности витрин
        public DbSet<SensorData> SensorData { get; set; } // таблица с данными прогноза продаж вручную вводимыми ТМ
    }
}
