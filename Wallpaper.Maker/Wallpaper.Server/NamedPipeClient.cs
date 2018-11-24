using System;
using System.IO;
using System.IO.Pipes;

namespace Wallpaper.Server
{
    public class NamedPipeClient : IDisposable
    {
        string _serverName;
        string _pipName;
        NamedPipeClientStream _pipeClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverName">服务器地址</param>
        /// <param name="pipName">管道名称</param>
        public NamedPipeClient(string serverName, string pipName)
        {
            _serverName = serverName;
            _pipName = pipName;

            _pipeClient = new NamedPipeClientStream(serverName, pipName, PipeDirection.InOut);

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public ServerMsg Query(ServerMsg request)
        {
            if (!_pipeClient.IsConnected)
            {
                _pipeClient.Connect(10000);
            }

            StreamWriter sw = new StreamWriter(_pipeClient);
            sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            sw.WriteLine();//连续2个换行外加"#END"表示结束
            sw.WriteLine();
            sw.WriteLine("#END");
            sw.Flush();

            StreamReader sr = new StreamReader(_pipeClient);
            string returnVal = sr.ReadToEnd();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ServerMsg>(returnVal);
        }

        #region IDisposable 成员

        bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed && _pipeClient != null)
            {
                _pipeClient.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}
