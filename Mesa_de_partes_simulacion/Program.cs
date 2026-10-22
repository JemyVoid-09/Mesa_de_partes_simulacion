using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mesa_de_partes_simulación
{
    public partial class Form1 : Form
    {
        public struct Expediente
        {
            public string cod_exp;
            public string dni;
            public string name;
            public string tip_doc;
            public string asun;
            public string est_tr;
        }

        Expediente[] Lista_de_expedientes = new Expediente[100];
        int cont_exp = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_load(object sender, EventArgs e)
        {

        }

        private void RegistrarExpediente(string cod_exp, string dni, string name, string tip_doc, string asun)
        {
            if (cod_exp == "" || dni == "" || name == "" || tip_doc == "" || asun == "")
            {
                MessageBox.Show("Error: Debe de rellenar todos los campos.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cont_exp >= 100)
            {
                MessageBox.Show("Error: El archivador del sistema se encuentra lleno.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int j = 0; j < cont_exp; j++)
            {
                if (Lista_de_expedientes[j].cod_exp == cod_exp)
                {
                    MessageBox.Show("Error: El código ya se encuentra registrado.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            int i = cont_exp - 1;
            while (i >= 0 && string.Compare(Lista_de_expedientes[i].cod_exp, cod_exp) > 0)
            {
                Lista_de_expedientes[i + 1] = Lista_de_expedientes[i];
                i--;
            }
            Lista_de_expedientes[i + 1].cod_exp = cod_exp;
            Lista_de_expedientes[i + 1].dni = dni;
            Lista_de_expedientes[i + 1].name = name;
            Lista_de_expedientes[i + 1].tip_doc = tip_doc;
            Lista_de_expedientes[i + 1].asun = asun;
            Lista_de_expedientes[i + 1].est_tr = "Recibido";
            cont_exp++;
            MessageBox.Show("Registrado y ordenado con éxito.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int BusquedaBinaria(string codBuscado)
        {
            int inicio = 0;
            int fin = cont_exp - 1;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                int comparacion = string.Compare(Lista_de_expedientes[medio].cod_exp, codBuscado, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                    return medio;
                else if (comparacion < 0)
                    inicio = medio + 1;
                else
                    fin = medio - 1;
            }
            return -1;
        }

        private void ConsultarExpediente(string codBuscado)
        {
            if (cont_exp == 0)
            {
                MessageBox.Show("No hay expedientes registrados en el sistema.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = BusquedaBinaria(codBuscado);
            if (index != -1)
            {
                MessageBox.Show($"[Operación Finalizada - Expediente Encontrado]\n\n" +
                                $"Código      : {Lista_de_expedientes[index].cod_exp}\n" +
                                $"DNI         : {Lista_de_expedientes[index].dni}\n" +
                                $"Ciudadano   : {Lista_de_expedientes[index].name}\n" +
                                $"Tipo Doc    : {Lista_de_expedientes[index].tip_doc}\n" +
                                $"Asunto      : {Lista_de_expedientes[index].asun}\n" +
                                $"Estado      : {Lista_de_expedientes[index].est_tr}",
                                "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Operación fallida: Expediente no encontrado.", "Mesa de Partes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarEnGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
            for (int j = 0; j < cont_exp; j++)
            {
                dgv.Rows.Add(
                    Lista_de_expedientes[j].cod_exp,
                    Lista_de_expedientes[j].dni,
                    Lista_de_expedientes[j].name,
                    Lista_de_expedientes[j].tip_doc,
                    Lista_de_expedientes[j].asun,
                    Lista_de_expedientes[j].est_tr
                );
            }
        }
    }
}