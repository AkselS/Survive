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
    static class Player
    {
        #region Declarations
        public static Sprite PlayerSprite;
        public static Sprite TorchSprite;
        public enum AnimationDirection { Forward, Backward, Left, Right };
        static public AnimationDirection CurrentAnimationDirection = AnimationDirection.Forward;

        private static Vector2 playerAngle = Vector2.Zero;
        public static float playerSpeed = 90f;

        private static Rectangle scrollArea = new Rectangle(150, 100, 500, 400);
        #endregion

        #region Initialization
        public static void Initialize(Texture2D texture,
            Rectangle playerInitialFrame, int playerFrameCount,
            Rectangle torchInitialFrame, int torchFrameCount,
            Vector2 worldLocation)
        {
            int frameWidth = playerInitialFrame.Width;
            int frameHeight = playerInitialFrame.Height;

            PlayerSprite = new Sprite(worldLocation, texture, playerInitialFrame, Vector2.Zero);
            PlayerSprite.BoundingXPadding = 4;
            PlayerSprite.BoundingYPadding = 4;
            PlayerSprite.AnimateWhenStopped = false;

            playerInitialFrame = new Rectangle(0, 0, 64, 64);
            for (int x = 1; x < playerFrameCount; x++)
            {
                PlayerSprite.AddFrame(new Rectangle(
                playerInitialFrame.X + (frameHeight * x),
                playerInitialFrame.Y,
                frameWidth, frameHeight));
            }

           


        }
        #endregion

        #region Input Handling
        private static Vector2 handleKeyboardMovement(KeyboardState keystate)
        {
            Vector2 keyMovement = Vector2.Zero;

            if (keystate.IsKeyDown(Keys.W))
            {
                CurrentAnimationDirection = AnimationDirection.Backward;
                keyMovement.Y--;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                CurrentAnimationDirection = AnimationDirection.Forward;
                keyMovement.Y++;
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                CurrentAnimationDirection = AnimationDirection.Left;
                keyMovement.X--;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                CurrentAnimationDirection = AnimationDirection.Right;
                keyMovement.X++;
            }
            return keyMovement;
        }

        private static Vector2 handleGamePadMovement(GamePadState gamepadState)
        {
            return new Vector2(gamepadState.ThumbSticks.Left.X, -gamepadState.ThumbSticks.Left.Y);
        }

        private static void handleInput(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 moveAngle = Vector2.Zero;
            Rectangle playerInitialFrame = new Rectangle(0, 0, 64, 64);
            int playerFrameCount = 4;
            int frameWidth = playerInitialFrame.Width;
            int frameHeight = playerInitialFrame.Height;

            switch (CurrentAnimationDirection)
            {
                case AnimationDirection.Forward:
                    playerInitialFrame = new Rectangle(0, 0, 64, 64);
                    for (int x = 1; x < playerFrameCount; x++)
                    {
                        PlayerSprite.AddFrame(new Rectangle(
                        playerInitialFrame.X + (frameHeight * x),
                        playerInitialFrame.Y,
                        frameWidth, frameHeight));
                    }
                    break;
                case AnimationDirection.Backward:
                    playerInitialFrame = new Rectangle(0, 64, 64, 64);
                    for (int x = 1; x < playerFrameCount; x++)
                    {
                        PlayerSprite.AddFrame(new Rectangle(
                        playerInitialFrame.X + (frameHeight * x),
                        playerInitialFrame.Y,
                        frameWidth, frameHeight));
                    }
                    break;
                case AnimationDirection.Left:
                    playerInitialFrame = new Rectangle(0, 128, 64, 64);
                    for (int x = 1; x < playerFrameCount; x++)
                    {
                        PlayerSprite.AddFrame(new Rectangle(
                        playerInitialFrame.X + (frameHeight * x),
                        playerInitialFrame.Y,
                        frameWidth, frameHeight));
                    }
                    break;
                case AnimationDirection.Right:
                    playerInitialFrame = new Rectangle(0, 192, 64, 64);
                    for (int x = 1; x < playerFrameCount; x++)
                    {
                        PlayerSprite.AddFrame(new Rectangle(
                        playerInitialFrame.X + (frameHeight * x),
                        playerInitialFrame.Y,
                        frameWidth, frameHeight));
                    }
                    break;
            }

            moveAngle += handleKeyboardMovement(Keyboard.GetState());
            moveAngle += handleGamePadMovement(GamePad.GetState(PlayerIndex.One));

            if (moveAngle != Vector2.Zero)
            {
                moveAngle.Normalize();
                playerAngle = moveAngle;
                moveAngle = checkTileObstacles(elapsed, moveAngle);
            }

            PlayerSprite.Velocity = moveAngle * playerSpeed;
            repositionCamera(gameTime, moveAngle);
        }

        private static Vector2 checkTileObstacles(
            float elapsedTime,
            Vector2 moveAngle)
        {
            Vector2 newHorizontalLocation = PlayerSprite.WorldLocation +
                (new Vector2(moveAngle.X, 0) * (playerSpeed * elapsedTime));

            Vector2 newVerticalLocation = PlayerSprite.WorldLocation +
                (new Vector2(0, moveAngle.Y) * (playerSpeed * elapsedTime));

            Rectangle newHorizontalRect = new Rectangle(
                (int)newHorizontalLocation.X,
                (int)PlayerSprite.WorldLocation.Y,
                PlayerSprite.FrameWidth,
                PlayerSprite.FrameHeight);

            Rectangle newVerticalRect = new Rectangle(
                (int)PlayerSprite.WorldLocation.X,
                (int)newVerticalLocation.Y,
                PlayerSprite.FrameWidth,
                PlayerSprite.FrameHeight);

            int horizLeftPixel = 0;
            int horizRightPixel = 0;

            int vertTopPixel = 0;
            int vertBottomPixel = 0;

            if (moveAngle.X < 0)
            {
                horizLeftPixel = (int)newHorizontalRect.Left;
                horizRightPixel = (int)PlayerSprite.WorldRectangle.Left;
            }

            if (moveAngle.X > 0)
            {
                horizLeftPixel = (int)PlayerSprite.WorldRectangle.Right;
                horizRightPixel = (int)newHorizontalRect.Right;
            }

            if (moveAngle.Y < 0)
            {
                vertTopPixel = (int)newVerticalRect.Top;
                vertBottomPixel = (int)PlayerSprite.WorldRectangle.Top;
            }

            if (moveAngle.Y > 0)
            {
                vertTopPixel = (int)PlayerSprite.WorldRectangle.Bottom;
                vertBottomPixel = (int)newVerticalRect.Bottom;
            }

            if (moveAngle.X != 0)
            {
                for (int x = horizLeftPixel; x < horizRightPixel; x++)
                {
                    for (int y = 0; y < PlayerSprite.FrameHeight; y++)
                    {
                        //if (TileMap.IsWallTileByPixel(
                            //new Vector2(x, newHorizontalLocation.Y + y)))
                        {
                            moveAngle.X = 0;
                            break;
                        }
                    }
                    if (moveAngle.X == 0)
                    {
                        break;
                    }
                }
            }


            if (moveAngle.Y != 0)
            {
                for (int y = vertTopPixel; y < vertBottomPixel; y++)
                {
                    for (int x = 0; x < PlayerSprite.FrameWidth; x++)
                    {
                        //if (TileMap.IsWallTileByPixel(
                            //new Vector2(newVerticalLocation.X + x, y)))
                        {
                            moveAngle.Y = 0;
                            break;
                        }
                    }
                    if (moveAngle.Y == 0)
                    {
                        break;
                    }
                }
            }

            return moveAngle;
        }


        #endregion

        #region Movement Limitation
        private static void clampToWorld()
        {
            float currentX = PlayerSprite.WorldLocation.X;
            float currentY = PlayerSprite.WorldLocation.Y;

            currentX = MathHelper.Clamp(currentX, 0, Camera.WorldRectangle.Right - PlayerSprite.FrameWidth);
            currentY = MathHelper.Clamp(currentY, 0, Camera.WorldRectangle.Bottom - PlayerSprite.FrameHeight);

            PlayerSprite.WorldLocation = new Vector2(currentX, currentY);
        }

        private static void repositionCamera(GameTime gameTime, Vector2 moveAngle)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float moveScale = playerSpeed * elapsed;

            if ((PlayerSprite.ScreenRectangle.X < scrollArea.X) && (moveAngle.X < 0))
                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);
            if ((PlayerSprite.ScreenRectangle.Right > scrollArea.Right) && (moveAngle.X > 0))
                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);

            if ((PlayerSprite.ScreenRectangle.Y < scrollArea.Y) && (moveAngle.Y < 0))
                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
            if ((PlayerSprite.ScreenRectangle.Bottom > scrollArea.Bottom) && (moveAngle.Y > 0))
                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
        }
        #endregion

        #region Update and Draw
        public static void Update(GameTime gameTime)
        {
            handleInput(gameTime);
            PlayerSprite.Update(gameTime);
            clampToWorld();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            PlayerSprite.Draw(spriteBatch);
        }
        #endregion
    }
}
