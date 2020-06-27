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
        public bool IsChange = false;

        void SetWindowConf()
        {
            var set = MainWindow.data.setting;

            

            _viewModel.period = set.period;

            _viewModel.day_st = set.day_st;
            _viewModel.day_en = set.day_en;

            _viewModel.timer_music = set.timer_music;

            _viewModel.display_mon = set.display_mon;
            _viewModel.display_tue = set.display_tue;
            _viewModel.display_wed = set.display_wed;
            _viewModel.display_thu = set.display_thu;
            _viewModel.display_fri = set.display_fri;
            _viewModel.display_sat = set.display_sat;


            List<Obj_Combobox> tmp = new List<Obj_Combobox>();

            tmp.Add(CreateTFObj(true, "表示"));
            tmp.Add(CreateTFObj(false, "非表示"));

            _viewModel.obj_tf = tmp;

            tmp = new List<Obj_Combobox>();

            tmp.Add(CreateDayObj(0, "今日"));
            tmp.Add(CreateDayObj(1, "1日前"));
            tmp.Add(CreateDayObj(2, "2日前"));
            tmp.Add(CreateDayObj(3, "3日前"));

            _viewModel.obj_day_st = tmp;

            tmp = new List<Obj_Combobox>();

            tmp.Add(CreateDayObj(0, "今日"));
            tmp.Add(CreateDayObj(1, "1日後"));
            tmp.Add(CreateDayObj(2, "2日後"));
            tmp.Add(CreateDayObj(3, "3日後"));

            _viewModel.obj_day_en = tmp;

            this.DataContext = _viewModel;
        }

        private void DecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.data.setting.period = _viewModel.period;
            MainWindow.data.setting.day_st = _viewModel.day_st;
            MainWindow.data.setting.day_en = _viewModel.day_en;
            MainWindow.data.setting.timer_music = _viewModel.timer_music;
            MainWindow.data.setting.display_mon = _viewModel.display_mon;
            MainWindow.data.setting.display_tue = _viewModel.display_tue;
            MainWindow.data.setting.display_wed = _viewModel.display_wed;
            MainWindow.data.setting.display_thu = _viewModel.display_thu;
            MainWindow.data.setting.display_fri = _viewModel.display_fri;
            MainWindow.data.setting.display_sat = _viewModel.display_sat;

            IsChange = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Obj_Combobox CreateDayObj(int id_in, string name_in)
        {
            var o = new Obj_Combobox()
            {
                id = id_in,
                name = name_in
            };

            return o;
        }
        Obj_Combobox CreateTFObj(bool tf_in, string name_in)
        {
            var o = new Obj_Combobox()
            {
                tf = tf_in,
                name = name_in
            };

            return o;
        }

        private void period_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void period_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
