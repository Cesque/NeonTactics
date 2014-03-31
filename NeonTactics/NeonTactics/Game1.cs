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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D p1Sprite, p2Sprite;
        Player player1, player2;

        NodeManager nodeManager;

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

            p1Sprite = Content.Load<Texture2D>("greenplayer");
            p2Sprite = Content.Load<Texture2D>("purpleplayer");

            Texture2D line = new Texture2D(GraphicsDevice,1,1);
            line.SetData<Color>(new Color[] {Color.White});

            nodeManager = new NodeManager(
                Content.Load<Texture2D>("greennode"),
                Content.Load<Texture2D>("purplenode"),
                Content.Load<Texture2D>("whitenode"),
                line
                );

            nodeManager.AddNode();
            nodeManager.AddNode();

            player1 = new Player(p1Sprite, new Vector2(100, 100), Team.GREEN);
            player2 = new Player(p2Sprite, new Vector2(700, 700), Team.PURPLE);
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
            player1.Update(gameTime);
            player2.Update(gameTime);
            nodeManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Globals.BackgroundColor);
            spriteBatch.Begin();
            {
                nodeManager.Draw(spriteBatch);
                player1.Draw(spriteBatch);
                player2.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
