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
using System.Text.RegularExpressions;

namespace TimeTable
{
    /// <summary>
    /// TimeTableWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TimeTableWindow : Window
    {
        public TimeTableWindow()
        {
            InitializeComponent();

            GetData();
            CreateGrid();


        }

        // 講義数
        public int count;
        // 講義ID 二次元リスト
        public List<List<int>> lectid;
        // 講義一覧
        public List<Lecture> lectures;
        // 開始.終了時間
        public List<LectTime> lecttime;

        void GetData()
        {
            var data = MainWindow.data;

            count = data.setting.period;
            lectid = data.lectid;
            lectures = data.lectures;
            lecttime = data.lecttime;


            int lec_c = lecttime.Count();
            if (lec_c != count)
            {
                if (lec_c > count)
                {
                    var lecttime_tmp = new List<LectTime>();
                    for (int i = 0; i < count; i++)
                    {
                        lecttime_tmp.Add(lecttime[i]);
                    }
                    lecttime = lecttime_tmp;
                }
                else
                {
                    for (int i = 0; i < count - lec_c; i++)
                    {
                        var t = new LectTime();
                        t.starthour = 0;
                        t.startminute = 0;
                        t.endhour = 0;
                        t.endminute = 0;
                        lecttime.Add(t);
                    }
                }
            }

            if (lectid.Count() != 6)
            {
                var lectid_c = lectid.Count();
                // データ型エラー時 月-土分の不足を追加
                for(int i = 0; i < 6 - lectid_c; i++)
                {
                    var t = new List<int>();
                    t.Add(0);
                    lectid.Add(t);
                }
            }


            for(int i = 0; i < 6; i++)
            {
                var c = lectid[i].Count();
                // TODO 講義数に揃える
                if(c != count)
                {
                    if(c > count)
                    {
                        var item_tmp = new List<int>();
                        for(int j = 0; j < count; j++)
                        {
                            item_tmp.Add(lectid[i][j]);
                        }
                        lectid[i] = item_tmp;
                    }
                    else
                    {
                        for(int j = 0; j < count - c; j++)
                        {
                            lectid[i].Add(0);
                        }
                    }
                }
            }
        }

        void CreateGrid()
        {
            var tablegrid = this.FindName("TableGrid") as RuledLineGrid;

            for(int i = 0; i < count; i++)
            {
                var rowdef = new RowDefinition();
                rowdef.Height = new GridLength(50);
                tablegrid.RowDefinitions.Add(rowdef);
            }

            for (int period = 0; period < count; period++)
            {
                var btn_t = new Button();
                btn_t.Content = ConnectTimeText(period, lecttime[period]);
                btn_t.SetValue(RuledLineGrid.RowProperty, period + 1);
                btn_t.SetValue(RuledLineGrid.ColumnProperty, 0);
                btn_t.Style = FindResource("ButtonTemplate") as Style;
                btn_t.Margin = new Thickness(1, 1, 1, 1);
                btn_t.Name = String.Format("timebtn_{0}", period);
                btn_t.Click += PushTimeButton;
                tablegrid.Children.Add(btn_t);

                for (int dayoftheweek = 0; dayoftheweek < 6; dayoftheweek++)
                {
                    var btn = new Button();
                    if (lectid[dayoftheweek][period] != 0)
                    {
                        btn.Content = ConnectLectText(lectid[dayoftheweek][period]);
                    }
                    else
                    {
                        btn.Content = "未設定";
                    }
                    btn.SetValue(RuledLineGrid.RowProperty, period + 1);
                    btn.SetValue(RuledLineGrid.ColumnProperty, dayoftheweek + 1);
                    btn.Style = FindResource("ButtonTemplate") as Style;
                    btn.Margin = new Thickness(1, 1, 1, 1);
                    btn.Name = String.Format("lectbtn_{0}", dayoftheweek + period * 6);
                    btn.Click += PushLectButton;
                    tablegrid.Children.Add(btn);
                }
            }

            tablegrid.GridShaping();
        }

        string ConnectTimeText(int time, LectTime lecttime)
        {
            String res = String.Format("{0}\n{1}:{2:00}～{3}:{4:00}", time+1, lecttime.starthour, lecttime.startminute, lecttime.endhour, lecttime.endminute);
            return res;
        }

        string ConnectLectText(int lectid)
        {
            var lecture = lectures.Find(m => m.id == lectid);
            // 教科, 教授
            var res = String.Format("{0}\n{1}", lecture.name, lecture.professor);
            return res;
        }

        void PushTimeButton(object sender, RoutedEventArgs e)
        {
            // 講義時間の方のボタン
            var btn = (Button)sender;
            var name = btn.Name;
            var sid = Regex.Replace(name, "[^0-9]", "");
            var id = int.Parse(sid);

            var win = new TimeTable_SetTimeWindow(id, lecttime[id]);
            win.ShowDialog();

            lecttime[id] = win.lecttime;
            btn.Content = ConnectTimeText(id, lecttime[id]);
        }

        void PushLectButton(object sender, RoutedEventArgs e)
        {
            // 講義の方のボタン
            var btn = (Button)sender;
            var name = btn.Name;
            var sid = Regex.Replace(name, "[^0-9]", "");
            var id = int.Parse(sid);
            int period = id / 6;
            int dayoftheweek = id % 6;

          //  var win = ;
        }

        void DecisionBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}