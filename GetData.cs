using SnmpSharpNet;

namespace GetSensorDataService
{
    internal static class GetData // класс для выполнения snmp запросов
    {

        internal static int GetTemperature(string ip) // получение температуры
        {
            string key = ".1.3.6.1.4.1.58162.1.0";
            try
            {
                string value = GetSnmpData(ip, key);
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("snmp connection error"))
                { throw new Exception(ex.Message); }
                throw new Exception($"{DateTime.Now} Ошибка при получении температуры для {ip}");
            }
        }

        internal static int GetHumidity(string ip) // получение влажности
        {
            string key = ".1.3.6.1.4.1.58162.2.0";
            try
            {
                string value = GetSnmpData(ip, key);
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("snmp connection error"))
                { throw new Exception(ex.Message); }
                throw new Exception($"{DateTime.Now} Ошибка при получении влажности для {ip}");
            }
        }

        private static string GetSnmpData(string ip, string key) // базовый метод запроса
        {
            string comunity = "public";
            Oid uints = new Oid(key);
            SimpleSnmp snmp = new SimpleSnmp(ip, comunity);
            if (snmp.Valid)
            {
                Pdu pdu = new Pdu();
                pdu.Type = PduType.Get;
                pdu.VbList.Add(key);
                var data = snmp.Get(SnmpVersion.Ver1, pdu);
                AsnType value;
                data.TryGetValue(uints, out value);
                return value.ToString();
            }
            throw new Exception("snmp connection error");
        }
    }
}
