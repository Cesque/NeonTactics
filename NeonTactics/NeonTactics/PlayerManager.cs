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
    class PlayerManager
    {
        private Texture2D green, purple;
        public List<Player> Players;
        private List<Vector2> startingpositions;

        public PlayerManager(Texture2D g, Texture2D p)
        {
            green = g;
            purple = p;
            startingpositions = new List<Vector2>();
            startingpositions.Add(new Vector2(100, 100));
            startingpositions.Add(new Vector2(700, 100));
            startingpositions.Add(new Vector2(100, 700));
            startingpositions.Add(new Vector2(700, 700));
            Players = new List<Player>();
        }

        public void AddPlayer(int playernumber)
        {
            Team t = (playernumber % 2) == 0 ? Team.GREEN : Team.PURPLE;
            Player p = new Player(t == Team.GREEN ? green : purple, startingpositions[playernumber], t, playernumber);

            Players.Add(p);
        }
        public void Update(GameTime gameTime)
        {
            Players.RemoveAll(x => x.Dead);
            Players.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch s)
        {
            Players.ForEach(x => x.Draw(s));
        }

        public void Clear()
        {
            Players.Clear();
        }
    }
}
