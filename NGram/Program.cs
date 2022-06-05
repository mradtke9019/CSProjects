using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGram
{
    class Program
    {
        static void Main(string[] args)
        {
            Chatbot bot = new Chatbot("./samplecorpus.txt", "./vocabulary.txt");
            Console.WriteLine(bot.PrintTokens());
            bot.Run(3,2,2110,311);
        }
    }
}
