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
    class ParticleManager
    {
        private Texture2D sprite;
        public List<Particle> Particles;

        public ParticleManager(Texture2D px)
        {
            sprite = px;
            Particles = new List<Particle>();
        }

        public void Update(GameTime gameTime)
        {
            Particles.ForEach(x => x.Update(gameTime));
            Particles.RemoveAll(x => x.Lifetime < 0.0f);
        }

        public void Draw(SpriteBatch s)
        {
            Particles.ForEach(x => x.Draw(s));
        }

        public void Clear()
        {
            Particles.Clear();
        }

        public void Add(Vector2 position, Vector2 velocity, Color color)
        {
            Particles.Add(new Particle(sprite, position, velocity, color));
        }
    }
}
