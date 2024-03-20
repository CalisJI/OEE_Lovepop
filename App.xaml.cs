using OEE_dotNET.Database;
using OEE_dotNET.API;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using OEE_dotNET.Control;

namespace OEE_dotNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Debug.WriteLine(DatabaseExcute_Main.Check_Database().ToString());
            ApplicationConfig.GetConfig();
            //Test
            //Publisher.Initialize_Req_Res();
            //Publisher.Initialize_Subcriber();
        }
    }
}