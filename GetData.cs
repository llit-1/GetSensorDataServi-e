using SnmpSharpNet;

namespace GetSensorDataService
{
    internal static class GetData // класс для выполнения snmp запросов
    {

        internal static int? GetTemperature(string ip) // получение температуры
        {
            string key = ".1.3.6.1.4.1.58162.1.0";
            try
            {
                string value = GetSnmpData(ip, key);
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                Logging.Log($"Ошибка при получении температуры для {ip}");
                return null;
            }
        }

        internal static int? GetHumidity(string ip) // получение влажности
        {
            string key = ".1.3.6.1.4.1.58162.2.0";
            try
            {
                string value = GetSnmpData(ip, key);
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                Logging.Log($"Ошибка при получении влажности для {ip}");
                return null;
            }
        }

        private static string GetSnmpData(string ip, string key) // базовый метод запроса
        {
            try
            {
                string comunity = "public";
                Oid uints = new Oid(key);
                SimpleSnmp snmp = new SimpleSnmp(ip, comunity);
                Pdu pdu = new Pdu();
                pdu.Type = PduType.Get;
                pdu.VbList.Add(key);
                var data = snmp.Get(SnmpVersion.Ver1, pdu);
                AsnType value;
                data.TryGetValue(uints, out value);
                return value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
