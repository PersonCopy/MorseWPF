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
        // morse string progress
        private string morseProgress = "";
        private string MorseProgress
        {
            get
            {
                return this.morseProgress;
            }
            set
            {
                this.morseProgress = value;
                UpdateWord(this.morseProgress);
                morseWord.UpdateWordProgress(this.morseProgress, this.ClearMorseInput);
            }
        }

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
            MakeNewWord();
        }

        private void MakeNewWord()
        {
            CharMorsePanel.Children.Clear();
            this.morseWord = new MorseWord();

            UpdateWord();
        }

        private void ClearMorseInput()
        {
            this.morseProgress = "";
            CharMorsePanel.Children.Clear();
        }

        private bool UpdateWord(string currentMorse = "")
        {
            if (morseWord.UpdateWordProgress(currentMorse, this.ClearMorseInput) == null)
            {
                MakeNewWord();
                return false;
            }

            CurrWordPanel.Children.Clear();
            for (int i = 0; i < this.morseWord.SelectedWord.Length; i++)
            {
                SolidColorBrush statusBrush;
                switch (morseWord.SelectedWord[i].status)
                {
                    case true:
                        statusBrush = Brushes.Green;
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
                    Content = this.morseWord.SelectedWord[i].letter,
                    FontSize = 55,
                    Foreground = statusBrush
                };
                CurrWordPanel.Children.Add(label);
            }

            return true;
        }

        private void DotBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CharMorsePanel.Children.Add(new Label { Content = "." });
            this.MorseProgress += ".";
        }

        private void DashBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CharMorsePanel.Children.Add(new Label { Content = "-" });
            this.MorseProgress += "-";
        }
    }
}
