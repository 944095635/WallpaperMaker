using DMSkin.Server;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows;

namespace DMSkin.Wallpaper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////判断播放器进程是否启动

            ////写入播放文件
            //File.WriteAllText("Play.dll",tb.Text);

            ////发送数据
            //IntPtr intPtr = (IntPtr)Convert.ToInt32(File.ReadAllText("Handle.dll"));
            //NativeMethods.SendMessage(intPtr, 0x0400, 0,0);

            //SendData();


            PlayServer.Play(tb.Text);

        }

        private static void SendData()
        {
            try
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "play.pipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None))
                {
                    pipeClient.Connect();
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.WriteLine("hahha");
                        sw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandle", SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
        public static extern IntPtr GetModuleHandle(String lpModuleName);


    }
}
