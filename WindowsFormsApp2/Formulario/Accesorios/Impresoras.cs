using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2.Formulario.Accesorios
{
   

    public partial class Impresoras : Form
    {
        public Impresoras()
        {
            InitializeComponent();
            llenar();
        }
        
        private void llenar()
        {
            System.Windows.Forms.ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox1.Items.Add("HP");
            comboBox1.Items.Add("EPSON");
            comboBox1.Items.Add("TODOS");
            comboBox1.SelectedIndex= 2; // Selecciona "TODOS" por defecto
            Panel cualquiera= new Panel();
            comboBox1.Location = new Point(10, 10);
            cualquiera.Size = new Size(1200, 40);
            cualquiera.Controls.Add(comboBox1);
            flowLayoutPanel1.Controls.Add(cualquiera);
            
           
            List<string> imagePathsHP = new List<string>
                {
                    @"Resources\imagen11.jpg",
                    @"Resources\imagen12.jpg",
                   
                };
            

            List<string> imagePathsEpson = new List<string>
                {
                    @"Resources\imagen1.jpg",
                    @"Resources\imagen2.png",
                    @"Resources\imagen3.jpg",
                    @"Resources\imagen4.png",
                    @"Resources\imagen5.png",
                    @"Resources\imagen6.jpg",
                    @"Resources\imagen7.jpg",
                    @"Resources\imagen8.jpg",
                    @"Resources\imagen9.jpg",
                    @"Resources\imagen10.jpg"
                };

            string[] nombres =
            {
                "epson L210",
                "epson L220",
                "epson L310",
                "epson L3150",
                "epson L3250",
                "epson L3550",
                "epson L3650",
                "epson L3750",
                "epson L3850",
                "epson L3950"
            };
            string[] nombresHP =
            {
                "HP DeskJet 1115",
                "HP DeskJet 2135",
                "HP DeskJet Ink Advantage 2675",
                "HP DeskJet Plus 4155",
                "HP DeskJet 2720",
                "HP LaserJet Pro M15w",
                "HP LaserJet Pro MFP M28w",
                "HP LaserJet Pro M404n",
                "HP OfficeJet Pro 6970",
                "HP OfficeJet 8025"
            };
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(cualquiera);
            imprimirPrint(imagePathsHP.ToArray(), nombresHP);
            imprimirPrint(imagePathsEpson.ToArray(), nombres);
            double precio = 1500;
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                int eleccion = comboBox1.SelectedIndex;
                switch (eleccion)
                {
                    case 0:
                        //codigo para hp
                        flowLayoutPanel1.Controls.Clear();
                        flowLayoutPanel1.Controls.Add(cualquiera);
                        imprimirPrint(imagePathsHP.ToArray(), nombresHP);
                        break;
                    case 1:
                        flowLayoutPanel1.Controls.Clear();
                        flowLayoutPanel1.Controls.Add(cualquiera);
                        imprimirPrint(imagePathsEpson.ToArray(), nombres);
                        //codigo para epson
                        break;

                    default:
                        flowLayoutPanel1.Controls.Clear();
                        flowLayoutPanel1.Controls.Add(cualquiera);
                        imprimirPrint(imagePathsHP.ToArray(), nombresHP);
                        imprimirPrint(imagePathsEpson.ToArray(), nombres);
                        //codigo para todos
                        break;
                }
            };

        }
        private void imprimirPrint(String[] imagePaths,String[] nombres) {
            for (int i = 0; i < imagePaths.Length; i++)
            {

                // Crear panel contenedor
                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 275,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.None,
                };

                PictureBox pictureBox = new PictureBox
                {
                    Width = 220,
                    Height = 200,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(10, 10),
                    Cursor = Cursors.Hand // para que se vea como enlace
                };

                if (i < imagePaths.Count())
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(imagePaths[i]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar la imagen {imagePaths[i]}: {ex.Message}");
                        // Opcional: Asignar una imagen por defecto si falla la carga
                        // pictureBox.Image = Properties.Resources.DefaultImage; 
                    }
                }
                // Agregar el PictureBox al formulario
                this.Controls.Add(pictureBox);


                Label label = new Label
                {
                    Text = nombres[i],
                    AutoSize = false,
                    Width = 220,
                    Height = 60,
                    Location = new Point(10, 210),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };


                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);
                flowLayoutPanel1.Controls.Add(contenedor);
            }

           
         }
    }
}
