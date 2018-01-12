namespace NiceMon
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.nicehashWalletAddressBox = new System.Windows.Forms.TextBox();
            this.startUpdatingButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.logOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NiceHash Wallet Address:";
            // 
            // nicehashWalletAddressBox
            // 
            this.nicehashWalletAddressBox.Location = new System.Drawing.Point(150, 10);
            this.nicehashWalletAddressBox.Name = "nicehashWalletAddressBox";
            this.nicehashWalletAddressBox.Size = new System.Drawing.Size(261, 20);
            this.nicehashWalletAddressBox.TabIndex = 1;
            this.nicehashWalletAddressBox.Text = "1DmxieDQDgSZnpjMoY2Y1nmcpUXEcnmVXH";
            // 
            // startUpdatingButton
            // 
            this.startUpdatingButton.Location = new System.Drawing.Point(417, 10);
            this.startUpdatingButton.Name = "startUpdatingButton";
            this.startUpdatingButton.Size = new System.Drawing.Size(77, 47);
            this.startUpdatingButton.TabIndex = 3;
            this.startUpdatingButton.Text = "Go!";
            this.startUpdatingButton.UseVisualStyleBackColor = true;
            this.startUpdatingButton.Click += new System.EventHandler(this.startUpdatingButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Target BTC Amount (optional):";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown1.DecimalPlaces = 8;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericUpDown1.Location = new System.Drawing.Point(169, 37);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(242, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            37711,
            0,
            0,
            327680});
            // 
            // logOutput
            // 
            this.logOutput.Location = new System.Drawing.Point(16, 410);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.Size = new System.Drawing.Size(634, 154);
            this.logOutput.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 576);
            this.Controls.Add(this.logOutput);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startUpdatingButton);
            this.Controls.Add(this.nicehashWalletAddressBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MineSuite";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nicehashWalletAddressBox;
        private System.Windows.Forms.Button startUpdatingButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox logOutput;
    }
}

