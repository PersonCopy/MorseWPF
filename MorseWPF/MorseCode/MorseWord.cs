using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseWPF.MorseCode
{
    public class MorseWord
    {
        // static array of random words
        public static string[] RandWords = null;

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

            // Initialize
            

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
            using (StreamReader r = new StreamReader(@"..\..\MorseCode\MorseData\wordsList.txt"))
            {
                RandWords = r.ReadToEnd().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
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
        private bool IsMorseOver(string currMorse)
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
        /// <returns>The current state of the word. Will return null if it has detected that the word has finished</returns>
        public LettersState[] UpdateWordProgress(string currMorse)
        {
            if (SelectedWord.Length - 1 < currentIndex)
            {
                return null;
            }
            if (IsMorseOver(currMorse))
            {
                this.SelectedWord[currentIndex].status = IsMorseCorrect(currMorse);
                this.currentIndex++;
            }
            return SelectedWord;
        }
    }
}
