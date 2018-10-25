using DMSkin.Server;
using DMSkin.WPF.API;
using System.IO;
using System.IO.Pipes;
using System.Windows;

namespace DMSkin.Player
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Execute.InitializeWithDispatcher();

            //手动关闭模式
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            NamedPipeListenServer server = new NamedPipeListenServer("Play.Server")
            {
                ProcessMessage = ProcessMessage
            };
            server.Run();
        }

        /// <summary>
        /// 播放器对象
        /// </summary>
        Player p;

        /// <summary>
        /// 处理客户端请求
        /// </summary>
        public void ProcessMessage(ServerMsg msg, NamedPipeServerStream pipeServer)
        {

            switch (msg.ServerMsgType)
            {
                case ServerMsgType.OpenUrl:
                    Execute.OnUIThread(() => {
                        if (p == null)//初始化
                        {
                            p = new Player();
                            p.Show();
                            p.InDeskTop();
                        }
                        p.Play(msg.Value.ToString());
                        System.Console.WriteLine();
                    });
                    break;
                case ServerMsgType.InDeskTop:
                    break;
                case ServerMsgType.Volume:
                    Execute.OnUIThread(() => {
                        p.SetVolume(msg.IntValue);
                    });
                    break;
            }
            pipeServer.Close();
        }
    }
}
