using System.Drawing;

namespace ScreenSaverApp_Jacinto
{
    public abstract class Shape
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VX { get; set; }
        public float VY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color ShapeColor { get; set; }

        protected Shape(float x, float y, float vx, float vy, int width, int height, Color color)
        {
            X = x;
            Y = y;
            VX = vx;
            VY = vy;
            Width = width;
            Height = height;
            ShapeColor = color;
        }

        public virtual void Move()
        {
            X += VX;
            Y += VY;
        }

        public abstract void Draw(Graphics g);

        public virtual void CheckWallCollision(int clientWidth, int clientHeight)
        {
            bool hitWall = false;

            if (X <= 0)
            {
                X = 0; 
                VX = -VX;
                hitWall = true;
            }
            else if (X + Width >= clientWidth)
            {
                X = clientWidth - Width; 
                VX = -VX;
                hitWall = true;
            }

            if (Y <= 0)
            {
                Y = 0; 
                VY = -VY;
                hitWall = true;
            }
            else if (Y + Height >= clientHeight)
            {
                Y = clientHeight - Height;
                VY = -VY;
                hitWall = true;
            }

            if (hitWall)
            {
                ApplyVisualChange();
            }
        }

        public abstract void ApplyVisualChange();

        public Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }
    }
}