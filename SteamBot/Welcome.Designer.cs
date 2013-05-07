namespace SteamBot
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.check_remember = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Purchase = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_trial = new System.Windows.Forms.Button();
            this.button_login = new System.Windows.Forms.Button();
            this.text_password = new System.Windows.Forms.TextBox();
            this.text_username = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // check_remember
            // 
            this.check_remember.AutoSize = true;
            this.check_remember.Location = new System.Drawing.Point(13, 138);
            this.check_remember.Name = "check_remember";
            this.check_remember.Size = new System.Drawing.Size(95, 17);
            this.check_remember.TabIndex = 15;
            this.check_remember.Text = "Remember Me";
            this.check_remember.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(10, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 52);
            this.label1.TabIndex = 14;
            this.label1.Text = "To unlock the full version for uninterrupted and \r\nunlimited inviting/gathering, " +
    "purchase a subscription today!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Purchase
            // 
            this.Purchase.Location = new System.Drawing.Point(289, 158);
            this.Purchase.Name = "Purchase";
            this.Purchase.Size = new System.Drawing.Size(124, 52);
            this.Purchase.TabIndex = 13;
            this.Purchase.Text = "Purchase a Subscription";
            this.Purchase.UseVisualStyleBackColor = true;
            this.Purchase.Click += new System.EventHandler(this.Purchase_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SteamGrouper.Properties.Resources.banner1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 67);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // button_trial
            // 
            this.button_trial.Location = new System.Drawing.Point(289, 86);
            this.button_trial.Name = "button_trial";
            this.button_trial.Size = new System.Drawing.Size(124, 46);
            this.button_trial.TabIndex = 11;
            this.button_trial.Text = "Trial";
            this.button_trial.UseVisualStyleBackColor = true;
            this.button_trial.Click += new System.EventHandler(this.button_trial_Click);
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(173, 86);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(110, 45);
            this.button_login.TabIndex = 10;
            this.button_login.Text = "Login to\r\nSteamGrouper";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(13, 112);
            this.text_password.Name = "text_password";
            this.text_password.Size = new System.Drawing.Size(154, 20);
            this.text_password.TabIndex = 9;
            this.text_password.Text = "Password";
            this.text_password.UseSystemPasswordChar = true;
            // 
            // text_username
            // 
            this.text_username.Location = new System.Drawing.Point(13, 86);
            this.text_username.Name = "text_username";
            this.text_username.Size = new System.Drawing.Size(154, 20);
            this.text_username.TabIndex = 8;
            this.text_username.Text = "Username";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 214);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(137, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Need help? Bugs to report?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(423, 239);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.check_remember);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Purchase);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_trial);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Welcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to SteamGrouper";
            this.Load += new System.EventHandler(this.Welcome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox check_remember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Purchase;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_trial;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.TextBox text_password;
        public System.Windows.Forms.TextBox text_username;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}