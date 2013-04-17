using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SteamBot
{
    public partial class SteamGuard : Form
    {
        public static string authCode;

        public SteamGuard()
        {
            InitializeComponent();
            authCode = "";
        }

        public void UpdateLabel(string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (label_steamguard.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                label_steamguard.Invoke((Action)(() => UpdateLabel(text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            label_steamguard.Text = text;
        }

        private void button_steamguard_Click(object sender, EventArgs e)
        {
            if (text_steamguard.Text == null || text_steamguard.Text == "")
            {
                MessageBox.Show("The field cannot be empty!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                authCode = text_steamguard.Text;
                this.Close();
                this.Dispose();
            }
        }
    }
}
