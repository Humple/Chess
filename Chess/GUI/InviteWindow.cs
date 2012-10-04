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
        private Bitmap pict = null;
        private delegate void DrawImgageDelegate(Image ing, int x, int y);
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
        }

        private void InviteForm_Load(object sender, EventArgs e)
        {

        }

        private void Draw(object sender, EventArgs e)
        {
            if (pict == null)
            {
                try
                {
                    pict = new Bitmap("images/figures/white_knight.png");
                }
                catch (System.Exception)
                {
                    System.Windows.Forms.MessageBox.Show("File " + "images/figures/white_knight.png".ToUpper() + " not found. Please, put it to the directory of executable file.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    pict = new Bitmap(1, 1);
                }
            }
            DrawImgageDelegate d = new DrawImgageDelegate(DrawImage);
            d.BeginInvoke(pict, 0, 0, null, null);
            
        }
        void DrawImage(Image img, int x, int y)
        {
            System.Threading.Thread.Sleep(100);
            this.CreateGraphics().DrawImage(img, 0, 0);
        }

        private void OfflineGameButton_Click(object sender, EventArgs e)
        {
            rv = "Offline";
            Close();
        }

        private void OnmineGameButton_Click(object sender, EventArgs e)
        {
            rv = "Online";

            OfflineGameButton.Visible = false;
            OnlineGameButton.Visible = false;
            ExitButton.Visible = false;

            label1.Visible = true;
            IPBox.Visible = true;
            ConnectButton.Visible = true;
            CancelButton.Visible = true;
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
    }
}
