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
                if(lec_c > count)
                {
                    var lecttime_tmp = new List<LectTime>();
                    for(int i = 0; i < count; i++)
                    {
                        lecttime_tmp.Add(lecttime[i]);
                    }
                    lecttime = lecttime_tmp;
                }
                else
                {
                    for(int i = 0; i < lec_c - count; i++)
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

            if(lectid.Count != 6)
            {
                // TODO データ型エラー時 月-土分の不足を追加
            }

            foreach(var item in lectid)
            {
                // TODO 講義数に揃える
            }
        }

        void CreateGrid()
        {
            var tablegrid = this.FindName("TableGrid") as RuledLineGrid;

        }
    }
}
