using Bioinfarmatics.Library.Utils;
using System;
using System.Windows.Forms;

namespace Bioinformatics.WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var parser = new FastaParser(@"D:\d.fna");
            //var sequenceList = parser.GetSequences();

            //var chart = new FakeChart(sequenceList[0].Stats.Sknewness(Bioinfarmatics.Library.Core.BasePair.GC,10000,false));
            //chart.Show();

            var alignment = new SequenceAlignment();
            var model = alignment.NeedlemanWunsch(
                new char[] { 'A', 'T', 'T', 'C' },
                new char[] { 'A', 'G', 'T', 'C' },
                5, -5, 2);
        }
    }
}