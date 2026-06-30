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
using System.Windows.Threading;
using TagLib;
using System.Diagnostics.Eventing.Reader;
namespace u盘音乐播放器
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer=new DispatcherTimer();//时间创建
        bool tp = false;//暂停与否
        #region 确定播放的歌曲是哪一个，已经规定了范围了，也做了随机播放数方法，里面还初始化了焦点，还有播放模式模式
        private int _order;
        int lastorder;//判断此前是否播放过一首歌
        bool lastset=false;
        int order
        {
            get { return _order; }
            set
            {
                if(lastset)
                {
                    lastorder = _order;
                }
                else
                {
                    lastset= true;
                    lastorder = value;
                }    
                if (value >= playermusic.Count) _order = 0;
                else _order= value;
            }
        }
        bool focus=false;
        char modelchange = '1';
        Random random = new Random();
        int rollorder()
        {
            int x = random.Next(playermusic.Count);
            if (x == order)
            {
                x++;
            }
            return x;
        }
        #endregion
        //直接使用order当作播放列表的次序，focus是焦点初始失焦，modelchange是char，‘1’‘2’‘3’分别对应顺序.随机.循环
        public MainWindow()
        {
            crefile();
            InitializeComponent();
            _=cache();
            timer.Interval=TimeSpan.FromMilliseconds(500);
            timer.Tick += timertick;
            player.MediaEnded += (s, e) =>
            {
                switch (modelchange)
                {
                    case '1': order++; musicchange(); break;
                    case '2': order = rollorder(); musicchange(); break;
                    case '3': musicchange(); break;
                }
            };
        }
        private void timertick(object sender, EventArgs e)
        { 
            bar.Value = player.Position.TotalSeconds;
            show.Content= player.Position.ToString(@"mm\:ss")+"/"+ TimeSpan.FromSeconds(playermusic[order].during).ToString(@"mm\:ss");
        }

        void musicchange()//直接使用，记得更新order，即可切换为需要播放的那一首，顺带着标题更新，timer启动，不检测焦点，还有要记得确定用哪个音乐list，更新了进度条
        {
            player.Pause();
            timer.Stop();
            player.Source = new Uri(playermusic[order].url);
            title.Content = playermusic[order].name;
            player.Play();
            
            bar.Maximum = playermusic[order].during;
            show.Content = "00:00/" + TimeSpan.FromSeconds(playermusic[order].during).ToString(@"mm\:ss");
            timer.Start();
            focus = true;
            tp = true;
        }
        void crefile()//检测所需的文件夹
        {
            string[] detection = new string[]
            {
        @"E:\个人听歌\MusicCollection\Anothermusic",
        @"E:\个人听歌\MusicCollection\Awakemusic",
        @"E:\个人听歌\MusicCollection\Highmusic",
            };

            foreach (string item in detection)
            {
                if (!Directory.Exists(item))
                {
                    Directory.CreateDirectory(item);
                }
            }
        }
        #region 缓存模块-----------------------------------------------------------------------------------------------------------------------
        public class musiccache//缓存列表模板
        {
            public string name;
            public string url;
            public double during;
        }
        List<musiccache> highmusician = new List<musiccache>(50);
        List<musiccache> awakemusician = new List<musiccache>(50);
        List<musiccache> anothermusician = new List<musiccache>(50);
        List<musiccache> allmusician = new List<musiccache>(200);
        List<musiccache> playermusic;
        async Task cache()//给外界调用的方法，自动刷新列表
        {
            title.Content = "正在更新列表当中......";
            oncelabel.Content = "刷新中";
            timer.Stop();
            await _cache(@"E:\个人听歌\MusicCollection\Anothermusic", anothermusician);
            await _cache(@"E:\个人听歌\MusicCollection\Awakemusic", awakemusician);
            await _cache(@"E:\个人听歌\MusicCollection\Highmusic", highmusician);
            allmusician.AddRange(awakemusician);
            allmusician.AddRange(highmusician);
            allmusician.AddRange(anothermusician);
            ui(allmusician, alllist);
            ui(awakemusician, awakelist);
            ui(highmusician, highlist);
            ui(anothermusician,anotherlist);
            title.Content = "更新完毕，已获取" + allmusician.Count.ToString() + "首音乐资源";
            oncelabel.Content = "总表";
        }
        void ui(List<musiccache> music_,ListBox list)//更新ui列表————输入对应的list名字和列表控件名字，会自动更新对应列表
        {
            foreach(var item in music_)
            {
                list.Items.Add(item.name);
            }
        }
        async Task _cache(string place, List<musiccache> musician_)//缓存数据用————输入（@+音乐文件地址，对应的list缓存列表），会自动检索文件夹内音乐缓存进列表，记得调用前加给await异步
        {
            string[] musicianpath_ = Directory.GetFiles(place);
            foreach (string item in musicianpath_)
            {
                musiccache music_ = new musiccache();
                music_.name = System.IO.Path.GetFileName(item);
                music_.url = item;
                music_.during = await getduring(item);
                musician_.Add(music_);
            }
        }
        async Task<double> getduring(string item)//异步获取音乐时长的方法————不要乱动
        {
            return await Task.Run(() =>
            {
                using (var file = TagLib.File.Create(item))
                {
                    return file.Properties.Duration.TotalSeconds;
                }
            });
        }

        #endregion
        #region 切换控件————————切换列表显示，顺便抹除焦点
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            alllist.Visibility=Visibility.Visible;
            oncelabel.Content = "总表";
            anotherlist.Visibility = Visibility.Collapsed;
            awakelist.Visibility = Visibility.Collapsed;
            highlist.Visibility = Visibility.Collapsed;
            focus = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            alllist.Visibility = Visibility.Collapsed;
            anotherlist.Visibility = Visibility.Collapsed;
            awakelist.Visibility = Visibility.Visible;
            oncelabel.Content = "缓音乐";
            highlist.Visibility = Visibility.Collapsed;
            focus=false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            alllist.Visibility = Visibility.Collapsed;
            anotherlist.Visibility = Visibility.Collapsed;
            awakelist.Visibility = Visibility.Collapsed;
            highlist.Visibility = Visibility.Visible;
            oncelabel.Content = "烈音乐";
            focus =false;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            alllist.Visibility = Visibility.Collapsed;
            anotherlist.Visibility = Visibility.Visible;
            oncelabel.Content = "其他音乐";
            awakelist.Visibility = Visibility.Collapsed;
            highlist.Visibility = Visibility.Collapsed;
            focus = false;
        }
# endregion
        #region 列表点击事件————————恢复焦点，启动计时器，音乐播放
        private void alllistchanged(object sender, RoutedEventArgs e)//总表的点击逻辑
        {
            playermusic = new List<musiccache>(allmusician);
            order = alllist.SelectedIndex;

            musicchange();
            stsp.Content = "暂停";
        }
        private void awakelistchanged(object sender, RoutedEventArgs e)
        {
            playermusic = new List<musiccache>(awakemusician);
            order = awakelist.SelectedIndex;

            musicchange();
            stsp.Content = "暂停";
        }
        private void highlistchanged(object sender, RoutedEventArgs e)
        {
            playermusic = new List<musiccache>(highmusician);
            order = highlist.SelectedIndex;

            musicchange();
            stsp.Content = "暂停";
        }
        private void anotherlistchanged(object sender, RoutedEventArgs e)
        {
            playermusic = new List<musiccache>(anothermusician);
            order = anotherlist.SelectedIndex;

            musicchange();
            stsp.Content = "暂停";
        }
        #endregion
        #region 关于切换和刷新的四个按键的配置
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(focus)
            {
                switch (modelchange)
                {
                    case '1': order--; musicchange(); break;
                    case '2': order = lastorder; musicchange(); break;
                    case '3': musicchange(); break;
                }
            }
            else
            {
                MessageBox.Show("请先从列表中选取一首歌曲");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(focus)
            {
                switch(modelchange)
                {
                    case '1':order++;musicchange();break;
                    case '2':order = rollorder();musicchange() ;break;
                    case '3':musicchange() ;break;
                }
            }
            else
            {
                MessageBox.Show("请先从列表中选取一首歌曲");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            switch (modelchange)
            {
                case '1':secondlabel.Content = "随机播放";modelchange = '2';break;
                case '2':secondlabel.Content = "单曲循环";modelchange = '3';break;
                case '3':secondlabel.Content = "顺序播放";modelchange = '1';break;
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("记住这里有bug，有时间回来修");
            //player.Source = null;
            //allmusician.Clear();
            //awakemusician.Clear();
            //highmusician.Clear();
            //anothermusician.Clear();
            //_ = cache();
            //focus = false;
            //tp = true;
            //stsp.Content = "暂停";
        }
        private void stsp_Click(object sender, RoutedEventArgs e)
        {
            if (focus)
            {
                if (tp)
                {
                    player.Pause();
                    timer.Stop();
                    tp = false;
                    stsp.Content = "启动";
                }
                else
                {
                    player.Play();
                    timer.Start();
                    tp= true;
                    stsp.Content = "暂停";
                }
            }
            else
            {
                MessageBox.Show("请先从列表中选取一首歌曲");
            }
        }


        #endregion
        #region 进度条的相关设置
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (focus)
            {
                show.Content = TimeSpan.FromSeconds(e.NewValue).ToString(@"mm\:ss") + "/" + TimeSpan.FromSeconds(playermusic[order].during).ToString(@"mm\:ss");
            }
        }
        private void st(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            timer.Stop();
        }
        private void co(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            player.Position = TimeSpan.FromSeconds(bar.Value);
            timer.Start();
        }
        #endregion
    }
}