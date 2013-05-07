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
    public partial class InviteSpeed : Form
    {
        public InviteSpeed()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            speedMin.Value = 0;
            speedMax.Value = 0;
        }

        private void InviteSpeed_Load(object sender, EventArgs e)
        {
            formInviteSpeed.Value = Interface.inviteSpeed;
            speedMin.Value = Interface.inviteSpeedMin;
            speedMax.Value = Interface.inviteSpeedMax;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interface mainForm = (Interface)this.Owner;
            if (speedMin.Value < 18 || speedMax.Value < 18 || formInviteSpeed.Value < 18)
            {
                DialogResult dr = MessageBox.Show("You have chosen an invite speed of less than 18 seconds.\r\nThis is not recommended because Steam limits invites to 250 per hour, per community.\r\nClick Okay if you wish to continue anyway, or click Cancel to set a different speed.", "Invite Time Under 18 Seconds", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            if (speedMin.Value == 0 && speedMax.Value == 0 && formInviteSpeed.Value != 0)
            {
                Interface.inviteSpeed = Convert.ToInt32(formInviteSpeed.Value);                
                Interface.inviteSpeedMin = Convert.ToInt32(speedMin.Value);
                Interface.inviteSpeedMax = Convert.ToInt32(speedMax.Value);
                Interface.MinMax = false;
                mainForm.label_info.Text = "Invite Speed: " + Interface.inviteSpeed + "s";
            }
            else if (speedMin.Value != 0 && speedMax.Value != 0 && formInviteSpeed.Value == 0)
            {
                if (speedMin.Value > speedMax.Value)
                {
                    int tempMin = Convert.ToInt32(speedMax.Value);
                    int tempMax = Convert.ToInt32(speedMin.Value);
                    Interface.inviteSpeedMin = tempMin;
                    Interface.inviteSpeedMax = tempMax;
                }
                else
                {
                    Interface.inviteSpeedMin = Convert.ToInt32(speedMin.Value);
                    Interface.inviteSpeedMax = Convert.ToInt32(speedMax.Value);
                }
                Interface.inviteSpeed = Convert.ToInt32(formInviteSpeed.Value);
                Interface.MinMax = true;
                mainForm.label_info.Text = "Invite Speed: " + Interface.inviteSpeedMin + "s to " + Interface.inviteSpeedMax + "s";
            }
            else
            {
                Interface.inviteSpeed = Convert.ToInt32(formInviteSpeed.Value);
                Interface.inviteSpeedMin = 0;
                Interface.inviteSpeedMax = 0;
                Interface.MinMax = false;
                mainForm.label_info.Text = "Invite Speed: " + Interface.inviteSpeed + "s";
            }
            if (((speedMin.Value != 0 || speedMax.Value != 0) && (speedMax.Value == 0 || speedMin.Value == 0)) || (formInviteSpeed.Value == 0 && speedMin.Value == 0 && speedMin.Value == 0))
            {
                MessageBox.Show("You cannot set 0 seconds as an invite speed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                Interface.inviteSpeed = Convert.ToInt32(formInviteSpeed.Value);
                Interface.inviteSpeedMin = 0;
                Interface.inviteSpeedMax = 0;
                Interface.MinMax = false;
                mainForm.label_info.Text = "Invite Speed: " + Interface.inviteSpeed + "s";
            }
            SteamGrouper.Properties.Settings.Default.invite_minmax = Interface.MinMax;
            SteamGrouper.Properties.Settings.Default.speed_max = Interface.inviteSpeedMax;
            SteamGrouper.Properties.Settings.Default.speed_min = Interface.inviteSpeedMin;
            SteamGrouper.Properties.Settings.Default.speed_normal = Interface.inviteSpeed;
            SteamGrouper.Properties.Settings.Default.Save();
            this.Close();
            this.Dispose();
        }

        private void speedMin_ValueChanged(object sender, EventArgs e)
        {
            formInviteSpeed.Value = 0;
        }

        private void speedMax_ValueChanged(object sender, EventArgs e)
        {
            formInviteSpeed.Value = 0;
        }
    }
}
