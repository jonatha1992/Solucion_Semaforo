using BE;
using BLL;
using Presentacion_UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion_UI
{
    public partial class Form_Camara : Form
    {


        BLLCamara bLLCamara;
        BECamara Camara;
        List<BEEstado> Estados = new List<BEEstado>() { new BEEstado ("Fuera de servicio"),
                                                        new BEEstado ("En servico con novedad"),
                                                        new BEEstado ("Sin novedad")
                                                       };

        List<BEAeropuerto> Aeropuertos = new List<BEAeropuerto>() { new BEAeropuerto ("Ezeiza"),
                                                        new BEAeropuerto ("Aeroparque"),
                                                        new BEAeropuerto ("San Fernando")
                                                       };
        List<BESistema> Sistemas = new List<BESistema>() { new BESistema ("Anyvision"),
                                                        new BESistema ("Indigo"),
                                                        new BESistema ("Milestone")
                                                       };

        public Form_Camara()
        {
            InitializeComponent();
        }
        private void Form_Camara_Load(object sender, EventArgs e)
        {
            try
            {
                bLLCamara = new BLLCamara();
                cargarcombos();
                CargarGrilla(bLLCamara.ListarTodo());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }




        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar_Texto())
                {
                    ObtenerCamara();

                    if (bLLCamara.Guardar(Camara))
                    {
                        if (Camara.Codigo == 0)
                        {
                            MessageBox.Show($"¡Camara Agregado Correctamente !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show($"¡Camara Modificada Correctamente !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                    else
                    {
                        MessageBox.Show($"¡Ya existe ese orden o denominacion coloque otro !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            finally
            {
                CargarGrilla(bLLCamara.ListarTodo().FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBoxAeropuerto.SelectedItem).Nombre));
                Camara = null;
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Camara != null) // si no hay nada seleccionado
                {
                    var result = MessageBox.Show($"¿Desea eliminar la camara {Camara.Denominacion}?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        if (bLLCamara.Eliminar(Camara))
                        {
                            MessageBox.Show($"¡Camara Eliminada correctamente Correctamente !", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"¡No selecciono ninguna camara!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de eliminacion {ex.Message}");
            }
            finally
            {
                CargarGrilla(bLLCamara.ListarTodo().FindAll(x => x.Aeropuerto.Nombre == ((BEAeropuerto)comboBoxAeropuerto.SelectedItem).Nombre));
                Camara = null;
            }

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                var busqueda = bLLCamara.Buscar(comboBoxAeropuerto.Text, textBoxLugar.Text, textBoxDenominacion.Text);
                if (busqueda != null)
                {
                    CargarGrilla(busqueda);
                }
                else
                {
                    MessageBox.Show($"No se encuentran camaras con las descripciones asignadas!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Camara = null;
            }
        }

        void CargarGrilla(object lista)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            ColocarImagen();
            this.dataGridView1.Columns["Codigo"].Visible = false;
            this.dataGridView1.Columns["Estado"].Visible = false;
            this.dataGridView1.Columns["Orden"].Width = 45;
            this.dataGridView1.Columns["Sistema"].Width = 60;
            this.dataGridView1.Columns["Aeropuerto"].Width = 80;
            this.dataGridView1.Columns["Fecha"].Width = 65;
            this.dataGridView1.Columns[9].Width = 60;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            this.dataGridView1.ClearSelection();
        }
        void cargarcombos()
        {
            comboBoxAeropuerto.DataSource = Aeropuertos;
            comboBoxEstado.DataSource = Estados;
            comboBoxSistema.DataSource = Sistemas;
        }

        void ColocarImagen()
        {
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            Image imageS_N = Resources.Bola_verde_DGV;
            Image imageC_N = Resources.Bola_Amarilla_DGV;
            Image imageF_S = Resources.bolaRoja_DGV;
            column.HeaderText = "Estado";
            this.dataGridView1.Columns.Add(column);

            foreach (DataGridViewRow fila in dataGridView1.Rows)
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
        void ObtenerCamara()
        {
            if (Camara == null)
            {
                Camara = new BECamara();
            }
            Camara.Aeropuerto = (BEAeropuerto)comboBoxAeropuerto.SelectedItem;
            Camara.Orden = int.Parse(textBoxOrden.Text.Trim());
            Camara.Denominacion = textBoxDenominacion.Text;
            Camara.Lugar = textBoxLugar.Text;
            Camara.Sistema = (BESistema)comboBoxSistema.SelectedItem;
            Camara.Estado = (BEEstado)comboBoxEstado.SelectedItem;
            Camara.Fecha = Camara.Estado.Estado == "Sin novedad" ? (DateTime?)null : dateTimePickerFechaReparacion.Value;
            Camara.Descripcion = textBoxDescripcion.Text;
        }
        void CargarCamara()
        {
            comboBoxAeropuerto.Text = Camara.Aeropuerto.ToString();
            textBoxOrden.Text = Camara.Orden.ToString();
            textBoxDenominacion.Text = Camara.Denominacion;
            textBoxLugar.Text = Camara.Lugar;
            comboBoxSistema.Text = Camara.Sistema.ToString();
            comboBoxEstado.Text = Camara.Estado.ToString();
            dateTimePickerFechaReparacion.Value = Camara.Fecha == null ? DateTime.Now : Camara.Fecha.Value;
            textBoxDescripcion.Text = Camara.Descripcion;
        }
        void Limpiar()
        {


            Camara = null;
            textBoxLugar.Text = String.Empty;
            comboBoxSistema.Text = String.Empty;
            comboBoxEstado.Text = String.Empty;
            dateTimePickerFechaReparacion.Text = DateTime.Now.ToString();
            textBoxDescripcion.Text = String.Empty;
            textBoxOrden.Text = bLLCamara.ObtenerUlitmoNroOrden((BEAeropuerto)comboBoxAeropuerto.SelectedItem);
            textBoxDenominacion.Text = textBoxOrden.Text + "-";
        }
        bool Validar_Texto()
        {
            if (textBoxOrden.Text != "" || textBoxDenominacion.Text != "" || textBoxLugar.Text != "")
            {

                if (Validar.SoloEnteros(textBoxOrden.Text))
                {
                    if (Validar.SoloAlfanumerico_Con_Espacios(textBoxDenominacion.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Solo Caracteres alfanumericos caracter de separación - en la DENOMINACIÓN ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("solo enteros en el ORDEN", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Complete todo los campos Habilitados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void comboBoxAeropuerto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Camara == null)
            {
                var Aeropuerto = (BEAeropuerto)comboBoxAeropuerto.SelectedItem;
                textBoxOrden.Text = bLLCamara.ObtenerUlitmoNroOrden(Aeropuerto);
                textBoxDenominacion.Text = textBoxOrden.Text + "-";
                CargarGrilla(bLLCamara.ListarTodo().FindAll(x => x.Aeropuerto.Nombre == Aeropuerto.Nombre));
            }
        }
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && Camara == null)
            {
                Camara = (BECamara)dataGridView1.CurrentRow.DataBoundItem;
                CargarCamara();
            }
            else
            {
                Limpiar();
            }
        }


    }

}
