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
    class NodeManager
    {
        private Texture2D green, purple, white, line;
        public List<Node> Nodes;

        public NodeManager(Texture2D g, Texture2D p, Texture2D w, Texture2D l)
        {
            green = g;
            purple = p;
            white = w;
            line = l;
            Nodes = new List<Node>();
        }

        public void AddNode()
        {
            AddNode(new Vector2(Globals.RNG.Next(Globals.Width), Globals.RNG.Next(Globals.Height)));
        }

        public void AddNode(Vector2 position)
        {
            Node n = new Node(green, purple, white, line, position);
            Nodes.Add(n);
        }

        public void Update(GameTime gameTime)
        {
            Nodes.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch s)
        {
            Nodes.ForEach(x => x.Draw(s));
        }

        public void Clear()
        {
            Nodes.Clear();
        }
    }
}
