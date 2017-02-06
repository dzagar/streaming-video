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
            this.FrameNoTextBox = new System.Windows.Forms.TextBox();
            this.FrameNoLabel = new System.Windows.Forms.Label();
            this.PrintHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.ServerDialogBox = new System.Windows.Forms.TextBox();
            this.ClientRequestsLabel = new System.Windows.Forms.Label();
            this.ClientRequestsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(443, 47);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(137, 49);
            this.ListenButton.TabIndex = 0;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // PortNumberTextBox
            // 
            this.PortNumberTextBox.Location = new System.Drawing.Point(258, 56);
            this.PortNumberTextBox.Name = "PortNumberTextBox";
            this.PortNumberTextBox.Size = new System.Drawing.Size(126, 31);
            this.PortNumberTextBox.TabIndex = 1;
            this.PortNumberTextBox.Text = "8000";
            // 
            // PortTextBoxLabel
            // 
            this.PortTextBoxLabel.AutoSize = true;
            this.PortTextBoxLabel.Location = new System.Drawing.Point(75, 59);
            this.PortTextBoxLabel.Name = "PortTextBoxLabel";
            this.PortTextBoxLabel.Size = new System.Drawing.Size(151, 25);
            this.PortTextBoxLabel.TabIndex = 2;
            this.PortTextBoxLabel.Text = "Listen on Port:";
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Enabled = false;
            this.IPAddressTextBox.Location = new System.Drawing.Point(258, 125);
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(251, 31);
            this.IPAddressTextBox.TabIndex = 3;
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(35, 128);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(191, 25);
            this.IPAddressLabel.TabIndex = 4;
            this.IPAddressLabel.Text = "Server IP Address:";
            // 
            // FrameNoTextBox
            // 
            this.FrameNoTextBox.Location = new System.Drawing.Point(938, 125);
            this.FrameNoTextBox.Name = "FrameNoTextBox";
            this.FrameNoTextBox.Size = new System.Drawing.Size(100, 31);
            this.FrameNoTextBox.TabIndex = 5;
            // 
            // FrameNoLabel
            // 
            this.FrameNoLabel.AutoSize = true;
            this.FrameNoLabel.Location = new System.Drawing.Point(796, 128);
            this.FrameNoLabel.Name = "FrameNoLabel";
            this.FrameNoLabel.Size = new System.Drawing.Size(112, 25);
            this.FrameNoLabel.TabIndex = 6;
            this.FrameNoLabel.Text = "Frame No:";
            // 
            // PrintHeaderCheckBox
            // 
            this.PrintHeaderCheckBox.AutoSize = true;
            this.PrintHeaderCheckBox.Location = new System.Drawing.Point(801, 58);
            this.PrintHeaderCheckBox.Name = "PrintHeaderCheckBox";
            this.PrintHeaderCheckBox.Size = new System.Drawing.Size(164, 29);
            this.PrintHeaderCheckBox.TabIndex = 7;
            this.PrintHeaderCheckBox.Text = "Print Header";
            this.PrintHeaderCheckBox.UseVisualStyleBackColor = true;
            // 
            // ServerDialogBox
            // 
            this.ServerDialogBox.Location = new System.Drawing.Point(80, 207);
            this.ServerDialogBox.Multiline = true;
            this.ServerDialogBox.Name = "ServerDialogBox";
            this.ServerDialogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServerDialogBox.Size = new System.Drawing.Size(972, 195);
            this.ServerDialogBox.TabIndex = 8;
            // 
            // ClientRequestsLabel
            // 
            this.ClientRequestsLabel.AutoSize = true;
            this.ClientRequestsLabel.Location = new System.Drawing.Point(80, 442);
            this.ClientRequestsLabel.Name = "ClientRequestsLabel";
            this.ClientRequestsLabel.Size = new System.Drawing.Size(170, 25);
            this.ClientRequestsLabel.TabIndex = 9;
            this.ClientRequestsLabel.Text = "Client Requests:";
            // 
            // ClientRequestsTextBox
            // 
            this.ClientRequestsTextBox.Location = new System.Drawing.Point(80, 493);
            this.ClientRequestsTextBox.Multiline = true;
            this.ClientRequestsTextBox.Name = "ClientRequestsTextBox";
            this.ClientRequestsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ClientRequestsTextBox.Size = new System.Drawing.Size(972, 150);
            this.ClientRequestsTextBox.TabIndex = 10;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 693);
            this.Controls.Add(this.ClientRequestsTextBox);
            this.Controls.Add(this.ClientRequestsLabel);
            this.Controls.Add(this.ServerDialogBox);
            this.Controls.Add(this.PrintHeaderCheckBox);
            this.Controls.Add(this.FrameNoLabel);
            this.Controls.Add(this.FrameNoTextBox);
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
        private System.Windows.Forms.TextBox FrameNoTextBox;
        private System.Windows.Forms.Label FrameNoLabel;
        private System.Windows.Forms.CheckBox PrintHeaderCheckBox;
        private System.Windows.Forms.TextBox ServerDialogBox;
        private System.Windows.Forms.Label ClientRequestsLabel;
        private System.Windows.Forms.TextBox ClientRequestsTextBox;
    }
}

