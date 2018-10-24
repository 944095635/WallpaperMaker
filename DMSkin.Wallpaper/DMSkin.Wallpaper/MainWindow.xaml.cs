using System;
using System.Windows;

namespace DMSkin.Wallpaper
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
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IntPtr Hwnd = (IntPtr)Convert.ToInt32(tb.Text);
            DMSkin.WPF.API.DesktopAPI.FullScreen(Hwnd);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            IntPtr Hwnd = (IntPtr)Convert.ToInt32(tb.Text);
            DMSkin.WPF.API.DesktopAPI.Initialization(Hwnd);
        }
    }
}
