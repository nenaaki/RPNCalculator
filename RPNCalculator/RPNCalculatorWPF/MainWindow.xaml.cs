using System.Windows;
using RPNCalculatorModel;

namespace RPNCalculatorWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new CalculatorViewModel() { Model = new Calculator() };
        }
    }
}