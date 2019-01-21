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
        private double cameraDistance = 200;
        private bool perspective = true;
        private double warping = 0.002;

        public Form1()
        {
            InitializeComponent();
            coordinates = new List<(double, double, double)>();
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void RotatePoint(double x, double y, double z)
        { }

        private void RenderCoordinates()
        {
            var brushSize = 10;

            SolidBrush myBrush = new SolidBrush(Color.Yellow);
            Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            formGraphics.Clear(Color.Black);
            var centerX = this.Width / 2;
            var centerY = this.Height / 2;

            coordinates.ForEach(c =>
            {
                var (x, y, z) = c;
                if (perspective)
                {
                    var distance = cameraDistance + z;
                    var warpedX = x / (distance* warping);
                    var warpedY = y / (distance* warping);
                    formGraphics.FillRectangle(myBrush, new Rectangle((int)warpedX + centerX, (int)warpedY + centerY, brushSize, brushSize));
                }
                else
                {
                    formGraphics.FillRectangle(myBrush, new Rectangle((int)x + centerX, (int)y + centerY, brushSize, brushSize));
                }
            });

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void btnBox_Click(object sender, EventArgs e)
        {
            coordinates = new List<(double, double, double)>();

            coordinates.Add((50, 50, 50));
            coordinates.Add((50, -50, 50));
            coordinates.Add((50, 50, -50));
            coordinates.Add((50, -50, -50));
            coordinates.Add((-50, 50, 50));
            coordinates.Add((-50, -50, 50));
            coordinates.Add((-50, 50, -50));
            coordinates.Add((-50, -50, -50));

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

                var newX = (x) * Math.Cos(angle) - (z) * Math.Sin(angle);
                var newY = y;
                var newZ = (x) * Math.Sin(angle) + (z) * Math.Cos(angle);

                return (newX, newY, newZ);
            }).ToList();

            RenderCoordinates();
        }

        private void btnPerspective_Click(object sender, EventArgs e)
        {
            perspective = !perspective;
            RenderCoordinates();
        }
    }
}
