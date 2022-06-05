using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGram
{
    public class Word : IComparable<Word>
    {
		public int Count { get; set; }
		public int Value { get; set; }
		public double Probability { get; set; }

		public Word()
		{
			Count = 0;
			Value = int.MaxValue;
			Probability = Double.MinValue;
		}
		public Word(int value)
		{
			Count = 0;
			this.Value = value;
			Probability = Double.MinValue;
		}
		public Word(int value, int count)
		{
			this.Count = count;
			this.Value = value;
			Probability = Double.MinValue;
		}

        public int CompareTo(Word other)
		{
			if (this.Value > other.Value)
				return 1;
			else if (this.Value < other.Value)
				return -1;
			return 0;
		}
    }
}
