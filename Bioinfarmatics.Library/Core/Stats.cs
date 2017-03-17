using Bioinfarmatics.Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;


namespace Bioinfarmatics.Library.Core
{
    public class Stats : IStats
    {
        public string Content { get; set; }

        public Stats(string content)
        {
            this.Content = content;
        }

        public void CpG()
        {
            throw new NotImplementedException();
        }

        public List<Tuple<char, int>> Frekans(char[] letters)
        {
            var frekansList = new List<Tuple<char, int>>();
            foreach (var letter in letters)
            {
                frekansList.Add(Tuple.Create(
                    letter,
                    Content.ToCharArray().Where(x => x.Equals(letter)).Count()
                    ));
            }
            return frekansList;
        }

        public ulong Lenght()
        {
            return Convert.ToUInt32(Content.ToArray().LongLength);
        }

        public void OrUr()
        { 
            throw new NotImplementedException();
        }

        public List<double> Sknewness(BasePair basePair, int frameSize, bool cumulative = false)
        {
            var result = new List<double>();
            if (basePair == BasePair.GC)
            {
                var gCount = Content.ToCharArray().Where(x => x.Equals('G')).Count();
                var cCount = Content.ToCharArray().Where(x => x.Equals('C')).Count();

                var frameCount = Math.Ceiling((double)Content.Length / (double)frameSize);

                for (int i = 0; i < frameCount; i++)
                {
                    if (Content.Length - i * frameSize > frameSize)
                    {
                        var tempList = Content.ToList().GetRange(i * frameSize, frameSize);
                        var frameGCount = tempList.Where(x => x.Equals('G')).Count();
                        var frameCCount = tempList.Where(x => x.Equals('C')).Count();
                        result.Add(((double)(frameGCount - frameCCount) / (double)(frameGCount + frameCCount)));
                    }
                }
            }
            else
            {
                var aCount = Content.ToCharArray().Where(x => x.Equals('A')).Count();
                var tCount = Content.ToCharArray().Where(x => x.Equals('T')).Count();

                var frameCount = Math.Ceiling((double)Content.Length / (double)frameSize);

                for (int i = 0; i < frameCount; i++)
                {
                    if (Content.Length - i * frameSize > frameSize)
                    {
                        var tempList = Content.ToList().GetRange(i * frameSize, frameSize);
                        var frameACount = tempList.Where(x => x.Equals('A')).Count();
                        var frameTCount = tempList.Where(x => x.Equals('T')).Count();
                        result.Add(((double)(frameACount - frameTCount) / (double)(frameACount + frameTCount)));
                    }
                }
            }

            if (cumulative)
            {
                var cumulativeResult = new List<double>();

                for (int i = 0; i < result.Count; i++)
                {
                    double temp = 0;
                    for (int j = 0; j <= i; j++)
                    {
                        temp += result[j];
                    }
                    cumulativeResult.Add(temp);
                }
                return cumulativeResult;
            }
            return result;
        }

        public List<Point3D> ZCurved(int frameSize)
        {
            var result = new List<Point3D>();
            var frameCount = Math.Ceiling((double)Content.Length / (double)frameSize);

            for (int i = 0; i < frameCount; i++)
            {
                if (Content.Length - i * frameSize > frameSize)
                {
                    var tempList = Content.ToList().GetRange(i * frameSize, frameSize);

                    var frameACount = tempList.Where(x => x.Equals('A')).Count();
                    var frameTCount = tempList.Where(x => x.Equals('T')).Count();
                    var frameGCount = tempList.Where(x => x.Equals('G')).Count();
                    var frameCCount = tempList.Where(x => x.Equals('C')).Count();

                    result.Add(new Point3D
                    {
                        X = (frameACount + frameGCount) - (frameCCount - frameTCount),
                        Y = (frameACount + frameCCount) - (frameGCount + frameTCount),
                        Z = (frameACount + frameTCount) - (frameGCount + frameCCount)
                    });
                }
            }
            return result;
        }
    }

    public enum BasePair
    {
        AT, GC
    }
}