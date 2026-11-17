using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverApp_Jacinto
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private System.Windows.Forms.Timer animationTimer;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            // Animation Timer 
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            // Interactions
            this.MouseClick += Form1_MouseClick;
            this.KeyDown += (s, e) => { this.Close(); };
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            // Move and check wall collisions
            foreach (Shape shape in shapes)
            {
                shape.Move();
                shape.CheckWallCollision(this.ClientSize.Width, this.ClientSize.Height);
            }

            // Shape-to-Shape Collision Detection
            CheckObjectCollisions();

            // Trigger redraw
            this.Invalidate();
        }

        private void CheckObjectCollisions()
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    Rectangle rect1 = shapes[i].GetBounds();
                    Rectangle rect2 = shapes[j].GetBounds();

                    if (rect1.IntersectsWith(rect2))
                    {
                        float tempVX = shapes[i].VX;
                        float tempVY = shapes[i].VY;

                        shapes[i].VX = shapes[j].VX;
                        shapes[i].VY = shapes[j].VY;
                        shapes[j].VX = tempVX;
                        shapes[j].VY = tempVY;

                        // Prevent clumping with a positional nudge
                        float pushFactor = 2.0f;
                        if (shapes[i].X < shapes[j].X)
                        {
                            shapes[i].X -= pushFactor;
                            shapes[j].X += pushFactor;
                        }
                        else
                        {
                            shapes[i].X += pushFactor;
                            shapes[j].X -= pushFactor;
                        }

                        if (shapes[i].Y < shapes[j].Y)
                        {
                            shapes[i].Y -= pushFactor;
                            shapes[j].Y += pushFactor;
                        }
                        else
                        {
                            shapes[i].Y += pushFactor;
                            shapes[j].Y -= pushFactor;
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw all shapes polymorphically on default form background
            foreach (Shape shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void Form1_MouseClick(object? sender, MouseEventArgs e)
        {
            Random rand = new Random();
            float vx = rand.Next(-4, 5);
            float vy = rand.Next(-4, 5);
            if (vx == 0) vx = 2;
            if (vy == 0) vy = 2;

            int randomSize = rand.Next(35, 100);

            // Shape things
            int choice = rand.Next(3);

            if (choice == 0)
            {
                shapes.Add(new HeartShape(e.X, e.Y, vx, vy, randomSize, Color.FromArgb(255, 192, 203)));
            }
            else if (choice == 1)
            {
                shapes.Add(new StarShape(e.X, e.Y, vx, vy, randomSize, Color.FromArgb(255, 248, 220)));
            }
            else
            {
                // Usagi part
                var usagiShape = new PictureBoxShape(e.X, e.Y, vx, vy, randomSize, Color.Transparent, "usagi.png");
                usagiShape.IconBox.Parent = this;
                shapes.Add(usagiShape);
                this.Controls.Add(usagiShape.IconBox);
            }
        }
    }
}