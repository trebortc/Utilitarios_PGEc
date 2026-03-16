using QrApp.Utilidades;
using QrServidor;
using P1 = ConsoleApp1;

namespace QrApp
{
    public partial class Form1 : Form
    {
        public List<string> participantes;
        public Form1()
        {
            InitializeComponent(); ;
            textBox1.Enabled = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox2.Text = P1.Program.nombreCurso;
            participantes = new List<string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Length == 0)
            {
                MessageBox.Show("Seleccione una sede PGE");
                return;
            }

            if (!(openFileDialog1.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            //Obtener participantes de archivo excel con formato
            participantes = P1.Program.CargarListadoDeParticipantes(openFileDialog1.FileName);

            if (participantes.Count == 0)
            {
                return;
            }

            //Definir el nombre del curso  en funcion del archivo excel
            P1.Program.nombreCurso = P1.UtilidadesString.LimpiarNombreParticipante(participantes.First());
            textBox2.Text = "";
            textBox2.Text = P1.Program.nombreCurso;

            //Limpiar los nombres de los participantes de tildes, espacios en blanco y enes
            participantes = P1.UtilidadesString.LimpiarNombresParticipantes(participantes);

            //Muestro el nombre del archivo en pantalla
            label5.Text = openFileDialog1.FileName;
            MessageBox.Show("Se realizo la carga del archivo excel " + openFileDialog1.FileName);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            P1.Program.nombreCurso = textBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                P1.Program.direccion = 0;
                textBox1.Text = P1.Program.direccionSitioWebP;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                P1.Program.direccion = 1;
                textBox1.Text = P1.Program.direccionSitioWebR1;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta seguro que desea generar los códigos QRs?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            progressBar1.Visible = true;

            if (!(resultado == DialogResult.Yes))
            {
                return;
            }

            if (participantes.Count == 0)
            {
                MessageBox.Show("No existen usuarios, en la plantilla de documento cargada. \nVerificar documento?");
                return;
            }

            //Calcular pasos
            int step = participantes.Count / 100;

            P1.Program.GenerarCodigoQRs(participantes, ActualizarProgreso);

            MessageBox.Show("Se han creado los codigos QRs, del listado de personas del archivo excel");
        }

        public void ActualizarProgreso(int progreso)
        {
            Console.WriteLine("--->Progreso: " + progreso);
            // Verifica si la actualización se realiza en el hilo de la interfaz de usuario
            if (InvokeRequired)
            {
                Invoke(new Action(() => progressBar1.Value = progreso));
            }
            else
            {
                progressBar1.Value = progreso;
            }
        }

        private void limpiarNombresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? ruta = folderBrowserDialog1.obtenerRutaCarpetaSeleccionada();

            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            DialogResult resultado = MessageBox.Show(" ¿Desea limpiar los nombres de los archivos de la ruta: \n " + ruta + " ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (!(resultado == DialogResult.Yes))
            {
                return;
            }

            AccionesArchivos.LimpiarNombreArchivo(ruta);
        }

        private void ordenNombresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? ruta = folderBrowserDialog1.obtenerRutaCarpetaSeleccionada();

            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            DialogResult resultado = MessageBox.Show(" ¿Desea cambiar el orden de los nombres de los archivos de la ruta: \n " + ruta + " ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            AccionesArchivos.InvertirNombresPorApellidos(ruta);
        }

        private void generarCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hello, World!");
            var credencialesServidor = UtilitariosServidor.ObtenerCredencialesServidor();
            var sessionOptions = UtilitariosServidor.IniciarSesion(credencialesServidor);
            UtilitariosServidor.CrearCarpeta(sessionOptions, textBox2.Text, 1);
        }

        private void subirArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(openFileDialog1.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            var credencialesServidor = UtilitariosServidor.ObtenerCredencialesServidor();
            var sessionOptions = UtilitariosServidor.IniciarSesion(credencialesServidor);

            UtilitariosServidor.SubirCarpeta(sessionOptions, textBox2.Text, openFileDialog1.FileName.ToString());
        }

        private void virarArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? ruta = folderBrowserDialog1.obtenerRutaCarpetaSeleccionada();

            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            DialogResult resultado = MessageBox.Show(" ¿Desea virar los archivos: \n " + ruta + " ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (!(resultado == DialogResult.Yes))
            {
                return;
            }

            AccionesArchivos.Virar(ruta);
        }
    }
}
