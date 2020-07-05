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
    /// TimeTable_SetTimeWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TimeTable_SetTimeWindow : Window
    {
        public TimeTable_SetTimeWindow(int id_in,LectTime lecttime_in)
        {
            InitializeComponent();

            _viewmodel = new SetTimeViewModel();
            this.DataContext = _viewmodel;

            id = id_in;
            lecttime = lecttime_in;

            SetData();
        }
        private SetTimeViewModel _viewmodel;
        int id;
        public LectTime lecttime;

        void SetData()
        {
            _viewmodel.st_h = lecttime.starthour;
            _viewmodel.st_m = lecttime.startminute;
            _viewmodel.en_h = lecttime.endhour;
            _viewmodel.en_m = lecttime.endminute;

            var tlist = new List<int>();

            for(int i=0; i<24; i++)
            {
                tlist.Add(i);
            }
            _viewmodel.obj_h = tlist;

            tlist = new List<int>();

            for(int i=0; i<12; i++)
            {
                tlist.Add(i * 5);
            }
            _viewmodel.obj_m = tlist;

            this.Title = String.Format("{0}限", id+1);
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lecttime.starthour = _viewmodel.st_h;
            lecttime.startminute = _viewmodel.st_m;
            lecttime.endhour = _viewmodel.en_h;
            lecttime.endminute = _viewmodel.en_m;
        }
    }
}
