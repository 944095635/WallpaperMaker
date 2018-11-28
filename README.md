# DMSkin-Wallpaper-Maker
Windows 桌面动态壁纸 视频壁纸

![](https://img.shields.io/badge/.NET-%3E%3D3.5-brightgreen.svg)
![](https://img.shields.io/badge/version-1.0.0.0-blue.svg)
![](https://img.shields.io/badge/license-MIT-green.svg)

#### 开源的动态桌面壁纸。

<a href="http://on9mnekns.bkt.clouddn.com/desktopdemo.mp4" target="_blank">
   <img src="https://raw.githubusercontent.com/944095635/DMSkin-Wallpaper-Maker/master/Screenshot/demo.gif">
</a>

## 【前言】 
Wallpaper.Maker 采用WIN32 接口实现视频嵌入桌面。

Wallpaper.Maker 最开始采用的是迅雷Aplayer,CPU使用率颇高15%-30%,

现阶段改为[libvlcsharp](https://github.com/videolan/libvlcsharp),CPU使用率降低至1%-5%(跟个人电脑配置有关)
#### 【项目结构】


| 项目名称                | 描述   |特性   |缺点      |
| :----:              | :---:          | :----:     | :----:     |
| Wallpaper.Maker |主程序   |  -   |  -  |
| DMSkin.WPF  |  UI库       | UI的一些封装 |  开发时需要从Github或Nuget获取       |
| Wallpaper.Player |Vlc解码器   |  HTTP支持,超多格式(GIF,MP4.AVI.FLV.WEBM.RMVB)  |  解码库体积(24MB)  |
| Wallpaper.Server |通讯协议   |  负责主程序和解码器之间的通讯  |  NamedPipe命名管道通讯  |

#### 【执行逻辑】
- `服务PlayServer`是作为程序之间数据交互的基础,通讯原理是NamedPipe命名管道通讯。

- `主程序Maker`程序启动的时候，会根据`服务PlayServer`检测系统进程中是否存在`解码器Player`。

- 如果存在对应`解码器Player`进程，程序不会执行任何操作(如果不存在，`主程序Maker`会启动`解码器Player`)。

- `主程序Maker`与`解码器Player`分离，减少内存消耗。主程序退出之后`解码器Player`依然会运行(主程序中可以`关闭解码器进程`)。


#### 【使用&修改】

#### 1. 下载使用

你可以直接下载压缩包解压运行。

[主程序](https://github.com/944095635/DMSkin-Wallpaper-Maker/releases)

[解码器百度网盘下载](https://pan.baidu.com/s/1LPBPWivXGvBoBJ6MvIloUw)

[解码器Vlc官网下载](http://www.videolan.org/vlc/download-windows.html)  下载最新的Vlc播放器压缩包解压之后,提取需要的解码器文件.

解码器文件放至主程序软件目录中的libvlc文件夹中

/libvlc

/libvlc/plugins

/libvlc/libvlc.dll 

/libvlc/libvlccore.dll


#### 2. 下载源码 然后自己编译
[下载源码](https://codeload.github.com/944095635/DMSkin-Wallpaper-Maker/zip/master) 点击 `Wallpaper.Maker.sln` 打开项目。

###### 目前解码器使用的是[libvlcsharp](https://github.com/videolan/libvlcsharp)
libvlcsharp 是根据开源视频解码器[Vlc](https://github.com/videolan/vlc)进一步封装的.NET 版本,它支持Winform和WPF.

当前项目的解码器部分使用的是Winform版本,因为它的效率更高资源占用更低。

开发的时候需要从Nuget 安装，Nuget版本没有修改解码器的加载路径

#### 【自定义解码器】
虽然目前只内置解码器libvlcsharp

但是你可以通过编写代码实现自己的解码器(例如用迅雷Aplayer解码器)
````csharp
/// <summary>
/// 放到解码器初始化的地方-解码器必须一直处于运行状态 Winform/WPF/Console 都支持
/// </summary>
NamedPipeListenServer server = new NamedPipeListenServer("Play.Server")
{
    ProcessMessage = ProcessMessage
};
server.Run();

/// <summary>
/// 处理请求-例如处理客户端 Open URL的请求
/// </summary>
public void ProcessMessage(ServerMsg msg, NamedPipeServerStream pipeServer)
{
    switch (msg.ServerMsgType)
    {
         case ServerMsgType.OpenUrl:
            ///让你的播放器执行播放视频操作
            break;
    }
    pipeServer.Close();
}
````

## 【联系】
欢迎加入我们：

- **[C# .NET (2000人) QQ交流群](http://qm.qq.com/cgi-bin/qm/qr?k=reTIeglEELMIW267mOO7amouFFwhJwwP)**

- **DMSkin QQ交流群: 194684812**

- **WPF 课程学习群 (收费): 611509631**
- **<a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=944095635&site=qq&menu=yes">联系作者</a>**
- **[DMSkin官方网站](http://www.dmskin.com)**

## 捐赠
如果你觉得这个框架真的对你很有帮助，欢迎给我捐赠，这将鼓励我做的更好!

<img src="http://dmskin.com/pay.jpg" width="500">

## 【更新日志】

### 1.0.0.2（2018-11-29）
1.解码器改为官网封装的libvlcsharp

### 1.0.0.1 (2018-11-24)

1. 因迅雷Aplayer解码器性能低,系统兼容性差，故舍弃。
2. 解码器改用[Vlc.DotNet](https://github.com/ZeBobo5/Vlc.DotNet),CPU使用率从 15-30% 降低到 3%-5% 。

### 1.0.0.0 (2018-10-25)

1. 解码器初步完成。
2. 操作软件初步完成。

<del>#### 【注意 源码编译&亚克力玻璃材质】</del>

<del>- 基于VS 2017 旗舰版开发，.NET 4.5.5开发环境（理论可修改至.NET 3.5），源码包括一些c# 6.0+语法，如果你在VS 2015甚至更低的VS版本上编译不通过的话，请自行修改中源码不兼容的部分。</del>

<del>- 截图是Windows 10 秋季创作者更新 中的亚克力玻璃 效果,其他系统请自行测试,相关内容请自行搜素Fluent Design System</del>

## MIT License
Copyright © 2018 <copyright holders>

Permission is hereby granted， free of charge， to any person obtaining a copy of this software and associated documentation files (the “Software”)， to deal in the Software without restriction， including without limitation the rights to use， copy， modify， merge， publish， distribute， sublicense， and/or sell copies of the Software， and to permit persons to whom the Software is furnished to do so， subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”， WITHOUT WARRANTY OF ANY KIND， EXPRESS OR IMPLIED， INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY， FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM， DAMAGES OR OTHER LIABILITY， WHETHER IN AN ACTION OF CONTRACT， TORT OR OTHERWISE， ARISING FROM， OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
