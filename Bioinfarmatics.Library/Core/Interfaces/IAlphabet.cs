using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfarmatics.Library.Core.Interfaces
{
    interface IAlphabet
    {
        char[] Letters { get; set; }
        bool IsDNA();
        bool IsRNA();
        bool IsProtein();
    }
}
