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
    /// SetWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SetWindow : Window
    {
        public SetWindow()
        {
            InitializeComponent();

            _viewModel = new SetViewModel();
            this.DataContext = _viewModel;

            SetWindowConf();
        }

        private SetViewModel _viewModel;

        void SetWindowConf()
        {
            // TimetableSetting set = MainWindow.data.setting;

            TimetableSetting set = new TimetableSetting();

            _viewModel.period = set.period;

            _viewModel.day_st = set.day_st;
            _viewModel.day_en = set.day_en;

            _viewModel.display_mon = set.display_mon;
            _viewModel.display_tue = set.display_tue;
            _viewModel.display_wed = set.display_wed;
            _viewModel.display_thu = set.display_thu;
            _viewModel.display_fri = set.display_fri;
            _viewModel.display_sat = set.display_sat;

            List<Obj_Combobox> tmp = new List<Obj_Combobox>();

            tmp.Add(CreateObj(1, "表示"));
            tmp.Add(CreateObj(0, "非表示"));

            _viewModel.obj_tf = tmp;

            tmp = new List<Obj_Combobox>();

            tmp.Add(CreateObj(0, "今日"));
            tmp.Add(CreateObj(1, "1日前"));
            tmp.Add(CreateObj(2, "2日前"));
            tmp.Add(CreateObj(3, "3日前"));

            _viewModel.obj_day_st = tmp;

            tmp = new List<Obj_Combobox>();

            tmp.Add(CreateObj(0, "今日"));
            tmp.Add(CreateObj(1, "1日後"));
            tmp.Add(CreateObj(2, "2日後"));
            tmp.Add(CreateObj(3, "3日後"));

            _viewModel.obj_day_en = tmp;

            

            this.DataContext = _viewModel;
        }

        Obj_Combobox CreateObj(int id_in, string name_in)
        {
            var o = new Obj_Combobox()
            {
                id = id_in, name = name_in
            };

            return o;
        }
    }
}
