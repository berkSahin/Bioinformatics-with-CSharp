using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bioinfarmatics.Library.Core.Interfaces
{
    internal interface IStats
    {
        ulong Lenght();

        List<double> Sknewness(BasePair basePair, int frameSize, bool cumulative);

        void CpG();

        List<Point3D> ZCurved(int frameSize);

        void OrUr();

        List<Tuple<char, int>> Frekans(char[] letters);
    }
}