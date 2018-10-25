using DMSkin.Server;
using DMSkin.WPF.API;
using System;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;

namespace DMSkin.Player.Xunlei
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

            NamedPipeListenServer server = new NamedPipeListenServer("Play.Server")
            {
                ProcessMessage = ProcessMessage
            };
            server.Run();

            play = new Player();
            play.InDeskTop();
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

    }
}
