using Bioinfarmatics.Library.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinformatics.WindowsForm
{
    public class FastaParser
    {
        private List<Sequence> SequenceList;
        public string Path { get; set; }

        public FastaParser(string filePath)
        {
            Path = filePath;
            SequenceList = new List<Sequence>();
        }

        public List<Sequence> GetSequences()
        {
            var contentStringBuilder = new StringBuilder();

            using (FileStream fs = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            contentStringBuilder.AppendLine(line);
                        }
                    }
                }

            }

            foreach (var sequence in contentStringBuilder.ToString().Split('>').Where(x => x != String.Empty))
            {
                var tempList = sequence.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
                SequenceList.Add(new Sequence(tempList.First(), String.Join("", tempList.Skip(1))));
            }

            return SequenceList;
        }
    }
}
