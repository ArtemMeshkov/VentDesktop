using BMC.ViewModel;
using System.Windows;

namespace BMC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            
            DataContext = PageChangeVentTypes.GetInstance(this);
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            DataContext = PageChangeVentTypes.ResetInstance(this);
        }

        
    }
}
