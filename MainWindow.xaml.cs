using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Panuon.UI.Silver;
using KMCCC.Launcher;
using KMCCC.Authentication;

namespace ECL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        public static LauncherCore Core = LauncherCore.Create();
        LoginUI.LiXian LiXian = new LoginUI.LiXian();
        //LoginUI.WeiRuan WeiRuan = new LoginUI.WeiRuan();
        LoginUI.ZhengBan ZhengBan = new LoginUI.ZhengBan();
       
        public int launchMode;
        public MainWindow()
        {
            InitializeComponent();
            var versions = Core.GetVersions().ToArray();
            versionCombo.ItemsSource = versions;//绑定数据源
            MessageBox.Show( "温馨提示：在使用前请填写Java路径和选择登录模式");
        }
        public void GameStart()//定义启动游戏函数
        {
              LaunchOptions launchOptions = new LaunchOptions();
            switch (launchMode)
            {
                
                case 1:
                    launchOptions.Authenticator = new OfflineAuthenticator(LiXian.IdTextbox.Text);
                    break;
                case 2:
                    launchOptions.Authenticator = new YggdrasilLogin(ZhengBan.Email.Text, ZhengBan.password.Password, false);
                    break;
            }
            
            launchOptions.MaxMemory = Convert.ToInt32(MemoryTextbox.Text);

            Core.JavaPath = javaCombo.Text+@"\bin\javaw.exe";
            var ver = (KMCCC.Launcher.Version)versionCombo.SelectedItem;
            launchOptions.Version = ver;
            var result = Core.Launch(launchOptions);
            
            if (!result.Success)
            {
                switch (result.ErrorType)
                {
                    case ErrorType.NoJAVA:
                        MessageBoxX.Show("java错误，详情信息：" + result.ErrorMessage, "错误");
                        break;
                    case ErrorType.AuthenticationFailed:
                        MessageBoxX.Show("登录错误，详情信息：" + result.ErrorMessage, "错误");
                        break;
                    case ErrorType.UncompressingFailed:
                        MessageBoxX.Show("文件错误，详情信息：" + result.ErrorMessage, "错误");
                        break;
                    default:
                        MessageBoxX.Show(result.ErrorMessage, "错误");
                        break;
                }
            }
            
            }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GameStart();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentControl1.Content = new Frame 
            { 
                Content = LiXian 
            };
            launchMode = 1;
        }
        /// <summary>
        /// 离线登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentControl1.Content = new Frame 
            {
                Content = ZhengBan 
            };
            launchMode = 2;
        }
        /// <summary>
        /// 正版登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ContentControl1.Content = new Frame
            {
                Content = WeiRuan
            };
            launchMode = 3;
        }
        /// <summary>
        /// 微软登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
    }
}
