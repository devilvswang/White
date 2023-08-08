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
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows.Navigation;
using static WhiteWpf.NotificationWindow;

namespace WhiteWpf
{
    /// <summary>
    /// ReceiveMsg.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiveMsg : Window
    {
        private NotifyIcon notifyIcon;
        DispatcherTimer icoTimer = new DispatcherTimer();
        string icoUrl = @"../../wechat.ico";
        string icoUrl2 = @"../../nothing.ico";
        public long i = 0;
        int j = 0;
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        public ReceiveMsg()
        {
            InitializeComponent();
            //不在任务栏显示
            this.ShowInTaskbar = false;
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "消息通知";
            this.notifyIcon.ShowBalloonTip(2000);
            this.notifyIcon.Text = "消息通知";
            //this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.notifyIcon.Icon = new System.Drawing.Icon(@"../../wechat.ico");
            //this.notifyIcon.Icon = new System.Drawing.Icon(@"AppIcon.ico");
            this.notifyIcon.Visible = true;
            //打开菜单项
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("Open");
            open.Click += new EventHandler(Show);
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");
            exit.Click += new EventHandler(Close);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            {
                if (e.Button == MouseButtons.Left) this.Show(o, e);
                j++;


                List<NotifyData> data = new List<NotifyData>();
                data.Add(new NotifyData { HeadImg = "../../head1.jpg", Name = "WN", Count = "2" });
                data.Add(new NotifyData { HeadImg = "../../head1.jpg", Name = "TEST", Count = "3" });

                showNotify(data);
            });

            this.notifyIcon.BalloonTipShown += new EventHandler((o, e) =>
            {
                //System.Windows.Forms.MessageBox.Show("WPF测试页面");

            });


            //闪烁图标
            icoTimer.Interval = TimeSpan.FromSeconds(0.3);
            icoTimer.Tick += new EventHandler(IcoTimer_Tick);
            icoTimer.Start();
        }

        private void showNotify(List<NotifyData> data)
        {
            NotificationWindow dialog = new NotificationWindow();//new 一个通知
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            dialog.DataContext = data;//设置通知里要显示的数据
            dialog.Show();

            _dialogs.Add(dialog);

        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }

        public void IcoTimer_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            if (i % 2 != 0)
            {
                this.notifyIcon.Icon = new System.Drawing.Icon(icoUrl);
            }
            else
            {
                this.notifyIcon.Icon = new System.Drawing.Icon(icoUrl2);
            }
        }

        private void Show(object sender, EventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Visible;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        private void Hide(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Close(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }






}
