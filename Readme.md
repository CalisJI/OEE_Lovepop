# OEE Application
https://github.com/CalisJI/OEE_Lovepop.git

## Screenshot
![](./Image/webdb.png)
![](./Image/dashboard.png)
![](./Image/login.png)
![](./Image/plan.png)
![](./Image/superviser.png)
![](./Image/technical.png)


## NuGet Were Utilized
- [`MVVM Toolkit`](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [`LiveCharts`](https://v0.lvcharts.com/App/examples/v1/Wpf/Install)
- [`Microsoft.Xaml.Behaviors.Wpf`](https://github.com/Microsoft/XamlBehaviorsWpf?tab=readme-ov-file)
- [`MahApps.Metro.IconPacks`](https://github.com/MahApps/MahApps.Metro.IconPacks)
- [`MaterialDesignThemes`](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- [`Microsoft.AspNet.WebApi.Owin`](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [`Microsoft.AspNet.WebApi.OwinSelfHost`](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [`NetMQ`](https://netmq.readthedocs.io/en/latest/pub-sub/)
## API 
- using RESTFul API to communicate with another application like (`Node-red`, web API, etc...)
- Using These NuGet: `Microsoft.AspNet.WebApi.Owin`; `Microsoft.Owin.Host.HttpListener`; `Microsoft.Owin.Hosting`; `Microsoft.AspNet.WebApi.OwinSelfHost`
```csharp 

// Define a self server host to lister request
var config = new HttpSelfHostConfiguration("http://0.0.0.0:5001"); // open port 5001
config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
var server = new HttpSelfHostServer(config);
server.OpenAsync().Wait();


```

```csharp
//Get request

[Route("Get_machine_runtime")]
[HttpGet]
public IHttpActionResult Get_machine_runtime() 
{
    try
    {
                
        return Ok();
    }
    catch (Exception)
    {
        return NotFound();
    }
} 

```

## ZeroMQ
- Using to `subscribe` and `publish` message through each node applications in local network
- Using this NuGet: [NetMQ](https://netmq.readthedocs.io/en/latest/pub-sub/)
```csharp
//Subscriber --- use in subscriber mode (Workstation mode)
public static void Initialize_Subcriber(string topic) 
{
    Task.Run(() =>
    {
        using (var subscriber = new SubscriberSocket())
        {
            subscriber.Connect("tcp://127.0.0.1:5556");
            subscriber.Subscribe(topic);

            while (true)
            {
                topic = subscriber.ReceiveFrameString();
                var msg = subscriber.ReceiveFrameString();
                Debug.WriteLine("From Publisher: {0} {1}", topic, msg);
            }
        }
    });
}
```
```csharp
//Publisher use in Plan option
/// <summary>
/// Publish message to subscribers in network (Workstation node)
/// </summary>
/// <param name="mQMessage"></param>
public static async Task Publisher_MQ(MQMessage mQMessage) 
{
    using (var publisher = new PublisherSocket())
    {
        publisher.Bind("tcp://*:5556");
        await Task.Delay(500);
        if (mQMessage!=null && mQMessage.Topic!=null && mQMessage.Content!=null) 
        {
            publisher
                .SendMoreFrame(mQMessage.Topic) // Topic
                .SendFrame(mQMessage.Content); // Message 
        }
    }
}
```
## Working with Database
- Using DatabaseExcute_Main

# Dashboard Web

- Using DashboardHtml Folder and that is in development 


## Configurate settings for Application
- Class: `ApplicationConfig.cs`
Using to store Initial settings configuration for application

```csharp
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

//Settings Parameter Model

public class SettingParameter
{
    public string? Machine_Id { get; set; }
    public bool Remember_me { get; set; }
    public int DecrypLength { get; set; }
}
```

# MVVM Pattern Reference
- **ViewModel**: `MainViewModel`, `Option1PageViewModel`, `Option2PageViewModel`, `Option3PageViewModel`, `DashboardViewModel`, `PlanOver_ViewModel`, `Create_Account_ViewModel`, `SettingWindowViewModel`
- **View**: `MainWindow`, `Option1PageView`,`Option2PageView`,`Option3PageView`,`DashboardView`, `PlanOver_View`,`Create_Account_View`, `SettingWindow_View`
 ### `ViewModelResource.xaml`
 ```xaml
 <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:vm="clr-namespace:OEE_dotNET.ViewModel"
                    xmlns:v="clr-namespace:OEE_dotNET.View">

    <DataTemplate DataType="{x:Type vm:Option1PageViewModel}"> <!--Option for planning department-->
        <v:Option1PageView/>
    </DataTemplate>
    <DataTemplate DataType="{x:Type vm:Option2PageViewModel}"> <!--Option for workspace department-->
        <v:Option2PageView/>
    </DataTemplate>
    <DataTemplate DataType="{x:Type vm:Option3PageViewModel}"> <!-- Option for technical department-->
        <v:Option3PageView/>
    </DataTemplate>
    <DataTemplate DataType="{x:Type vm:DashboardViewModel}">
        <v:DashboardView/>
    </DataTemplate>
    <DataTemplate DataType="{x:Type vm:PlanOverview_ViewModel}">
        <v:PlanOverview_View/>
    </DataTemplate>
</ResourceDictionary>
 
 ```
 ### `App.xaml`
 ```xaml
 <Application x:Class="OEE_dotNET.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:OEE_dotNET"
             xmlns:Icon="http://metro.mahapps.com/winfx/xaml/iconpacks"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/ViewModelResource.xaml"/>
                <ResourceDictionary Source="pack://application:,,,/Style.xaml"/>
                <ResourceDictionary Source="pack://application:,,,/LiveCharts.Wpf;component/Themes/Colors/material.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
       
    </Application.Resources>
    
</Application>

 ```
# Style
 `Style.xaml`

