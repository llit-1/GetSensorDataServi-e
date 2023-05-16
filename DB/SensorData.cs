using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSensorDataService.DB
{
    internal class SensorData
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
