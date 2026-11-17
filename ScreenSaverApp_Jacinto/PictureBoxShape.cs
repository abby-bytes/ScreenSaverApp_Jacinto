using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverApp_Jacinto
{
    public class PictureBoxShape : Shape
    {
        public PictureBox IconBox { get; set; }
        private bool isEnlarged = false;
        private int collisionCooldown = 0; 

        public PictureBoxShape(float x, float y, float vx, float vy, int size, Color color, string imageName)
            : base(x, y, vx, vy, size, size, color)
        {
            IconBox = new PictureBox
            {
                Width = size,
                Height = size,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                IconBox.Image = Image.FromFile(imageName);
            }
            catch
            {
                IconBox.BackColor = Color.LightPink;
            }
        }

        public override void Move()
        {
            base.Move();
            IconBox.Location = new Point((int)X, (int)Y);

            if (collisionCooldown > 0)
            {
                collisionCooldown--;
            }
        }

        public override void Draw(Graphics g)
        {
            // PictureBox handles its own rendering
        }

        public override void ApplyVisualChange()
        {
            // Only trigger visual change if no cd
            if (collisionCooldown > 0) return;

            isEnlarged = !isEnlarged;

            int targetSize = isEnlarged ? (int)(Width * 1.35f) : (int)(Width / 1.35f);

            if (targetSize < 35) targetSize = 35;
            if (targetSize > 120) targetSize = 100;

            Width = targetSize;
            Height = targetSize;
            IconBox.Width = Width;
            IconBox.Height = Height;

            // Cd
            collisionCooldown = 15;
        }
    }
}