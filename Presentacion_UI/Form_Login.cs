using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Presentacion_UI
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();

        }

        BLLUsuario bLLUsuario;
        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (bLLUsuario.Verificar(new BEUsuario (textBoxLegajo.Text,textBoxPassword.Text)))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Error en la Contraseña o Usuario");
                }
            }
            catch (Exception ex )
            {

                MessageBox.Show($"{ex.Message}");
            }

        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            bLLUsuario = new BLLUsuario();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
    }
} 
