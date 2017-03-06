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
        void Sknewness();
        void CpG();
        void ZCurved();
        void OrUr();
        void Frekans();
    }
}
