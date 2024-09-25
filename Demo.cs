using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engine.Classes;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.Devices;
using System.Windows.Input;

namespace Engine
{
    internal class Demo : Classes.Engine
    {
        static int tileWidth = 60;
        static Vector tileScale = new Vector(tileWidth, tileWidth/2);
        Rectangle spriteSource;
        static Vector worldSize = new Vector(14, 10);
        static Vector origin = new Vector(7, 1);

        static Vector screen = new Vector(1000, 500);

        Shape2D outline;
        Bitmap tileOffset;

        public Demo() : base(new Classes.Vector(screen.x, screen.y), "Demo") { }

        public override void OnLoad()
        {
            BackgroundColor = Color.Black;
            for (int i = 0; i < worldSize.x; i++)
            {
                for (int j = 0; j < worldSize.y; j++)
                {
                    new Sprite2D(translateCoords(origin, new Vector(i, j), tileScale), new Vector(tileScale.x - 1, tileScale.y - 1), "tiles", new Rectangle(128 * 0, 64 * 0, 128, 64), "tile"); 
                }
            }
            outline = new Shape2D(new Vector(0, 0), tileScale, "outline", Color.Yellow);

            Image tmp = Image.FromFile($"Assets/Sprites/tileOffset.png");
            tileOffset = new Bitmap(tmp);
        }
        public override void OnDraw()
        {
        }
        Vector ScreenCell = new Vector();
        Vector worldCell = new Vector();
        Vector mouseOffset = new Vector();
        float OffsetX = 0;
        float OffsetY = 0;
        
        public override void OnUpdate()
        {
            if (mouseLocation.X < 1000 && mouseLocation.Y < 500 && mouseLocation.X > 0 && mouseLocation.Y > 0)
            {
                worldCell.x = 0; worldCell.y = 0;
                OffsetX = 0; OffsetY = 0;

                ScreenCell.x = mouseLocation.X / (int)tileScale.x;
                ScreenCell.y = mouseLocation.Y / (int)tileScale.y;
                mouseOffset.x = mouseLocation.X - (ScreenCell.x * tileScale.x);
                mouseOffset.y = mouseLocation.Y - (ScreenCell.y * tileScale.y);
                worldCell.x = ((ScreenCell.y - origin.y) + (ScreenCell.x - origin.x));
                worldCell.y = ((ScreenCell.y - origin.y) - (ScreenCell.x - origin.x));

                if (tileOffset.GetPixel((int)mouseOffset.x * 2, (int)mouseOffset.y * 2) == Color.FromArgb(255, 237, 28, 36)) { worldCell.x -= 1; OffsetX = (float)-0.5; OffsetY = (float)-0.5; }
                if (tileOffset.GetPixel((int)mouseOffset.x * 2, (int)mouseOffset.y * 2) == Color.FromArgb(255, 63, 72, 204)) { worldCell.y -= 1; OffsetX = (float)0.5; OffsetY = (float)-0.5; }
                if (tileOffset.GetPixel((int)mouseOffset.x * 2, (int)mouseOffset.y * 2) == Color.FromArgb(255, 255, 242, 0)) { worldCell.x += 1; OffsetX = (float)0.5; OffsetY = (float)0.5; }
                if (tileOffset.GetPixel((int)mouseOffset.x * 2, (int)mouseOffset.y * 2) == Color.FromArgb(255, 34, 177, 76)) { worldCell.y += 1; OffsetX = (float)-0.5; OffsetY = (float)0.5; }
                //Log.Warn($"{tileOffset.GetPixel(mouseOffsetX * 2, mouseOffsetY * 2)}");

                if (worldCell.x < worldSize.x && worldCell.y < worldSize.y && worldCell.x >= 0 && worldCell.y >= 0)
                {
                    //Log.Info($"Mouse: {ScreenCell.x},{ScreenCell.y} Cell: {worldCell.x},{worldCell.y} Offset: {mouseOffset.x},{mouseOffset.y}");
                    outline.UpdateShape(new Vector((ScreenCell.x + OffsetX) * tileScale.x, (ScreenCell.y + OffsetY) * tileScale.y), tileScale, Color.Yellow);
                }
                else
                {
                    outline.UpdateShape(outline.Position, outline.Scale, Color.Transparent);
                }
            }
        }
        public override void GetKeyDown(KeyEventArgs e)
        {
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
        }


    }
}
