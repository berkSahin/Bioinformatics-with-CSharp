using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfarmatics.Library.Core.Abstracts
{
    public abstract class SequenceBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _content;

        virtual public string Content
        {
            get { return _content; }
            set { _content = value; }
        }



    }
}
