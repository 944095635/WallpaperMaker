using System.Diagnostics;
using System.IO;
using System.Web;

namespace Wallpaper.Server
{
    /// <summary>
    /// 播放服务
    /// </summary>
    public class PlayServer
    {
        #region 全部解码器操作
        /// <summary>
        /// 初始化解码器
        /// </summary>
        public static void Initialize()
        {
            StartPlayer();
        }

        /// <summary>
        /// 播放文件
        /// </summary>
        public static void Play(string Url)
        {
            StartPlayer();
            if (IsRun())
            {
                using (NamedPipeClient client = new NamedPipeClient(".", "Play.Server"))
                {
                    client.Query(new ServerMsg
                    {
                        ServerMsgType = ServerMsgType.OpenUrl,
                        Value = Url
                    });
                }
            }
        }

        /// <summary>
        /// 关掉全部解码器
        /// </summary>
        public static void CloseAll()
        {
            ClosePlayer();
        }
        #endregion

        public static void SetVolume(int Volume)
        {
            if (IsRun())
            {
                using (NamedPipeClient client = new NamedPipeClient(".", "Play.Server"))
                {
                    client.Query(new ServerMsg
                    {
                        ServerMsgType = ServerMsgType.Volume,
                        IntValue = Volume
                    });
                }
            }
        }

        #region 解码器
        public static bool IsRun()
        {
            //判断进程中是否存在 Wallpaper.Player
            Process[] process = Process.GetProcessesByName("Wallpaper.Player");
            if (process.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 启动迅雷解码器
        /// </summary>
        private static void StartPlayer()
        {
            //没有DMSkin.Player 再创建 DMSkin.Player 
            if (!IsRun())
            {
                Process.Start("Wallpaper.Player.exe");
            }
        }

        /// <summary>
        /// 关闭迅雷的解码器
        /// </summary>
        private static void ClosePlayer()
        {
            Process[] process = Process.GetProcessesByName("Wallpaper.Player");
            foreach (var item in process)
            {
                item.Kill();
            }
        }
        #endregion

    }
}
