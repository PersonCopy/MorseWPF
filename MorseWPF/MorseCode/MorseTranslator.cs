﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseWPF.MorseCode
{
    public class MorseTranslator
    {
        // dictionary for morse code translation
        private readonly Dictionary<string, string> morseDictionary;

        // instance of singleton to avoid reading json every time
        private static MorseTranslator instance = null;
        public static MorseTranslator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorseTranslator();
                }
                return instance;
            }
        }

        private MorseTranslator()
        {
            // reads json file and inserts values into dictionary
            using (StreamReader r = new StreamReader(@"MorseData\translation.json"))
            {
                this.morseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.ReadToEnd());
            }
        }

        /// <summary>
        /// Turns a character into it's morse code equivalent
        /// </summary>
        /// <param name="character">
        /// Make sure this one is only one character
        /// </param>
        /// <returns>Morse code equivalent of inserted character</returns>
        public string GetCharToMorse(string character)
        {
            string returnValue;
            if (!this.morseDictionary.TryGetValue(character.ToLower(), out returnValue))
            {
                return "";
            }
            return returnValue;
        }


        /// <summary>
        /// Turns a morse character into a real character
        /// </summary>
        /// <param name="morse">String of morse </param>
        /// <returns>Returns the character</returns>
        public string GetMorseToChar(string morse)
        {
            return this.morseDictionary.FirstOrDefault(x => x.Value == morse).Key;
        }

        /// <summary>
        /// Translates text into morse code
        /// </summary>
        /// <param name="text">Text to be translated</param>
        /// <returns>Translated morse code</returns>
        public string GetTextToMorse(string text)
        {
            string morseReturn = "";
            foreach (char s in text)
            {
                // gets every character translated to 
                morseReturn += GetMorseToChar(s.ToString()) + " ";
            }
            // removes the last space character from the foreach group
            return morseReturn.Remove(morseReturn.Length - 1);
        }

        /// <summary>
        /// Turns morse code into normal text
        /// </summary>
        /// <param name="morse">Morse code text</param>
        /// <returns>Normal text</returns>
        public string GetMorseToText(string morse)
        {
            string returnString = "";
            foreach (string chars in morse.Split(' '))
            {
                returnString += GetMorseToChar(chars);
            }
            return returnString;
        }
    }
}
