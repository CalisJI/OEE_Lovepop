﻿using OEE_dotNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OEE_dotNET.View
{
    /// <summary>
    /// Interaction logic for Create_Account_Window_View.xaml
    /// </summary>
    public partial class Create_Account_Window_View : Window
    {
        public Create_Account_Window_View()
        {
            DataContext = new Create_Account_ViewModel();
            InitializeComponent();
        }
    }
}
