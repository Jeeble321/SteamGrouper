namespace SteamGrouper
{
    partial class CustomGatherlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomGatherlist));
            this.text_list = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.text_filename = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // text_list
            // 
            this.text_list.Location = new System.Drawing.Point(12, 27);
            this.text_list.MaxLength = 999999;
            this.text_list.Multiline = true;
            this.text_list.Name = "text_list";
            this.text_list.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_list.Size = new System.Drawing.Size(284, 348);
            this.text_list.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paste a list of SteamID64s here, one per line.";
            // 
            // text_filename
            // 
            this.text_filename.Location = new System.Drawing.Point(75, 381);
            this.text_filename.Name = "text_filename";
            this.text_filename.Size = new System.Drawing.Size(141, 20);
            this.text_filename.TabIndex = 2;
            this.text_filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(222, 379);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(74, 23);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "List Name:";
            // 
            // CustomGatherlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 407);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.text_filename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_list);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomGatherlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Custom Gatherlist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_filename;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label2;
    }
}