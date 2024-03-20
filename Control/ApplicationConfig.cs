using Newtonsoft.Json;
using OEE_dotNET.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OEE_dotNET.Control
{
    public static class ApplicationConfig
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Settings.json";
        public static string Datapath = Directory.GetCurrentDirectory() + @"/Data.dat";
        public static SettingParameter SettingParameter;

        public static void GetConfig() 
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    SettingParameter = new SettingParameter()
                    {
                        Machine_Id = "Laser 1",
                        Remember_me = true
                    };
                    UpdateConfig(SettingParameter);
                }
                else
                {
                    string jsonString = File.ReadAllText(FilePath);
                    SettingParameter = JsonConvert.DeserializeObject<SettingParameter>(jsonString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UpdateConfig(SettingParameter setting) 
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(SettingParameter, Formatting.Indented);
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
