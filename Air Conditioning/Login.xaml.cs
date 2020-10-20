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
using System.Windows.Shapes;
using NLECloudSDK;
using Air_Conditioning;
using System.Threading;

namespace Air_Conditioning
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>


    public partial class Login : Window//登录窗口类
    {
        public string Schoolname;
        public static string accessToken;
        public static NLECloudAPI SDK = null;//实例化云平台API
        
        public Login()
        {//主窗口对象
            InitializeComponent();
            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//WPF窗体在屏幕上居中
        }
        #region --账号密码判空-- 

        public void SentencedToEmpty()
        {//账号密码判空
            while (getUsername.Text == "" || getPassword.Password == "")
            {
                if (getUsername.Text == "")
                {
                    MessageBox.Show("账号不能为空！", "登陆失败");
                    this.getUsername.Focus();
                    return;
                }
                if (getPassword.Password == "")
                {
                    MessageBox.Show("密码不能为空！", "登陆失败");
                    this.getPassword.Focus();
                    return;
                }
            }
        }
        #endregion
        public void Button_Click_login(object sender, RoutedEventArgs e)
        {
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            SentencedToEmpty();
            ProgressBar.IsIndeterminate =true ;
            //string LocalLoginAuthentication = ""
            AccountLoginDTO data_account = new AccountLoginDTO()//实例化登录信息
            {
                Account = getUsername.Text,//从xaml中的TextBox中的name中获取账号文本
                Password = getPassword.Password//从xaml中的PasswordBox中的name中获取密码
            };


            /*
             * 自己写的账号密码判空提示
             * 缺陷：代码冗余，且不符合使用逻辑
             * 改进：使用this.getUsername.Focus聚焦文本框提示用户、使用return跳出方法
            while (LocalLoginAuthentication == "")
            {
                LocalLoginAuthentication = "账号密码判空通过";
                if (getUsername.Text == "")
                {//账号判空提示
                    MessageBox.Show("账号不能为空！", "登陆失败");
                    LocalLoginAuthentication = "";
                    break;
                }
                //密码判空提示
                if (getPassword.Password == "")
                {
                      LocalLoginAuthentication = "";
                      MessageBox.Show("密码不能为空！", "登陆失败");
                      break;
                }
            }
            */
            //Thread.Sleep(5000);//全界面Sleep 5秒
            ResultMsg<AccountLoginResultDTO> data_account_verify = SDK.UserLogin(data_account);
            if (data_account_verify.IsSuccess())
            {//问题：输入账号时，textbox内预留的提示文字应消失（×）
             //问题：password内没有预留的提示文字（√）
             //问题：记住密码（×）
                
                Schoolname = data_account_verify.ResultObj.CollegeName;//获取ResultObj.CollegeName学校名
                accessToken = data_account_verify.ResultObj.AccessToken;//获取ResultObj.AccessToken,用于后续API调用
                MessageBox.Show("登陆成功！，您位于" + Schoolname + "\n即将进入空调控制器！", "登录成功");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("登录失败！，请重试", "登录失败");
            }
        }
    }
    
}
