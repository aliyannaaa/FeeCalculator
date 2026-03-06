using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing.Printing;

namespace FeeCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // Allow pressing Enter in the textboxes to trigger calculations
            this.textBox1.KeyDown += TextBox1_KeyDown;
            this.textBox2.KeyDown += TextBox2Or3_KeyDown;
            this.textBox3.KeyDown += TextBox2Or3_KeyDown;

            label1.Text = "Surchage";
            label1.Visible = false;
            textBox1.Visible = false;
            label3.Text = "Fee";
            label3.Visible = false;
            button1.Visible = false;
            panel2.Visible = false;

            label2.Text = "Ticket Total";
            label2.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            textBox3.Visible = false;
            panel3.Visible = false;


        }

        // Toggle visibility of the admin button using a hotkey (Ctrl+Shift+A)
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                // Toggle the admin button visibility
                button3.Visible = !button3.Visible;
                e.SuppressKeyPress = true;
            }
        }

        private string surchargeFormulaTemplate = "(({surcharge} + 5) * 0.045) + 10";
        private string arFormulaTemplate = "{ticketTotal} + {serviceFee}";

        // Note: numeric calculations use the original straightforward logic.
        // Admin templates are used only to display the breakdown text by replacing placeholders.

        // Supports basic arithmetic and a CEILING(...) function.
        // Templates should contain placeholders like {surcharge}, {ticketTotal}, {serviceFee} which will be
        // replaced with numeric values before evaluation.
        private bool TryEvaluateTemplate(string template, IDictionary<string, double> values, out double result, out string replaced)
        {
            replaced = template;
            // Replace placeholders with invariant-formatted numbers
            foreach (var kv in values)
            {
                replaced = replaced.Replace(kv.Key, kv.Value.ToString(CultureInfo.InvariantCulture));
            }

            // Normalize decimal separator
            replaced = replaced.Replace(',', '.');

            try
            {
                // Handle CEILING(...) occurrences from innermost to outer
                while (true)
                {
                    int idx = IndexOfIgnoreCase(replaced, "CEILING(");
                    if (idx == -1) break;

                    int start = idx + "CEILING(".Length;
                    int depth = 1;
                    int i = start;
                    for (; i < replaced.Length; i++)
                    {
                        if (replaced[i] == '(') depth++;
                        else if (replaced[i] == ')') depth--;
                        if (depth == 0) break;
                    }
                    if (depth != 0) throw new ArgumentException("Mismatched parentheses in CEILING().");

                    string inner = replaced.Substring(start, i - start);
                    // Evaluate inner using DataTable
                    var tbl = new DataTable();
                    object innerObj = tbl.Compute(inner, "");
                    double innerVal = Convert.ToDouble(innerObj, CultureInfo.InvariantCulture);
                    double ceilVal = Math.Ceiling(innerVal);

                    // Replace CEILING(inner) with numeric value
                    replaced = replaced.Substring(0, idx) + ceilVal.ToString(CultureInfo.InvariantCulture) + replaced.Substring(i + 1);
                }

                // Evaluate remaining expression
                var table = new DataTable();
                object resultObj = table.Compute(replaced, "");
                result = Convert.ToDouble(resultObj, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        private static int IndexOfIgnoreCase(string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase);
        }

        // Format numeric result for display.
        // If forceInteger is true or the template uses CEILING/ROUNDUP, show integer (rounded up) without decimals.
        // Otherwise show whole numbers without decimals and keep decimals for fractional values.
        private string FormatResult(double value, string template, bool forceInteger)
        {
            if (forceInteger || IndexOfIgnoreCase(template, "CEILING(") != -1 || IndexOfIgnoreCase(template, "ROUNDUP(") != -1)
            {
                return Math.Ceiling(value).ToString(CultureInfo.InvariantCulture);
            }

            // If value is whole number, show without decimal point
            if (Math.Abs(value - Math.Round(value)) < 1e-9)
                return Math.Round(value).ToString(CultureInfo.InvariantCulture);

            return value.ToString(CultureInfo.InvariantCulture);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Simple numeric calculation (keeps original behavior)
            double surcharge = Convert.ToDouble(textBox1.Text);
            double fee = Math.Ceiling((surcharge + 5) * 0.045) + 10;

            // Try to evaluate admin template to override numeric fee when possible
            var vals = new Dictionary<string, double> { ["{surcharge}"] = surcharge };
            bool evalUsed = TryEvaluateTemplate(surchargeFormulaTemplate, vals, out double evalFee, out string replaced);
            if (evalUsed)
            {
                fee = evalFee;
            }

            // Use admin template for displayed breakdown (placeholders replaced)
            string breakdown = surchargeFormulaTemplate.Replace("{surcharge}", surcharge.ToString(CultureInfo.InvariantCulture));

            label3.Text = "Fee: " + FormatResult(fee, surchargeFormulaTemplate, evalUsed) +
                "\nBreakdown: " + breakdown;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void TextBox1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger surcharge calculation
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox2Or3_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger AR amount calculation
                button2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = comboBox1.SelectedItem.ToString();

            if (selectedType == "Surcharge Fee")
            {
                label1.Text = "Surchage";
                label1.Visible = true;
                textBox1.Visible = true;
                label3.Text = "Fee";
                label3.Visible = true;
                button1.Visible = true;
                panel2.Visible = true;

                label2.Text = "Ticket Total";
                label2.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox3.Visible = false;
                panel3.Visible = false;

                textBox1.Clear();
                label3.Text = "Fee:";
            }
            else if (selectedType == "AR Amount Fee")
            {
                label2.Text = "Ticket Total";
                label2.Visible = true;
                textBox2.Visible = true;
                button2.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                textBox3.Visible = true;
                panel3.Visible = true;

                label1.Text = "Surchage";
                label1.Visible = false;
                textBox1.Visible = false;
                label3.Text = "Fee";
                label3.Visible = false;
                button1.Visible = false;
                panel2.Visible = false;
            }
            else if (selectedType == "")
            {
                label1.Text = "Surchage";
                label1.Visible = false;
                textBox1.Visible = false;
                label3.Text = "Fee";
                label3.Visible = false;
                button1.Visible = false;
                panel2.Visible = false;

                label2.Text = "Ticket Total";
                label2.Visible = false;
                textBox2.Visible = false;
                button2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox3.Visible = false;
                panel3.Visible = false;

                textBox2.Clear();
                textBox3.Clear();
                label5.Text = "AR Amount:";
            }
            /* comboBox1.Items.Add("Surcharge Fee");
            comboBox1.Items.Add("AR Amount Fee"); */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Simple numeric calculation (keeps original behavior)
            double ticketTotal = Convert.ToDouble(textBox2.Text);
            double serviceFee = Convert.ToDouble(textBox3.Text);
            double arAmount = ticketTotal + serviceFee;

            // Try to evaluate admin template to override numeric AR amount when possible
            var vals = new Dictionary<string, double>
            {
                ["{ticketTotal}"] = ticketTotal,
                ["{serviceFee}"] = serviceFee
            };
            bool evalUsed = TryEvaluateTemplate(arFormulaTemplate, vals, out double evalAr, out string replaced);
            if (evalUsed)
            {
                arAmount = evalAr;
            }

            // Use admin template for displayed breakdown (placeholders replaced)
            string breakdown = arFormulaTemplate.Replace("{ticketTotal}", ticketTotal.ToString(CultureInfo.InvariantCulture))
                                                .Replace("{serviceFee}", serviceFee.ToString(CultureInfo.InvariantCulture));

            label5.Text = "AR Amount: " + FormatResult(arAmount, arFormulaTemplate, evalUsed) +
                "\nBreakdown: " + breakdown;
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var admin = new AdminForm(surchargeFormulaTemplate, arFormulaTemplate))
            {
                if (admin.ShowDialog(this) == DialogResult.OK)
                {
                    surchargeFormulaTemplate = admin.SurchargeFormula;
                    arFormulaTemplate = admin.ARFormula;
                    MessageBox.Show("Formulas updated.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            label3.Text = "Fee:";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            label5.Text = "AR Amount:";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Check if there is actually a result to print
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please calculate the AR Amount first.", "Empty Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build the Receipt String
            string receiptContent = "        OFFICIAL RECEIPT        \n";
            receiptContent += "--------------------------------\n";
            receiptContent += $"Date: {DateTime.Now.ToString("MM/dd/yyyy HH:mm")}\n";
            receiptContent += "Type: AR Amount Calculation\n";
            receiptContent += "--------------------------------\n";
            receiptContent += $"Ticket Total:  {textBox2.Text}\n";
            receiptContent += $"Service Fee:   {textBox3.Text}\n";
            receiptContent += "--------------------------------\n";
            receiptContent += $"{label5.Text}\n"; // This contains your AR Amount and Breakdown
            receiptContent += "--------------------------------\n";
            receiptContent += "       THANK YOU!       \n";

            // Call the PrintReceipt helper method
            PrintReceipt(receiptContent);
        }

        // Variable to store what we want to print
        private string printText = "";

        private void PrintReceipt(string content)
        {
            printText = content;
            PrintDocument pd = new PrintDocument();

            // This tells the printer WHAT to draw
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            // This creates the Pop-Up Window
            PrintPreviewDialog receiptPopUp = new PrintPreviewDialog();
            receiptPopUp.Document = pd;

            // Show the pop-up on top of your main form
            receiptPopUp.ShowDialog(this);
        }

        // This is where the "drawing" happens
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            // Define the font and layout
            Font printFont = new Font("Courier New", 10);
            //float linesPerPage = 0;
            //float yPos = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            // Draw the text onto the page
            ev.Graphics.DrawString(printText, printFont, Brushes.Black, leftMargin, topMargin, new StringFormat());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 1. Validation: Don't print if there's no data
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a surcharge amount and calculate first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Build the Receipt String
            string receiptContent = "        OFFICIAL RECEIPT       \n";
            receiptContent += "--------------------------------\n";
            receiptContent += $"Date: {DateTime.Now.ToString("MM/dd/yyyy HH:mm")}\n";
            receiptContent += "Type: Surcharge Calculation\n";
            receiptContent += "--------------------------------\n";
            receiptContent += $"Surcharge: {textBox1.Text}\n";
            receiptContent += "--------------------------------\n";

            // label3.Text already contains "Fee: X \nBreakdown: Y" from your button1_Click
            receiptContent += $"{label3.Text}\n";

            receiptContent += "--------------------------------\n";
            receiptContent += "         THANK YOU!       \n";

            // 3. Send to the printer engine
            PrintReceipt(receiptContent);
        }

        /*private void SendToPhysicalPrinter(string content)
        {
            printText = content; // Your receipt string
            PrintDocument pd = new PrintDocument();

        // Standard 80mm thermal paper is roughly 300 hundredths of an inch wide
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 300, 1000);

            // 1. Link the drawing logic
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            // 2. Create the real Windows Print Dialog
            PrintDialog printDlg = new PrintDialog();
            printDlg.Document = pd;

            // 3. Execution
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                // This command sends the data to the printer's queue
                pd.Print();
            }
        }*/
    }
}
