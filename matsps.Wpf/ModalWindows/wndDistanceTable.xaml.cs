using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace matsps.Wpf.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для wndDistanceTable.xaml
    /// </summary>
    public partial class wndDistanceTable : Window
    {
        public wndDistanceTable()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Матрица расстояний для вывода в таблицу
        /// </summary>
        public double[,] Matrix {
            set{
                dataGrid.ItemsSource = value;
            }
        }
    }
}
