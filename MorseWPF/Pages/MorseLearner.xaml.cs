using System.Windows.Controls;
using System.Windows.Media;
using MorseWPF.MorseCode;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;

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

        // Button for starting the thing
        private Button StartBtn;

        // Label for tip, informing user of keyboard input
        private Label TipLabel;

        // Setter method for morseProgress
        private void SetMorseProgress(string value)
        {
            this.morseProgress = value;
            UpdateWord();
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
                instance.DashBtn.Focus();
                return instance;
            }
        }

        // private constructor
        private MorseLearner()
        {
            InitializeComponent();
            MakeNewWord();

            // Innitialize start button
            this.StartBtn = new Button()
            {
                Content = "Start!",
                FontSize = 30,
                Height = 50,
                Width = 100
        };
            this.StartBtn.Click += (s, e) => { StartBtn_Click(s, e); };

            // Init TipLabel
            this.TipLabel = new Label()
            {
                Content = "Use \".\" or \"-\" to enter dots and dashes"
            };

            // Add start button to WrapPanel
            CharMorsePanel.Children.Add(this.StartBtn);

            // Add TipLabel to TipGrid
            TipGrid.Children.Add(this.TipLabel);
        }


        /// <summary>
        /// Method that will generate and display a new word
        /// </summary>
        private void MakeNewWord()
        {
            

            ClearMorseInput();
            this.morseWord = new MorseWord();

            UpdateWord();
        }

        /// <summary>
        /// Clears morse input
        /// </summary>
        private void ClearMorseInput()
        {
            this.morseProgress = "";
            CharMorsePanel.Children.Clear();
        }

        /// <summary>
        /// Updates the word to display correct, wrong or generate a new word
        /// </summary>
        /// <returns>False if word is finished. Made to avoid unnesseairy else statement</returns>
        private bool UpdateWord()
        {
            // checks if word has been completed
            if (morseWord.UpdateWordProgress(this.morseProgress, this.ClearMorseInput) == null)
            {
                MakeNewWord();
                return false;
            }

            // redraws the word panel with colors

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

                UniformGrid uniformGrid = new UniformGrid
                {
                    Rows = 2
                };

                uniformGrid.Children.Add(label);

                if (statusBrush == Brushes.Red)
                {
                    Label morseLabel = new Label
                    {
                        Content = MorseTranslator.Instance.GetCharToMorse(this.morseWord.SelectedWord[i].letter),
                        FontSize = 20
                    };

                    uniformGrid.Children.Add(morseLabel);
                }

                CurrWordPanel.Children.Add(uniformGrid);
            }

            return true;
        }


        // button click methods

        private void DotBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CharMorsePanel.Children.Add(new Label { Content = "." });
            this.SetMorseProgress(this.morseProgress + ".");
        }

        private void DashBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CharMorsePanel.Children.Add(new Label { Content = "-" });
            this.SetMorseProgress(this.morseProgress + "-");
        }

        // key pressed method
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Key down");
            if (e.Key == Key.OemPeriod)
            {
                DotBtn_Click(null, null);
            }
            else if (e.Key == Key.OemMinus)
            {
                DashBtn_Click(null, null);
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            // Hidden elements
            StartBtn.Visibility = Visibility.Hidden;
            TipLabel.Visibility = Visibility.Collapsed;
            TipGrid.Visibility = Visibility.Collapsed;

            // Visible elements
            DashBtn.Visibility = Visibility.Visible;
            DotBtn.Visibility = Visibility.Visible;

            // Sets focus on button in page, enabling key strokes to register
            this.DashBtn.Focus();
        }
    }
}
