using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordTransform
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter 2 valid English Dictionary words you wish to transform from one word to another.");
            Console.Write("Word 1: ");
            string word1 = Console.ReadLine();
            Console.Write("Word 2: ");
            string word2 = Console.ReadLine();

            CheckBothWords(word1, word2);

            List<String> wordList = new List<String>();
            wordList.Add(word1);


            CharReplace(word1, word2, wordList);

            if (!wordList.Contains(word2))
                Console.WriteLine("There is no path to tansform from first word to second word !!!");
            else
            {
                Console.WriteLine("-----------------------");
                foreach (string mylist in wordList)
                    Console.WriteLine(mylist);
            }
        }

        private static void CheckBothWords(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                Console.WriteLine("Please Enter same length words !!!");

            string filePath = @".\json\words_dictionary.json";
            var myJsonString = File.ReadAllText(filePath);
            var myJObject = JObject.Parse(myJsonString);

            JToken myJT1 = myJObject.SelectToken(word1.ToLower());
            JToken myJT2 = myJObject.SelectToken(word2.ToLower());
            if (myJT1 == null || myJT2 == null)
                Console.WriteLine("Please Enter valid English Dictionary words !!!");
        }

        private static void CharReplace(string word1, string word2, List<String> wordList)
        {
            string filePath = @".\json\words_dictionary.json";
            var myJsonString = File.ReadAllText(filePath);
            var myJObject = JObject.Parse(myJsonString);

            string newWord = "";
            char[] char_word2 = word2.ToCharArray();
            if (word1 == word2)
                return;
            else
            {
                for (int i = 0; i < char_word2.Length; i++)
                {
                    StringBuilder char_word1 = new StringBuilder(word1);
                    if (char_word1[i] == char_word2[i])
                    {
                        continue;
                    }
                    else
                    {
                        char_word1[i] = char_word2[i];
                        newWord = char_word1.ToString();
                        JToken myJToken = myJObject.SelectToken(newWord.ToLower());
                        if (myJToken != null && !wordList.Contains(word2))
                        {
                            wordList.Add(newWord);
                            word1 = newWord;
                            //Console.WriteLine(word1);
                            CharReplace(word1, word2, wordList);
                        }
                    }
                }
            }
        }
    }
}
