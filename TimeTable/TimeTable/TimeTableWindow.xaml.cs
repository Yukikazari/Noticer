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
            Console.WriteLine(lecttime.Count());

            for(int row = 0; row < count; row++)
            {
                Console.WriteLine(row);
                var btn_t = new Button();
                btn_t.Content = ConnectTimeString(row, lecttime[row]);
                btn_t.SetValue(RuledLineGrid.RowProperty, row + 1);
                btn_t.SetValue(RuledLineGrid.ColumnProperty, 0);
                btn_t.Style = FindResource("ButtonTemplate") as Style;
                btn_t.Margin = new Thickness(1, 1, 1, 1);
                tablegrid.Children.Add(btn_t);

                for (int col = 0; col < 6; col++)
                {
                    var btn = new Button();
                }
            }

            tablegrid.GridShaping();
        }

        string ConnectTimeString(int time, LectTime lecttime)
        {
            String res = String.Format("{0}\n{1}:{2:00}～{3}:{4:00}", time+1, lecttime.starthour, lecttime.startminute, lecttime.endhour, lecttime.endminute);
            return res;
        }
    }
}