using System;
using System.IO;
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
using System.Xml.Serialization;
using TimeTable.Properties;

namespace TimeTable
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;

        public MainWindow()
        {
            InitializeComponent();

            RecoverWindowBounds();

            data = new Data();

            try
            {
                using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "Data.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    data = (Data)serializer.Deserialize(fs);
                }

                var lt = new Dictionary<int, Lecture>();

                foreach(var obj in data.lectures_list)
                {
                    lt.Add(obj.id, obj);
                }

                data.lectures = lt;

                var tt = new Dictionary<int, Task>();

                foreach(var obj in data.tasks_list)
                {
                    tt.Add(obj.id, obj);
                }

                data.tasks = tt;

            }
            catch (FileNotFoundException e){}

            CreateLecturePanel();
            CreateTaskPanel();
        }

        public void UpdateWindow(object sender, RoutedEventArgs e)
        {
            // 更新ボタン押した時のヤツ。日付変更を想定
            CreateLecturePanel();
            CreateTaskPanel();
        }

        void RecoverWindowBounds()
        {
            var windowset = Settings.Default;
            // 左
            if (windowset.WindowLeft >= 0 &&
                (windowset.WindowLeft + windowset.WindowWidth) < SystemParameters.VirtualScreenWidth)
            { Left = windowset.WindowLeft; }
            // 上
            if (windowset.WindowTop >= 0 &&
                (windowset.WindowTop + windowset.WindowHeight) < SystemParameters.VirtualScreenHeight)
            { Top = windowset.WindowTop; }
            // 幅
            if (windowset.WindowWidth > 0 &&
                windowset.WindowWidth <= SystemParameters.WorkArea.Width)
            { Width = windowset.WindowWidth; }
            // 高さ
            if (windowset.WindowHeight > 0 &&
                windowset.WindowHeight <= SystemParameters.WorkArea.Height)
            { Height = windowset.WindowHeight; }
        }

        void CreateLecturePanel()
        {
            // 日付別で出す奴。for文っすかね

        }

        void CreateTaskPanel()
        {
            // こっちは適当に taskの子要素にButton増やすだけ
            tasks_panel.Children.Clear();

            if(data.tasks!= null)
            {
                foreach(var item in data.tasks)
                {
                    var btn = new Button();

                    btn.Name = "Btn" + item.Value.id.ToString();
                    btn.Click += (sender, e) => PushTaskButton(sender, e);
                    btn.Content = String.Format("{0}\n{1}\n{2}", item.Value.lecture, item.Value.name, item.Value.time);
                    btn.Style = FindResource("ButtonTemplate") as Style;
                    btn.Margin = new Thickness(5, 0, 0, 0);
                    btn.Width = 100;
                    btn.Height = 70;

                    tasks_panel.Children.Add(btn);
                }
            }
        }

        void PushLectureButton(object sender, RoutedEventArgs e)
        {
            // 講義ボタン 左右で飛ばす
        }

        void PushTaskButton(object sender, RoutedEventArgs e)
        {
            // 講義ボタン 左右で飛ばす
        }

        void PushLeftLectureButton(object sender)
        {
            // 講義の方のボタン押されたとき 左クリック
        }

        void PushRightLectureButton(object sender)
        {
            // 講義の方のボタン押されたとき 右クリック
        }

        void PushTaskButton(object sender)
        {
            // 課題の方のボタン押されたとき 左クリック
        }

        void PushLectureFinishButton(object sender, RoutedEventArgs e)
        {
            // 講義終了ボタン その講義の削除からの更新かな
        }

        void PushTaskFinishButton(object sender, RoutedEventArgs e)
        {
            // 課題終了ボタン 上記と同じく
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // バージョン設定
            data.version = "1.0.0";


            var lt = new List<Lecture>();

            foreach (var obj in data.lectures)
            {
                lt.Add(obj.Value);
            }

            data.lectures_list = lt;

            var tt = new List<Task>();

            foreach (var obj in data.tasks)
            {
                tt.Add(obj.Value);
            }

            data.tasks_list = tt;

            XmlSerializer serializer = new XmlSerializer(typeof(Data));

            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "Data.xml", FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }

            SaveWindowBounds();
        }

        void SaveWindowBounds()
        {
            var windowset = Settings.Default;
            WindowState = WindowState.Normal;
            windowset.WindowLeft = Left;
            windowset.WindowTop = Top;
            windowset.WindowWidth = Width;
            windowset.WindowHeight = Height;
            windowset.Save();
        }

        private void MenuItem_Setting_Click(object sender, RoutedEventArgs e)
        {
            var sw = new SetWindow();
            sw.ShowDialog();

            if (sw.IsChange)
            {
                var st = sw.set.day_st;
                var en = sw.set.day_en;

                data.setting = sw.set;

                if (data.setting.day_st != st || data.setting.day_en != en)
                {
                   CreateLecturePanel();
                }
            }
        }

        private void MenuItem_TimeTable_Click(object sender, RoutedEventArgs e)
        {
            var win = new TimeTableWindow();
            win.ShowDialog();

            data.lectures = win.lectures;
            data.lectid = win.lectid;
            data.lecttime = win.lecttime;
        
        }
    }
}
