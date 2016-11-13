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

namespace SpuskDANIIL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Method m = new Method();

            double x1 = 2; //Это x1 нулевое
            double x2 = 3; //Это x2 нулевое
            double lambda = 0.1; //Это лямбда
            double eps = 0.1; //Это эпсила

            //m.MethodS(2, 3, 0.1, 0.1);
            m.MethodS(x1, x2, lambda, eps);
            dgMethod.ItemsSource = m.FindResult;
        }

        private void dgMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
