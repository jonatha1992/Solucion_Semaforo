using BE;
using BLL;
using Presentacion_UI.Properties;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion_UI
{
    public partial class Form_Listado : Form
    {
        public Form_Listado()
        {
            InitializeComponent();
        }
        BLLCamara bLLCamara;
        BEAeropuerto Aeropuerto;
        List<BECamara> ListaCamaras;

        List<BEAeropuerto> Aeropuertos = new List<BEAeropuerto>() { new BEAeropuerto ("Ezeiza"),
                                                        new BEAeropuerto ("Aeroparque"),
                                                        new BEAeropuerto ("San Fernando")
                                                       };
        List<BESistema> Sistemas = new List<BESistema>() { new BESistema ("Anyvision"),
                                                        new BESistema ("Indigo"),
                                                        new BESistema ("Milestone")
                                                       };
        List<BEEstado> Estados = new List<BEEstado>() { new BEEstado ("Fuera de servicio"),
                                                        new BEEstado ("En servicio con Novedad"),
                                                        new BEEstado ("Sin novedad")
                                                       };
        private void Form_Listado_Load(object sender, EventArgs e)
        {
            bLLCamara = new BLLCamara();
            ListaCamaras = bLLCamara.ListarTodo();
            CargarCombos();
            MostrarCamaras();
            Contabilizar();
        }


        void CargarCombos()
        {
            comboBox1.DataSource = Aeropuertos;
        }

        void Contabilizar()
        {
            labelS_N.Text = (ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBox1.SelectedItem).Nombre
                                 && x.Estado.Estado == "Sin novedad").Count).ToString();
            label_C_N.Text = (ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBox1.SelectedItem).Nombre
                                 && x.Estado.Estado == "En servicio con novedad").Count).ToString();
            label_F_S.Text = (ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBox1.SelectedItem).Nombre
                                 && x.Estado.Estado == "Fuera de servicio").Count).ToString();
            labelTotalCamaras.Text = ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBox1.SelectedItem).Nombre).Count.ToString();
        }
        void ColocarImagen()
        {
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            Image imageS_N = Resources.Bola_verde_DGV;
            Image imageC_N = Resources.Bola_Amarilla_DGV;
            Image imageF_S = Resources.bolaRoja_DGV;
            this.dataGridView.Columns.Add(column);
            column.Width = 70;
            column.HeaderText = "Estado";

            foreach (DataGridViewRow fila in dataGridView.Rows)
            {
                if (fila.Cells[7].Value.ToString() == "Fuera de servicio")
                {
                    fila.Cells[9].Value = imageF_S;
                }
                else if (fila.Cells[7].Value.ToString() == "Sin novedad")
                {
                    fila.Cells[9].Value = imageS_N;
                }
                else
                {
                    fila.Cells[9].Value = imageC_N;
                }
            }
        }
        void MostrarCamaras()
        {
            ListaCamaras = bLLCamara.ListarTodo();
            List<BECamara> ListaFiltradra = new List<BECamara>();

            if (!checkBoxTodos.Checked)
            {
                if (checkBoxVerde.Checked)
                {
                    ListaFiltradra.AddRange(ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre
                                     && x.Estado.Estado == "Sin novedad"));
                }
                if (checkBoxAmarilla.Checked)
                {
                    ListaFiltradra.AddRange(ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre
                                     && x.Estado.Estado == "En servicio con novedad"));
                }
                if (checkBoxRojo.Checked)
                {
                    ListaFiltradra.AddRange(ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre
                                   && x.Estado.Estado == "Fuera de servicio"));
                }

            }
            else
            {
                ListaFiltradra = ListaCamaras.FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre);
            }
            if (ListaFiltradra != null)
            {
                dataGridView.Columns.Clear();
                dataGridView.DataSource = null;
                dataGridView.DataSource = ListaFiltradra;
                ColocarImagen();
                this.dataGridView.Columns["Codigo"].Visible = false;
                this.dataGridView.Columns["Estado"].Visible = false;
                this.dataGridView.Columns["Orden"].Width = 80;
                this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                this.dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
                this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
                this.dataGridView.ClearSelection();
            }
            Contabilizar();
        }






        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Aeropuerto = (BEAeropuerto)comboBox1.SelectedItem;
            MostrarCamaras();
            Contabilizar();
        }


        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            MostrarCamaras();
        }
        private void checkBoxTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTodos.Checked)
            {
                checkBoxVerde.Checked = true;
                checkBoxAmarilla.Checked = true;
                checkBoxRojo.Checked = true;
            }
            else
            {
                checkBoxVerde.Checked = false;
                checkBoxAmarilla.Checked = false;
                checkBoxRojo.Checked = false;
            }
            MostrarCamaras();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            var camara = (BECamara)dataGridView.CurrentRow.DataBoundItem;
            var result = MessageBox.Show($"¿Desea eliminar la camara {camara.Denominacion}?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                if (bLLCamara.Eliminar(camara))
                {
                    MessageBox.Show($"¡Camara eliminada correctamente {camara.Denominacion}!", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
        }

        private void buttonCamaras_Click(object sender, EventArgs e)
        {
            Form_Camara form_Camara = new Form_Camara();
            form_Camara.ShowDialog();
            MostrarCamaras();
        }

        private void buttonCargarExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel |*.xls;*.xlsx;*.xlsm ";
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SLDocument excel = new SLDocument(openFileDialog.FileName, "Camaras");

                int fila = 2;

                List<BECamara> listaParaActualizar = new List<BECamara>();
                while (!string.IsNullOrEmpty(excel.GetCellValueAsString(fila, 1)))
                {
                    var camara = new BECamara()
                    {
                        Codigo = excel.GetCellValueAsInt32(fila, 1),
                        Orden = excel.GetCellValueAsInt32(fila, 2),
                        Denominacion = excel.GetCellValueAsString(fila, 3),
                        Fecha = excel.GetCellValueAsDateTime(fila, 4),
                        Descripcion = excel.GetCellValueAsString(fila, 5),
                        Lugar = excel.GetCellValueAsString(fila, 6),
                        Sistema = new BESistema(excel.GetCellValueAsString(fila, 7)),
                        Aeropuerto = new BEAeropuerto(excel.GetCellValueAsString(fila, 8)),
                        Estado = new BEEstado(excel.GetCellValueAsString(fila, 9))
                    };
                    if (camara.Fecha < DateTime.Parse("01/01/2000"))
                    {
                        camara.Fecha = null;
                    }

                    listaParaActualizar.Add(camara);

                    fila++;
                }

                listaParaActualizar = listaParaActualizar.FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre);
                bLLCamara.GuardarExcel(listaParaActualizar);
                MessageBox.Show($"¡Base de datos Actualizada Correctamente !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarCamaras();
            }

        }
        private void buttonGenerarExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel |*.xls;*.xlsx;*.xlsm ";
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SLDocument excel = new SLDocument(openFileDialog.FileName, "Camaras");
                //SLDocument excel = new SLDocument();
                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo", typeof(int));
                dt.Columns.Add("Orden", typeof(int));
                dt.Columns.Add("Denominacion", typeof(string));
                dt.Columns.Add("Fecha_Reparacion", typeof(DateTime));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("Lugar", typeof(string));
                dt.Columns.Add("Sistema", typeof(string));
                dt.Columns.Add("Aeropuerto", typeof(string));
                dt.Columns.Add("Estado", typeof(string));

                foreach (var camara in ListaCamaras)
                {
                    dt.Rows.Add(camara.Codigo, camara.Orden, camara.Denominacion, camara.Fecha, camara.Descripcion, camara.Lugar, camara.Sistema, camara.Aeropuerto, camara.Estado);
                }
                excel.ImportDataTable(2, 1, dt, false);

                excel.Save();
                MessageBox.Show($"¡Excel Actualizado Correctamente !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarCamaras();
            }
        }
    }
}
