using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace ScreenSaverApp_Jacinto
{
    public class StarShape : Shape
    {
        public StarShape(float x, float y, float vx, float vy, int size, Color color)
            : base(x, y, vx, vy, size, size, color) { }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            PointF[] starPoints = CalculateStarPoints(X + Width / 2f, Y + Height / 2f, Width / 2f, Width / 4f, 5);

            using (SolidBrush brush = new SolidBrush(ShapeColor))
            {
                g.FillPolygon(brush, starPoints);
            }

            using (Pen pen = new Pen(Color.LightYellow, 1.5f))
            {
                g.DrawPolygon(pen, starPoints);
            }
        }

        private PointF[] CalculateStarPoints(float cx, float cy, float outerRadius, float innerRadius, int numPoints)
        {
            PointF[] points = new PointF[numPoints * 2];
            double angle = -Math.PI / 2;
            double angleStep = Math.PI / numPoints;

            for (int i = 0; i < numPoints * 2; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                float x = cx + (float)(Math.Cos(angle) * radius);
                float y = cy + (float)(Math.Sin(angle) * radius);
                points[i] = new PointF(x, y);
                angle += angleStep;
            }
            return points;
        }

        public override void ApplyVisualChange()
        {
            if (ShapeColor == Color.DeepPink)
                ShapeColor = Color.Gold;
            else if (ShapeColor == Color.Gold)
                ShapeColor = Color.LightPink;
            else
                ShapeColor = Color.DeepPink;
        }
    }
}