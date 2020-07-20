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

        // dictionary with string (the character) and
        // a bool? (if character was spelled correctly,
        // incorrectly or not yet spelled)
        public Dictionary<string, bool?> SelectedWord;

        public MorseWord()
        {
            // checks if list has been created
            if (RandWords == null)
            {
                InitRandWords();
            }

            //initialize
            SelectedWord = new Dictionary<string, bool?>();

            // selects a random word from the array
            Random random = new Random();
            string word = RandWords[random.Next(RandWords.Length + 1)];

            foreach (char c in word)
            {
                SelectedWord.Add(c.ToString(), null);
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
    }
}
