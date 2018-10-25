using System;
using System.Windows;
using System.Windows.Interop;

namespace DMSkin.Player
{
    public partial class Player : Window
    {
        public Player()
        {
            InitializeComponent();
        }

        public string Url = string.Empty;
        /// <summary>
        /// 播放一个路径
        /// </summary>
        public void Play(string url)
        {
            Url = url;
            media.Source = new Uri(Url);
            media.Volume = 0;
            media.Play();
        }

        bool iSDeskTop = false;
        /// <summary>
        /// 放入桌面
        /// </summary>
        public void DeskTop()
        {
            if (!iSDeskTop)
            {
                //放入桌面
                WPF.API.DesktopAPI.Initialization(new WindowInteropHelper(this).Handle);
                WindowState = WindowState.Maximized;
                iSDeskTop = true;
            }
        }
        
        /// <summary>
        /// 视频循环播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                media.Stop();
                media.Play();
            }
        }
    }
}
