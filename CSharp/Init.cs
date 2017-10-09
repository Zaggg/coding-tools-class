using Common.Static;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Tools
{
    public class Init
    {
        public static string file_name = ConfigName.custom_config_filename;
        private static Dictionary<string, string> config_dictionary = null;
        public static object lock_obj = new object();
        static Init()
        {
            
        }

        public static Dictionary<string,string> GetConfig
        {
            get
            {
                if (config_dictionary == null)
                {
                    lock (lock_obj)
                    {
                        if (config_dictionary == null)
                            config_dictionary = ReadConfig();
                    }
                }
                return config_dictionary;
            }
        }
        private static Dictionary<string,string> ReadConfig()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + file_name))
                {
                    var lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory+file_name, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string temp_line = string.Empty;
                            temp_line = line.Trim();
                            string[] valuesPair = null;
                            if (temp_line.IndexOf("//") >= 0)
                            {
                                temp_line = temp_line.Substring(0, temp_line.IndexOf("//"));
                            }
                            if (temp_line.IndexOf("#") >= 0)
                            {
                                temp_line = temp_line.Substring(0, temp_line.IndexOf("#"));
                            }
                            if (temp_line.Contains("="))
                            {
                                valuesPair = new string[] { temp_line.Substring(0, temp_line.IndexOf('=')), temp_line.Substring(temp_line.IndexOf('=') + 1) };
                                if (valuesPair.Length == 2 && valuesPair[0] != string.Empty && valuesPair[1] != null)
                                {
                                    ArrayTrim(ref valuesPair);
                                    dic.Add(valuesPair[0], valuesPair[1]);
                                }
                            }
                        }
                    }
                    return dic;
                }
            }
            catch(Exception ex)
            {
                LogHelper.GetInstance().WriteErrors("error|readconfig|",ex);
            }
            return null;
        }

        private static void ArrayTrim(ref string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim();
            }
        }
    }
}
