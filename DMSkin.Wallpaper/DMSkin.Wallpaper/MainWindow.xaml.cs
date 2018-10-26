using DMSkin.Server;
using DMSkin.Wallpaper.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;
using static DMSkin.Wallpaper.API.WindowAPI;

namespace DMSkin.Wallpaper
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)PlayerMINI.IsChecked)
            {
                PlayServer.PlayerType = PlayerType.MN;
            }
            else
            {
                PlayServer.PlayerType = PlayerType.XL;
            }

            //初始化解码器
            PlayServer.Initialize();
            //播放
            PlayServer.Play(TbUrl.Text);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            PlayServer.CloseAll();
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PlayServer.SetVolume((int)SliderVolume.Value);
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)//注意，此处一定要手动引入System.Window.Forms空间，否则你如果使用默认的DialogResult会发现没有OK属性
            {
                TbUrl.Text = openFileDialog.FileName;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(bool)PlayerRun.IsChecked)
            {
                PlayServer.CloseAll();
            }
        }


        #region 新版Acrylic效果实现

        private uint _blurOpacity = 90;

        public uint BlurOpacity
        {
            get => _blurOpacity;
            set
            {
                _blurOpacity = value;
            }
        }

        private const uint BlurBackgroundColor = 0xFFFFFF; /* BGR color format */

        private void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);
            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                GradientColor = (BlurOpacity << 24) | (BlurBackgroundColor & 0xFFFFFF)
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            Dispatcher.Invoke(() => { SetWindowCompositionAttribute(windowHelper.Handle, ref data); });

            Marshal.FreeHGlobal(accentPtr);

            Dispatcher.Invoke(() =>
            {
                //BorderThickness = new Thickness(0, 0, 0, 0); 
            });
        }

        #endregion

        private void DMSkinSimpleWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
