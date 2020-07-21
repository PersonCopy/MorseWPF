using System.Windows.Controls;
using System.Windows.Media;
using MorseWPF.MorseCode;

namespace MorseWPF.Pages
{
    /// <summary>
    /// Interaktionslogik für MorseLearner.xaml
    /// </summary>
    public partial class MorseLearner : Page
    {
        // private attribute of MorseWord so that it can be destroyed later
        private MorseWord morseWord;

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
            this.morseWord = new MorseWord();

            UpdateWord(this.morseWord);
        }

        private void UpdateWord(MorseWord morseWord, string currentMorse = "")
        {
            morseWord.UpdateWordProgress(currentMorse);

            for (int i = 0; i < morseWord.SelectedWord.Length; i++)
            {
                SolidColorBrush statusBrush;
                switch (morseWord.SelectedWord[i].status)
                {
                    case true:
                        statusBrush = Brushes.Turquoise;
                        break;
                    case false:
                        statusBrush = Brushes.Red;
                        break;
                    default:
                        statusBrush = Brushes.Black;
                        break;
                }


                Label label = new Label
                {
                    Content = morseWord.SelectedWord[i].letter,
                    FontSize = 55,
                    Foreground = statusBrush
                };
                CurrWordPanel.Children.Add(label);
            }
        }

        private void DotBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DisplayNewWord();
        }
    }
}
