using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace 傻逼网易云
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] musicain;//音乐文件名称和地址
        string[] name;//音乐名称
        private int relist;//和下面坐一桌
        public int list//循环列表，有大小限制
        {
            get { return relist; }
            set
            {
                relist=value;
                if(relist>musicain.Length-1)
                {
                    relist = 0;
                }
                else if(relist<0)
                {
                    relist=musicain.Length-1;
                }
            }
        }
        bool stsp = true;
        bool ch = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 自动事件

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)//录入事件
        {
            musicain = Directory.GetFiles(@"D:\aaa.mymusic");
            name=new string[musicain.Length];
            for (int i = 0; i < musicain.Length; i++)
            {
                name[i]=System.IO.Path.GetFileName(musicain[i]);
                listt.Items.Add(name[i]);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//暂停/播放键
        {
            if (ch)
            {
                if (stsp)
                {
                    music.Play();
                    stsp = false;
                    tp.Content = "暂停";
                }
                else
                {
                    music.Pause();
                    stsp = true;
                    tp.Content = "播放";
                }
            }
            else
            {
                MessageBox.Show("请先选择要播放的歌曲");
            }
        }

        private void listt_SelectionChanged(object sender, SelectionChangedEventArgs e)//歌曲列表事件
        {
            list = listt.SelectedIndex;
            musicshow.Content=name[list];
            music.Source=new Uri(musicain[list]);
            ch=true;
        }

        private void change_Click(object sender, RoutedEventArgs e)//下一首
        {
            if (ch)
            {
                list++;
                musicshow.Content = name[list];
                music.Source = new Uri(musicain[list]);
            }
            else
            {
                MessageBox.Show("在未播放音乐时无法切换上一首或下一首");
            }
        }

        private void rechange_Click(object sender, RoutedEventArgs e)//上一首
        {
            if (ch)
            {
                list--;
                musicshow.Content = name[list];
                music.Source = new Uri(musicain[list]);
            }
            else
            {
                MessageBox.Show("在未播放音乐时无法切换上一首或下一首");
            }
        }
    }
}