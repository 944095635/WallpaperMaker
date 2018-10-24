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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMSkin.Player
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr Hwnd = new WindowInteropHelper(this).Handle;
            DMSkin.WPF.API.DesktopAPI.Initialization(Hwnd);
            //DMSkin.WPF.API.DesktopAPI.FullScreen(Hwnd);

            WindowState = WindowState.Maximized;
        }
    }
}
