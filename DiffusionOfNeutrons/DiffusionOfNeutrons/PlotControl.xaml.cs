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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DiffusionOfNeutrons
{
    /// <summary>
    /// Логика взаимодействия для PlotControl.xaml
    /// </summary>
    public partial class PlotControl : UserControl
    {
        public PlotControl()
        {
            InitializeComponent();

            DataContextChanged += (s, _) => {
                if (s is UserControl se && se.DataContext is PlotViewModel pvm)
					pvm.PropertyChanged += OnPropertyChanged;
            };
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            thePlot.InvalidatePlot(true);
        }
    }
}
