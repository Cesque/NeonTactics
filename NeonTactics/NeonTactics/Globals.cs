using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NeonTactics
{
    static class Globals
    {
        public static int Width = 800;
        public static int Height = 800;

        public static float PlayerMoveSpeed = 2.0f;
        public static float PlayerRotationSpeed = 0.01f;

        public static float NodeRotationSpeed = 0.005f;

        public static Color BackgroundColor = Color.Black;

        public static Random RNG = new Random();
    }
}
