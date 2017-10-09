using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
   public  class ToolsManager
    {
        //public static ToolsManager manager;

        //private ToolsManager()
        //{

        //}

        //public static ToolsManager GetInstance()
        //{
        //    if (manager == null)
        //    {
        //        object o = new object();
        //        lock (o)
        //        {
        //            if (manager == null)
        //            {
        //                manager = new ToolsManager();
        //            }
        //        }
        //    }
        //    return manager;
        //}

        public static dynamic Get(string name)
        {
            dynamic obj = null;
            switch (name)
            {
                case "config":obj = ConfigReader.GetInstance();break;
                case "json":obj = JsonSerializeHelper.GetInstance();break;
                case "xml":obj = XMLSerializeHelper.GetInstance();break;
            }
            return obj;
        }
    }
}
