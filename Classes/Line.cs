using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Engine.Classes
{
    public class Line
    {
        public Vector Position = null;
        public Vector Offset = null;
        public float Angle;
        public float Length;
        public Vector Position2 = null;
        public Line(Vector Position, Vector Offset, float Angle)
        {
            this.Position = Position;
            this.Offset = Offset;
            this.Angle = Angle;

            Position2 = new Vector((float)((Position.x + Offset.x / 2) + (1 * Math.Cos(Angle))), (float)((Position.y + Offset.y / 2) + (1 * Math.Sin(Angle))));
            while (this.IsColliding("Ground") == false && Length < 500)
            {
                Position2 = new Vector((float)((Position.x + Offset.x / 2) + (Length * Math.Cos(Angle))), (float)((Position.y + Offset.y / 2) + (Length * Math.Sin(Angle))));
                Length++;
            }



            Engine.RegisterLine(this);
        }

        public void ChangeAngle(float change)
        {
            Length = 1;
            Angle += change;

            Position2 = new Vector((float)((Position.x + Offset.x / 2) + (Length * Math.Cos(Angle))), (float)((Position.y + Offset.y / 2) + (Length * Math.Sin(Angle))));
            while (this.IsColliding("Ground") == false && Length < 500)
            {
                Position2 = new Vector((float)((Position.x + Offset.x / 2) + (Length * Math.Cos(Angle))), (float)((Position.y + Offset.y / 2) + (Length * Math.Sin(Angle))));

                Length++;
            }
        }

        public bool IsColliding(string tag)
        {
            foreach (Sprite2D b in Engine.AllSprites)
            {
                if (b.tag == tag)
                {
                    if (Position2.x < b.Position.x + b.Scale.x &&
                    Position2.x > b.Position.x &&
                    Position2.y < b.Position.y + b.Scale.y &&
                    Position2.y > b.Position.y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
