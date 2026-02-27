namespace FeeCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

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

        // Templates for admin-editable formula breakdowns. Use placeholders:
        // {surcharge} - replaced with the surcharge input value
        // {ticketTotal} - replaced with ticket total input value
        // {serviceFee} - replaced with service fee input value
        private string surchargeFormulaTemplate = "(({surcharge} - 5) * 0.045) + 10";
        private string arFormulaTemplate = "{ticketTotal} + {serviceFee}";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double surcharge = Convert.ToDouble(textBox1.Text);
            double fee = Math.Ceiling((surcharge - 5) * 0.045) + 10;

            label3.Text = "Fee: " + fee.ToString() +
                "\nBreakdown: ((" + surcharge + " - 5) * 0.045) + 10";


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
            double ticketTotal = Convert.ToDouble(textBox2.Text);
            double serviceFee = Convert.ToDouble(textBox3.Text);
            double arAmount = ticketTotal + serviceFee;

            label5.Text = "AR Amount: " + arAmount.ToString() +
                "\nBreakdown: " + ticketTotal + " + " + serviceFee;

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
    }
}
