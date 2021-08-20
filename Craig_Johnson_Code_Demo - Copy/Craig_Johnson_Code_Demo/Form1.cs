using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Craig_Johnson_Code_Demo
{
    public partial class Form1 : Form
    {
        /// prevents integer overflow
        private const int MaxInt = 715827882;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /// only procceeds if the entered value is a positive digit
            int value;
            if (int.TryParse(textBox2.Text, out value))
            {
                var steps = 0;
                var sequence = new List<int>();
                var status = 0;
                int number = Int32.Parse(textBox2.Text);

                if (number > 0)
                {
                    /// refreshes dataset used to graph
                    sequence.Add(number);
                    chart1.Series.Clear();
                    chart1.Series.Add("Series1");
                    chart1.Series[0].LegendText = "cycle for " + number.ToString();
                    chart1.Series[0].ChartType = SeriesChartType.Line;
                    chart1.AutoSize = true;

                    while (status != 1)
                    {
                        steps += 1;
                        if (number == 1)
                        {
                            status = 1;
                            sequence.Add(4);
                            sequence.Add(2);
                            sequence.Add(1);
                        }
                        else if (number % 2 == 0)
                        {
                            number /= 2;
                            sequence.Add(number);
                        }
                        else
                        {
                            if (MaxInt > number)
                            {
                                number = number * 3 + 1;
                                sequence.Add(number);
                            }
                            else
                            {
                                ///catches any possible overflow not caught previously--perhaps unneeded
                                throw new Exception("Sequence contains a number beyond integer values.");
                            }
                        }
                    }

                    var result = String.Join(", ", sequence.ToArray());
                    var values = sequence.ToArray();
                    chart1.Titles.Clear();
                    string title = string.Concat(" steps to reach base cycle: ", steps.ToString());
                    chart1.Titles.Add(title);
                    textBox3.Text = result;

                    /// binds new dataset
                    chart1.DataSource = values;
                    int[] x = Enumerable.Range(0, values.Length).ToArray();
                    int[] y = sequence.ToArray();
                    chart1.Series[0].Points.DataBindXY(x, y);
                    chart1.Update();
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            /// gives curosr y value in an effort to help clearly show graph values
            double XVal = chart1.ChartAreas[0].CursorY.Position;
            label3.Text = XVal.ToString();
        }
    }
}
