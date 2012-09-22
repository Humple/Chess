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
        public string returnValue = null;
        public InviteWindow()
        {
            InitializeComponent();
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
            returnValue = "Offline";
            Close();
        }

        private void OnmineGameButton_Click(object sender, EventArgs e)
        {
            returnValue = "Online";
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
