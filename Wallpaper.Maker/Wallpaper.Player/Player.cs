using DMSkin.Core.WIN32;
using LibVLCSharp.Shared;
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
        /// 构造函数
        /// </summary>
        public Player()
        {
            InitializeComponent();

            _libVLC = new LibVLC();
            media = new MediaPlayer(_libVLC)
            {
                Hwnd = this.Handle
            };
            media.EndReached += media_EndReached;
        }

        /// <summary>
        /// 视频播放完毕
        /// </summary>
        private void media_EndReached(object sender, EventArgs e)
        {
            //ThreadPool.QueueUserWorkItem((p) => media.Play(Url));
        }

        MediaPlayer media;
        LibVLC _libVLC;
        public Uri Url;
        /// <summary>
        /// 播放一个路径
        /// </summary>
        public void Play(string url)
        {
            Url = new Uri(url);
            media.Play(new Media(_libVLC,url, Media.FromType.FromPath));
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
                media.AspectRatio = $"{Width}:{Height}";
                iSDeskTop = true;
            }
        }

        /// <summary>
        /// 铺满屏幕
        /// </summary>
        internal void FullScreen()
        {
            //Vlc.DotNet.Core.VlcMedia m = new Vlc.DotNet.Core.VlcMedia();
            //media.SetConfig(204, Width + ";" + Height);
        }

        public void SetVolume(int Volume)
        {
            media.Volume = Volume;
        }

    }
}
