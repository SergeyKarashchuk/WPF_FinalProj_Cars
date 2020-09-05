using CarsCatalog.ViewModel;
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

namespace CarsCatalog.View
{
    /// <summary>
    /// Interaction logic for EditCarWindow.xaml
    /// </summary>
    public partial class EditCarUC
    {
        public EditCarUC(int? carID = null)
        {
            InitializeComponent();

            DataContext = new EditCarViewModel(carID);
        }
    }
}
