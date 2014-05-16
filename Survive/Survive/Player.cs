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

namespace Survive
{
    public class Player : GameObject
    {
        //private Vector2 fallSpeed = new Vector2(0, 20);
        private static float moveScale = 150.0f;
        private bool dead = false;

        public bool Dead
        {
            get { return dead; }
        }

        #region Constructor
        public Player(ContentManager content)
        {
            animations.Add("Idle", new Animation(content.Load<Texture2D>(@"Textures/Player/IdleDown"), 64, "Idle"));
            animations["Idle"].LoopAnimation = false;

            animations.Add("Up", new Animation(content.Load<Texture2D>(@"Textures/Player/Up"), 64, "Up"));
            animations["Up"].LoopAnimation = true;

            animations.Add("Down", new Animation(content.Load<Texture2D>(@"Textures/Player/Down"), 64, "Down"));
            animations["Down"].LoopAnimation = true;

            animations.Add("Left", new Animation(content.Load<Texture2D>(@"Textures/Player/Left"), 64, "Left"));
            animations["Left"].LoopAnimation = true;

            animations.Add("Right", new Animation(content.Load<Texture2D>(@"Textures/Player/Right"), 64, "Right"));
            animations["Right"].LoopAnimation = true;

            frameWidth = 48;
            frameHeight = 48;
            CollisionRectangle = new Rectangle(9, 1, 30, 46);

            drawDepth = 1f;

            enabled = true;
            codeBasedBlocks = false;
            PlayAnimation("idle");
        }
        #endregion

        #region Helper Methods
        private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;
            int screenLocY = (int)Camera.WorldToScreen(worldLocation).Y;

            if (screenLocX > 375)
            {
                Camera.Move(new Vector2(screenLocX - 375, 0));
            }

            if (screenLocX < 375)
            {
                Camera.Move(new Vector2(screenLocX - 375, 0));
            }

            if (screenLocY > 258)
            {
                Camera.Move(new Vector2(0, screenLocY - 258));
            }

            if (screenLocY < 258)
            {
                Camera.Move(new Vector2(0, screenLocY - 258));
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (!Dead)
            {
                string newAnimation = "Idle";

                velocity = new Vector2(0, 0);
                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.A) ||
                    (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    newAnimation = "Left";
                    velocity = new Vector2(-moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.D) ||
                    (gamePad.ThumbSticks.Left.X > 0.3f))
                {
                    newAnimation = "Right";
                    velocity = new Vector2(moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.W) ||
                    (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    newAnimation = "Up";
                    velocity = new Vector2(0, -moveScale);
                }

                if (keyState.IsKeyDown(Keys.S) ||
                    (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    newAnimation = "Down";
                    velocity = new Vector2(0, moveScale);
                }

                if (keyState.IsKeyDown(Keys.LeftShift) ||
                    (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    moveScale = 1500;
                }
                else
                {
                    moveScale = 150;
                }

                if (newAnimation != currentAnimation)
                {
                    PlayAnimation(newAnimation);
                }
            }

            repositionCamera();

            base.Update(gameTime);
        }
    }
}
