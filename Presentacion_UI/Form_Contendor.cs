using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_UI
{
    public partial class Form_Contenedor : Form
    {
        public Form_Contenedor()
        {
            InitializeComponent();
        }

        Form_Listado form_Listado;

        private void Form_Contenedor_Load(object sender, EventArgs e)
        {
            Form_Login form_login = new Form_Login();
            var result = form_login.ShowDialog();
            if (result != DialogResult.OK)
            {
                Application.Exit();
            }
        }


        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CamarasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Listado = new Form_Listado();
            form_Listado.ShowDialog();
            this.Hide();

        }

        private void entregaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
