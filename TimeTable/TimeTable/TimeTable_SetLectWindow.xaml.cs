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
                _viewmodel.name = lecture[lectid].name;
                _viewmodel.professor = lecture[lectid].professor;
                _viewmodel.continuous = lecture[lectid].continuous;
                _viewmodel.syllabus = lecture[lectid].syllabus;
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

            // comboboxのデータ

            this.Title = String.Format("{0}曜{1}限","月火水木金土".Substring(dayoftheweek, 1), period+1);
        }
        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lect.name = _viewmodel.name;
            lect.professor = _viewmodel.professor;
            lect.continuous = _viewmodel.continuous;
            lect.syllabus = _viewmodel.syllabus;
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
    }
}
