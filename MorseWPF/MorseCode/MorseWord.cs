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
        private int currentIndex;

        public struct LettersState
        {
            public string letter;
            public int status;
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
            string word = RandWords[random.Next(RandWords.Length + 1)];
            SelectedWord = new LettersState[word.Length];

            foreach (char c in word)
            {
                LettersState lettersState;
                lettersState.letter = c.ToString();
                lettersState.status = 0;

                SelectedWord.Append(lettersState);
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

        public void CheckWordProgress(string currMorse)
        {

        }
    }
}
