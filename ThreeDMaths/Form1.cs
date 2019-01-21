using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace ThreeDMaths
{
    public partial class Form1 : Form
    {
        private List<(double, double, double)> coordinates;

        public Form1()
        {
            InitializeComponent();
            coordinates = new List<(double, double, double)>();
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void RenderCoordinates()
        {
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            var centerX = this.Width / 2;
            var centerY = this.Height / 2;

            coordinates.ForEach(c =>
            {
                var (x, y, z) = c;
                formGraphics.FillRectangle(myBrush, new Rectangle((int)x + centerX, (int)y + centerY, 1, 1));
            });

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void btnBox_Click(object sender, EventArgs e)
        {
            coordinates = new List<(double, double, double)>();

            coordinates.Add((5, 5, 5));
            coordinates.Add((5, -5, 5));
            coordinates.Add((5, 5, -5));
            coordinates.Add((5, -5, -5));
            coordinates.Add((-5, 5, 5));
            coordinates.Add((-5, -5, 5));
            coordinates.Add((-5, 5, -5));
            coordinates.Add((-5, -5, -5));

            RenderCoordinates();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            coordinates = coordinates.Select(c =>
            {
                var (x, y, z) = c;

                var centerX = this.Height/2;
                var centerY = this.Width/2;

                var angle = ConvertToRadians(10);

                var newX = (x) * Math.Cos(angle) - (y) * Math.Sin(angle);
                var newY = (x) * Math.Sin(angle) + (y) * Math.Cos(angle);

                return (newX, newY, z);
            }).ToList();

            RenderCoordinates();
        }
    }
}
