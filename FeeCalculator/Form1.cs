using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace FeeCalculator
{
    public partial class Form1 : Form
    {
        private string _surchargeFormulaTemplate = "(({surcharge} + 5) * 0.045) + 10";
        private string _arFormulaTemplate = "{ticketTotal} + {serviceFee}";
        private readonly DataTable _evaluatorTable = new();

        public Form1()
        {
            InitializeComponent();
            SetupEvents();
            SetInitialUIState();
            FixTabOrder();

            // Set focus to the ComboBox so you don't need the mouse at startup
            this.ActiveControl = comboBox1;
        }

        private void SetupEvents()
        {
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.textBox1.KeyDown += TextBox1_KeyDown;
            this.textBox2.KeyDown += TextBox2Or3_KeyDown;
            this.textBox3.KeyDown += TextBox2Or3_KeyDown;

            // This is required for the Enter key to open the dropdown
            this.comboBox1.KeyDown += ComboBox1_KeyDown;
        }

        private void SetInitialUIState()
        {
            UpdateUIVisibility(string.Empty);
            button3.Visible = false;
        }

        private void FixTabOrder()
        {
            comboBox1.TabIndex = 0; // ComboBox should be first

            // AR Amount Sequence
            textBox2.TabIndex = 1;
            textBox3.TabIndex = 2;
            button2.TabIndex = 3;
            button4.TabIndex = 4;

            // Surcharge Sequence
            textBox1.TabIndex = 1;
            button1.TabIndex = 2;
            button5.TabIndex = 3;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        #region Navigation & Hotkeys

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                button3.Visible = !button3.Visible;
                e.SuppressKeyPress = true;
            }
        }

        // This makes the ComboBox interactive via keyboard
        private void ComboBox1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Toggle the dropdown list
                comboBox1.DroppedDown = !comboBox1.DroppedDown;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Calculation Logic

        private bool TryEvaluateTemplate(string template, IDictionary<string, double> values, out double result)
        {
            string expression = template;
            foreach (var kv in values)
                expression = expression.Replace(kv.Key, kv.Value.ToString(CultureInfo.InvariantCulture));

            expression = expression.Replace(',', '.');

            try
            {
                while (expression.Contains("CEILING(", StringComparison.OrdinalIgnoreCase))
                {
                    int idx = expression.IndexOf("CEILING(", StringComparison.OrdinalIgnoreCase);
                    int start = idx + 8;
                    int depth = 1, i = start;
                    for (; i < expression.Length && depth > 0; i++)
                    {
                        if (expression[i] == '(') depth++;
                        else if (expression[i] == ')') depth--;
                    }
                    string inner = expression.Substring(start, i - start - 1);
                    double innerVal = Convert.ToDouble(_evaluatorTable.Compute(inner, ""), CultureInfo.InvariantCulture);
                    expression = expression.Substring(0, idx) + Math.Ceiling(innerVal) + expression.Substring(i);
                }
                result = Convert.ToDouble(_evaluatorTable.Compute(expression, ""), CultureInfo.InvariantCulture);
                return true;
            }
            catch { result = 0; return false; }
        }

        private string FormatResult(double value, string template, bool forceInteger)
        {
            bool hasCeil = template.Contains("CEILING(", StringComparison.OrdinalIgnoreCase) ||
                           template.Contains("ROUNDUP(", StringComparison.OrdinalIgnoreCase);

            if (forceInteger || hasCeil)
                return Math.Ceiling(value).ToString(CultureInfo.InvariantCulture);

            return (Math.Abs(value - Math.Round(value)) < 1e-9)
                ? Math.Round(value).ToString(CultureInfo.InvariantCulture)
                : value.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region UI Event Handlers

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIVisibility(comboBox1.SelectedItem?.ToString() ?? string.Empty);

            // Move focus to the first textbox automatically after selecting a type
            if (comboBox1.SelectedItem?.ToString() == "Surcharge Fee") textBox1.Focus();
            else if (comboBox1.SelectedItem?.ToString() == "AR Amount Fee") textBox2.Focus();
        }

        private void UpdateUIVisibility(string type)
        {
            bool isSurcharge = type == "Surcharge Fee";
            bool isAR = type == "AR Amount Fee";

            label1.Visible = textBox1.Visible = label3.Visible = button1.Visible = panel2.Visible = isSurcharge;
            label2.Visible = textBox2.Visible = label5.Visible = label6.Visible =
            textBox3.Visible = button2.Visible = panel3.Visible = isAR;

            if (isSurcharge) { textBox1.Clear(); label3.Text = "Fee:"; }
            if (isAR) { textBox2.Clear(); textBox3.Clear(); label5.Text = "AR Amount:"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double surcharge)) return;
            var vals = new Dictionary<string, double> { ["{surcharge}"] = surcharge };
            if (TryEvaluateTemplate(_surchargeFormulaTemplate, vals, out double fee))
            {
                label3.Text = $"Fee: {FormatResult(fee, _surchargeFormulaTemplate, true)}\n" +
                             $"Breakdown: {_surchargeFormulaTemplate.Replace("{surcharge}", surcharge.ToString(CultureInfo.InvariantCulture))}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox2.Text, out double ticket) || !double.TryParse(textBox3.Text, out double service)) return;
            var vals = new Dictionary<string, double> { ["{ticketTotal}"] = ticket, ["{serviceFee}"] = service };
            if (TryEvaluateTemplate(_arFormulaTemplate, vals, out double arAmount))
            {
                string breakdown = _arFormulaTemplate.Replace("{ticketTotal}", ticket.ToString(CultureInfo.InvariantCulture))
                                                     .Replace("{serviceFee}", service.ToString(CultureInfo.InvariantCulture));
                label5.Text = $"AR Amount: {FormatResult(arAmount, _arFormulaTemplate, false)}\nBreakdown: {breakdown}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using var admin = new AdminForm(_surchargeFormulaTemplate, _arFormulaTemplate);
            if (admin.ShowDialog(this) == DialogResult.OK)
            {
                _surchargeFormulaTemplate = admin.SurchargeFormula;
                _arFormulaTemplate = admin.ARFormula;
            }
        }

        private void button5_Click(object sender, EventArgs e) { textBox1.Clear(); label3.Text = "Fee:"; }
        private void button4_Click(object sender, EventArgs e) { textBox2.Clear(); textBox3.Clear(); label5.Text = "AR Amount:"; }

        private void TextBox1_KeyDown(object? sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { button1.PerformClick(); e.SuppressKeyPress = true; } }
        private void TextBox2Or3_KeyDown(object? sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { button2.PerformClick(); e.SuppressKeyPress = true; } }

        #endregion
    }
}