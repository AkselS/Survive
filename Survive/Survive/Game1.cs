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

namespace Survive
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Enemy enemy1;
        Enemy2 enemy2;
        Enemy enemy3;
        Enemy enemy4;
        

        Texture2D spritesheet, overlay, tilemap;

        Sprite tempSprite, tempSprite2, tempSprite3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.ApplyChanges();
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
            spritesheet = Content.Load<Texture2D>(@"Textures\spritesheet");
            tilemap = Content.Load<Texture2D>(@"Textures\TileMap");
            overlay = Content.Load<Texture2D>(@"Textures\Overlay");
            TileMap.Initialize(tilemap);
            Camera.WorldRectangle = new Rectangle(0, 0, 2560, 2560);
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;

            //Construct Player
            //tempSprite = new Sprite(new Vector2 (position x, position y), spritesheet, new Rectangle(co-ordinate x, co-ordinate y, width, height), Vector2.Zero (animationspeed));
            /*Player.Initialize(spritesheet,
                new Rectangle(0, 0, 64, 64),
                4,
                new Rectangle(0, 0, 0, 0),
                1,
                new Vector2(300, 300));*/
            player = new Player(Content);
            player.WorldLocation = new Vector2(550, 550);
            enemy1 = new Enemy(Content);
            enemy1.WorldLocation = new Vector2(300, 550);
            enemy2 = new Enemy2(Content);
            enemy2.WorldLocation = new Vector2(350, 550);
            enemy3 = new Enemy(Content);
            enemy3.WorldLocation = new Vector2(300, 500);
            enemy4 = new Enemy(Content);
            enemy4.WorldLocation = new Vector2(250, 550);
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

            player.Update(gameTime);
            /*temp input handling

            Vector2 spriteMove = Vector2.Zero;
            Vector2 cameraMove = Vector2.Zero;

            //Character Move
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                spriteMove.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                spriteMove.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                spriteMove.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                spriteMove.Y = 1;

            //Camera Move
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                cameraMove.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                cameraMove.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                cameraMove.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                cameraMove.Y = 1;

            Camera.Move(cameraMove);
            tempSprite.Velocity = spriteMove * 60;

            tempSprite.Update(gameTime);
            tempSprite2.Update(gameTime);
            tempSprite3.Update(gameTime);
            */
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Rectangle CamLoc = new Rectangle(0, 0, 800, 600);

            spriteBatch.Begin();
            TileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy1.Draw(spriteBatch);
            enemy2.Draw(spriteBatch);
            enemy3.Draw(spriteBatch);
            enemy4.Draw(spriteBatch);
            spriteBatch.Draw(overlay, new Rectangle(0, 0, 800, 600), CamLoc, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
