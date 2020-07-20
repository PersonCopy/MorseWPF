using MorseWPF.MorseCode;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MorseWPF.Pages
{
    /// <summary>
    /// Interaktionslogik für MorseTable.xaml
    /// </summary>
    public partial class MorseTable : Page
    {
        // Singleton so it doesn't get created a bunch of times
        // by the navigation menu
        private static MorseTable instance = null;
        public static MorseTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorseTable();
                }
                return instance;
            }
        }

        private MorseTable()
        {
            InitializeComponent();
            DisplayTable();
        }

        // displays all the content of the morse dictionary attribute
        public void DisplayTable()
        {
            foreach (KeyValuePair<string, string> textMorsePair in MorseTranslator.Instance.MorseDictionary)
            {
                Label key = new Label
                {
                    Content = textMorsePair.Key,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                Label value = new Label
                {
                    Content = textMorsePair.Value,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                TranslationGrid.Children.Add(key);
                TranslationGrid.Children.Add(value);
            }
        }
    }
}
