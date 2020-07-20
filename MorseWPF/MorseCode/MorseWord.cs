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
        public static string[] RandWords = null;

        public MorseWord()
        {
            using (StreamReader r = new StreamReader(@"..\..\MorseCode\MorseData\wordsList.txt"))
            {
                RandWords = r.ReadToEnd().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach (string s in RandWords)
            {
                Console.WriteLine(s);
            }
        }
    }
}
