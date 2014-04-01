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
using BloomPostprocess;

namespace NeonTactics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D bg;

        NodeManager nodeManager;
        PlayerManager playerManager;
        ParticleManager particleManager;

        BloomComponent bloom;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.Height;
            graphics.PreferredBackBufferWidth = Globals.Width;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bloom = new BloomComponent(this);
            Components.Add(bloom);
            //bloom.Settings = new BloomSettings(null, 0.25f, 4, 2, 1, 1.5f, 1);
            bloom.Settings = new BloomSettings(null, 0.0f, 4, 3, 1, 1.5f, 1);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("bg");

            Texture2D line = new Texture2D(GraphicsDevice, 1, 1);
            line.SetData<Color>(new Color[] { Color.White });

            playerManager = new PlayerManager(
                Content.Load<Texture2D>("greenplayer"),
                Content.Load<Texture2D>("purpleplayer"));
            nodeManager = new NodeManager(
                Content.Load<Texture2D>("greennode"),
                Content.Load<Texture2D>("purplenode"),
                Content.Load<Texture2D>("whitenode"),
                line);
            particleManager = new ParticleManager(line);

            for (int i = 0; i < 4; i++)
            {
                nodeManager.AddNode();
                playerManager.AddPlayer(i);
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            playerManager.Update(gameTime);
            nodeManager.Update(gameTime);
            particleManager.Update(gameTime);

            nodeManager.Nodes.ForEach(n =>
            {
                playerManager.Players.ForEach(p =>
                {
                    if (p.GetBoundingBox().Intersects(n.GetBoundingBox()))
                    {
                        n.BelongsTo = p.PlayerTeam;
                    }

                    if (Globals.LineIntersectsRect(n.GetLine().Start, n.GetLine().End, p.GetBoundingBox()))
                    {
                        if (n.BelongsTo != Team.NEUTRAL && n.BelongsTo != p.PlayerTeam)
                        {
                            //player dies
                            p.Die();
                        }
                    }
                });
                if (n.BelongsTo != Team.NEUTRAL)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        var line = n.GetLine();
                        var f = (float)Globals.RNG.NextDouble();
                        var x = MathHelper.Lerp(line.Start.X, line.End.X, f);//((float)i) / 100);
                        var y = MathHelper.Lerp(line.Start.Y, line.End.Y, f);//((float)i) / 100);
                        var p = new Vector2(x, y);
                        var v = new Vector2((float)(Globals.RNG.NextDouble()*0.4) - 0.2f, (float)(Globals.RNG.NextDouble()*0.4) - 0.2f);
                        particleManager.Add(p, v, n.GetTeamColor()*0.3f);
                    }
                }
            });

            for (int i = 0; i < playerManager.Players.Count; i++)
            {
                var player = playerManager.Players[i];               



                var velocity = GamePad.GetState((PlayerIndex)player.PlayerNumber).ThumbSticks.Left;

                for (int j = 0; j < 2; j++)
                {
                    particleManager.Add(player.Position, new Vector2(-velocity.X + 0.5f-(float)Globals.RNG.NextDouble(), velocity.Y + 0.5f-(float)Globals.RNG.NextDouble()), player.PlayerTeam == Team.GREEN ? Color.Green : Color.Purple);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Globals.BackgroundColor);
            bloom.BeginDraw();
            spriteBatch.Begin();
            {
                spriteBatch.Draw(bg, new Rectangle(0, 0, Globals.Width, Globals.Height), Color.White);
                particleManager.Draw(spriteBatch);
                nodeManager.Draw(spriteBatch);
                playerManager.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
}
