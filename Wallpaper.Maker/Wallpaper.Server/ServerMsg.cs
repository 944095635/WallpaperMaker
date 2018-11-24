using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper.Server
{
    public class ServerMsg
    {
        private ServerMsgType serverMsgTypeVar;
        /// <summary>
        /// 消息类型
        /// </summary>
        public ServerMsgType ServerMsgType
        {
            get
            {
                return serverMsgTypeVar;
            }
            set
            {
                serverMsgTypeVar = value;
            }
        }

        public string Value { get; set; }
        public int IntValue { get; set; }
    }

    public enum ServerMsgType
    {
        /// <summary>
        /// 打开链接
        /// </summary>
        OpenUrl,
        /// <summary>
        /// 放入桌面
        /// </summary>
        InDeskTop,
        /// <summary>
        /// 音量
        /// </summary>
        Volume
    }
}
