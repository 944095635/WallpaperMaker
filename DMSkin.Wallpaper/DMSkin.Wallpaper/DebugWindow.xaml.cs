using DMSkin.Server;
using System.Windows;

namespace DMSkin.Wallpaper
{
    public partial class DebugWindow : Window
    {
        public DebugWindow()
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
            PlayServer.Close();
        }
    }
}
