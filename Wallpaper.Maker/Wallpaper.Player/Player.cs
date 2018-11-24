using DMSkin.WPF.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallpaper.Player
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
            media.Play(new Uri(url));
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
            //media.SetVolume(Volume);
        }

        /// <summary>
        /// 当VLC控制需要查找LIVVLC.DLL的位置时。
        /// 您可以在设计器中设置VlcLibDirectory，但是对于本示例，我们处于AnyCPU模式，并且我们不知道进程位。
        /// </summary>
        private void media_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc"));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Vlc libraries folder.";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void media_Log(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {
            string message = string.Format("libVlc : {0} {1} @ {2}", e.Level, e.Message, e.Module);
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
