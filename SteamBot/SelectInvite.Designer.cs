namespace SteamBot
{
    partial class SelectInvite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectInvite));
            this.button_inviteconfirm = new System.Windows.Forms.Button();
            this.dropdown_invitelist = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button_inviteconfirm
            // 
            this.button_inviteconfirm.Location = new System.Drawing.Point(40, 37);
            this.button_inviteconfirm.Name = "button_inviteconfirm";
            this.button_inviteconfirm.Size = new System.Drawing.Size(84, 36);
            this.button_inviteconfirm.TabIndex = 1;
            this.button_inviteconfirm.Text = "Confirm";
            this.button_inviteconfirm.UseVisualStyleBackColor = true;
            this.button_inviteconfirm.Click += new System.EventHandler(this.button_inviteconfirm_Click);
            // 
            // dropdown_invitelist
            // 
            this.dropdown_invitelist.FormattingEnabled = true;
            this.dropdown_invitelist.Location = new System.Drawing.Point(12, 10);
            this.dropdown_invitelist.Name = "dropdown_invitelist";
            this.dropdown_invitelist.Size = new System.Drawing.Size(143, 21);
            this.dropdown_invitelist.TabIndex = 0;
            this.dropdown_invitelist.Text = "Select a Group";
            this.dropdown_invitelist.SelectedIndexChanged += new System.EventHandler(this.dropdown_invitelist_SelectedIndexChanged);
            // 
            // SelectInvite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(166, 83);
            this.Controls.Add(this.dropdown_invitelist);
            this.Controls.Add(this.button_inviteconfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectInvite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Group";
            this.Load += new System.EventHandler(this.SelectInvite_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_inviteconfirm;
        public System.Windows.Forms.ComboBox dropdown_invitelist;

    }
}