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

namespace WhiteWpf
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("WPF测试页面");
            //TestName.Content = "WPF测试页面";

            //NavigationWindow window = new NavigationWindow();
            //window.Source = new Uri("test.xaml", UriKind.Relative);
            //window.ShowDialog();//模式，弹出！
            //window.Show();//无模式，弹出！

            ReceiveMsg win1 = new ReceiveMsg();

            win1.Show();
        }

    }
}
