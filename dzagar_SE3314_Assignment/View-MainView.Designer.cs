namespace dzagar_SE3314_Assignment
{
    partial class MainView
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
            this.ListenButton = new System.Windows.Forms.Button();
            this.PortNumberTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBoxLabel = new System.Windows.Forms.Label();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(641, 47);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(137, 49);
            this.ListenButton.TabIndex = 0;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // PortNumberTextBox
            // 
            this.PortNumberTextBox.Location = new System.Drawing.Point(481, 56);
            this.PortNumberTextBox.Name = "PortNumberTextBox";
            this.PortNumberTextBox.Size = new System.Drawing.Size(126, 31);
            this.PortNumberTextBox.TabIndex = 1;
            this.PortNumberTextBox.Text = "8000";
            // 
            // PortTextBoxLabel
            // 
            this.PortTextBoxLabel.AutoSize = true;
            this.PortTextBoxLabel.Location = new System.Drawing.Point(308, 59);
            this.PortTextBoxLabel.Name = "PortTextBoxLabel";
            this.PortTextBoxLabel.Size = new System.Drawing.Size(151, 25);
            this.PortTextBoxLabel.TabIndex = 2;
            this.PortTextBoxLabel.Text = "Listen on Port:";
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Enabled = false;
            this.IPAddressTextBox.Location = new System.Drawing.Point(481, 138);
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(251, 31);
            this.IPAddressTextBox.TabIndex = 3;
            this.IPAddressTextBox.Text = "127.0.0.1";
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(268, 141);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(191, 25);
            this.IPAddressLabel.TabIndex = 4;
            this.IPAddressLabel.Text = "Server IP Address:";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 693);
            this.Controls.Add(this.IPAddressLabel);
            this.Controls.Add(this.IPAddressTextBox);
            this.Controls.Add(this.PortTextBoxLabel);
            this.Controls.Add(this.PortNumberTextBox);
            this.Controls.Add(this.ListenButton);
            this.Enabled = false;
            this.Name = "MainView";
            this.Text = "Streaming Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.TextBox PortNumberTextBox;
        private System.Windows.Forms.Label PortTextBoxLabel;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.Label IPAddressLabel;
    }
}

