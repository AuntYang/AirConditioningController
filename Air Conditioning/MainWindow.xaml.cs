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
using NLECloudSDK;
using NLECloudSDK.Model;
using Air_Conditioning;
using System.Windows.Threading;


namespace Air_Conditioning
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window//主窗口类
    {
        public NLECloudAPI SDK = null;

        #region -- 主函数 --
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//WPF窗体在屏幕上居中
            this.Loaded += MainWindow_Loaded;
        }
        DispatcherTimer time;//实例化一个定时器
        #endregion

        #region -- 定时器 --
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(3);//设置定时时间
            //time.Tick += ModifyTemperatureThreshold;
            time.Tick += AirStatus;
            time.Tick += CurrentRoomTemperature;
            time.Tick += DeviceStatusQuery;
            time.Tick += UpperLimitTemperature;
            time.Tick += LowerLimitTemperature;
            time.Start();

        }
        #endregion

        #region --功能实现--
        private void DeviceStatusQuery(object sender,EventArgs e)
        {//设备在线/离线状态查询函数
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            var yxh = SDK.GetDeviceInfo(113177, Login.accessToken);
            try
            {
                if (yxh.ResultObj.IsOnline)
                {
                    StateTheQuery.Text = "在线";
                }
                else
                {
                    StateTheQuery.Text = "离线";
                }
            }
            catch (Exception ex)
            {
                StateTheQuery.Text = "无法查询\n"+ex;
            }
            finally
            {
                StateTheQuery.Text = "无法查询";
            }
        }
        private void AirStatus(object sender, EventArgs e)
        {//空调状态获取
            if(StateTheQuery.Text == "在线")
            {
                SDK = new NLECloudAPI("http://api.nlecloud.com");
                var yxh = SDK.GetSensorInfo(113177,"power", Login.accessToken);
                if ((int)yxh.ResultObj.Value == 1)
                {
                    air_mode.Text = "开启";
                    AirConditioningSwitch.IsChecked = true;//同步空调开关状态
                }
                else
                {
                    air_mode.Text = "关闭";
                    AirConditioningSwitch.IsChecked = false;//同步空调开关状态
                }
            }
            else
            {
                air_mode.Text = "设备离线，获取失败";
            }
        }
        private void CurrentRoomTemperature(object sender ,EventArgs e)
        {//当前室温获取
            SensorDataFuzzyQryPagingParas query = new SensorDataFuzzyQryPagingParas()
            {
                DeviceID = 113177,
                ApiTags = "currentTemp"
            };
            if(StateTheQuery.Text == "在线")
            {
                SDK = new NLECloudAPI("http://api.nlecloud.com");
                var yxh = SDK.GetSensorInfo(113177, "currentTemp", Login.accessToken);
                room_temperature.Text = yxh.ResultObj.Value.ToString();
            }
            else
            {
                room_temperature.Text = "设备离线，获取失败";
            }
            
        }
        private void UpperLimitTemperature(object sender ,EventArgs e)
        {//上限温度获取
            
            if (StateTheQuery.Text == "在线")
            {
                SDK = new NLECloudAPI("http://api.nlecloud.com");
                var yxh = SDK.GetSensorInfo(113177,"upperLimit",Login.accessToken);
                upper_temperature.Text = yxh.ResultObj.Value.ToString();
                UpperTemperatureChanges.Value = double.Parse(yxh.ResultObj.Value.ToString()); //同步上限温度值Slider数值
            }
            else
            {
                upper_temperature.Text = "设备离线，获取失败";
            }
                
        }
        private void LowerLimitTemperature(object sender, EventArgs e)
        {//下限温度获取
            
            if (StateTheQuery.Text == "在线")
            {
                SDK = new NLECloudAPI("http://api.nlecloud.com");
                var yxh = SDK.GetSensorInfo(113177, Login.accessToken);
                lower_temperature.Text = yxh.ResultObj.Value.ToString();
                LowerTemperatureChanges.Value = double.Parse(yxh.ResultObj.Value.ToString());//同步下限温度值Slider数值
            }
            else
            {
                lower_temperature.Text = "设备离线，获取失败";
            }
        }
        #endregion

        #region --修改上下限温度值(旧版本)--
        /*
        private void ModifyTemperatureThreshold(object sender, EventArgs e)
        {
            //int UpperTemperatureChangesValue;
            //int LowerTemperatureChangesValue;
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            var yxh = SDK.Cmds(113177, "upper_Limit", UpperTemperatureChanges.Value, Login.accessToken);
            yxh = SDK.Cmds(113177, "lower_Limit", LowerTemperatureChanges.Value, Login.accessToken);
        }
        private void click_upper(object sender, RoutedEventArgs e)
        {//修改上限温度值
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            var yxh = SDK.Cmds(113177, "upper_Limit", UpperTemperatureChanges.Value, Login.accessToken);
        }
        private void click_lower(object sender, RoutedEventArgs e)
        {//修改下限温度值
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            var yxh = SDK.Cmds(113177, "lower_Limit", LowerTemperatureChanges.Value, Login.accessToken);
        }
        */
        #endregion

        #region --空调开关--
        private void click_switch(object sender , RoutedEventArgs e)
        {//在checkbox控件发生点击事件时向云平台发送指令
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            var yxh = SDK.Cmds(113177, "poweCtrl", AirConditioningSwitch.IsChecked, Login.accessToken);
        }
        #endregion

        #region --上下限温度值Slider修改--
        private void UpperTemperatureChanges_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {//上限温度值修改,这里在执行云平台SDK时，Slider控件动画卡顿
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            SDK.Cmds(113177, "upperLimitCtrl", UpperTemperatureChanges.Value, Login.accessToken);
        }
        private void LowerTemperatureChanges_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {//下限温度值修改
            SDK = new NLECloudAPI("http://api.nlecloud.com");
            SDK.Cmds(113177, "lowerLimitCtrl", LowerTemperatureChanges.Value, Login.accessToken);
        }
        #endregion
    }
}
