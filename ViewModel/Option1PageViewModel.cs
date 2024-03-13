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

namespace OEE_dotNET.ViewModel;

public partial class Option1PageViewModel: ObservableObject
{
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

    public Option1PageViewModel()
    {
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = new TimeSpan(0, 0, 5);
        timer.Tick += Timer_Tick;
        timer.Start();
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
        Quantity_actual = Math.Round(result.Item4, 0);
        Quantity_require = Math.Round(result.Item5, 0);

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

        OeeLine.Add(Oee);
        AvailabilityLine.Add(Availability);
        QualityLine.Add(Quality);
        PerformanceLine.Add(Performance);
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
