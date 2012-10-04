namespace Chess
{
    partial class InviteWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InviteWindow));
            this.OfflineGameButton = new System.Windows.Forms.Button();
            this.OnlineGameButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.layoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.IPBox = new System.Windows.Forms.MaskedTextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OfflineGameButton
            // 
            this.OfflineGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OfflineGameButton.AutoSize = true;
            this.OfflineGameButton.Location = new System.Drawing.Point(3, 3);
            this.OfflineGameButton.Name = "OfflineGameButton";
            this.OfflineGameButton.Size = new System.Drawing.Size(125, 25);
            this.OfflineGameButton.TabIndex = 0;
            this.OfflineGameButton.Text = "Start offline game";
            this.OfflineGameButton.UseVisualStyleBackColor = true;
            this.OfflineGameButton.Click += new System.EventHandler(this.OfflineGameButton_Click);
            // 
            // OnlineGameButton
            // 
            this.OnlineGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OnlineGameButton.AutoSize = true;
            this.OnlineGameButton.Location = new System.Drawing.Point(3, 34);
            this.OnlineGameButton.Name = "OnlineGameButton";
            this.OnlineGameButton.Size = new System.Drawing.Size(125, 25);
            this.OnlineGameButton.TabIndex = 1;
            this.OnlineGameButton.Text = "Start online game";
            this.OnlineGameButton.UseVisualStyleBackColor = true;
            this.OnlineGameButton.Click += new System.EventHandler(this.OnmineGameButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitButton.AutoSize = true;
            this.ExitButton.Location = new System.Drawing.Point(3, 65);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(125, 25);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // layoutPanel
            // 
            this.layoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutPanel.Controls.Add(this.OfflineGameButton);
            this.layoutPanel.Controls.Add(this.OnlineGameButton);
            this.layoutPanel.Controls.Add(this.ExitButton);
            this.layoutPanel.Controls.Add(this.label1);
            this.layoutPanel.Controls.Add(this.IPBox);
            this.layoutPanel.Controls.Add(this.ConnectButton);
            this.layoutPanel.Controls.Add(this.CancelButton);
            this.layoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.layoutPanel.Location = new System.Drawing.Point(12, 12);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.Size = new System.Drawing.Size(131, 112);
            this.layoutPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter IP adress:";
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(134, 3);
            this.IPBox.Mask = "###.###.###.###";
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(125, 20);
            this.IPBox.TabIndex = 4;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConnectButton.AutoSize = true;
            this.ConnectButton.Location = new System.Drawing.Point(134, 29);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(125, 25);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelButton.AutoSize = true;
            this.CancelButton.Location = new System.Drawing.Point(134, 60);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(125, 25);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Canсel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InviteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 136);
            this.Controls.Add(this.layoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InviteWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.InviteForm_Load);
            this.Shown += new System.EventHandler(this.Draw);
            this.Move += new System.EventHandler(this.Draw);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OfflineGameButton;
        private System.Windows.Forms.Button OnlineGameButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.FlowLayoutPanel layoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox IPBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CancelButton;
    }
}