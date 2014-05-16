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
    public class GameObject
    {
        #region Declarations
        protected static Vector2 worldLocation;
        protected Vector2 velocity;
        protected int frameWidth;
        protected int frameHeight;

        protected bool enabled;
        protected bool flipped = false;
        protected bool onGround;

        protected Rectangle collisionRectangle;
        protected int collideWidth;
        protected int collideHeight;
        protected bool codeBasedBlocks = true;

        protected float drawDepth = 0.85f;
        protected Dictionary<string, Animation> animations =
            new Dictionary<string, Animation>();
        protected string currentAnimation;
        #endregion

        #region Properties
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public int FrameWidth
        {
            get { return frameWidth; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
        }

        public Vector2 WorldLocation
        {
            get { return worldLocation; }
            set { worldLocation = value; }
        }

        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                  (int)worldLocation.X + (int)(frameWidth / 2),
                  (int)worldLocation.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X,
                    (int)worldLocation.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X + collisionRectangle.X,
                    (int)WorldRectangle.Y + collisionRectangle.Y,
                    collisionRectangle.Width,
                    collisionRectangle.Height);
            }
            set { collisionRectangle = value; }
        }
        #endregion

        #region Public Methods
        public void PlayAnimation(string name)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            updateAnimation(gameTime);

            if (velocity.Y != 0)
            {
                onGround = false;
            }

            float pSpeed = 150f;

            Vector2 moveAmount = velocity * elapsed;

            moveAmount = horizontalCollisionTest(moveAmount, gameTime, pSpeed);
            moveAmount = verticalCollisionTest(moveAmount, gameTime, pSpeed);

            Vector2 newPosition = worldLocation + moveAmount;

            newPosition = new Vector2(
                MathHelper.Clamp(newPosition.X, 0,
                  Camera.WorldRectangle.Width - frameWidth),
                MathHelper.Clamp(newPosition.Y, 0,
                  Camera.WorldRectangle.Height - frameHeight));
             
            worldLocation = newPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled)
                return;

            if (animations.ContainsKey(currentAnimation))
            {

                SpriteEffects effect = SpriteEffects.None;

                if (flipped)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    Camera.WorldToScreen(WorldRectangle),
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
            }
        }
        #endregion

        #region Helper Methods
        private void updateAnimation(GameTime gameTime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }
        #endregion

        #region Map-Based Collision Detection Methods
        private Vector2 horizontalCollisionTest(Vector2 moveAmount, GameTime gameTime, float playerSpeed)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newHorizontalLocation = WorldLocation +
                (new Vector2(moveAmount.X, 0) * (playerSpeed * elapsedTime));

            if (moveAmount.X == 0)
                return moveAmount;

            Rectangle newHorizontalRect = new Rectangle(
                (int)newHorizontalLocation.X,
                (int)WorldLocation.Y,
                FrameWidth,
                FrameHeight);

            int horizLeftPixel = 0;
            int horizRightPixel = 0;

            if (moveAmount.X < 0)
            {
                horizLeftPixel = (int)newHorizontalRect.Left;
                horizRightPixel = (int)WorldRectangle.Left;
            }

            if (moveAmount.X > 0)
            {
                horizLeftPixel = (int)WorldRectangle.Right;
                horizRightPixel = (int)newHorizontalRect.Right;
            }

            if (moveAmount.X != 0)
            {
                for (int x = horizLeftPixel; x < horizRightPixel; x++)
                {
                    for (int y = 0; y < FrameHeight; y++)
                    {
                        if (TileMap.IsWallTileByPixel(
                            new Vector2(x, newHorizontalLocation.Y + y)))
                        {
                            moveAmount.X = 0;
                            break;
                        }
                    }
                    if (moveAmount.X == 0)
                    {
                        break;
                    }
                }
            }

            /*
            Rectangle afterMoveRect = CollisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, 0);
            Vector2 corner1, corner2;

            if (moveAmount.X < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Bottom - 1);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Bottom - 1);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(mapCell1) ||
                !TileMap.CellIsPassable(mapCell2))
            {
                moveAmount.X = 0;
                velocity.X = 0;
            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    moveAmount.X = 0;
                    velocity.X = 0;
                }
            }
            */
            return moveAmount;
        }

        private Vector2 verticalCollisionTest(Vector2 moveAmount, GameTime gameTime, float playerSpeed)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moveAmount.Y == 0)
                return moveAmount;

            Vector2 newVerticalLocation = WorldLocation +
                (new Vector2(0, moveAmount.Y) * (playerSpeed * elapsedTime));

            Rectangle newVerticalRect = new Rectangle(
                (int)WorldLocation.X,
                (int)newVerticalLocation.Y,
                FrameWidth,
                FrameHeight);

            int vertTopPixel = 0;
            int vertBottomPixel = 0;

            if (moveAmount.Y < 0)
            {
                vertTopPixel = (int)newVerticalRect.Top;
                vertBottomPixel = (int)WorldRectangle.Top;
            }

            if (moveAmount.Y > 0)
            {
                vertTopPixel = (int)WorldRectangle.Bottom;
                vertBottomPixel = (int)newVerticalRect.Bottom;
            }

            if (moveAmount.Y != 0)
            {
                for (int y = vertTopPixel; y < vertBottomPixel; y++)
                {
                    for (int x = 0; x < FrameWidth; x++)
                    {
                        if (TileMap.IsWallTileByPixel(
                            new Vector2(newVerticalLocation.X + x, y)))
                        {
                            moveAmount.Y = 0;
                            break;
                        }
                    }
                    if (moveAmount.Y == 0)
                    {
                        break;
                    }
                }
            }
            /*Rectangle afterMoveRect = CollisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, (int)moveAmount.Y);
            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Top);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Bottom);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Bottom);
            }

            Vector2 mapCell1 = TileMap.GetTileAtPixel((int)corner1.X, (int)corner1.Y);
            Vector2 mapCell2 = TileMap.GetByPixel(corner2);

            if (!TileMap.IsWallTile(mapCell1) ||
                !TileMap.IsWallTile(mapCell2))
            {
                if (moveAmount.Y > 0)
                    onGround = true;
                moveAmount.Y = 0;
                velocity.Y = 0;
            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    if (moveAmount.Y > 0)
                        onGround = true;
                    moveAmount.Y = 0;
                    velocity.Y = 0;
                }
            }
            */
            return moveAmount;
        }

        #endregion
    }
}
