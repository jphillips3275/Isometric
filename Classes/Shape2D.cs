﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Engine.Classes
{
    public class Shape2D
    {
        public Vector Position = null;
        public Vector Scale = null;
        public string Tag = "";
        public Color Color;

        public Shape2D(Vector Position, Vector Scale, string tag, Color Color)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = tag;
            this.Color = Color;

            Log.Info($"[SHAPE2D]({tag}) has been registered");
            Engine.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D] has been destroyed");
            Engine.UnRegisterShape(this);
        }

        public void UpdateShape(Vector Position, Vector Scale, Color Color)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
        }
    }
}
