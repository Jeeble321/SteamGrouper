namespace SteamBot
{
    partial class SteamGuard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamGuard));
            this.label_steamguard = new System.Windows.Forms.Label();
            this.text_steamguard = new System.Windows.Forms.TextBox();
            this.button_steamguard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_steamguard
            // 
            this.label_steamguard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_steamguard.AutoSize = true;
            this.label_steamguard.Location = new System.Drawing.Point(12, 10);
            this.label_steamguard.Name = "label_steamguard";
            this.label_steamguard.Size = new System.Drawing.Size(263, 26);
            this.label_steamguard.TabIndex = 0;
            this.label_steamguard.Text = "This account is protected by Steam Guard.\r\nEnter the authentication code sent to " +
    "the proper email:";
            this.label_steamguard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // text_steamguard
            // 
            this.text_steamguard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text_steamguard.Location = new System.Drawing.Point(98, 45);
            this.text_steamguard.MaxLength = 5;
            this.text_steamguard.Name = "text_steamguard";
            this.text_steamguard.Size = new System.Drawing.Size(90, 20);
            this.text_steamguard.TabIndex = 0;
            // 
            // button_steamguard
            // 
            this.button_steamguard.Location = new System.Drawing.Point(108, 72);
            this.button_steamguard.Name = "button_steamguard";
            this.button_steamguard.Size = new System.Drawing.Size(70, 34);
            this.button_steamguard.TabIndex = 1;
            this.button_steamguard.Text = "Confirm";
            this.button_steamguard.UseVisualStyleBackColor = true;
            this.button_steamguard.Click += new System.EventHandler(this.button_steamguard_Click);
            // 
            // SteamGuard
            // 
            this.AcceptButton = this.button_steamguard;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(286, 117);
            this.Controls.Add(this.button_steamguard);
            this.Controls.Add(this.text_steamguard);
            this.Controls.Add(this.label_steamguard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SteamGuard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Steam Guard";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_steamguard;
        private System.Windows.Forms.Button button_steamguard;
        private System.Windows.Forms.Label label_steamguard;
    }
}