namespace dzagar_SE3314_Assignment2
{
    partial class View
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
            this.PortNumberTextBox = new System.Windows.Forms.TextBox();
            this.PortNumberLabel = new System.Windows.Forms.Label();
            this.ServerIPAddressLabel = new System.Windows.Forms.Label();
            this.ServerIPAddressTextBox = new System.Windows.Forms.TextBox();
            this.VideoNameLabel = new System.Windows.Forms.Label();
            this.VideoImageBox = new System.Windows.Forms.PictureBox();
            this.SetupButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.TeardownButton = new System.Windows.Forms.Button();
            this.ClientActivityTextBox = new System.Windows.Forms.TextBox();
            this.ServerActivityTextBox = new System.Windows.Forms.TextBox();
            this.ServerActivityLabel = new System.Windows.Forms.Label();
            this.PacketReportCheckBox = new System.Windows.Forms.CheckBox();
            this.PrintHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.VideoNameComboBox = new System.Windows.Forms.ComboBox();
            this.VideoGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.VideoImageBox)).BeginInit();
            this.VideoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PortNumberTextBox
            // 
            this.PortNumberTextBox.Location = new System.Drawing.Point(179, 539);
            this.PortNumberTextBox.Name = "PortNumberTextBox";
            this.PortNumberTextBox.Size = new System.Drawing.Size(100, 31);
            this.PortNumberTextBox.TabIndex = 0;
            this.PortNumberTextBox.Text = "3000";
            // 
            // PortNumberLabel
            // 
            this.PortNumberLabel.AutoSize = true;
            this.PortNumberLabel.Location = new System.Drawing.Point(12, 542);
            this.PortNumberLabel.Name = "PortNumberLabel";
            this.PortNumberLabel.Size = new System.Drawing.Size(167, 25);
            this.PortNumberLabel.TabIndex = 1;
            this.PortNumberLabel.Text = "Connect to Port:";
            // 
            // ServerIPAddressLabel
            // 
            this.ServerIPAddressLabel.AutoSize = true;
            this.ServerIPAddressLabel.Location = new System.Drawing.Point(283, 542);
            this.ServerIPAddressLabel.Name = "ServerIPAddressLabel";
            this.ServerIPAddressLabel.Size = new System.Drawing.Size(189, 25);
            this.ServerIPAddressLabel.TabIndex = 2;
            this.ServerIPAddressLabel.Text = "Server IP address:";
            // 
            // ServerIPAddressTextBox
            // 
            this.ServerIPAddressTextBox.Location = new System.Drawing.Point(469, 539);
            this.ServerIPAddressTextBox.Name = "ServerIPAddressTextBox";
            this.ServerIPAddressTextBox.Size = new System.Drawing.Size(100, 31);
            this.ServerIPAddressTextBox.TabIndex = 3;
            this.ServerIPAddressTextBox.Text = "127.0.0.1";
            // 
            // VideoNameLabel
            // 
            this.VideoNameLabel.AutoSize = true;
            this.VideoNameLabel.Location = new System.Drawing.Point(575, 542);
            this.VideoNameLabel.Name = "VideoNameLabel";
            this.VideoNameLabel.Size = new System.Drawing.Size(132, 25);
            this.VideoNameLabel.TabIndex = 4;
            this.VideoNameLabel.Text = "Video name:";
            // 
            // VideoImageBox
            // 
            this.VideoImageBox.BackColor = System.Drawing.Color.Black;
            this.VideoImageBox.Location = new System.Drawing.Point(6, 0);
            this.VideoImageBox.Name = "VideoImageBox";
            this.VideoImageBox.Size = new System.Drawing.Size(668, 368);
            this.VideoImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoImageBox.TabIndex = 6;
            this.VideoImageBox.TabStop = false;
            // 
            // SetupButton
            // 
            this.SetupButton.Location = new System.Drawing.Point(6, 374);
            this.SetupButton.Name = "SetupButton";
            this.SetupButton.Size = new System.Drawing.Size(148, 101);
            this.SetupButton.TabIndex = 7;
            this.SetupButton.Text = "Setup";
            this.SetupButton.UseVisualStyleBackColor = true;
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(170, 374);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(148, 101);
            this.PlayButton.TabIndex = 8;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(340, 374);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(148, 101);
            this.PauseButton.TabIndex = 9;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            // 
            // TeardownButton
            // 
            this.TeardownButton.Location = new System.Drawing.Point(526, 374);
            this.TeardownButton.Name = "TeardownButton";
            this.TeardownButton.Size = new System.Drawing.Size(148, 101);
            this.TeardownButton.TabIndex = 10;
            this.TeardownButton.Text = "Teardown";
            this.TeardownButton.UseVisualStyleBackColor = true;
            // 
            // ClientActivityTextBox
            // 
            this.ClientActivityTextBox.Location = new System.Drawing.Point(713, 12);
            this.ClientActivityTextBox.Multiline = true;
            this.ClientActivityTextBox.Name = "ClientActivityTextBox";
            this.ClientActivityTextBox.ReadOnly = true;
            this.ClientActivityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ClientActivityTextBox.Size = new System.Drawing.Size(804, 235);
            this.ClientActivityTextBox.TabIndex = 11;
            // 
            // ServerActivityTextBox
            // 
            this.ServerActivityTextBox.Location = new System.Drawing.Point(713, 299);
            this.ServerActivityTextBox.Multiline = true;
            this.ServerActivityTextBox.Name = "ServerActivityTextBox";
            this.ServerActivityTextBox.ReadOnly = true;
            this.ServerActivityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServerActivityTextBox.Size = new System.Drawing.Size(520, 209);
            this.ServerActivityTextBox.TabIndex = 12;
            // 
            // ServerActivityLabel
            // 
            this.ServerActivityLabel.AutoSize = true;
            this.ServerActivityLabel.Location = new System.Drawing.Point(708, 265);
            this.ServerActivityLabel.Name = "ServerActivityLabel";
            this.ServerActivityLabel.Size = new System.Drawing.Size(195, 25);
            this.ServerActivityLabel.TabIndex = 13;
            this.ServerActivityLabel.Text = "Server Responses:";
            // 
            // PacketReportCheckBox
            // 
            this.PacketReportCheckBox.AutoSize = true;
            this.PacketReportCheckBox.Location = new System.Drawing.Point(1262, 312);
            this.PacketReportCheckBox.Name = "PacketReportCheckBox";
            this.PacketReportCheckBox.Size = new System.Drawing.Size(180, 29);
            this.PacketReportCheckBox.TabIndex = 14;
            this.PacketReportCheckBox.Text = "Packet Report";
            this.PacketReportCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrintHeaderCheckBox
            // 
            this.PrintHeaderCheckBox.AutoSize = true;
            this.PrintHeaderCheckBox.Location = new System.Drawing.Point(1262, 370);
            this.PrintHeaderCheckBox.Name = "PrintHeaderCheckBox";
            this.PrintHeaderCheckBox.Size = new System.Drawing.Size(164, 29);
            this.PrintHeaderCheckBox.TabIndex = 15;
            this.PrintHeaderCheckBox.Text = "Print Header";
            this.PrintHeaderCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(905, 528);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(151, 53);
            this.ConnectButton.TabIndex = 16;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(1303, 528);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(151, 53);
            this.ExitButton.TabIndex = 17;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // VideoNameComboBox
            // 
            this.VideoNameComboBox.AllowDrop = true;
            this.VideoNameComboBox.FormattingEnabled = true;
            this.VideoNameComboBox.Items.AddRange(new object[] {
            "video1.mjpeg",
            "video2.mjpeg"});
            this.VideoNameComboBox.Location = new System.Drawing.Point(713, 539);
            this.VideoNameComboBox.Name = "VideoNameComboBox";
            this.VideoNameComboBox.Size = new System.Drawing.Size(173, 33);
            this.VideoNameComboBox.TabIndex = 18;
            this.VideoNameComboBox.Text = "video1.mjpeg";
            // 
            // VideoGroupBox
            // 
            this.VideoGroupBox.AutoSize = true;
            this.VideoGroupBox.Controls.Add(this.VideoImageBox);
            this.VideoGroupBox.Controls.Add(this.SetupButton);
            this.VideoGroupBox.Controls.Add(this.PlayButton);
            this.VideoGroupBox.Controls.Add(this.PauseButton);
            this.VideoGroupBox.Controls.Add(this.TeardownButton);
            this.VideoGroupBox.Enabled = false;
            this.VideoGroupBox.Location = new System.Drawing.Point(19, 24);
            this.VideoGroupBox.Name = "VideoGroupBox";
            this.VideoGroupBox.Size = new System.Drawing.Size(688, 505);
            this.VideoGroupBox.TabIndex = 19;
            this.VideoGroupBox.TabStop = false;
            this.VideoGroupBox.Visible = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1536, 610);
            this.Controls.Add(this.VideoGroupBox);
            this.Controls.Add(this.VideoNameComboBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.PrintHeaderCheckBox);
            this.Controls.Add(this.PacketReportCheckBox);
            this.Controls.Add(this.ServerActivityLabel);
            this.Controls.Add(this.ServerActivityTextBox);
            this.Controls.Add(this.ClientActivityTextBox);
            this.Controls.Add(this.VideoNameLabel);
            this.Controls.Add(this.ServerIPAddressTextBox);
            this.Controls.Add(this.ServerIPAddressLabel);
            this.Controls.Add(this.PortNumberLabel);
            this.Controls.Add(this.PortNumberTextBox);
            this.Name = "View";
            this.Text = "Streaming Video Client";
            ((System.ComponentModel.ISupportInitialize)(this.VideoImageBox)).EndInit();
            this.VideoGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PortNumberTextBox;
        private System.Windows.Forms.Label PortNumberLabel;
        private System.Windows.Forms.Label ServerIPAddressLabel;
        private System.Windows.Forms.TextBox ServerIPAddressTextBox;
        private System.Windows.Forms.Label VideoNameLabel;
        private System.Windows.Forms.PictureBox VideoImageBox;
        private System.Windows.Forms.Button SetupButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button TeardownButton;
        private System.Windows.Forms.TextBox ClientActivityTextBox;
        private System.Windows.Forms.TextBox ServerActivityTextBox;
        private System.Windows.Forms.Label ServerActivityLabel;
        private System.Windows.Forms.CheckBox PacketReportCheckBox;
        private System.Windows.Forms.CheckBox PrintHeaderCheckBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox VideoNameComboBox;
        private System.Windows.Forms.GroupBox VideoGroupBox;
    }
}

