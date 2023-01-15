using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Presentacion_UI
{
    #region "campos"
    #endregion
    #region "Propiedades"
    #endregion
    #region "Constructor"
    #endregion
    #region "Metodos"
    #endregion
    #region "Botones"
    #endregion
    #region "Finalizador"
    #endregion



    public class Validar
    {
        public static void SoloEnterossSinEspacios(KeyPressEventArgs V)
        {
            if (Char.IsDigit(V.KeyChar))
            {
                V.Handled = false;
            }
            else if (Char.IsControl(V.KeyChar))
            {
                V.Handled = false;
            }

            else
            {
                V.Handled = true;
                MessageBox.Show("Solo Numeros decimales, sin Espacios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public static void soloLetrasConEspacios(KeyPressEventArgs V)
        {
            if (Char.IsLetter(V.KeyChar))
            {
                V.Handled = false;
            }
            else if (Char.IsSeparator(V.KeyChar))
            {
                V.Handled = true;

            }
            else if (Char.IsControl(V.KeyChar))
            {
                V.Handled = false;
            }
            else
            {
                V.Handled = true;
                MessageBox.Show("Solo Letras", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        public static void soloLetrasSinEspacios(KeyPressEventArgs V)
        {
            if (Char.IsLetter(V.KeyChar))
            {
                V.Handled = false;
            }
            else if (Char.IsSeparator(V.KeyChar))
            {
                V.Handled = false;
            }
            else
            {
                V.Handled = true;
                MessageBox.Show("Solo letras sin espacios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        public static void soloAlfanumerico(KeyPressEventArgs V)
        {
            if (Char.IsLetter(V.KeyChar))
            {
                V.Handled = false;
            }
            else if (Char.IsDigit(V.KeyChar))
            {

                V.Handled = false;

            }
            else if (Char.IsControl(V.KeyChar))
            {
                V.Handled = false;
            }
            else
            {
                V.Handled = true;
                MessageBox.Show("Solo Alfanumerico", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public static void soloDecimales(KeyPressEventArgs V)
        {
            if (V.KeyChar.ToString().Equals(","))
            {
                V.Handled = false;
            }
            else if (Char.IsDigit(V.KeyChar))
            {

                V.Handled = false;

            }
            else if (Char.IsControl(V.KeyChar))
            {
                V.Handled = false;
            }
            else
            {
                V.Handled = true;
                MessageBox.Show("Solo Decimales", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        public static void NoSaltosDelinea(KeyPressEventArgs V)
        {
            if (V.KeyChar == (char)Keys.Enter)
            {
                V.Handled = true;
                MessageBox.Show("No se permiten saltos de linea", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else
            {
                V.Handled = false;


            }

        }
        static public string generarCodigo(string Categoria)
        {
            Random random = new Random();
            string result = "";

            string parametros = "0123456789";   // Creamos una instancia del generador de números aleatorios


            for (int i = 0; i < 4; i++)
            {
                //Obtenemos un número aleatorio que se corresponde con una
                //posición dentro del pattern.

                int mIndex = random.Next(parametros.Length);

                //Vamos formando el código
                result += parametros[mIndex];
            }

            return result + Categoria;


        }
        static public int generarId()
        {
            Random random = new Random();
            int result = 0; // Nuestro patrón de caracteres para formar el código

            string parametros = "0123456789";

            for (int i = 0; i < 12; i++)
            {
                int mIndex = random.Next(parametros.Length);

                result += parametros[mIndex];
            }

            return result;


        }

        static public bool SoloEnteros(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([0-9]+$)");


            return respuesta;
        }

        static public bool SoloDNI(string text)
        {
            bool respuesta = Regex.IsMatch(text, @"^([0-9]{7,8}$)");

            return respuesta;
        }

        static public bool SoloContraseña(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([A-Za-z0-9]+$)");

            return respuesta;
        }

        static public bool SoloAlfabetico_Con_Espacios(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([A-Za-z ]+$)");

            return respuesta;
        }
        static public bool SoloAlfanumerico_Con_Espacios(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([A-Za-z0-9- ]+$)");

            return respuesta;
        }
        static public bool SoloFormatoMail(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([\\w-]+\\.)*?[\\w-]+@[\\w-]+\\.([\\w-]+\\.)*?[\\w]+$");
            return respuesta;
        }


        static public bool FechaNacimiento(string text)
        {
            bool respuesta = Regex.IsMatch(text, "^([0-2][0-9]|3[0-1])(\\/|-)(0[1-9]|1[0-2])\\2(\\d{4})$$");

            var Fecha = Convert.ToDateTime(text);

            if (Fecha >= DateTime.Now)
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

    }
}
