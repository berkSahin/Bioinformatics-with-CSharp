using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bioinfarmatics.Library.Core.Interfaces;

namespace Bioinfarmatics.Library.Core
{
    public class Alphabet: IAlphabet
    {
        private char[] _letters;

        public char[] Letters
        {
            get { return _letters; }
            set { _letters = value; }
        }

        private string Content { get; set; }


        public Alphabet(string content)
        {
            this.Content = content;
            SetLetters();
        }

        private void SetLetters()
        {
            //string[] words = { "Car", "Car", "Car", "Bird", "Sky", "Sky" };
            Letters = Content.ToCharArray()
                .GroupBy(word => word)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key).ToArray();
        }

        public bool IsDNA() { return Letters.Where(x => x.Equals("T")).FirstOrDefault() != null ? true : false; }

        public bool IsRNA() { return Letters.Where(x => x.Equals("T")).FirstOrDefault() != null ? true : false; }
        public bool IsProtein() { return false; }
    }
}
