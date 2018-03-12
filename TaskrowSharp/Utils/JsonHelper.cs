using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Utils
{
    internal class JsonHelper
    {
        public static string Serialize(object obj, bool ignoreNull = false, bool indented = false, bool camelCase = false)
        {
            if (obj == null)
                return null;

            var settings = new Newtonsoft.Json.JsonSerializerSettings();

            if (ignoreNull)
                settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            if (indented)
                settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            if (camelCase)
                settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings);
        }

        public static T Deserialize<T>(string json)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            return obj;
        }

        public static Newtonsoft.Json.Linq.JObject DeserializeToJObject(string json, bool removeDateFunc = true)
        {
            Newtonsoft.Json.Linq.JObject jObject;
            if (removeDateFunc)
                json = RepairJsonDateFunction(json);

            try
            {
                jObject = Newtonsoft.Json.Linq.JObject.Parse(json);
            }
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException(string.Format("Erro na conversão do JSON para JObject -- Exception: {0} -- JSON: {1}", ex.Message, json));
            }

            return jObject;
        }

        private static string RepairJsonDateFunction(string json)
        {
            if (json.Contains("/Date("))
            {
                //Padrão retornado pelo TaskRow, que gera erro no Parser:
                // {
                //  "TaskTitle": "Titulo",
                //  "DueDate": "/Date(\"2014-07-04T00:00:00.000-03:00\")/"
                // }

                json = json.Replace("/Date(\\\"", "").Replace("\\\")/", "");
            }

            return json;
        }
    }
}
