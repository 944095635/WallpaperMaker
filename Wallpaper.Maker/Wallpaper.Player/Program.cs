using DMSkin.Core.Common;
using LibVLCSharp.Shared;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Windows.Forms;
using Wallpaper.Server;

namespace Wallpaper.Player
{
    static class Program
    {
        /// <summary>
        /// 播放器对象
        /// </summary>
        static Player play;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Execute.InitializeWithDispatcher();

            //全局捕获异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            NamedPipeListenServer server = new NamedPipeListenServer("Play.Server")
            {
                ProcessMessage = ProcessMessage
            };
            server.Run();

            //解码器初始化
            Core.Initialize();

            //程序路径
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (Directory.Exists(Path.Combine(currentDirectory, "libvlc")))
            {
                play = new Player()
                {
                    Width = Screen.PrimaryScreen.Bounds.Width,
                    Height = Screen.PrimaryScreen.Bounds.Height,
                    Location = new Point(0, 0)
                };
                Application.Run(play);
            }
            else
            {
                MessageBox.Show("缺少视频解码器,前往官网下载.");
                Process.Start("http://www.dmskin.com");
                Application.Exit();
            }
        }

        /// <summary>
        /// 处理客户端请求
        /// </summary>
        public static void ProcessMessage(ServerMsg msg, NamedPipeServerStream pipeServer)
        {
            switch (msg.ServerMsgType)
            {
                case ServerMsgType.OpenUrl:
                    Execute.OnUIThread(() => {
                        play.Play(msg.Value);
                    });
                    break;
                case ServerMsgType.InDeskTop:
                    break;
                case ServerMsgType.Volume:
                    Execute.OnUIThread(() => {
                        play.SetVolume(msg.IntValue);
                    });
                    break;
            }
            pipeServer.Close();
        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //LogHelper.WriteLog(this.GetType(), e.ExceptionObject.ToString());
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }
}
