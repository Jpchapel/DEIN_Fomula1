using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _012_Formula1
{
    public partial class f_inicial : Form
    {
        public f_inicial()
        {
            InitializeComponent();
        }

        string[,] aConstructores = new String[5, 2]
        {
            { "2", "Red Bull" },
            { "1", "Ferrari" },
            { "5", "Williams" },
            { "3", "Mercedes" },
            { "4", "Mclaren" }
        };

        string[,] aPilotos = new String[10, 4]
        {
            {"5", "S.Vettel", "1", "0"},
            {"7", "K.Raikkonen", "1", "0"},
            {"3", "D.Ricciardo", "2", "0"},
            {"26", "M.Verstappen", "2", "0"},
            {"44", "L.Hamilton", "3", "0"},
            {"6", "V.Bottas", "3", "0"},
            {"14", "F.Alonso", "4", "0"},
            {"22", "S.Vandoorme", "4", "0"},
            {"19", "F.Massa", "5", "0"},
            {"77", "L.Stroll", "5", "0"}
        };

        string[] aCircuitos = new String[5] { "Sepang Malasia", "Interlagos Brasil", "Yas Marina Abu Dhabi", "Spa-Francorchamps Bélgica", "Silverstone Gran Bretaña" };

        int nCircuito = 0;

        int[] aResultado = new int[10];

        Random r = new Random();

        private void f_inicial_Load(object sender, EventArgs e)
        {
            cargarConstructores();
            cargarPilotos();
            cargarCircuitos();
        }

        private void btnXerarResultado_Click(object sender, EventArgs e)
        {
            lbxResultado.Items.Clear();

            generarResultado();
            generarPuntos();

            btnVerClasificacions.Enabled = true;
            btnXerarResultado.Enabled = false;
        }
        
        private void btnVerClasificacions_Click(object sender, EventArgs e)
        {
            lbxClasConstructores.Items.Clear();
            lbxClasPilotos.Items.Clear();

            cargarClasificacionPilotos();
            cargarClasificacionConstructores();

            btnOutroCircuito.Enabled = true;
            btnVerClasificacions.Enabled = false;
        }
        
        private void btnOutroCircuito_Click(object sender, EventArgs e)
        {
            nCircuito++;

            if (nCircuito < 5)
            {
                iluminarCircuitos();
                btnXerarResultado.Enabled = true;
            }
            else
            {
                MessageBox.Show("El campeonato ha terminado", "FIN de campeonato", MessageBoxButtons.OK);
                btnOutroCircuito.Enabled = false;
            }
        }

        private void cargarConstructores()
        {
            for (int i = 0; i < aConstructores.GetLength(0); i++)
            {
                lbxConstructores.Items.Add(aConstructores[i, 1]);
            }
        }

        private void cargarPilotos()
        {
            for (int piloto = 0; piloto < aPilotos.GetLength(0); piloto++)
            {
                for (int constructor = 0; constructor < aConstructores.GetLength(0); constructor++)
                {
                    if (aConstructores[constructor, 0].Equals(aPilotos[piloto, 2]))
                    {
                        lbxPilotos.Items.Add(aPilotos[piloto, 1] + " \t" + aConstructores[constructor, 1]);
                    }
                }
            }
        }

        private void cargarCircuitos()
        {
            foreach (Control item in gbxCircuitos.Controls)
            {
                item.Text = Convert.ToString(aCircuitos[Convert.ToInt32(item.Name.Substring(11, 1))]);
            }

            iluminarCircuitos();
        }

        private void iluminarCircuitos()
        {
            foreach (Control item in gbxCircuitos.Controls)
            {
                if (nCircuito == Convert.ToInt32(item.Name.Substring(11, 1)))
                {
                    item.BackColor = Color.Red;
                    item.ForeColor = Color.White;
                }
                else
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                }
            }
        }

        private void generarResultado()
        {
            btnVerClasificacions.Enabled = false;            
            int numeroRandom = 0;

            for (int i = 0; i < aResultado.GetLength(0); i++)
            {
                numeroRandom = r.Next(10);

                if (Array.IndexOf(aResultado, numeroRandom, 0, i) < 0)
                {
                    aResultado[i] = numeroRandom; 
                }
                else
                {
                    i--;
                }
            }
        }

        private void generarPuntos()
        {
            for (int i = 0; i < aResultado.GetLength(0); i++)
            {
                lbxResultado.Items.Add(aPilotos[aResultado[i], 1]);

                int puntos = Convert.ToInt32(aPilotos[aResultado[i], 3]);

                if (i == 0)
                {
                    puntos += 25;
                }
                if (i == 1)
                {
                    puntos += 15;
                }
                if (i == 2)
                {
                    puntos += 10;
                }
                if (i == 3)
                {
                    puntos += 5;
                }
                aPilotos[aResultado[i], 3] = Convert.ToString(puntos);
            }
        }

        private void cargarClasificacionPilotos()
        {
            for (int i = 0; i < aPilotos.GetLength(0); i++)
            {
                lbxClasPilotos.Items.Add(aPilotos[i, 1] + " \t " + aPilotos[i, 3]);
            }
        }

        private void cargarClasificacionConstructores()
        {
            for (int constructor = 0; constructor < aConstructores.GetLength(0); constructor++)
            {
                int puntosConstructores = 0;

                for (int piloto = 0; piloto < aPilotos.GetLength(0); piloto++)
                {
                    if (aPilotos[piloto, 2].Equals(aConstructores[constructor, 0]))
                    {
                        puntosConstructores += Convert.ToInt32(aPilotos[piloto, 3]);
                    }
                }

                lbxClasConstructores.Items.Add(string.Format("{0}\t\t{1}", aConstructores[constructor, 1], puntosConstructores));
            }
        }

    }
}
