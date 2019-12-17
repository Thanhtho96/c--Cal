using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private bool isResult = false;

        string specifier = "G";
        CultureInfo culture = CultureInfo.CreateSpecificCulture("eu-ES");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button_zero_Click(object sender, EventArgs e)
        {
            initNumber("0");

        }
        private void button_one_Click(object sender, EventArgs e)
        {
            initNumber("1");
        }
        private void button_two_Click(object sender, EventArgs e)
        {
            initNumber("2");
        }
        private void button_three_Click(object sender, EventArgs e)
        {
            initNumber("3");
        }
        private void button_four_Click(object sender, EventArgs e)
        {
            initNumber("4");
        }
        private void button_five_Click(object sender, EventArgs e)
        {
            initNumber("5");
        }
        private void button_six_Click(object sender, EventArgs e)
        {
            initNumber("6");
        }
        private void button_seven_Click(object sender, EventArgs e)
        {
            initNumber("7");
        }
        private void button_eight_Click(object sender, EventArgs e)
        {
            initNumber("8");
        }
        private void button_nine_Click(object sender, EventArgs e)
        {
            initNumber("9");
        }

        private void doDivide(object sender, EventArgs e)
        {
            this.lbFunction.Text += this.textBoxResult.Text + " / ";
            this.textBoxResult.Text = "0";
        }

        private void doMultiple(object sender, EventArgs e)
        {
            this.lbFunction.Text += this.textBoxResult.Text + " * ";
            this.textBoxResult.Text = "0";
        }

        private void doSubtract(object sender, EventArgs e)
        {
            this.lbFunction.Text += this.textBoxResult.Text + " - ";
            this.textBoxResult.Text = "0";
        }

        private void doAdd(object sender, EventArgs e)
        {
            this.lbFunction.Text += this.textBoxResult.Text + " + ";
            this.textBoxResult.Text = "0";
        }

        private void backSpace(object sender, EventArgs e)
        {
            if (this.textBoxResult.TextLength == 1)
            {
                this.textBoxResult.Text = "0";
            }
            else
            {
                int indexToRemove = this.textBoxResult.TextLength - 1;
                this.textBoxResult.Text = this.textBoxResult.Text.Remove(indexToRemove, 1);
            }
        }

        private void clearEdit(object sender, EventArgs e)
        {
            this.textBoxResult.Text = "0";
        }

        private void clearAll(object sender, EventArgs e)
        {
            this.textBoxResult.Text = "0";
            this.lbFunction.Text = "";
        }

        private void initNumber(String numberInPut)
        {
            if (isResult)
            {
                this.textBoxResult.Text = numberInPut;
                isResult = false;
            }
            else
            {
                if (this.textBoxResult.TextLength == 1 && this.textBoxResult.Text.Equals("0"))
                {
                    this.textBoxResult.Text = numberInPut;
                }
                else
                {
                    this.textBoxResult.AppendText(numberInPut);
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void doResult(object sender, EventArgs e)
        {
            if (this.lbFunction.Text.Contains("/ 0")
                || (this.lbFunction.Text.Trim().Last().ToString().Equals("/") && this.textBoxResult.Text.Equals("0")))
            {
                this.textBoxResult.Text = "Can't divide Zero";
                this.lbFunction.Text = "";
            }
            else
            {
                this.lbFunction.Text += this.textBoxResult.Text;
                this.textBoxResult.Text = new DataTable().Compute(this.lbFunction.Text, null).ToString();
                this.lbFunction.Text = "";
            }
            this.isResult = true;
        }

        private void doSquareRoot(object sender, EventArgs e)
        {
            if (!this.lbFunction.Text.Equals(""))
            {
                this.lbFunction.Text += this.textBoxResult.Text;
                String result = new DataTable().Compute(this.lbFunction.Text, null).ToString();
                this.textBoxResult.Text = Math.Sqrt(Convert.ToDouble(result)).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
            else
            {
                this.textBoxResult.Text = Math.Sqrt(Convert.ToDouble(this.textBoxResult.Text)).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
        }

        private void doSquared(object sender, EventArgs e)
        {
            if (!this.lbFunction.Text.Equals(""))
            {
                this.lbFunction.Text += this.textBoxResult.Text;
                String result = new DataTable().Compute(this.lbFunction.Text, null).ToString();
                this.textBoxResult.Text = Math.Pow(Convert.ToDouble(result), 2).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
            else
            {
                this.textBoxResult.Text = Math.Pow(Convert.ToDouble(this.textBoxResult.Text), 2).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
        }

        private void doOneHalf(object sender, EventArgs e)
        {
            if (!this.lbFunction.Text.Equals(""))
            {
                this.lbFunction.Text += this.textBoxResult.Text;
                String result = new DataTable().Compute(this.lbFunction.Text, null).ToString();
                this.textBoxResult.Text = (1 / (Convert.ToDouble(result))).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
            else
            {
                this.textBoxResult.Text = (1 / (Convert.ToDouble(this.textBoxResult.Text))).ToString(specifier, culture);
                this.lbFunction.Text = "";
            }
        }

        private void addPoint(object sender, EventArgs e)
        {
            if (!textBoxResult.Text.Contains("."))
            {
                textBoxResult.AppendText(".");
            }
        }

        private void toggleMinus(object sender, EventArgs e)
        {
            if (this.textBoxResult.Text.StartsWith("-"))
            {
                this.textBoxResult.Text = this.textBoxResult.Text.Substring(1);
            }
            else if (!string.IsNullOrEmpty(this.textBoxResult.Text) && decimal.Parse(this.textBoxResult.Text) != 0)
            {
                this.textBoxResult.Text = "-" + this.textBoxResult.Text;
            }
        }
    }
}
