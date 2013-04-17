namespace SteamBot
{
    partial class Interface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.button_gather = new System.Windows.Forms.Button();
            this.progress_gather = new System.Windows.Forms.ProgressBar();
            this.label_progress = new System.Windows.Forms.Label();
            this.progress_invite = new System.Windows.Forms.ProgressBar();
            this.button_invite = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteOfflineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gatherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCustomGatherlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.blacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockFullVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_info = new System.Windows.Forms.Label();
            this.label_targeting = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_username = new System.Windows.Forms.TextBox();
            this.text_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.text_gather = new System.Windows.Forms.TextBox();
            this.text_invite = new System.Windows.Forms.TextBox();
            this.text_log = new System.Windows.Forms.TextBox();
            this.label_invite = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.check_remember = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label_gather_est = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_gather
            // 
            this.button_gather.Location = new System.Drawing.Point(190, 116);
            this.button_gather.Name = "button_gather";
            this.button_gather.Size = new System.Drawing.Size(106, 20);
            this.button_gather.TabIndex = 5;
            this.button_gather.Text = "Gather";
            this.button_gather.UseVisualStyleBackColor = true;
            this.button_gather.Click += new System.EventHandler(this.button_gather_Click);
            // 
            // progress_gather
            // 
            this.progress_gather.Location = new System.Drawing.Point(12, 147);
            this.progress_gather.Name = "progress_gather";
            this.progress_gather.Size = new System.Drawing.Size(284, 26);
            this.progress_gather.TabIndex = 3;
            // 
            // label_progress
            // 
            this.label_progress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(9, 176);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(55, 13);
            this.label_progress.TabIndex = 4;
            this.label_progress.Text = "State: Idle";
            this.label_progress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progress_invite
            // 
            this.progress_invite.Location = new System.Drawing.Point(323, 147);
            this.progress_invite.Name = "progress_invite";
            this.progress_invite.Size = new System.Drawing.Size(278, 26);
            this.progress_invite.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress_invite.TabIndex = 7;
            // 
            // button_invite
            // 
            this.button_invite.Location = new System.Drawing.Point(495, 116);
            this.button_invite.Name = "button_invite";
            this.button_invite.Size = new System.Drawing.Size(106, 20);
            this.button_invite.TabIndex = 7;
            this.button_invite.Text = "Invite";
            this.button_invite.UseVisualStyleBackColor = true;
            this.button_invite.Click += new System.EventHandler(this.button_invite_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.listsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inviteSpeedToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // inviteSpeedToolStripMenuItem
            // 
            this.inviteSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speedToolStripMenuItem,
            this.inviteOfflineToolStripMenuItem});
            this.inviteSpeedToolStripMenuItem.Name = "inviteSpeedToolStripMenuItem";
            this.inviteSpeedToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.inviteSpeedToolStripMenuItem.Text = "Invite";
            this.inviteSpeedToolStripMenuItem.Click += new System.EventHandler(this.inviteSpeedToolStripMenuItem_Click);
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.speedToolStripMenuItem.Text = "Speed";
            this.speedToolStripMenuItem.Click += new System.EventHandler(this.speedToolStripMenuItem_Click);
            // 
            // inviteOfflineToolStripMenuItem
            // 
            this.inviteOfflineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offlineUsersToolStripMenuItem});
            this.inviteOfflineToolStripMenuItem.Name = "inviteOfflineToolStripMenuItem";
            this.inviteOfflineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.inviteOfflineToolStripMenuItem.Text = "Targeting";
            // 
            // offlineUsersToolStripMenuItem
            // 
            this.offlineUsersToolStripMenuItem.CheckOnClick = true;
            this.offlineUsersToolStripMenuItem.Name = "offlineUsersToolStripMenuItem";
            this.offlineUsersToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.offlineUsersToolStripMenuItem.Text = "Offline Users";
            this.offlineUsersToolStripMenuItem.Click += new System.EventHandler(this.offlineUsersToolStripMenuItem_Click);
            // 
            // listsToolStripMenuItem
            // 
            this.listsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gatherToolStripMenuItem,
            this.blacklistToolStripMenuItem});
            this.listsToolStripMenuItem.Name = "listsToolStripMenuItem";
            this.listsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.listsToolStripMenuItem.Text = "Lists";
            // 
            // gatherToolStripMenuItem
            // 
            this.gatherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCustomGatherlistToolStripMenuItem,
            this.clearAllToolStripMenuItem1});
            this.gatherToolStripMenuItem.Name = "gatherToolStripMenuItem";
            this.gatherToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gatherToolStripMenuItem.Text = "Gatherlist";
            // 
            // createCustomGatherlistToolStripMenuItem
            // 
            this.createCustomGatherlistToolStripMenuItem.Name = "createCustomGatherlistToolStripMenuItem";
            this.createCustomGatherlistToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createCustomGatherlistToolStripMenuItem.Text = "Create Custom Gatherlist";
            this.createCustomGatherlistToolStripMenuItem.Click += new System.EventHandler(this.createCustomGatherlistToolStripMenuItem_Click);
            // 
            // clearAllToolStripMenuItem1
            // 
            this.clearAllToolStripMenuItem1.Name = "clearAllToolStripMenuItem1";
            this.clearAllToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.clearAllToolStripMenuItem1.Text = "Clear All";
            this.clearAllToolStripMenuItem1.Click += new System.EventHandler(this.clearAllToolStripMenuItem1_Click);
            // 
            // blacklistToolStripMenuItem
            // 
            this.blacklistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem});
            this.blacklistToolStripMenuItem.Name = "blacklistToolStripMenuItem";
            this.blacklistToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blacklistToolStripMenuItem.Text = "Blacklist";
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unlockFullVersionToolStripMenuItem,
            this.creditsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // unlockFullVersionToolStripMenuItem
            // 
            this.unlockFullVersionToolStripMenuItem.Name = "unlockFullVersionToolStripMenuItem";
            this.unlockFullVersionToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.unlockFullVersionToolStripMenuItem.Text = "Unlock Full Version";
            this.unlockFullVersionToolStripMenuItem.Click += new System.EventHandler(this.unlockFullVersionToolStripMenuItem_Click);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(320, 176);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(70, 13);
            this.label_info.TabIndex = 10;
            this.label_info.Text = "Invite Speed:";
            this.label_info.Click += new System.EventHandler(this.label_info_Click);
            // 
            // label_targeting
            // 
            this.label_targeting.Location = new System.Drawing.Point(459, 176);
            this.label_targeting.Name = "label_targeting";
            this.label_targeting.Size = new System.Drawing.Size(142, 13);
            this.label_targeting.TabIndex = 11;
            this.label_targeting.Text = "Invite offline users:";
            this.label_targeting.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Username:";
            // 
            // text_username
            // 
            this.text_username.Location = new System.Drawing.Point(74, 34);
            this.text_username.Name = "text_username";
            this.text_username.Size = new System.Drawing.Size(93, 20);
            this.text_username.TabIndex = 0;
            this.text_username.TextChanged += new System.EventHandler(this.text_username_Changed);
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(74, 60);
            this.text_password.Name = "text_password";
            this.text_password.Size = new System.Drawing.Size(93, 20);
            this.text_password.TabIndex = 1;
            this.text_password.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password:";
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(173, 34);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(123, 46);
            this.button_login.TabIndex = 3;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // text_gather
            // 
            this.text_gather.Location = new System.Drawing.Point(12, 116);
            this.text_gather.Name = "text_gather";
            this.text_gather.Size = new System.Drawing.Size(172, 20);
            this.text_gather.TabIndex = 4;
            this.text_gather.Text = "Group to gather from";
            this.text_gather.TextChanged += new System.EventHandler(this.text_gather_TextChanged_1);
            // 
            // text_invite
            // 
            this.text_invite.ForeColor = System.Drawing.SystemColors.WindowText;
            this.text_invite.Location = new System.Drawing.Point(323, 116);
            this.text_invite.Name = "text_invite";
            this.text_invite.Size = new System.Drawing.Size(166, 20);
            this.text_invite.TabIndex = 6;
            this.text_invite.Text = "Group to invite members to";
            this.text_invite.TextChanged += new System.EventHandler(this.text_invite_TextChanged);
            // 
            // text_log
            // 
            this.text_log.BackColor = System.Drawing.SystemColors.Window;
            this.text_log.HideSelection = false;
            this.text_log.Location = new System.Drawing.Point(12, 218);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(589, 218);
            this.text_log.TabIndex = 19;
            this.text_log.TabStop = false;
            // 
            // label_invite
            // 
            this.label_invite.AutoSize = true;
            this.label_invite.BackColor = System.Drawing.Color.Transparent;
            this.label_invite.Location = new System.Drawing.Point(320, 194);
            this.label_invite.Name = "label_invite";
            this.label_invite.Size = new System.Drawing.Size(55, 13);
            this.label_invite.TabIndex = 20;
            this.label_invite.Text = "State: Idle";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SteamGrouper.Properties.Resources.banner1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(323, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 76);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // check_remember
            // 
            this.check_remember.AutoSize = true;
            this.check_remember.Location = new System.Drawing.Point(74, 86);
            this.check_remember.Name = "check_remember";
            this.check_remember.Size = new System.Drawing.Size(95, 17);
            this.check_remember.TabIndex = 2;
            this.check_remember.Text = "Remember Me";
            this.check_remember.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label_gather_est
            // 
            this.label_gather_est.AutoSize = true;
            this.label_gather_est.Location = new System.Drawing.Point(9, 194);
            this.label_gather_est.Name = "label_gather_est";
            this.label_gather_est.Size = new System.Drawing.Size(126, 13);
            this.label_gather_est.TabIndex = 22;
            this.label_gather_est.Text = "Estimated time remaining:";
            // 
            // Interface
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(613, 448);
            this.Controls.Add(this.label_gather_est);
            this.Controls.Add(this.check_remember);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_invite);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.text_invite);
            this.Controls.Add(this.text_gather);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_username);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_targeting);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.progress_invite);
            this.Controls.Add(this.button_invite);
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.progress_gather);
            this.Controls.Add(this.button_gather);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Interface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SteamGrouper";
            this.Load += new System.EventHandler(this.Interface_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_gather;
        private System.Windows.Forms.ProgressBar progress_gather;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.ProgressBar progress_invite;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inviteSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inviteOfflineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockFullVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        public System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Label label_targeting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button_invite;
        public System.Windows.Forms.TextBox text_invite;
        public System.Windows.Forms.TextBox text_gather;
        public System.Windows.Forms.Label label_invite;
        public System.Windows.Forms.TextBox text_log;
        public System.Windows.Forms.TextBox text_username;
        public System.Windows.Forms.TextBox text_password;
        public System.Windows.Forms.Button button_login;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem listsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gatherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem blacklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.CheckBox check_remember;
        private System.Windows.Forms.ToolStripMenuItem createCustomGatherlistToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label_gather_est;
    }
}