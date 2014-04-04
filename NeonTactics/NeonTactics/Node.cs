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
    class Node
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

        private Team _belongsto;
        public Team BelongsTo
        {
            get
            {
                return _belongsto;
            }
            set
            {
                _belongsto = value;
                switch (value)
                {
                    case Team.NEUTRAL:
                        CurrentSprite = SpriteWhite;
                        break;
                    case Team.GREEN:
                        CurrentSprite = SpriteGreen;
                        break;
                    case Team.PURPLE:
                        CurrentSprite = SpritePurple;
                        break;
                }
            }
        }
        public Texture2D SpriteGreen { get; set; }
        public Texture2D SpritePurple { get; set; }
        public Texture2D SpriteWhite { get; set; }
        public Texture2D CurrentSprite { get; set; }
        public Texture2D LineSprite { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }
        public Vector2 Velocity { get; set; }
        public float LifetimeMax = 12.0f;
        public float LifetimeMin = 6.0f;
        public float Lifetime { get; set; }
        public bool IsDead { get; set; }

        public Node(Texture2D green, Texture2D purple, Texture2D white, Texture2D line, Vector2 position) : this(green, purple, white, line, position, 0.0f ) 
        {
            Rotation = (float)Globals.RNG.NextDouble() * MathHelper.TwoPi;
            var x = Rotation;
        }
        public Node(Texture2D green, Texture2D purple, Texture2D white, Texture2D line, Vector2 position, float rotation)
        {
            SpriteGreen = green;
            SpritePurple = purple;
            SpriteWhite = white;
            Position = position;
            Rotation = rotation;
            BelongsTo = Team.NEUTRAL;
            Velocity = new Vector2(0.5f - (float)(Globals.RNG.NextDouble() * 1), 0.5f - (float)(Globals.RNG.NextDouble() * 1));
            LineSprite = line;
            RotationSpeed = (float)(Globals.NodeRotationSpeed - (Globals.RNG.NextDouble() * Globals.NodeRotationSpeed * 2));
            Lifetime = LifetimeMin + ((float)Globals.RNG.NextDouble() * (LifetimeMax - LifetimeMin));
            IsDead = false;
        }

        public void Update(GameTime gameTime)
        {
            Lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Lifetime < 0.0f)
            {
                IsDead = true;
            }
            Position += Velocity;
            Rotation += RotationSpeed;
            Rotation %= MathHelper.TwoPi;
        }

        public void Draw(SpriteBatch s)
        {
            //here we want to draw the line, this code doesn't work but demonstrates the ability to draw a line from point A to point B
            //now we need to work out what point A and B are!
            if (BelongsTo != Team.NEUTRAL)
            {
                DrawLineTo(s, LineSprite, GetLine().Start, GetLine().End, GetTeamColor());
            }
            s.Draw(CurrentSprite, GetBoundingBox(), null, Color.White, Rotation, new Vector2(CurrentSprite.Width / 2, CurrentSprite.Height / 2), SpriteEffects.None, 0.0f);

        }

        public Line GetLine()
        {
            //maybe use this to determine collisions?
            var a = Rotation;
            var x = Position.X + Math.Sin(-Rotation) * Globals.Width;
            var y = Position.Y + Math.Cos(-Rotation) * Globals.Height;

            var x2 = Position.X - Math.Sin(-Rotation) * Globals.Width;
            var y2 = Position.Y - Math.Cos(-Rotation) * Globals.Height;

            return new Line(new Vector2((float)x, (float)y), new Vector2((float)x2, (float)y2));
            //return null;
        }

        public Color GetTeamColor()
        {
            if (BelongsTo == Team.GREEN)
            {
                return Color.LightGreen;
            }
            else if (BelongsTo == Team.PURPLE)
            {
                return Color.Purple;
            }
            else
            {
                return Color.White;
            }
        }

        //adapted from http://neptunecentury.blogspot.co.uk/2013/06/xna-40-draw-line-between-two-points.html
        private void DrawLineTo(SpriteBatch sb, Texture2D texture, Vector2 src, Vector2 dst, Color color)
        {
            //direction is destination - source vectors
            Vector2 direction = dst - src;
            //get the angle from 2 specified numbers (our point)
            var angle = (float)Math.Atan2(direction.Y, direction.X);
            //calculate the distance between our two vectors
            float distance;
            Vector2.Distance(ref src, ref dst, out distance);

            //draw the sprite with rotation
            sb.Draw(texture, src, new Rectangle((int)src.X, (int)src.Y, (int)distance, 2), color, angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, CurrentSprite.Width, CurrentSprite.Height);
        }
    }
}
