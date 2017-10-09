using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public class JsonSerializeHelper
    {
        public static JsonSerializeHelper jsonTools = new JsonSerializeHelper();

        private JsonSerializeHelper()
        {

        }

        public static JsonSerializeHelper GetInstance()
        {
            if (jsonTools == null)
            {
                object o = new object();
                lock (o)
                {
                    if (jsonTools == null)
                    {
                        jsonTools = new JsonSerializeHelper();
                    }
                }
            }
            return jsonTools;

        }

        public T DeserializeObject<T>(string request)
        {
            //var m_request = JsonConvert.DeserializeObject<Request>(request);
            return JsonConvert.DeserializeObject<T>(request);
        }
        public string SerializeObject<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
