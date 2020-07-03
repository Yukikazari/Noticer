using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TimeTable
{
    public class RuledLineGrid : Grid
    {
        static RuledLineGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RuledLineGrid), new FrameworkPropertyMetadata(typeof(RuledLineGrid)));
        }

        public void GridShaping()
        {
            //罫線の太さはここで指定。
            var thickness = new GridLength(1);

            // 罫線用の行・列を追加
            var columns = ColumnDefinitions.ToArray();
            ColumnDefinitions.Clear();

            if (columns.Any())
            {
                foreach (var c in columns)
                {
                    ColumnDefinitions.Add(new ColumnDefinition { Width = thickness });
                    ColumnDefinitions.Add(c);
                }
            }
            else
            {
                ColumnDefinitions.Add(new ColumnDefinition { Width = thickness });
                ColumnDefinitions.Add(new ColumnDefinition());
            }
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5) });

            var rows = RowDefinitions.ToArray();
            RowDefinitions.Clear();

            if (rows.Any())
            {
                foreach (var r in rows)
                {
                    RowDefinitions.Add(new RowDefinition { Height = thickness });
                    RowDefinitions.Add(r);
                }
            }
            else
            {
                RowDefinitions.Add(new RowDefinition { Height = thickness });
                RowDefinitions.Add(new RowDefinition());
            }
            RowDefinitions.Add(new RowDefinition { Height = thickness });

            //行・列を追加した分、Column,Row,ColumnSpan,RowSpanがずれるのでその補正再設定
            foreach (UIElement c in Children)
            {
                SetColumn(c, GetColumn(c) * 2 + 1);
                SetColumnSpan(c, GetColumnSpan(c) * 2 - 1);
                SetRow(c, GetRow(c) * 2 + 1);
                SetRowSpan(c, GetRowSpan(c) * 2 - 1);
            }

            //罫線用に追加した行・列にRectangleを配置
            for (var i = 0; i < ColumnDefinitions.Count; i += 2)
            {
                var rectangle = new Rectangle() { Fill = Brushes.Black };
                Children.Add(rectangle);
                SetColumn(rectangle, i);
                SetRowSpan(rectangle, RowDefinitions.Count);
                SetZIndex(rectangle, int.MinValue);
            }
            for (var i = 0; i < RowDefinitions.Count; i += 2)
            {
                var rectangle = new Rectangle() { Fill = Brushes.Black };
                Children.Add(rectangle);
                SetRow(rectangle, i);
                SetColumnSpan(rectangle, ColumnDefinitions.Count);
                SetZIndex(rectangle, int.MinValue);
            }
        }

    }
}
