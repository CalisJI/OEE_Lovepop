using OEE_dotNET.Database;
using OEE_dotNET.API;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

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
            //Test
            //Publisher.Initialize_Req_Res();
            //Publisher.Initialize_Subcriber();
        }
    }
}