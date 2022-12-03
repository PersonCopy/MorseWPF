using MorseWPF.Pages;
using System.Windows;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = SettingsPage.Instance;
        }
    }
}
