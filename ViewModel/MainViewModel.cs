using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Identity;
using OEE_dotNET.Control;
using OEE_dotNET.Database;
using OEE_dotNET.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OEE_dotNET.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private string? password;

    Option1PageViewModel? option1PageView;
    Option2PageViewModel? option2PageView;
    Option3PageViewModel? option3PageView;
    DashboardViewModel? dashboardView;
    PlanOverview_ViewModel? planOverview_View;
    Create_Account_Window_View? Create_Account_WD;
    SettingWindow_View? settingWindow_WD;

    [ObservableProperty]
    private bool remember_me = false;

    [ObservableProperty]
    private bool can_access_dashboard = false;
    [ObservableProperty]
    private string pw = "12344";

    [ObservableProperty]
    private string? aboutcontent = "About company";

    [ObservableProperty]
    private ObservableObject? view1;

    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private bool loggedIn = false;

    [ObservableProperty]
    private bool loggedOut = true;

    [ObservableProperty]
    private bool passIsEmpty = true;

    [RelayCommand]
    public void About() 
    {
        MessageBox.Show(Aboutcontent);
    }

    [RelayCommand]
    public void Exit() 
    {
        Environment.Exit(0);
        Application.Current.Shutdown();
    }

    [RelayCommand]
    public void Logout() 
    {
        Login(false);
        DatabaseExcute_Main.Current_User = null;
        View1 = null;
        option1PageView?.Dispose();
        option1PageView = null;
        option2PageView = null;
        option3PageView = null;
        dashboardView = null;
        planOverview_View = null;
       
        //a.mo_code = ab["mo_code"]
    }

    [RelayCommand]
    public void Dashboard() 
    {
        Login(true);
        if (option1PageView == null)
        {
            option1PageView = new Option1PageViewModel();
            View1 = option1PageView;
        }
        else
        {
            View1 = option1PageView;
        }
    }

    [RelayCommand]
    public void Open_settings() 
    {
        if(settingWindow_WD == null) 
        {
            settingWindow_WD = new SettingWindow_View();
            settingWindow_WD.ShowDialog();
        }
    }
    [RelayCommand]
    public void Monitoring() 
    {
        Login(true);
        if (dashboardView == null)
        {
            dashboardView = new DashboardViewModel();
            View1 = dashboardView;
        }
        else
        {
            View1 = dashboardView;
        }
        option1PageView = null;
    }
    [RelayCommand]
    public void Open_Create_Form() 
    {
        Create_Account_WD = new Create_Account_Window_View();
        Create_Account_WD.ShowDialog();
    }

    [RelayCommand]
    public void Log_In() 
    {
        try
        {
            DatabaseExcute_Main.Login_User(Username, password);

            if(DatabaseExcute_Main.Current_User !=null ) 
            {
                if(DatabaseExcute_Main.Current_User.Permission == "Admin") 
                {
                    Login(true);
                    if(option1PageView == null) 
                    {
                        option1PageView = new Option1PageViewModel();
                        View1 = option1PageView;
                    }
                    else 
                    {
                        View1 = option1PageView;
                    }
                    Can_access_dashboard = true;
                }
                else if(DatabaseExcute_Main.Current_User.Permission == "Superviser")
                {
                    Login(true);
                    if (planOverview_View == null)
                    {
                        planOverview_View = new PlanOverview_ViewModel();
                        View1 = planOverview_View;
                    }
                    else
                    {
                        View1 = planOverview_View;
                    }
                    Can_access_dashboard = true;
                }
                else if (DatabaseExcute_Main.Current_User.Permission == "Plan")
                {
                    Login(true);
                    if (option2PageView == null)
                    {
                        option2PageView = new Option2PageViewModel();
                        View1 = option2PageView;
                    }
                    else
                    {
                        View1 = option2PageView;
                    }
                }
                else if (DatabaseExcute_Main.Current_User.Permission == "Technical")
                {
                    Login(true);
                    if (option3PageView == null)
                    {
                        option3PageView = new Option3PageViewModel();
                        View1 = option3PageView;
                    }
                    else
                    {
                        View1 = option3PageView;
                    }
                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    public void Cancel() 
    {
        Environment.Exit(0);
        Application.Current.Shutdown();
    }

    [RelayCommand]
    public void PasswordChanged(object p) 
    {
        var passwordBox = p as PasswordBox;
        if (passwordBox != null)
        {
            var secureString = passwordBox.SecurePassword;
            password = ConvertToUnsecureString(secureString);

            if (secureString.Length > 0)
            {
                PassIsEmpty = false;
            }
            else
            {
                PassIsEmpty = true;
            }
        }
    }
    [RelayCommand]
    public void Txtbox_UserLoad(object p) 
    {
        var txtbox_user = p as TextBox;
        if (txtbox_user != null) 
        {
            Username = txtbox_user.Text;
        }
    }

    [RelayCommand]
    public void Create_account() 
    {
        // Do some stuff...
    }
    [RelayCommand]
    public void  ViewChanged() 
    {
        Debug.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - View Changed");
    }



    private void Login(bool loggedIn)
    {
        if (loggedIn) 
        {
            LoggedIn = true;
            LoggedOut = false;
            ApplicationConfig.SettingParameter.Remember_me = Remember_me;
            if (Remember_me) 
            {
                DataProtection.WriteData($"{Username}:{password}");
            }
        }
        else 
        {
            LoggedIn = false;
            LoggedOut = true;
            Can_access_dashboard = false;
        }
        
    }
    private string ConvertToUnsecureString(SecureString securePassword)
    {
        IntPtr unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
}


