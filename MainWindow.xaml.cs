using System.Windows;

namespace Stiralka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var stiralka = new StiralkaModel();
            DataContext = stiralka;
        }
    }
}
