using OEE_dotNET.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OEE_dotNET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left) 
            {
                this.DragMove();
            }
        }

        private bool Ismaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if (Ismaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1024;
                    this.Height = 720;
                }
                else
                {
                    this.WindowState = WindowState.Minimized;

                    Ismaximized = true;
                }

            }
        }

        private void txtboxpass_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}