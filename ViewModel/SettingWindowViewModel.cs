using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OEE_dotNET.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OEE_dotNET.ViewModel
{
    public partial class SettingWindowViewModel:ObservableObject
    {
        [ObservableProperty]
        private string machine_id = string.Empty;

        [RelayCommand]
        public void Loaded() 
        {
            Machine_id = ApplicationConfig.SettingParameter.Machine_Id;
        }

        
    }
}
