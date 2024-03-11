using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using OEE_dotNET.API;
using OEE_dotNET.Database;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OEE_dotNET.ViewModel
{
    public partial class Option2PageViewModel :ObservableObject
    {
        [ObservableProperty]
        private bool insert2DB = true; //Check douoble insert data

        private static string pre_plan = "";
        [ObservableProperty]
        private string? filepath;

        [ObservableProperty]
        private DataTable? plan_table = new DataTable()
        {
            Columns = 
            {
                new DataColumn("mo_code",typeof(string)),
                new DataColumn("lp_code",typeof(string)),
                new DataColumn("cutting_code",typeof(string)),
                new DataColumn("priority",typeof(int)),
                new DataColumn("paper",typeof(string)),
                new DataColumn("filename",typeof(string)),
                new DataColumn("quantity",typeof(int)),
                new DataColumn("machine_id",typeof (string)),
                new DataColumn("created_at",typeof(DateTime)),
                new DataColumn("id",typeof(int))
            }
        };

        
        partial void OnFilepathChanged(string? value) 
        {
            if (Filepath!=null && pre_plan != Filepath) 
            {
                pre_plan = Filepath;
                Insert2DB = false;
                
            }
            else 
            {
                Insert2DB = true;
            }
        }

        [RelayCommand]
        public void Openfile()
        {
            var dialog = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv"};
            var result = dialog.ShowDialog();
            if(result == true) 
            {
                Filepath = dialog.FileName;
                Plan_table = ReadCSV(Filepath);
            }
        }

        [RelayCommand]
        public async void Apply() 
        {
            try
            {
                if (Plan_table != null && !Insert2DB) 
                {
                    var result = MessageBox.Show(
                    "Do you want to perform this action?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes) 
                    {
                        await DatabaseExcute_Main.InsertDataTableToMySQLExceptId(Plan_table);
                        Insert2DB = true;
                        MQMessage mQ = new MQMessage
                        {
                            Topic = "NewPlan",
                            Content = "New Plan"
                        };
                        await Publisher.Publisher_MQ(mQ);
                        MessageBox.Show("Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [RelayCommand]
        public void Cancel_action() 
        {
            
        }

        static DataTable ReadCSV(string filePath)
        {
            DataTable dataTable = new DataTable() 
            {
                Columns =
                {
                    
                    new DataColumn("mo_code",typeof(string)),
                    new DataColumn("lp_code",typeof(string)),
                    new DataColumn("cutting_code",typeof(string)),
                    new DataColumn("priority",typeof(int)),
                    new DataColumn("paper",typeof(string)),
                    new DataColumn("filename",typeof(string)),
                    new DataColumn("quantity",typeof(int)),
                    new DataColumn("machine_id",typeof (string)),
                    new DataColumn("created_at",typeof(DateTime)),
                    new DataColumn("id",typeof(int))
                }
            };

            using (StreamReader reader = new StreamReader(filePath))
            {
                bool isFirstLine = true;
                int id = 0;
                while (!reader.EndOfStream)
                {
                    object[] data = reader.ReadLine().Split(',');
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                    }
                    
                    else
                    {
                        DataRow row = dataTable.NewRow();
                        object[] newData = new object[data.Length + 2];
                        Array.Copy(data, newData, data.Length);
                        newData[data.Length] = DateTime.Now;
                        newData[data.Length + 1] = id++;
                        row.ItemArray = newData;
                        dataTable.Rows.Add(row);
                    }
                }
            }
            return dataTable;
        }
    }
    public class Invertboolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
