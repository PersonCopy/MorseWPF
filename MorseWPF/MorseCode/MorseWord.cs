using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MorseWPF.MorseCode
{
    public class MorseWord
    {
        // static array of random words
        public static string[] RandWords = null;

        // static word list location
        public static string WordListLocation;

        // word that has been randomly selected
        public LettersState[] SelectedWord;
        private int currentIndex = 0;

        public struct LettersState
        {
            public string letter;
            public bool? status;
        }

        public MorseWord()
        {
            // checks if list has been created
            if (RandWords == null)
            {
                InitRandWords();
            }

            // selects a random word from the array
            Random random = new Random();
            string word = RandWords[random.Next(RandWords.Length)];
            SelectedWord = new LettersState[word.Length];
            int i = 0;
            foreach (char c in word)
            {
                LettersState lettersState;
                lettersState.letter = c.ToString();
                lettersState.status = null;

                SelectedWord[i] = lettersState;
                i++;
            }
        }

        /// <summary>
        /// Inserts the random words from
        /// the text file into the RandWords
        /// string array
        /// </summary>
        public static void InitRandWords()
        {
            RefreshWordList();
        }

        /// <summary>
        /// Method for refreshing words from the word list
        /// </summary>
        public static void RefreshWordList()
        {
            using (StreamReader r = new StreamReader(@"..\..\MorseCode\MorseData\settings.txt"))
            {
                WordListLocation = r.ReadToEnd();
            }


            if (string.IsNullOrEmpty(WordListLocation))
            {
                UseDefaultWords();
            }
            else
            {
                try
                {
                    RandWords = File.ReadAllText(WordListLocation).Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        "Words list is not in correct format!\nPlease make sure to separate words by ','\n\n"
                        + e.InnerException
                        );
                    UseDefaultWords(); 
                }
            }   
        }

        /// <summary>
        /// Method for setting game settings back to default words list
        /// </summary>
        private static void UseDefaultWords()
        {
            using (StreamReader r = new StreamReader(@"..\..\MorseCode\MorseData\wordsList.txt"))
            {
                RandWords = r.ReadToEnd().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            WordListLocation = @"..\..\MorseCode\MorseData\wordsList.txt";
        }

        /// <summary>
        /// Checks if entered morse code is correct thus far
        /// </summary>
        /// <param name="currMorse">Current progress of the morse input</param>
        /// <returns>If it is correct</returns>
        private bool IsMorseCorrect(string currMorse)
        {
            LettersState character = SelectedWord[this.currentIndex];
            char[] charArray = currMorse.ToCharArray();

            string morse = MorseTranslator.Instance.GetCharToMorse(character.letter);
            char[] morseArray = morse.ToCharArray();

            for (int i = 0; i < currMorse.Length; i++)
            {
                if (charArray[i] != morseArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the morse input is over
        /// </summary>
        /// <param name="currMorse">Current morse progress</param>
        /// <returns>Wether or not it is over</returns>
        public bool IsMorseOver(string currMorse)
        {
            LettersState character = SelectedWord[this.currentIndex];

            string morse = MorseTranslator.Instance.GetCharToMorse(character.letter);

            if (currMorse.Length != morse.Length)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates the status of the words character
        /// </summary>
        /// <param name="currMorse">Current morse code progress</param>
        /// <param name="action">what to do when it detects that morse input has been finished</param>
        /// <returns>The current state of the word. Will return null if it has detected that the word has finished</returns>
        public LettersState[] UpdateWordProgress(string currMorse, Action action)
        {
            if (SelectedWord.Length - 1 < currentIndex)
            {
                return null;
            }
            if (IsMorseOver(currMorse))
            {
                this.SelectedWord[currentIndex].status = IsMorseCorrect(currMorse);
                this.currentIndex++;
                action.Invoke();
            }
            return SelectedWord;
        }
    }
}
