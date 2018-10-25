using System.Diagnostics;

namespace DMSkin.Server
{
    /// <summary>
    /// 播放服务
    /// </summary>
    public  class PlayServer
    {
        #region 全部解码器操作
        /// <summary>
        /// 初始化解码器
        /// </summary>
        public static void Initialize()
        {
            switch (PlayerType)
            {
                case PlayerType.MN:
                    CloseXL();
                    StartMN();
                    break;
                case PlayerType.XL:
                    CloseMN();
                    StartXL();
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

        /// <summary>
        /// 关掉全部解码器
        /// </summary>
        /// <param name="Url"></param>
        public static void Close()
        {
            CloseXL();
            CloseMN();
        } 
        #endregion

        #region 迅雷解码器
        /// <summary>
        /// 启动迅雷解码器
        /// </summary>
        private static void StartXL()
        {
            //判断进程中是否存在 DMSkin.Player 
            Process[] process = Process.GetProcessesByName("DMSkin.Player.Xunlei");
            //没有DMSkin.Player 再创建 DMSkin.Player 
            if (process.Length == 0)
            {
                Process.Start("DMSkin.Player.Xunlei.exe");
            }
        }

        /// <summary>
        /// 关闭迅雷的解码器
        /// </summary>
        private static void CloseXL()
        {
            Process[] process = Process.GetProcessesByName("DMSkin.Player.Xunlei");
            foreach (var item in process)
            {
                item.Kill();
            }
        } 
        #endregion

        #region 迷你解码器
        /// <summary>
        /// 启动迷你解码器
        /// </summary>
        private static void StartMN()
        {
            //判断进程中是否存在 DMSkin.Player 
            Process[] process = Process.GetProcessesByName("DMSkin.Player");
            //没有DMSkin.Player 再创建 DMSkin.Player 
            if (process.Length == 0)
            {
                Process.Start("DMSkin.Player.exe");
            }
        }

        /// <summary>
        /// 关闭迷你的解码器
        /// </summary>
        private static void CloseMN()
        {
            Process[] process = Process.GetProcessesByName("DMSkin.Player");
            foreach (var item in process)
            {
                item.Kill();
            }
        }
        #endregion

        #region 当前开启的解码器类型
        /// <summary>
        /// 解码器类型
        /// </summary>
        public static PlayerType PlayerType { get; set; } 
        #endregion
    }

    /// <summary>
    /// 解码器类型 - 未来可以自定义更多解码器
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// 迷你解码
        /// </summary>
        MN,
        /// <summary>
        /// 迅雷解码
        /// </summary>
        XL
    }
}
