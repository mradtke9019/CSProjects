using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGram
{
    public class Chatbot
    {
        public List<int> Corpus { get; set; }
        public List<string> Vocabulary { get; set; }
        public static string[] Specials = { "`", "``", "-", "--", ",", ";", ":", ":\\", "!", "?", "/", ".", "...", "'", "''", "$", "\\*", "\\", "*", "&", "#", "%", "+"};

        /// <summary>
        /// Converts a file to a list of tokens given a vocabulary
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private List<int> Tokenize(List<string> words)
        {
            List<int> tokens = new List<int>();

            foreach (var word in words)
            {
                int idx = Vocabulary.IndexOf(word);
                tokens.Add(idx);
            }

            return tokens;
        }

        public string DeTokenize(int word)
        {
            try
            {
                return Vocabulary.ElementAt(word);
            }
            catch (Exception e)
            {
                return "undefined";
            }
        }


        public Chatbot(string filepath, string vocabularyPath)
        {
            string file = File.ReadAllText(filepath);
            Specials.OrderByDescending(x => x.Length).ToList().ForEach(x => file = file.Replace(x, " "));
            Vocabulary = File.ReadAllLines(vocabularyPath).ToList();
            Corpus = Tokenize(file.Split(' ').Select(x => x.ToLower()).ToList());
        }

        public string PrintTokens()
        {
            return string.Join(" ", Corpus.Select(x => DeTokenize(x)));
        }

        public void Run(int n1, int n2, int h1, int h2)
        {

            double r = (double)n1 / (double)n2;
            Trigram.Get(r, h1, h2, Corpus, true);
        }
    }
}
