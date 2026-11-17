using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverApp_Jacinto
{
    public class HeartShape : Shape
    {
        private Color glowColor = Color.HotPink;

        public HeartShape(float x, float y, float vx, float vy, int size, Color color)
            : base(x, y, vx, vy, size, size, color) { }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath path = new GraphicsPath();
            float w = Width;
            float h = Height;
            float rx = X;
            float ry = Y;

            path.AddBezier(rx + w / 2, ry + h / 4, rx + w, ry, rx + w, ry + h / 2, rx + w / 2, ry + h);
            path.AddBezier(rx + w / 2, ry + h, rx, ry + h / 2, rx, ry, rx + w / 2, ry + h / 4);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle((int)X, (int)Y, Width, Height), ShapeColor, glowColor, 45f))
            {
                g.FillPath(brush, path);
            }

            using (Pen pen = new Pen(Color.White, 2f))
            {
                g.DrawPath(pen, path);
            }
        }

        public override void ApplyVisualChange()
        {
            Width = (int)(Width * 1.08f);
            Height = (int)(Height * 1.08f);
            if (Width > 90) { Width = 45; Height = 45; }
        }
    }
}