using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SteamBot
{
    public partial class SelectInvite : Form
    {
        public static string selected;
        public static bool selectContinue = false;

        public SelectInvite()
        {
            InitializeComponent();
        }

        private void button_inviteconfirm_Click(object sender, EventArgs e)
        {
            if (selected == "" || selected == null)
            {
                MessageBox.Show("You need to select a group!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                selectContinue = true;
                this.Close();
                this.Dispose();
            }
        }

        private void SelectInvite_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("lists", "gl_*")
            .Select(Path.GetFileNameWithoutExtension)
            .Select(p => p.Substring(3)).ToArray();
            for (int count = 0; count < files.Length; count++)
            {
                dropdown_invitelist.Items.Add(files[count]);
            }
            selectContinue = false;
        }

        private void dropdown_invitelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = dropdown_invitelist.SelectedItem.ToString();
        }
    }
}
