using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSkin.Player.Xunlei
{
    public partial class Player : Form
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
            media.Open(url);
            media.SetVolume(0);
            media.SetConfig(119,"1");

            Task.Run(async ()=> 
            {
                await Task.Delay(100);
                FullScreen();

                await Task.Delay(200);
                FullScreen();

                await Task.Delay(200);
                FullScreen();
            });
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
                WPF.API.DesktopAPI.Initialization(Handle);
                Width = Screen.PrimaryScreen.Bounds.Width;
                Height = Screen.PrimaryScreen.Bounds.Height;
                Location = new Point(0,0);
                iSDeskTop = true;
            }
        }

        /// <summary>
        ///     铺满屏幕
        /// </summary>
        internal void FullScreen()
        {
            media.SetConfig(204, Width + ";" + Height);
        }

        public void SetVolume(int Volume)
        {
            media.SetVolume(Volume);
        }
    }
}
