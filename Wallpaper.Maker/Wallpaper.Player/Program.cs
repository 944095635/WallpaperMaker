using DMSkin.WPF.API;
using System;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
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
            play = new Player
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height,
                Location = new Point(0, 0)
            };
            Application.Run(play);
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
