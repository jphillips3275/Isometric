using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engine.Classes
{
    public class Sprite2D
    {
        public Vector Position = null;
        public Vector Scale = null;
        public string Directory = "";
        public string tag = "";
        public Bitmap Sprite = null;
        public Rectangle section;

        public Sprite2D(Vector Position, Vector Scale, string Directory, string tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.tag = tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({tag}) has been registered at {Position.x},{Position.y}");
            Engine.RegisterSprite(this);
        }

        public Sprite2D(Vector Position, Vector Scale, string Directory, Rectangle section, string tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.tag = tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);

            Sprite = sprite.Clone(section, sprite.PixelFormat);
            Engine.RegisterSprite(this);
        }

        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            if (a.Position.x < b.Position.x + b.Scale.x &&
                a.Position.x + a.Scale.x > b.Position.x &&
                a.Position.y < b.Position.y + b.Scale.y &&
                a.Position.y + a.Scale.y > b.Position.y)
            {
                return true;
            }

            return false;
        }
        public bool IsColliding(string tag)
        {
            foreach (Sprite2D b in Engine.AllSprites)
            {
                if (b.tag == tag)
                {
                    if (Position.x < b.Position.x + b.Scale.x &&
                    Position.x + Scale.x > b.Position.x &&
                    Position.y < b.Position.y + b.Scale.y &&
                    Position.y + Scale.y > b.Position.y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void DestroySelf()
        {
            Engine.UnRegisterSprite(this);
        }
    }
}
