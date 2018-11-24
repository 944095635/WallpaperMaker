using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace Wallpaper.Server
{
    public class NamedPipeListenServer
    {
        List<NamedPipeServerStream> _serverPool = new List<NamedPipeServerStream>();
        string _pipName = "test";
        public NamedPipeListenServer(string pipName)
        {
            _pipName = pipName;
        }

        /// <summary>
        /// 创建一个NamedPipeServerStream
        /// </summary>
        /// <returns></returns>
        protected NamedPipeServerStream CreateNamedPipeServerStream()
        {
            NamedPipeServerStream npss = new NamedPipeServerStream(_pipName, PipeDirection.InOut, 10);
            _serverPool.Add(npss);
            Console.WriteLine("启动了一个NamedPipeServerStream " + npss.GetHashCode());
            return npss;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="npss"></param>
        protected void DistroyObject(NamedPipeServerStream npss)
        {
            npss.Close();
            if (_serverPool.Contains(npss))
            {
                _serverPool.Remove(npss);
            }
            Console.WriteLine("销毁一个NamedPipeServerStream " + npss.GetHashCode());
        }

        public void Run()
        {
            Task.Run(()=> 
            {
                using (NamedPipeServerStream pipeServer = CreateNamedPipeServerStream())
                {
                    pipeServer.WaitForConnection();
                    Console.WriteLine("建立一个连接 " + pipeServer.GetHashCode());

                    Action act = new Action(Run);
                    act.BeginInvoke(null, null);

                    try
                    {
                        bool isRun = true;
                        while (isRun)
                        {
                            string str = null;
                            string strAll = null;
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();

                            StreamReader sr = new StreamReader(pipeServer);
                            while (pipeServer.CanRead && (null != (str = sr.ReadLine())))
                            {

                                //当遇到连续2个换行外加#END，表示输入结束
                                if (str == "#END")
                                {
                                    strAll = sb.ToString();
                                    if (strAll.EndsWith("\r\n\r\n"))
                                        break;
                                }
                                else
                                {
                                    if (str == "")
                                        sb.AppendLine();
                                    else
                                        sb.AppendLine(str);
                                }
                            }

                            strAll = strAll.Substring(0, strAll.Length - "\r\n\r\n\r\n".Length);
                            ProcessMessage(Newtonsoft.Json.JsonConvert.DeserializeObject<ServerMsg>(strAll), pipeServer);

                            if (!pipeServer.IsConnected)
                            {
                                isRun = false;
                                break;
                            }

                            Thread.Sleep(50);
                        }
                    }
                    // Catch the IOException that is raised if the pipe is broken
                    // or disconnected.
                    catch (IOException e)
                    {
                        Console.WriteLine("ERROR: {0}", e.Message);
                    }
                    finally
                    {
                        DistroyObject(pipeServer);
                    }
                }
            });
        }

        public Action<ServerMsg, NamedPipeServerStream> ProcessMessage;
        /// <summary>
        /// 处理消息
        /// </summary>
        //public void ProcessMessage(string str, NamedPipeServerStream pipeServer)'
        //{
        //    // Read user input and send that to the client process.
        //    using (StreamWriter sw = new StreamWriter(pipeServer))
        //    {
        //        sw.AutoFlush = true;
        //        sw.Write("收到你的消息 " + str);
        //    }
        //}

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < _serverPool.Count; i++)
            {
                var item = _serverPool[i];

                DistroyObject(item);
            }
        }
    }
}
