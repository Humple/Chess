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
            this.OnmineGameButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OfflineGameButton
            // 
            this.OfflineGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OfflineGameButton.Location = new System.Drawing.Point(30, 28);
            this.OfflineGameButton.Name = "OfflineGameButton";
            this.OfflineGameButton.Size = new System.Drawing.Size(114, 23);
            this.OfflineGameButton.TabIndex = 0;
            this.OfflineGameButton.Text = "Start offline game";
            this.OfflineGameButton.UseVisualStyleBackColor = true;
            this.OfflineGameButton.Click += new System.EventHandler(this.OfflineGameButton_Click);
            // 
            // OnmineGameButton
            // 
            this.OnmineGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OnmineGameButton.Location = new System.Drawing.Point(30, 57);
            this.OnmineGameButton.Name = "OnmineGameButton";
            this.OnmineGameButton.Size = new System.Drawing.Size(114, 23);
            this.OnmineGameButton.TabIndex = 1;
            this.OnmineGameButton.Text = "Start online game";
            this.OnmineGameButton.UseVisualStyleBackColor = true;
            this.OnmineGameButton.Click += new System.EventHandler(this.OnmineGameButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.Location = new System.Drawing.Point(30, 86);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(114, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // InviteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 135);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.OnmineGameButton);
            this.Controls.Add(this.OfflineGameButton);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OfflineGameButton;
        private System.Windows.Forms.Button OnmineGameButton;
        private System.Windows.Forms.Button ExitButton;
    }
}