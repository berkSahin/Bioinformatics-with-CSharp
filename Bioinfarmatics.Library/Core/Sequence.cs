using Bioinfarmatics.Library.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfarmatics.Library.Core
{
    public class Sequence:SequenceBase
    {
        public Direction Direction;
        public Alphabet Alphabet;
        public Stats Stats;

        public override string Content
        {
            get
            {
                return base.Content;
            }

            set
            {
                base.Content = value;
                Alphabet = new Alphabet(value);
                Direction = new Direction();
                if (!Alphabet.IsDNA())
                    Direction.Complement();
                Stats = new Stats(value);
                //var t = Stats.Lenght();
                //var tt = Stats.Frekans(Alphabet.Letters);
                //Stats.Sknewness(BasePair.GC, 10000);
            }
        }

        public Sequence(string title, string content)
        {
            Title = title;
            Content = content;
      
        }
    }
}
