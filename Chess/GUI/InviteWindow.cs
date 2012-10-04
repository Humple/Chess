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
        public delegate void OnChoiceEventHandler(object sender, OnChoiceEventArgs e);
        public event OnChoiceEventHandler OnChoice;

        private string rv = null;
        public string returnValue { get { return rv; } }
        private string ip = null;


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

        private void OfflineGameButton_Click(object sender, EventArgs e)
        {
            if (OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.OFFLINE, null));
            rv = "Offline";
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.EXIT, null));
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ip = IPBox.Text;
            ip.Replace(',', '.');
            ip.Replace(" ", "");
            if (OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.CLIENT, ip));
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
            if (OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.SERVER, null));
            Close();
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
            OfflineGameButton.Visible = false;
            OnlineGameButton.Visible = false;
            ExitButton.Visible = false;

            StartServerButton.Visible = true;
            StartClientButton.Visible = true;
        }
    }

    public class OnChoiceEventArgs: Object
    {
        public enum ConnectionType { SERVER, CLIENT, OFFLINE, EXIT };
        private ConnectionType type;
        public ConnectionType Type { get { return type; } }
        private string ip = null;
        public string IP { get { return ip; } }
        public OnChoiceEventArgs(ConnectionType connectionType, string InternetProtocol)
        {
            ip = InternetProtocol;
            type = connectionType;
        }
    }
}
