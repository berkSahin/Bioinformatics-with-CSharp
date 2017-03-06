using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bioinfarmatics.Library.Core.Interfaces;
using System.Windows.Forms;
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

        public void Sknewness(BasePair basePair, int frameSize)
        {
            var resultList = new List<double>();
            if(basePair == BasePair.GC)
            {
                var gCount = Content.ToCharArray().Where(x => x.Equals('G')).Count();
                var cCount = Content.ToCharArray().Where(x => x.Equals('C')).Count();

                var frameCount = Math.Ceiling((double)Content.Length / (double)frameSize);

                for (int i = 0; i < frameCount; i++)
                {
                    if (Content.Length - i * frameSize > frameSize)
                    {
                        var tempList = Content.ToList().GetRange(i * frameSize, frameSize);
                        var frameGCCount = tempList.Where(x => x.Equals('G') || x.Equals('C')).Count();
                        resultList.Add(((double)(gCount - cCount) / (double)frameGCCount));
                    }
                }
            }
            var chart = new FakeChartForm1(resultList);
            chart.Show();
        }

        public void ZCurved()
        {
            throw new NotImplementedException();
        }

    }

    public enum BasePair
    {
        AT,GC
    }

    public class FakeChartForm1 : Form
    {
        private System.ComponentModel.IContainer components = null;
        System.Windows.Forms.DataVisualization.Charting.Chart chart1;

        private List<double> results;

        public FakeChartForm1 (List<double> results)
        {
            InitializeComponent();
            this.results = results;
        }

        public FakeChartForm1()
        {
        }

        private double f(int i)
        {
            var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            this.chart1.Series.Add(series1);

            for (int i = 0; i < results.Count; i++)
            {
                series1.Points.AddXY(i, results[i]);
            }
            chart1.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            //
            // chart1
            //
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 50);
            this.chart1.Name = "chart1";
            // this.chart1.Size = new System.Drawing.Size(284, 212);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "FakeChart";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FakeChartForm1());
        }
    }
}
