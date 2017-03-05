using System;

namespace Bioinfarmatics.Library.Core.Abstracts
{
    public class Direction
    {
        private int start;

        public int Start
        {
            get { return start; }
            private set
            {
                if (!(value != 5 || value != 3))
                    throw new ArgumentException("Invalid Start Parameter");
                else
                {
                    if (start == 5)
                    {
                        start = value;
                        Finish = 3;
                    }
                    else
                    {
                        start = value;
                        Finish = 5;
                    }
                }
            }
        }

        private int finish;

        public int Finish
        {
            get { return finish; }
            private set
            {
                if (!(value != 5 || value != 3))
                    throw new ArgumentException("Invalid Finish Parameter");
                else
                    finish = value;
            }
        }

        public Direction()
        {
            Start = 3;
        }

        public Direction(int start)
        {
            Start = start;
        }

        public void Complement()
        {
            var tempValue = Start;
            Start = Finish;
            Finish = tempValue;
        }
    }
}