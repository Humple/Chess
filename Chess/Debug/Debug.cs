using System;
using System.Windows.Forms;


namespace Chess
{
    class Debug: Form
    {
        protected static Debug debug;
        protected RichTextBox textBox;

        public Debug()
        {
            textBox = new RichTextBox();
            textBox.ReadOnly = true;
            textBox.Dock = DockStyle.Fill;
            this.SuspendLayout();
            this.Controls.Add(textBox);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Opacity = 50;
            this.Width = 800;
        }

        public static void NewMessage(string msg)
        {
            Debug dbg = GetInstance();
            if (dbg.InvokeRequired)
            {
                dbg.Invoke(new MethodInvoker(delegate
                {
                    debug.Show();
                }));
            }
            else
                dbg.Show();

            if( debug.InvokeRequired ) {
                debug.textBox.Invoke( new MethodInvoker( delegate {
                    debug.textBox.Text += "\n " + DateTime.Now + " " + msg;
                } ) );
            } else
                debug.textBox.Text += "\n " + DateTime.Now +" "+ msg;
        }

        public static Debug GetInstance()
        {
            if (debug == null)
                debug = new Debug();

                return debug;
        }
    }
}
