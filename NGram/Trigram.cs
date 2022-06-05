using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGram
{
    class Trigram
    {
        public static bool contains(List<Word> list, int v)
        {
            for (int i = 0; i < list.Count; i++)
                if (list.ElementAt(i).Value == v)
                    return true;
            return false;
        }

        public static void increment(List<Word> list, int v)
        {
            for (int i = 0; i < list.Count; i++)
                if (list.ElementAt(i).Value == v) { list.ElementAt(i).Count++; return; }
        }

        public static int Get(double r, int h1, int h2, List<int> corpus, bool printOut)
        {

            List<Word> wh1h2 = new List<Word>();
            int count = 0;


            // Add all words that appear after h to the list
            for (int i = 0; i < corpus.Count - 2; i++)
                if (corpus.ElementAt(i) == h1 && corpus.ElementAt(i + 1) == h2)
                {
                    if(!wh1h2.Select(x => x.Value).Contains(corpus.ElementAt(i + 2)))
                    {
                        wh1h2.Add(new Word(corpus.ElementAt(i + 2), 1));
                        count++;
                    }
                    else
                    {
                        wh1h2.ElementAt(corpus.ElementAt(i + 2)).Count++;
                        count++;
                    }
                }


            // Set the probabilities
            for (int i = 0; i < wh1h2.Count; i++)
                wh1h2.ElementAt(i).Probability = (double)wh1h2.ElementAt(i).Count / (double)count;

            //wh1h2.sort(null);

            // Set probability intervals for each word 
            List<Double> leftBound = new List<Double>();
            List<Double> rightBound = new List<Double>();
            List<int> index = new List<int>();

            double sum = 0;
            for (int i = 0; i < wh1h2.Count; i++)
            {
                sum = 0;
                // base case
                if (i == 0)
                {
                    leftBound.Insert(0, 0.0);
                    rightBound.Insert(0, wh1h2.ElementAt(0).Probability);
                    index.Insert(0, 0);
                }
                else
                {
                    // The left bound will be the same as the last right bound
                    leftBound.Insert(i, rightBound.ElementAt(i - 1));
                    for (int j = 0; j < i + 1; j++)
                    {
                        sum += wh1h2.ElementAt(j).Probability;
                    }
                    // Keep track of what value this interval is in the index array
                    index.Add(/*wh1h2.get(i).value*/ i);
                    rightBound.Insert(i, sum);
                }
            }
            int w = -1;
            bool isDefined = false;
            //Now that we have our intervals established, actually find the value the range falls into
            // and output
            for (int i = 0; i < leftBound.Count && i < rightBound.Count; i++)
            {
                if (r <= rightBound.ElementAt(i) && r >= leftBound.ElementAt(i))
                {
                    w = wh1h2.ElementAt(index.ElementAt(i)).Value;
                    if (printOut)
                    {
                        Console.WriteLine(w);
                        Console.WriteLine(leftBound.ElementAt(i));
                        Console.WriteLine(rightBound.ElementAt(i));
                    }
                    isDefined = true;
                    break;
                }
            }
            // If no possibility exists, it is an undefined value
            if (!isDefined && printOut)
                Console.WriteLine("undefined");
            return w;
        }
    }
}
