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
using MorseWPF.MorseCode;

namespace MorseWPF.Pages
{
    /// <summary>
    /// Interaktionslogik für TranslatorPage.xaml
    /// </summary>
    public partial class TranslatorPage : Page
    {
        private bool isToMorse = true;

        // Singleton so it doesn't get created a bunch of times
        // by the navigation menu
        private static TranslatorPage instance = null;
        public static TranslatorPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TranslatorPage();
                }
                return instance;
            }
        }
        private TranslatorPage()
        {
            InitializeComponent();
        }


        // Button click methods

        private void ToMorseBtn_Click(object sender, RoutedEventArgs e)
        {
            isToMorse = true;
            ToMorseBtn.Background = Brushes.Black;
            ToTextBtn.ClearValue(Button.BackgroundProperty);
        }

        private void ToTextBtn_Click(object sender, RoutedEventArgs e)
        {
            isToMorse = false;
            ToTextBtn.Background = Brushes.Black;
            ToMorseBtn.ClearValue(Button.BackgroundProperty);
        }

        // will translate text in input according to isToMorse variable
        private void TranslateBtn_Click(object sender, RoutedEventArgs e)
        {
            // store instance in variable
            MorseTranslator morseTranslator = MorseTranslator.Instance;

            // checks which translation mode is selected
            switch (isToMorse)
            {
                case true:
                    OutputBox.Text = morseTranslator.GetTextToMorse(InputBox.Text);
                    break;
                default:
                    OutputBox.Text = morseTranslator.GetMorseToText(InputBox.Text);
                    break;
            }
        }
    }
}
