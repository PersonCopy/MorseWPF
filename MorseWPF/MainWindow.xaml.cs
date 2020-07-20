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
using MorseWPF.Pages;

namespace MorseWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DisplayMorseTable(); // to first display the morse table on app start
        }

        private void DisplayMorseTable(object sender = null, RoutedEventArgs e = null)
        {
            ContentFrame.Content = MorseTable.Instance;
        }

        private void DisplayLearnMorse(object sender = null, RoutedEventArgs e = null)
        {
            ContentFrame.Content = MorseLearner.Instance;
        }

        private void DisplayMorseTranslator(object sender = null, RoutedEventArgs e = null)
        {
            ContentFrame.Content = TranslatorPage.Instance;
        }
    }
}
