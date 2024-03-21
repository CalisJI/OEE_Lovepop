using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OEE_dotNET.Control;
using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OEE_dotNET.ViewModel
{
    public partial class SettingWindowViewModel:ObservableObject
    {
        [ObservableProperty]
        private string machine_id = string.Empty;
        [ObservableProperty]
        private ObservableCollection<string> list_machine_id = new ObservableCollection<string>();

        [RelayCommand]
        public void Loaded() 
        {
            List_machine_id = DatabaseExcute_Main.GetMachineId();
            Machine_id = ApplicationConfig.SettingParameter.Machine_Id;
        }

        [RelayCommand]
        public void SelectionChanged(object p) 
        {
            var id = p.ToString();
            Machine_id = id;

            ApplicationConfig.SettingParameter.Machine_Id = id;
            ApplicationConfig.UpdateConfig(ApplicationConfig.SettingParameter);
        }
    }
}
