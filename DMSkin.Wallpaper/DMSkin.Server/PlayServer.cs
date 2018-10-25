using System.Diagnostics;

namespace DMSkin.Server
{
    /// <summary>
    /// 播放服务
    /// </summary>
    public  class PlayServer
    {
        /// <summary>
        /// 初始化解码器
        /// </summary>
        public static void Initialize()
        {
            switch (PlayerType)
            {
                case PlayerType.W:
                    //结束掉进程中的 DMSkin.Player.Xunlei

                    //判断进程中是否存在 DMSkin.Player 
                    Process[] process = Process.GetProcessesByName("DMSkin.Player");
                    //没有DMSkin.Player 再创建 DMSkin.Player 
                    if (process.Length == 0)
                    {
                        Process.Start("DMSkin.Player.exe");
                    }
                    break;
                case PlayerType.XL:
                    //结束掉进程中的 DMSkin.Player 

                    //判断进程中是否存在 DMSkin.Player.Xunlei

                    //没有DMSkin.Player.Xunlei 再创建 DMSkin.Player.Xunlei
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 播放文件
        /// </summary>
        public static void Play(string Url)
        {
            using (NamedPipeClient client = new NamedPipeClient(".", "Play.Server"))
            {
                client.Query(new ServerMsg
                {
                    ServerMsgType = ServerMsgType.OpenUrl,
                    Url = Url
                });
            }
        }

        public static PlayerType PlayerType { get; set; }
    }

    /// <summary>
    /// 解码器类型
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// 默认
        /// </summary>
        W,
        /// <summary>
        /// 迅雷解码
        /// </summary>
        XL
    }
}
