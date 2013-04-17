namespace SteamBot
{
    partial class InviteSpeed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InviteSpeed));
            this.formInviteSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.speedMin = new System.Windows.Forms.NumericUpDown();
            this.speedMax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.formInviteSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedMax)).BeginInit();
            this.SuspendLayout();
            // 
            // formInviteSpeed
            // 
            this.formInviteSpeed.Location = new System.Drawing.Point(87, 28);
            this.formInviteSpeed.Name = "formInviteSpeed";
            this.formInviteSpeed.Size = new System.Drawing.Size(62, 20);
            this.formInviteSpeed.TabIndex = 0;
            this.formInviteSpeed.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Set new invite speed (in seconds):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(76, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "or";
            // 
            // speedMin
            // 
            this.speedMin.Location = new System.Drawing.Point(39, 68);
            this.speedMin.Name = "speedMin";
            this.speedMin.Size = new System.Drawing.Size(62, 20);
            this.speedMin.TabIndex = 5;
            this.speedMin.ValueChanged += new System.EventHandler(this.speedMin_ValueChanged);
            // 
            // speedMax
            // 
            this.speedMax.Location = new System.Drawing.Point(129, 68);
            this.speedMax.Name = "speedMax";
            this.speedMax.Size = new System.Drawing.Size(62, 20);
            this.speedMax.TabIndex = 6;
            this.speedMax.ValueChanged += new System.EventHandler(this.speedMax_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "to";
            // 
            // InviteSpeed
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(244, 141);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.speedMax);
            this.Controls.Add(this.speedMin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formInviteSpeed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InviteSpeed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InviteSpeed";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InviteSpeed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.formInviteSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown formInviteSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown speedMin;
        private System.Windows.Forms.NumericUpDown speedMax;
        private System.Windows.Forms.Label label3;
    }
}