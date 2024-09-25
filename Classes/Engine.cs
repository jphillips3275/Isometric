using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Engine.Classes
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class Engine
    {
        private Vector ScreenSize = new Vector(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Line> AllLines = new List<Line>();

        public Color BackgroundColor = Color.Red;
        public Vector CameraPos = new Vector();
        public float CameraAngle = 0f;
        public Point mouseLocation;

        public Engine(Vector ScreenSize, string Title)
        {
            Log.Info("Starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.x, (int)this.ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }
        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }
        public static void RegisterLine(Line line)
        {
            AllLines.Add(line);
        }
        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }
        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }
        public static void UnRegisterLine(Line line)
        {
            AllLines.Remove(line);
        }

        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });      //this forces the window to refresh as long as the GameLoopThread is running
                    OnUpdate();
                    Thread.Sleep(10);        //have to sleep because the next refresh will overlap with the current refresh
                }
                catch
                {
                }
            }
        }


        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);
            mouseLocation = Window.PointToClient(Control.MousePosition);

            g.TranslateTransform(CameraPos.x, CameraPos.y);
            g.RotateTransform(CameraAngle);
            try
            {
                foreach (Sprite2D sprite in AllSprites)
                {
                    g.DrawImage(sprite.Sprite, sprite.Position.x, sprite.Position.y, sprite.Scale.x, sprite.Scale.y);
                }
                foreach (Shape2D shape in AllShapes)
                {
                    g.DrawRectangle(new Pen(shape.Color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
                }
            }
            catch { }
        }

        public Vector translateCoords(Vector origin, Vector point, Vector tileScale)
        {
            return new Vector((origin.x * tileScale.x) + (point.x - point.y) * (tileScale.x / 2), (origin.y * tileScale.y) + (point.x + point.y) * (tileScale.y / 2));
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();    //to do with movement or physics
        public abstract void OnDraw();      //to do with drawing
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
