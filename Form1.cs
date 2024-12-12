using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void registration_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            registration form = new registration();
            form.ShowDialog();
            Close();
        }

        private void authorization_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            authorization form = new authorization();
            form.ShowDialog();
            Close();
        }
    }
}
