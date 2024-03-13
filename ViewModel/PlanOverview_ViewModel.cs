using CommunityToolkit.Mvvm.ComponentModel;
using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
