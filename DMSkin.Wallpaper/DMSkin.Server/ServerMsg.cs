using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSkin.Server
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

        public string Url { get; set; }
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
    }
}
