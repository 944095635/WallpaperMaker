using DMSkin.WPF.API;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Wallpaper.Player
{
    public partial class Player : Form
    {
        /// <summary>
        /// 解码器路径
        /// </summary>
        DirectoryInfo vlclib;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="directoryInfo">解码器路径</param>
        public Player(DirectoryInfo directoryInfo)
        {
            //设置路径必须要放在前面
            vlclib = directoryInfo;
            InitializeComponent();
        }

        public Uri Url;
        /// <summary>
        /// 播放一个路径
        /// </summary>
        public void Play(string url)
        {
            Url = new Uri(url);
            media.Play(Url);
            InDeskTop();
        }

        bool iSDeskTop = false;
        /// <summary>
        /// 放入桌面
        /// </summary>
        public void InDeskTop()
        {
            if (!iSDeskTop)
            {
                //放入桌面
                DesktopAPI.Initialization(Handle);
                media.Video.AspectRatio = $"{Width}:{Height}";
                iSDeskTop = true;
            }
        }

        /// <summary>
        /// 铺满屏幕
        /// </summary>
        internal void FullScreen()
        {
        
            //media.SetConfig(204, Width + ";" + Height);
        }

        public void SetVolume(int Volume)
        {
            media.Audio.Volume = Volume;
        }

        /// <summary>
        /// 当VLC控制需要查找LIVVLC.DLL的位置时。
        /// 您可以在设计器中设置VlcLibDirectory，但是对于本示例，我们处于AnyCPU模式，并且我们不知道进程位。
        /// </summary>
        private void media_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            e.VlcLibDirectory = vlclib;
        }

        private void media_Log(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {
            string message = string.Format("libVlc : {0} {1} @ {2}", e.Level, e.Message, e.Module);
            Debug.WriteLine(message);
        }

        /// <summary>
        /// 视频播放完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void media_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((p) => media.Play(Url));
        }
    }
}
