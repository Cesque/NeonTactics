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

namespace NeonTactics
{
    class Particle
    {
        private Texture2D sprite;
        public Vector2 Position, Velocity;
        public float Lifetime;
        public Color ParticleColor;

        public Particle(Texture2D s, Vector2 p, Vector2 v, Color c)
        {
            sprite = s;
            Position = p;
            Velocity = v;
            Lifetime = 1.0f;
            ParticleColor = c;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            Lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(sprite, new Rectangle((int)Position.X, (int)Position.Y, 2, 2), ParticleColor*Lifetime);
        }
    }
}
