using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SteamGrouper
{
    public partial class CustomGatherlist : Form
    {
        public CustomGatherlist()
        {
            InitializeComponent();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (text_list.Text == "")
            {
                MessageBox.Show("The list cannot be blank!", "Error: Empty List", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (!IsDigitsOnly(text_list.Text))
            {
                MessageBox.Show("The list can only contain numbers.", "Error: Unallowed Characters", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (text_filename.Text == "")
            {
                MessageBox.Show("The file name cannot be blank!", "Error: Blank File Name", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            string fileName = text_filename.Text;
            if (fileName != "")
            {
                char[] arr = fileName.ToCharArray();
                arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c))));
                fileName = new string(arr);
            }
            string listDir = Path.Combine(Application.StartupPath, "lists");
            string file = Path.Combine(listDir, "gl_" + fileName);
            if (File.Exists(file))
            {
                DialogResult overwrite = MessageBox.Show("The file name you have entered already exists. Overwrite?", "Warning: File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);//show messagebox
                if (overwrite == DialogResult.Yes)
                {
                    File.WriteAllText(file, text_list.Text);
                }
                else if (overwrite == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                File.WriteAllText(file, text_list.Text);
            }
            MessageBox.Show("The new gatherlist has been successfully created.", "New Gatherlist Created", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            this.Close();
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    if ((int) c != 13)
                    {
                        if ((int) c != 10)
                        {
                            Console.WriteLine((int)c);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
