# DMSkin-LiveWallpaper
Windows 桌面动态壁纸 视频壁纸

![](https://img.shields.io/badge/.NET-%3E%3D3.5-brightgreen.svg)
![](https://img.shields.io/badge/version-1.0.0.0-blue.svg)
![](https://img.shields.io/badge/license-MIT-green.svg)

#### 开源的动态桌面壁纸。

<img src="https://raw.githubusercontent.com/94x4095635/DMSkin-for-WPF/master/DMSkin.ScreenShot/demo.png" align="center">

## 前言 
DMSkin.Wallpaper 采用WIN32 接口实现视频嵌入桌面。

#### 项目结构


| 项目名称                | 描述   |特性   |缺点      |
| :----:              | :---:          | :----:     | :----:     |
| DMSkin.Wallpaper |主程序   |  -   |  -  |
| DMSkin.Player  |  迷你解码器       |WPF-MediaElement |  内存占用稍高       |
| DMSkin.Player.Xunlei |迅雷Aplayer解码器   |  开发时需要安装环境,支持更多格式,更小的内存占用   |  安装包体积偏大(+40MB)  |

#### 执行逻辑
1.[主程序]程序启动的时候，会根据PlayServer中的PlayerType检测系统进程中是否存在[迷你解码器]或者[迅雷Aplayer解码器]。

2.如果存在对应解码器进程，程序不会执行任何操作(如果不存在，主程序会启动解码器)。

3.[主程序]与[解码器]分离，减少内存消耗。主程序退出之后[解码器]依然会运行(主程序中可以关闭解码器进程)。

#### 注意
1. 基于VS 2017 旗舰版开发，.NET 4.5.5开发环境（理论可修改至.NET 3.5），源码包括一些c# 6.0+语法，如果你在VS 2015甚至更低的VS版本上编译不通过的话，请自行修改中源码不兼容的部分。

2. 本项目会开源并且正式发布。


#### 下载&安装&修改
你可以通过以下两种方式获取 **`DMSkin.WPF.dll`**：

#### 1. [直接下载 DMSkin.WPF.dll](https://github.com/944095635/DMSkin-for-WPF/releases/download/2.5.0.1/Release.zip)

这种方式的缺点是你下载到的**dll**并不总是最新的。

#### 2. [下载源码](https://github.com/944095635/DMSkin-for-WPF/archive/master.zip) 然后自己编译
点击 `DMSkin-for-WPF.sln` 打开项目， 右击解决方案资源管理器上的DMSkin.WPF 然后点击**生成**按钮即可编译获得**dll**文件. 接着打开资源管理器，你会发现 `bin\Debug`目录下已经多了一个DMSkin.WPF.dll.

还有一些其它的方法可以获取到 `DMSkin.WPF.dll`和源码：

- Nuget  `PM> Install-Package DMSkin.WPF -Version 2.5.0.4`
- Git  `git clone git@github.com:944095635/DMSkin-for-WPF.git`

## 用法 & 配置
#### 1. 创建一个WPF项目
#### 2. [添加 DMSkin.WPF.dll 引用](https://www.jb51.net/softjc/466183.html)
#### 3. 添加 App.xaml Resources
````xml
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!--  样式分离 不用的可以不引用 减少内存暂用  -->
                <!--  DMSKin内置转换器 配色  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;Component/Styles/DMSkin.xaml" />
                <!--  DMSKin内置滚动容器  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;Component/Styles/DMScrollViewer.xaml" />
                <!--  DMSKin内置SVG图标  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMIcon.xaml" />
                <!--  DMSKin内置按钮  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMButton.xaml" />
                <!--  DMSKin内置选择框  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMCheckBox.xaml" />
                <!--  DMSKin内置动画  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;Component/Styles/Animation.xaml" />
                <!--  DMSKin内置输入框  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMTextBox.xaml" />
                <!--  DMSKin内置滑动  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMSlider.xaml" />
                <!--  DMSKin提示框  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMToolTip.xaml" />
                <!--  DMSKin右键菜单  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMContextMenu.xaml" />
                <!--  DMSKin其他样式  -->
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMTabControl.xaml" />
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMRadioButton.xaml" />
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMTreeView.xaml" />
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMDataGrid.xaml" />
                <ResourceDictionary Source="pack://application:,,,/DMSkin.WPF;component/Styles/DMListBox.xaml" />
                <!--  最后加载项目其他的样式  -->
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
````
#### 4. 修改 `MainWindow.cs`
这里以引入单层方案 `DMSkinSimpleWindow` 为例，使用双层方案将`DMSkinSimpleWindow`改成`DMComplexWindow`即可。
````csharp
+ using DMSkin.WPF;
  using System.Windows;

  namespace DMSkinTest
  {
-      public partial class MainWindow : Window
+      public partial class MainWindow : DMSkinSimpleWindow
      {
          public MainWindow()
          {
              InitializeComponent();
          }
      }
  }
````

#### 5. 修改 `MainWindow.xaml`
````xml
- <Window x:Class="DMSkinTest.MainWindow" 
+ <DMSkin:DMSkinSimpleWindow
+         x:Class="DMSkinTest.MainWindow" <!-- 这里你需要将 DMSkinTest 换成你自己的项目名 -->  
+         xmlns:DMSkin="clr-namespace:DMSkin.WPF;assembly=DMSkin.WPF" 
          xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
          xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
          xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
          mc:Ignorable="d">
      <Grid>

      </Grid>
- </Window>
+ </DMSkin:DMSkinSimpleWindow>
````
若想使用双层方案，需要将上方的`DMSkinSimpleWindow`改成`DMComplexWindow`。
#### 6. 添加系统按钮 (可选)
````xml
<!-- 将下面的代码添加进 MainWindow.xaml -->
<!-- 系统按钮属性:
  -- DMSystemButtonSize // 系统按钮大小
  -- DMSystemButtonForeground // 系统按钮前景色
  -- DMSystemButtonHoverColor // 光标悬浮在按钮上时系统按钮的背景色
  -- DMSystemButtonHoverForeground // 光标悬浮在按钮上时系统按钮的前景色
  -- DMSystemButtonCloseHoverColor // 系统关闭按钮的颜色
  -->
<WrapPanel Height="{Binding DMSystemButtonSize}" Orientation="Horizontal" 
            VerticalAlignment="Top" HorizontalAlignment="Right">
  <controls:DMSystemMinButton DMSystemButtonSize="50" 
            DMSystemButtonHoverForeground="#383838" DMSystemButtonForeground="#383838">
			</controls:DMSystemMinButton>
  <controls:DMSystemMaxButton DMSystemButtonSize="50" 
            DMSystemButtonHoverForeground="#FFFFFF" DMSystemButtonForeground="#383838">
			</controls:DMSystemMaxButton>
  <controls:DMSystemCloseButton DMSystemButtonSize="50" 
            DMSystemButtonHoverForeground="#FFFFFF" DMSystemButtonForeground="#383838">
			</controls:DMSystemCloseButton>
</WrapPanel>
````

#### 7. 配置你的 DFW 窗体属性 (可选)
````js
DMWindowShadowSize="10"               // 窗体阴影大小
DMWindowShadowColor="#FFC8C8C8"       // 窗体阴影颜色
DMWindowShadowOpacity="0.8"           // 窗体阴影透明度
DMWindowShadowDragVisibility="False"  // 当窗体被拖动时是否显示阴影
DMWindowShadowVisibility="False"      // 是否显示窗体阴影
DMWindowShadowBackColor="#FF323CAD"   // 阴影背景色 (只对双层方案有效)
````

#### 8.制作圆角窗体 (可选)
````xml
<Border Background="White" CornerRadius="5"  BorderThickness="1">
        <Border.Effect>
            <DropShadowEffect BlurRadius="12" ShadowDepth="0" Color="#88000000"/>
        </Border.Effect>
        <Grid Margin="0，0，0，0">
            <Grid Background="Transparent">
                <Grid.RowDefinitions>
                    <RowDefinition Height="30"></RowDefinition>
                    <RowDefinition Height="*"></RowDefinition>
                    <RowDefinition Height="30"></RowDefinition>
                </Grid.RowDefinitions>
                <Grid Grid.Row="0" Name="DMTitle">
                </Grid>
            </Grid>
            <ResizeGrip VerticalContentAlignment="Bottom" HorizontalContentAlignment="Right" 
			HorizontalAlignment="Right" VerticalAlignment="Bottom"></ResizeGrip>
        </Grid>
</Border>
````

## 效果预览

<img src="https://gitee.com/DreamMachine/Image/raw/master/Preview1.jpg" width="600" height="400" align="center">
<img src="https://gitee.com/DreamMachine/Image/raw/master/Preview2.png" width="600" height="400" align="center">

## 联系
欢迎加入我们：

- **[C# .NET (2000人) QQ交流群](http://qm.qq.com/cgi-bin/qm/qr?k=reTIeglEELMIW267mOO7amouFFwhJwwP)**

- **DMSkin QQ交流群: 194684812**

- **WPF 课程学习群 (收费): 611509631**
- **<a href="http://dmskin.lolimay.cn" target="_blank">联系作者</a>**
- **[DMSkin官方网站](http://www.dmskin.com)**

## 捐赠
如果你觉得这个框架真的对你很有帮助，欢迎给我捐赠，这将鼓励我做的更好!

<img src="http://p40kjburh.bkt.clouddn.com/18-6-13/9034578.jpg" width="500">

## 更新日志
### 2.5.0 (2018-06-07)
1. 将2个项目合二为一。

2. 添加一些WPF 常用的class 如ViewModelBase，UI调度器，转换器。

3. 加入了水印输入框等，代码重构，准备发布到Nuget，以后可以通过Nuget安装 和 更新。

### 2.1.0 (2018-04-17)
1. 修改逻辑，目前窗口支持MVVM。

2. 修复一个启动时阴影分层的BUG。

3. 系统按钮被分离出窗口模板，具体查看本文底部使用方法。

### 2.0.1 (2018-01-30)
1. 新增一个窗口Demo。

### 2.0.0 (2017-10-15)
1. 移除WindowMode。

2. 目前WIN7有点小瑕疵。

### 1.3.0 (2017-09-21)
1. Win7以及以下采用单层。

2. Win8、Win10采用双层。

### 1.2.4 (2017-09-21)
1. 窗口边缘拉伸(右，右下，下)。

2. 阴影恢复速度调为200ms。

3. 阴影可以完全关闭(高效率，配合窗口虚线使用)。

### 1.1.3 (2017-09-20)
1. 修复ALT+TAB 出现2个窗体的BUG。

2. 阴影层背景色，拉伸 拖拽时 出现的颜色。选择跟主窗体 接近的颜色 用户体验更好。

### 1.1.2 (2017-09-20)
1. 修复多个窗口无法激活聚焦的BUG。

2. 拖动窗口支持显示阴影层。

3. 阴影层延迟显示的BUG修复。

### 1.1.1 (2017-09-19)
1. 优化最小化恢复阴影顺序，不会像网易云音乐一样出现双层了。

2. 去除窗口裁剪代码(之前的裁剪操作多此一举)。

3. 拖动窗口位置时隐藏阴影提高效率。

### 1.0.0 (2017-09-13)
1. 最小化动画终于解决，此方案可以移植到winform无边框中，这是我所知道的世界第一例WPF/winfrom无边框最小化动画方案。

**备注:**

【1.0版本】采用双层窗体+Win32实现无边框，1.0版本不支持圆角窗体，不支持窗体透明，但是拥有完美最小化的动画。如果采用虚线边框，则可以去除双层窗体。

【1.0版本之前】采用WindowStyle.None + 透明实现无边框，版本缺陷是无边框通病，窗体最小化 动画失效了。但是我用xaml实现了动画(动画流畅程度取决于显卡)。

### 0.8.0 (2017-08-26)
1. 修复最小化动画以及恢复动画(尚可优化)。

### 0.7.0 (2017-08-25)
1. 代码托管到GITHUB。

2. 新增Demo:周杰伦音乐播放器。

3. 新增Demo:默认模板窗体。

### 0.6.0 (2017-03-06)
1. 新增DMSystemButtonHoverColor 系统按钮鼠标悬浮的背景色(圆角窗体请设为透明，效果更好)。

2. 新增窗体模式：扁平化Metro+阴影Shadow 2种风格窗体。


## MIT License
Copyright © 2018 <copyright holders>

Permission is hereby granted， free of charge， to any person obtaining a copy of this software and associated documentation files (the “Software”)， to deal in the Software without restriction， including without limitation the rights to use， copy， modify， merge， publish， distribute， sublicense， and/or sell copies of the Software， and to permit persons to whom the Software is furnished to do so， subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”， WITHOUT WARRANTY OF ANY KIND， EXPRESS OR IMPLIED， INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY， FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM， DAMAGES OR OTHER LIABILITY， WHETHER IN AN ACTION OF CONTRACT， TORT OR OTHERWISE， ARISING FROM， OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
