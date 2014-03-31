using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace NeonTactics
{
    class Player
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                float x = value.X % Globals.Width;
                float y = value.Y % Globals.Height;
                if (x < 0) { x += Globals.Width; }
                if (y < 0) { y += Globals.Height; }
                _position = new Vector2(x, y);
            }
        }

        public Team PlayerTeam { get; set; }
        public Texture2D Sprite { get; set; }
        public float Rotation { get; set; }

        public Player(Texture2D sprite, Vector2 position, Team team)
        {
            Position = position;
            Sprite = sprite;
            PlayerTeam = team;
        }

        public void Update(GameTime gameTime)
        {
            Rotation += Globals.PlayerRotationSpeed;
            Rotation %= MathHelper.TwoPi;
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(Sprite, new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height),null,Color.White,Rotation,new Vector2(Sprite.Width/2, Sprite.Height/2), SpriteEffects.None, 0.0f);
        }
    }
}
