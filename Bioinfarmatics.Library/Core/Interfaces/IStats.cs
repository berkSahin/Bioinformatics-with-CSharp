using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfarmatics.Library.Core.Interfaces
{
    interface IStats
    {
        ulong Lenght();
        void Sknewness(BasePair basePair, int frameSize);
        void CpG();
        void ZCurved();
        void OrUr();
        List<Tuple<char, int>> Frekans(char[] letters);
    }
}
