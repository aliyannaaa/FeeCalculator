namespace FeeCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            button2 = new Button();
            label3 = new Label();
            panel1 = new Panel();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            textBox3 = new TextBox();
            panel2 = new Panel();
            button5 = new Button();
            panel4 = new Panel();
            panel3 = new Panel();
            button4 = new Button();
            panel5 = new Panel();
            button3 = new Button();
            label7 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(31, 188);
            button1.Name = "button1";
            button1.Size = new Size(90, 24);
            button1.TabIndex = 0;
            button1.Text = "Compute";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(82, 80);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(145, 23);
            textBox1.TabIndex = 1;
            textBox1.Visible = false;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(17, 84);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 2;
            label1.Text = "Surcharge:";
            label1.Visible = false;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(25, 77);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Ticket Total:";
            label2.Visible = false;
            label2.Click += label2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(99, 73);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(145, 23);
            textBox2.TabIndex = 5;
            textBox2.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(37, 188);
            button2.Name = "button2";
            button2.Size = new Size(90, 24);
            button2.TabIndex = 4;
            button2.Text = "Compute";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(25, 123);
            label3.Name = "label3";
            label3.Size = new Size(28, 15);
            label3.TabIndex = 6;
            label3.Text = "Fee:";
            label3.Visible = false;
            label3.Click += label3_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Thistle;
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(764, 31);
            panel1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label4.Location = new Point(8, 7);
            label4.Name = "label4";
            label4.Size = new Size(138, 15);
            label4.TabIndex = 0;
            label4.Text = "welcome to fee calculator";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "", "Surcharge Fee", "AR Amount Fee" });
            comboBox1.Location = new Point(315, 46);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(124, 23);
            comboBox1.TabIndex = 8;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(25, 151);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 9;
            label5.Text = "AR Amount:";
            label5.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(25, 118);
            label6.Name = "label6";
            label6.Size = new Size(118, 15);
            label6.TabIndex = 10;
            label6.Text = "Payment Service Fee:";
            label6.Visible = false;
            label6.Click += label6_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(146, 115);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(98, 23);
            textBox3.TabIndex = 11;
            textBox3.Visible = false;
            // 
            // panel2
            // 
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(button5);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(47, 93);
            panel2.Name = "panel2";
            panel2.Size = new Size(254, 297);
            panel2.TabIndex = 12;
            // 
            // button5
            // 
            button5.Location = new Point(137, 188);
            button5.Name = "button5";
            button5.Size = new Size(90, 24);
            button5.TabIndex = 14;
            button5.Text = "Clear";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(6, 141, 157);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(254, 31);
            panel4.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(button4);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(textBox3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(textBox2);
            panel3.Location = new Point(443, 93);
            panel3.Name = "panel3";
            panel3.Size = new Size(273, 297);
            panel3.TabIndex = 13;
            // 
            // button4
            // 
            button4.Location = new Point(152, 188);
            button4.Name = "button4";
            button4.Size = new Size(90, 24);
            button4.TabIndex = 13;
            button4.Text = "Clear";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(6, 141, 157);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(273, 31);
            panel5.TabIndex = 12;
            // 
            // button3
            // 
            button3.Location = new Point(476, 47);
            button3.Name = "button3";
            button3.Size = new Size(123, 23);
            button3.TabIndex = 14;
            button3.Text = "Admin";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label7.ForeColor = SystemColors.ControlLightLight;
            label7.Location = new Point(133, 50);
            label7.Name = "label7";
            label7.Size = new Size(180, 15);
            label7.TabIndex = 15;
            label7.Text = "Choose what fee to be calculated";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(764, 423);
            Controls.Add(label7);
            Controls.Add(button3);
            Controls.Add(panel2);
            Controls.Add(comboBox1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "Fee Calculator";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Button button2;
        private Label label3;
        private Panel panel1;
        private ComboBox comboBox1;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox3;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Button button3;
        private Label label7;
        private Button button4;
        private Button button5;
    }
}
