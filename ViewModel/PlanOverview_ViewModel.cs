using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OEE_dotNET.ViewModel
{
    public partial class PlanOverview_ViewModel:ObservableObject
    {
        [ObservableProperty]
        private DataTable? total_Plan;
        [ObservableProperty]
        private DateTime from_date = DateTime.Now.AddDays(-7);
        [ObservableProperty]
        private DateTime to_date = DateTime.Now;

        public PlanOverview_ViewModel()
        {
            try
            {
                Total_Plan = DatabaseExcute_Main.Load_Total_Plan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [RelayCommand]
        private void Search() 
        {
            try
            {
                Total_Plan = DatabaseExcute_Main.Load_Total_Plan(From_date.ToString("dd/MM/yyyy"),To_date.ToString("dd/MM/yyyy"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
