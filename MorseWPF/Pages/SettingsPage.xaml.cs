using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        // Singleton so it doesn't get created a bunch of times
        // by the navigation menu
        private static SettingsPage instance = null;
        public static SettingsPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsPage();
                }
                return instance;
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            MorseWord.RefreshWordList();
            // Setting default text
            FileNameLabel.Content = "Current words list location:\n" + MorseWord.WordListLocation;

            // Showing the words list in the TextBox
            TextEditor.Text = File.ReadAllText(MorseWord.WordListLocation).Replace(",", "\n");
        }


        /// <summary>
        /// Opens FileDialog and lets user chose words file
        /// </summary>
        /// <param name="sender">Button that called the function</param>
        /// <param name="e">RoutedEventArgs</param>
        private void SelectWordsBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Setting location of chosen file
                FileNameLabel.Content = "Current words list location:\n" + openFileDialog.FileName;

                // Replaces all commas with new line
                string fileData = File.ReadAllText(openFileDialog.FileName);
                TextEditor.Text = fileData.Replace(",", "\n");

                // Refreshing the new words list
                using (StreamWriter r = new StreamWriter(@"..\..\MorseCode\MorseData\settings.txt"))
                {
                    r.Write(openFileDialog.FileName);
                }
                MorseWord.RefreshWordList();
            }
        }
    }
}
