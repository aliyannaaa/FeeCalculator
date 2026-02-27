using System;
using System.Drawing;
using System.Windows.Forms;

namespace FeeCalculator
{
    public class AdminForm : Form
    {
        private TextBox txtSurcharge;
        private TextBox txtAR;
        private Button btnOk;
        private Button btnCancel;

        public string SurchargeFormula { get; private set; }
        public string ARFormula { get; private set; }

        public AdminForm(string surchargeTemplate, string arTemplate)
        {
            Text = "Admin: Edit Formulas";
            Size = new Size(520, 280);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lbl1 = new Label() { Text = "Surcharge Fee Formula (use {surcharge}):", AutoSize = true, Location = new Point(10, 10) };
            txtSurcharge = new TextBox() { Text = surchargeTemplate, Location = new Point(10, 35), Width = 480 };

            var lbl2 = new Label() { Text = "AR Amount Formula (use {ticketTotal} and {serviceFee}):", AutoSize = true, Location = new Point(10, 75) };
            txtAR = new TextBox() { Text = arTemplate, Location = new Point(10, 100), Width = 480 };

            btnOk = new Button() { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(320, 180), Width = 75 };
            btnCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(405, 180), Width = 75 };

            btnOk.Click += (s, e) => { SurchargeFormula = txtSurcharge.Text; ARFormula = txtAR.Text; DialogResult = DialogResult.OK; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lbl1);
            Controls.Add(txtSurcharge);
            Controls.Add(lbl2);
            Controls.Add(txtAR);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
        }
    }
}
