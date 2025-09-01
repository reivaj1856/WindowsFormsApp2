using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Formulario.Accesorios;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible == false)
            {
                flowLayoutPanel1.Visible = true;
                flowLayoutPanel2.Visible = false;
                flowLayoutPanel3.Visible = false;
            }
            else {
                flowLayoutPanel1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.Visible == false)
            {
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Visible = true;
                flowLayoutPanel3.Visible = false;
            }
            else
            {
                flowLayoutPanel2.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel3.Visible == false)
            {
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel2.Visible = false;
                flowLayoutPanel3.Visible = true;
            }
            else
            {
                flowLayoutPanel3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Impresoras impresoras = new Impresoras();
            AbrirFormulario(impresoras);
            //hideSubMenu();
        }
        private void hideSubMenu()
        {
            if (flowLayoutPanel1.Visible == true)
            {
                flowLayoutPanel1.Visible = false;
            }
            if (flowLayoutPanel2.Visible == true)
            {
                flowLayoutPanel2.Visible = false;
            }
            if (flowLayoutPanel3.Visible == true)
            {
                flowLayoutPanel3.Visible = false;
            }
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            cuerpo.Controls.Clear();
            cuerpo.Controls.Add(formulario);
            cuerpo.Tag = formulario;

            formulario.Show();
        }
        
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.cuerpo.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
