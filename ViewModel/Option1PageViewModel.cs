using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Net.Http.Headers;
using LiveCharts.Defaults;
using System.Timers;
using System.Windows.Threading;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Measure;
using System.Net.NetworkInformation;
using OEE_dotNET.Database;
using System.Windows.Media;
using System.Net.WebSockets;
using System.Windows.Data;
using System.Globalization;
using System.Data;

namespace OEE_dotNET.ViewModel;

public partial class Option1PageViewModel: ObservableObject, IDisposable
{
    [ObservableProperty]
    private string current_state = "Stop";

    [ObservableProperty]
    private double running_time = 0;
    [ObservableProperty]
    private double pause_time = 0;
    [ObservableProperty]
    private double stop_time = 0;
    [ObservableProperty]
    private double quantity_actual = 0;
    [ObservableProperty]
    private double quantity_require = 0;


    [ObservableProperty]
    private double oee = 100;
    [ObservableProperty]
    private double availability = 100;
    [ObservableProperty]
    private double performance = 100;
    [ObservableProperty]
    private double quality = 0;
    [ObservableProperty]
    private double total = 0;

    public Func<double, string> Formatter { get; set; } = value => value.ToString() + "%";

    [ObservableProperty]
    private SeriesCollection seriesViews = new SeriesCollection()
    {
        new PieSeries
        {
            Title = "Running Time",
            Values = new ChartValues<double>{100},
            DataLabels = true,
            Fill = Brushes.Green
        },

        new PieSeries
        {
            Title = "Pause Time",
            Values = new ChartValues<double>{100},
            DataLabels = true,
            Fill = Brushes.Orange
        },
        new PieSeries
        {
            Title = "Stop Time",
            Values = new ChartValues<double>{100},
            DataLabels = true,
            Fill = Brushes.Red
        }
    };
    [ObservableProperty]
    private ChartValues<double> oeeLine = new ChartValues<double>();

    [ObservableProperty]
    private ChartValues<double> availabilityLine = new ChartValues<double>();

    [ObservableProperty]
    private ChartValues<double> performanceLine = new ChartValues<double>();

    [ObservableProperty]
    private ChartValues<double> qualityLine = new ChartValues<double>();

    DispatcherTimer timer = new DispatcherTimer();
    public Option1PageViewModel()
    {
        //DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = new TimeSpan(0, 0, 5);
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    ~Option1PageViewModel() 
    {
        timer.Tick -= Timer_Tick;
        timer.Stop();
    }
    private void Timer_Tick(object? sender, EventArgs e)
    {
        RandomeValue();
    }

    private void RandomeValue() 
    {
        var result = DatabaseExcute_Main.Get_status_runtime();
        Stop_time = Math.Round(result.Item1 / 60,2);
        Running_time = Math.Round(result.Item2 / 60,2);
        Pause_time = Math.Round(result.Item3/ 60, 2);
        Quantity_actual = Math.Round(result.Item5, 0);
        Quantity_require = Math.Round(result.Item6, 0);

        CaculateTotal();
        // Tính toán chỉ số OEE
        var oee_caculated = APQ();

        Availability = Math.Round(oee_caculated.Item2, 2);
        Performance = Math.Round(oee_caculated.Item3, 2);
        Quality = Math.Round(oee_caculated.Item4, 0);
        Oee = Math.Round(oee_caculated.Item1, 2);

        SeriesViews.FirstOrDefault(x => x.Title == "Running Time").Values[0] = Running_time;
        SeriesViews.FirstOrDefault(x => x.Title == "Pause Time").Values[0] = Pause_time;
        SeriesViews.FirstOrDefault(x => x.Title == "Stop Time").Values[0] = Stop_time;

        //var data_line = DatabaseExcute_Main.Read_OEE_History();


        //AvailabilityLine.Add(Availability);
        //QualityLine.Add(Quality);
        //PerformanceLine.Add(Performance);
        Update_ChartLine();
    }
    private void Update_ChartLine()
    {
        var data_line = Add_LineChart(DatabaseExcute_Main.Read_OEE_History());
        OeeLine = data_line.Item1;
        AvailabilityLine = data_line.Item2;
        QualityLine = data_line.Item3;
        PerformanceLine = data_line.Item4;
    }

    private (ChartValues<double>,ChartValues<double>,ChartValues<double>,ChartValues<double>) Add_LineChart(DataTable data) 
    {
        var oee_l = data.AsEnumerable().Select(row => Convert.ToDouble(row["oee_rate"])).ToList();
        var avai_l = data.AsEnumerable().Select(row => Convert.ToDouble(row["availability_rate"])).ToList();
        var quality_l = data.AsEnumerable().Select(row => Convert.ToDouble(row["quality_rate"])).ToList();
        var perform_l = data.AsEnumerable().Select(row => Convert.ToDouble(row["performance_rate"])).ToList();

        ChartValues<double> oee_Line = new ChartValues<double>();
        ChartValues<double> availabilityLine = new ChartValues<double>();
        ChartValues<double> qualityLine = new ChartValues<double>();
        ChartValues<double> performanceLine = new ChartValues<double>();
        foreach (var item in oee_l)
        {
            oee_Line.Add(item);
        }
        foreach (var item in avai_l)
        {
            availabilityLine.Add(item);
        }
        foreach (var item in quality_l)
        {
            qualityLine.Add(item);
        }
        foreach (var item in perform_l)
        {
            performanceLine.Add(item);
        }
        return (oee_Line, availabilityLine,qualityLine,performanceLine);
    }

    /// <summary>
    /// Assign value from node-red to check status of machine 
    /// </summary>
    /// <param name="mode">Status: 0: Stop, 1:Run, 2:Pause</param>
    private void Read_status(int mode) 
    {
        switch (mode)
        {
            case 0:
                Current_state = "Stop";
                break;
            case 1:
                Current_state = "Run";
                break;
            case 2:
                Current_state = "Pause";
                break;
            default:
                Current_state = "Stop";
                break;
        }
    }

    private void CaculateTotal()
    {
        Total = Math.Round(Running_time + Pause_time + Stop_time, 2);
    }

    /// <summary>
    /// Return (Oee, Availability, Performance, Quality)
    /// </summary>
    /// <returns></returns>
    private (double,double,double,double) APQ() 
    {
        var duty = 1; //30s 

        //Tỉ lệ vận hành(Availability) Tỉ lệ vận hành theo thời gian = (Thời gian vận hành ký thuyết – Thời gian dừng máy) / Thời gian vận hành lý thuyết
        var availability_ratio = (Total - Stop_time - Pause_time) / Total;

        //Tỷ lệ Chất lượng(Quality) Tỷ lệ Chất lượng = Tổng sản phẩm chất lượng / Tổng số sản phẩm đã thực hiện
        var quality_ratio = (Quantity_actual) / Quantity_actual;


        //Tỷ lệ Hiệu suất(Performance) Tỷ lệ Hiệu suất = (Thời gian chu kỳ lý tưởng × Tổng sản phẩm) / Thời gian chạy máy
        var performance_ratio = (duty * Quantity_actual) / Running_time;

        //OEE = Tỷ lệ vận hành × Tỷ lệ hiệu suất × Tỷ lệ chất lượng
        var Oee_ratio = availability_ratio * quality_ratio * performance_ratio;

        return (Oee_ratio*100, availability_ratio*100, performance_ratio*100,quality_ratio*100);
    }

    public void Dispose()
    {
        timer.Tick -= Timer_Tick;
        timer.Stop();
    }
}

public class Status_color : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value switch
        {
            "Run" => Brushes.Green,
            "Pause" => Brushes.Orange,
            "Stop" => Brushes.Red,
            _ => Brushes.Gray,
        };
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
