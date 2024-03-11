using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Identity;
using OEE_dotNET.Database;
using System;
using System.Collections.Generic;
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
    public void Dashboard() 
    {
        Login(true);
        //if (dashboardView == null) 
        //{
        //    dashboardView = new DashboardViewModel();
        //    View1 = dashboardView;
        //}
        //else 
        //{
        //    View1 = dashboardView;
        //}

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
    public void Home() 
    {
        Login(true);
        if(option3PageView == null) 
        {
            option3PageView = new Option3PageViewModel();
            View1 = option3PageView;
        }
        else 
        {
            View1 = option3PageView;
        }
        
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
                }
                else if(DatabaseExcute_Main.Current_User.Permission == "Operater")
                {

                }
                else if (DatabaseExcute_Main.Current_User.Permission == "Teachnical")
                {

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
    public void Create_account() 
    {
        // Do some stuff...
    }


    private void Login(bool loggedIn)
    {
        if (loggedIn) 
        {
            LoggedIn = true;
            LoggedOut = false;
        }
        else 
        {
            LoggedIn = false;
            LoggedOut = true;
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


