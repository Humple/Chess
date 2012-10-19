using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess.GUI
{
    public partial class FigureChoiceWindow : Form
    {
        public FigureChoiceWindow(Point startPosition)
        {
            InitializeComponent();
            this.Location = startPosition;
        }

        private void FigureChoiceWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
