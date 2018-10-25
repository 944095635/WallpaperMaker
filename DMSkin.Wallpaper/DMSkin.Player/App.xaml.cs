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
            NamedPipeListenServer svr = new NamedPipeListenServer("Play.Server")
            {
                ProcessMessage = ProcessMessage
            };
            svr.Run();
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        public void ProcessMessage(ServerMsg msg, NamedPipeServerStream pipeServer)
        {

            switch (msg.ServerMsgType)
            {
                case ServerMsgType.OpenUrl:
                    OpenUrl(msg);
                    break;
                case ServerMsgType.InDeskTop:
                    break;
                default:
                    break;
            }
            pipeServer.Close();
        }

        /// <summary>
        /// 播放器对象
        /// </summary>
        Player p;
        /// <summary>
        /// 播放一个链接
        /// </summary>
        /// <param name="msg"></param>
        public void OpenUrl(ServerMsg msg)
        {
            Execute.OnUIThread(()=> {
                if (p == null)//初始化
                {
                    p = new Player();
                    p.Show();
                    p.DeskTop();
                }
                p.Play(msg.Url);
            });
        }
    }
}
