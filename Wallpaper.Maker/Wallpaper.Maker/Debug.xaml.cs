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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wallpaper.Server;

namespace Wallpaper.Maker
{
    public partial class Debug : Window
    {
        public Debug()
        {
            InitializeComponent();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)//注意，此处一定要手动引入System.Window.Forms空间，否则你如果使用默认的DialogResult会发现没有OK属性
            {
                TbUrl.Text = openFileDialog.FileName;
            }
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PlayServer.SetVolume((int)SliderVolume.Value);
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            //初始化解码器
            PlayServer.Initialize();
            //播放
            PlayServer.Play(TbUrl.Text);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)PlayerRun.IsChecked)
            {
                PlayServer.CloseAll();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(bool)PlayerRun.IsChecked)
            {
                PlayServer.CloseAll();
            }
        }
    }
}
