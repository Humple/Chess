using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class InviteWindow : Form
    {
        private string rv = null;
        public string returnValue { get { return rv; } }
        private string ip = null;
        public string IP { get { return ip; } }


        public InviteWindow()
        {
            InitializeComponent();
            label1.Visible = false;
            IPBox.Visible = false;
            ConnectButton.Visible = false;
            CancelButton.Visible = false;
            StartServerButton.Visible = false;
            StartClientButton.Visible = false;
        }

        private void InviteForm_Load(object sender, EventArgs e)
        {

        }

        private void OfflineGameButton_Click(object sender, EventArgs e)
        {
            rv = "Offline";
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ip = IPBox.Text;
            ip.Replace(',', '.');
            ip.Replace(" ", "");
            // вызов кода подключения;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            IPBox.Visible = false;
            ConnectButton.Visible = false;
            CancelButton.Visible = false;

            OfflineGameButton.Visible = true;
            OnlineGameButton.Visible = true;
            ExitButton.Visible = true;
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            // создание сервера
        }

        private void StartClientButton_Click(object sender, EventArgs e)
        {
            StartServerButton.Visible = false;
            StartClientButton.Visible = false;

            label1.Visible = true;
            IPBox.Visible = true;
            ConnectButton.Visible = true;
            CancelButton.Visible = true;
        }

        private void OnlineGameButton_Click(object sender, EventArgs e)
        {
            rv = "Online";

            OfflineGameButton.Visible = false;
            OnlineGameButton.Visible = false;
            ExitButton.Visible = false;

            StartServerButton.Visible = true;
            StartClientButton.Visible = true;
        }
    }
}
