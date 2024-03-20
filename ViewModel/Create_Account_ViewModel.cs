using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OEE_dotNET.Database;
using OEE_dotNET.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OEE_dotNET.ViewModel
{
    public partial class Create_Account_ViewModel:ObservableObject
    {
        private string? password;
        [ObservableProperty]
        private bool can_create = false;

        [ObservableProperty]
        private string username = "";

        [ObservableProperty]
        private bool passIsEmpty = true;

        [ObservableProperty]
        private string resgister_user = "";



        private string? permission;
        private string register_pass = "";
        private string confirm_register_pass = "";

        [RelayCommand]
        public void CheckedChanged(object p) 
        {
            var checkbox = p as RadioButton;
            if (checkbox != null) 
            {
                 permission = (string?)checkbox.Content;
            }
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
        public void Register_PasswordChanged(object p)
        {
            var passwordBox = p as PasswordBox;
            if (passwordBox != null)
            {
                var secureString = passwordBox.SecurePassword;
                register_pass = ConvertToUnsecureString(secureString);

            }
        }

        [RelayCommand]
        public void Register_Confirm_PasswordChanged(object p)
        {
            var passwordBox = p as PasswordBox;
            if (passwordBox != null)
            {
                var secureString = passwordBox.SecurePassword;
                confirm_register_pass = ConvertToUnsecureString(secureString);
            }
        }

        [RelayCommand]
        public void Log_In()
        {
            try
            {
                DatabaseExcute_Main.Login_User(Username, password,true);

                if (DatabaseExcute_Main.Authentication_permission == "Admin")
                {
                    Can_create = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void Create_Account()
        {
            try
            {
                if(register_pass == confirm_register_pass && register_pass !="" && Resgister_user!="") 
                {
                    DatabaseExcute_Main.User_register(Resgister_user, register_pass, "", permission);

                    MessageBox.Show("Create Account Successfully");
                    Resgister_user = "";
                }
                else if(register_pass != confirm_register_pass)
                {
                    MessageBox.Show("Confirm password does not match");
                }
                else 
                {
                    MessageBox.Show("Please enter password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [RelayCommand]
        public void Unloaded() 
        {
            Can_create = false;
            Username = "";
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
    public enum PermissionOption 
    {
        Admin,
        Plan,
        Superviser,
        Techincal
    }
}
