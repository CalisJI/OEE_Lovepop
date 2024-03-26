using OEE_dotNET.Database;
using OEE_dotNET.API;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using OEE_dotNET.Control;
using System.Web.Http;
using System.Web.Http.SelfHost;

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

            // SelfHost API Server

            //var config = new HttpSelfHostConfiguration("http://0.0.0.0:5001"); // open port 5001
            //config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
            //var server = new HttpSelfHostServer(config);
            //server.OpenAsync().Wait();
        }
    }
}