using System.Windows.Controls;
using MorseWPF.MorseCode;

namespace MorseWPF.Pages
{
    /// <summary>
    /// Interaktionslogik für MorseLearner.xaml
    /// </summary>
    public partial class MorseLearner : Page
    {
        // Singleton so it doesn't get created a bunch of times
        // by the navigation menu
        private static MorseLearner instance = null;
        public static MorseLearner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorseLearner();
                }
                return instance;
            }
        }
        private MorseLearner()
        {
            InitializeComponent();
            DisplayNewWord();
        }

        private void DisplayNewWord()
        {
            CurrWordPanel.Children.Clear();
            MorseWord morseWord = new MorseWord();
            /*
            foreach (char c in morseWord.SelectedWord.)
            {
                Label label = new Label
                {
                    Content = c.ToString(),
                    FontSize = 55
                };
                CurrWordPanel.Children.Add(label);
            }*/
        }

        private void DotBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DisplayNewWord();
        }
    }
}
