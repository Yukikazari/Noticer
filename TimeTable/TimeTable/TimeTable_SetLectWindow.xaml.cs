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
    /// TimeTable_SetLectWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TimeTable_SetLectWindow : Window
    {
        public TimeTable_SetLectWindow(int dayoftheweek_in, int period_in, int lectid_in,Dictionary<int, Lecture> lecture_in)
        {
            InitializeComponent();

            dayoftheweek = dayoftheweek_in;
            period = period_in;
            lectid = lectid_in;
            lecture = lecture_in;

            _viewmodel = new SetLectViewModel();
            this.DataContext = _viewmodel;

            _viewmodel.lect_tenp = TimeTableWindow.lecttemp[dayoftheweek][period];

            SetData();
        }

        private SetLectViewModel _viewmodel;

        int dayoftheweek;
        int period;
        int lectid;
        Dictionary<int, Lecture> lecture;
        public Lecture lect = new Lecture();

        void SetData()
        {
            if (lectid > 0)
            {
                if (lecture.ContainsKey(lectid))
                {
                    _viewmodel.name = lecture[lectid].name;
                    _viewmodel.professor = lecture[lectid].professor;
                    _viewmodel.continuous = lecture[lectid].continuous;
                    _viewmodel.syllabus = lecture[lectid].syllabus;

                    switch (lecture[lectid].style)
                    {
                        case "オンデマンド":
                            style.SelectedIndex = 0;
                            break;
                        case "ライブ配信":
                            style.SelectedIndex = 1;
                            break;
                        case "対面授業":
                            style.SelectedIndex = 2;
                            break;
                    }

                    if(lecture[lectid].otherurl != null)
                    {
                        switch (lecture[lectid].otherurl.Count())
                        {
                            case 3:
                                _viewmodel.otherurl3 = lecture[lectid].otherurl[2];
                                _viewmodel.otherurl2 = lecture[lectid].otherurl[1];
                                _viewmodel.otherurl1 = lecture[lectid].otherurl[0];
                                break;
                            case 2:
                                _viewmodel.otherurl2 = lecture[lectid].otherurl[1];
                                _viewmodel.otherurl1 = lecture[lectid].otherurl[0];
                                break;
                            case 1:
                                _viewmodel.otherurl1 = lecture[lectid].otherurl[0];
                                break;
                        }
                    }
                }
            }
            else
            {
                _viewmodel.continuous = 1;
            }

            // comboboxのデータ

            this.Title = String.Format("{0}曜{1}限","月火水木金土".Substring(dayoftheweek, 1), period+1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lect.name = _viewmodel.name;
            lect.professor = _viewmodel.professor;
            lect.continuous = _viewmodel.continuous;
            lect.syllabus = _viewmodel.syllabus;
            lect.style = style.Text;
            if(_viewmodel.otherurl1 != null)
            {
                lect.otherurl.Add(_viewmodel.otherurl1);
            }
            if (_viewmodel.otherurl2 != null)
            {
                lect.otherurl.Add(_viewmodel.otherurl2);
            }
            if (_viewmodel.otherurl3 != null)
            {
                lect.otherurl.Add(_viewmodel.otherurl3);
            }
        }

        private void style_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void temp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var nm = cb.SelectedIndex;

            var obj = TimeTableWindow.lecttemp[dayoftheweek][period][nm];

            _viewmodel.name = obj.name;
            _viewmodel.professor = obj.professor;
            _viewmodel.continuous = obj.continuous;
            _viewmodel.syllabus = obj.syllabus;

            style.SelectedItem = obj.style;

            if (obj.otherurl != null)
            {
                switch (obj.otherurl.Count())
                {
                    case 3:
                        _viewmodel.otherurl3 = obj.otherurl[2];
                        _viewmodel.otherurl2 = obj.otherurl[1];
                        _viewmodel.otherurl1 = obj.otherurl[0];
                        break;
                    case 2:
                        _viewmodel.otherurl2 = obj.otherurl[1];
                        _viewmodel.otherurl1 = obj.otherurl[0];
                        break;
                    case 1:
                        _viewmodel.otherurl1 = obj.otherurl[0];
                        break;
                }
            }
        }
    }
}
