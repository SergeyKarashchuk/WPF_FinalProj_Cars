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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarsCatalog.View
{
    /// <summary>
    /// Interaction logic for AwaitUC.xaml
    /// </summary>
    public partial class AwaitUC : UserControl
    {
        public AwaitUC()
        {
            InitializeComponent();
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            var resource = FindResource("BorderAnymationStoryboard");
            if (!(sender is Canvas canvas && resource is Storyboard storyboard))
                return;
          
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Border border)
                {
                    storyboard.BeginTime = TimeSpan.FromMilliseconds(500.0 * i);
                    storyboard.Begin(border);
                }
            }
        }
    }
}
