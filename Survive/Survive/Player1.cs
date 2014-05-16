//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//namespace Survive
//{
//    public class Player : GameObject
//    {
//        #region Declarations
//        //public static Sprite PlayerSprite;
//        //public static Sprite TorchSprite;
//        public enum AnimationDirection { Forward, Backward, Left, Right };
//        static public AnimationDirection CurrentAnimationDirection = AnimationDirection.Forward;
//        static private Texture2D spTexture;

//        private bool dead = false;

//        //static private int frameWidth;
//        //static private int frameHeight;
//        private static Vector2 playerAngle = Vector2.Zero;
//        public static float moveScale = 9f;

//        private static Rectangle scrollArea = new Rectangle(150, 100, 500, 400);
//        #endregion

//        #region Initialization
//        /*public static void Initialize(Texture2D texture,
//            Rectangle playerInitialFrame, int playerFrameCount,
//            Rectangle torchInitialFrame, int torchFrameCount,
//            Vector2 worldLocation)
//        {
//            spTexture = texture;
//            frameWidth = playerInitialFrame.Width;
//            frameHeight = playerInitialFrame.Height;

//            PlayerSprite = new Sprite(worldLocation, texture, playerInitialFrame, Vector2.Zero);
//            PlayerSprite.BoundingXPadding = 4;
//            PlayerSprite.BoundingYPadding = 4;
//            PlayerSprite.AnimateWhenStopped = false;

//            //playerInitialFrame = new Rectangle(0, 0, 64, 64);
//            //for (int x = 1; x < 4; x++)
//            //    {
//            //        PlayerSprite.AddFrame(new Rectangle(
//            //        playerInitialFrame.X + (frameHeight * x),
//            //        playerInitialFrame.Y,
//            //        frameWidth, frameHeight));
//            //    }

//            //playerInitialFrame = new Rectangle(0, 64, 64, 64);
//            //for (int x = 5; x < 8; x++)
//            //{
//            //    PlayerSprite.AddFrame(new Rectangle(
//            //    playerInitialFrame.X + (frameHeight * x),
//            //    playerInitialFrame.Y,
//            //    frameWidth, frameHeight));
//            //}

//            //playerInitialFrame = new Rectangle(0, 128, 64, 64);
//            //for (int x = 9; x < 12; x++)
//            //{
//            //    PlayerSprite.AddFrame(new Rectangle(
//            //    playerInitialFrame.X + (frameHeight * x),
//            //    playerInitialFrame.Y,
//            //    frameWidth, frameHeight));
//            //}

//            //playerInitialFrame = new Rectangle(0, 192, 64, 64);
//            //for (int x = 13; x < 16; x++)
//            //{
//            //    PlayerSprite.AddFrame(new Rectangle(
//            //    playerInitialFrame.X + (frameHeight * x),
//            //    playerInitialFrame.Y,
//            //    frameWidth, frameHeight));
//            //}
//        }*/

//        public bool Dead
//        {
//            get { return dead; }
//        }
//        #endregion

//        #region Constructor
//        public Player(ContentManager content)
//        {
//            animations.Add("Idle", new Animation(content.Load<Texture2D>(@"Textures/Player/Idle"), 64, "Idle"));
//            animations["Idle"].LoopAnimation = false;

//            animations.Add("Up", new Animation(content.Load<Texture2D>(@"Textures/Player/Up"), 64, "Up"));
//            animations["Up"].LoopAnimation = true;

//            animations.Add("Down", new Animation(content.Load<Texture2D>(@"Textures/Player/Down"), 64, "Down"));
//            animations["Down"].LoopAnimation = true;

//            animations.Add("Left", new Animation(content.Load<Texture2D>(@"Textures/Player/Left"), 64, "Left"));
//            animations["Left"].LoopAnimation = true;

//            animations.Add("Right", new Animation(content.Load<Texture2D>(@"Textures/Player/Right"), 64, "Right"));
//            animations["Right"].LoopAnimation = true;

//            frameHeight = 64;
//            frameWidth = 64;

//            drawDepth = 0.825f;

//            enabled = true;
//            PlayAnimation("Idle");
//        }
//        #endregion

//        #region Input Handling
//        /*private static Vector2 handleKeyboardMovement(KeyboardState keystate)
//        {
//            Vector2 keyMovement = Vector2.Zero;

//            if (keystate.IsKeyDown(Keys.W))
//            {
//                //CurrentAnimationDirection = AnimationDirection.Backward;
//                keyMovement.Y--;
//                Player.AnimateSpriteUp(spTexture, new Rectangle(0, 64, 64, 64), 4);
//            }
//            if (keystate.IsKeyDown(Keys.S))
//            {
//                //CurrentAnimationDirection = AnimationDirection.Forward;
//                keyMovement.Y++;
//                Player.AnimateSpriteDown(spTexture, new Rectangle(0, 0, 64, 64), 4);
//            }
//            if (keystate.IsKeyDown(Keys.A))
//            {
//                //CurrentAnimationDirection = AnimationDirection.Left;
//                keyMovement.X--;
//                Player.AnimateSpriteLeft(spTexture, new Rectangle(0, 128, 64, 64), 4);
//            }
//            if (keystate.IsKeyDown(Keys.D))
//            {
//                //CurrentAnimationDirection = AnimationDirection.Right;
//                keyMovement.X++;
//                Player.AnimateSpriteRight(spTexture, new Rectangle(0, 192, 64, 64), 4);
//            }
//            return keyMovement;
//        }*/

//        /*private static Vector2 handleGamePadMovement(GamePadState gamepadState)
//        {
//            return new Vector2(gamepadState.ThumbSticks.Left.X, -gamepadState.ThumbSticks.Left.Y);
//        }*/

//        /*private static void handleInput(GameTime gameTime)
//        {
//            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
//            Vector2 moveAngle = Vector2.Zero;
//            Rectangle playerInitialFrame = new Rectangle(0, 0, 64, 64);
//            //int playerFrameCount = 4;
//            int frameWidth = playerInitialFrame.Width;
//            int frameHeight = playerInitialFrame.Height;

            

//            moveAngle += handleKeyboardMovement(Keyboard.GetState());
//            moveAngle += handleGamePadMovement(GamePad.GetState(PlayerIndex.One));

//            if (moveAngle != Vector2.Zero)
//            {
//                moveAngle.Normalize();
//                playerAngle = moveAngle;
//                //moveAngle = checkTileObstacles(elapsed, moveAngle);
//            }

//            PlayerSprite.Velocity = moveAngle * playerSpeed;
//            repositionCamera(gameTime, moveAngle);
//        }*/

       
//        #endregion

//        #region Animation
//        //public static void AnimateSpriteUp(Texture2D texture,
//        //    Rectangle playerInitialFrame, int playerFrameCount)
//        //{
//        //    frameWidth = playerInitialFrame.Width;
//        //    frameHeight = playerInitialFrame.Height;

//        //    for (int x = 1; x < playerFrameCount; x++)
//        //    {
//        //        PlayerSprite.AddFrame(new Rectangle(
//        //        playerInitialFrame.X + (frameHeight * x),
//        //        playerInitialFrame.Y,
//        //        frameWidth, frameHeight));
//        //    }
//        //}

//        //public static void AnimateSpriteDown(Texture2D texture,
//        //    Rectangle playerInitialFrame, int playerFrameCount)
//        //{
//        //    frameWidth = playerInitialFrame.Width;
//        //    frameHeight = playerInitialFrame.Height;

//        //    for (int x = 1; x < playerFrameCount; x++)
//        //    {
//        //        PlayerSprite.AddFrame(new Rectangle(
//        //        playerInitialFrame.X + (frameHeight * x),
//        //        playerInitialFrame.Y,
//        //        frameWidth, frameHeight));
//        //    }
//        //}

//        //public static void AnimateSpriteLeft(Texture2D texture,
//        //    Rectangle playerInitialFrame, int playerFrameCount)
//        //{
//        //    frameWidth = playerInitialFrame.Width;
//        //    frameHeight = playerInitialFrame.Height;

//        //    for (int x = 1; x < playerFrameCount; x++)
//        //    {
//        //        PlayerSprite.AddFrame(new Rectangle(
//        //        playerInitialFrame.X + (frameHeight * x),
//        //        playerInitialFrame.Y,
//        //        frameWidth, frameHeight));
//        //    }
//        //}

//        //public static void AnimateSpriteRight(Texture2D texture,
//        //    Rectangle playerInitialFrame, int playerFrameCount)
//        //{
//        //    frameWidth = playerInitialFrame.Width;
//        //    frameHeight = playerInitialFrame.Height;

//        //    for (int x = 1; x < playerFrameCount; x++)
//        //    {
//        //        PlayerSprite.AddFrame(new Rectangle(
//        //        playerInitialFrame.X + (frameHeight * x),
//        //        playerInitialFrame.Y,
//        //        frameWidth, frameHeight));
                
//        //    }
//        //}
//        #endregion

//        #region Movement Limitation
//        /*private static void clampToWorld()
//        {
//            float currentX = PlayerSprite.WorldLocation.X;
//            float currentY = PlayerSprite.WorldLocation.Y;

//            currentX = MathHelper.Clamp(currentX, 0, Camera.WorldRectangle.Right - PlayerSprite.FrameWidth);
//            currentY = MathHelper.Clamp(currentY, 0, Camera.WorldRectangle.Bottom - PlayerSprite.FrameHeight);

//            PlayerSprite.WorldLocation = new Vector2(currentX, currentY);
//        }

//        private static void repositionCamera(GameTime gameTime, Vector2 moveAngle)
//        {
//            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
//            float moveScale = playerSpeed * elapsed;

//            if ((PlayerSprite.ScreenRectangle.X < scrollArea.X) && (moveAngle.X < 0))
//                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);
//            if ((PlayerSprite.ScreenRectangle.Right > scrollArea.Right) && (moveAngle.X > 0))
//                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);

//            if ((PlayerSprite.ScreenRectangle.Y < scrollArea.Y) && (moveAngle.Y < 0))
//                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
//            if ((PlayerSprite.ScreenRectangle.Bottom > scrollArea.Bottom) && (moveAngle.Y > 0))
//                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
//        }*/
//        #endregion

//        #region Update and Draw
//        public override void Update(GameTime gameTime)
//        {
//            if (!dead)
//            {
//                string newAnimation = "Idle";

//                velocity = new Vector2(0, velocity.Y);
//                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
//                KeyboardState keyState = Keyboard.GetState();

//                if (keyState.IsKeyDown(Keys.W) ||
//                    (gamePad.ThumbSticks.Left.Y > 0.3f))
//                {
//                    newAnimation = "Up";
//                }

//                if (keyState.IsKeyDown(Keys.A) ||
//                    (gamePad.ThumbSticks.Left.X < -0.3f))
//                {
//                    newAnimation = "Left";
//                }

//                if (keyState.IsKeyDown(Keys.D) ||
//                    (gamePad.ThumbSticks.Left.X > 0.3f))
//                {
//                    newAnimation = "Right";
//                }

//                if (keyState.IsKeyDown(Keys.S) ||
//                    (gamePad.ThumbSticks.Left.Y > -0.3f))
//                {
//                    newAnimation = "Down";
//                }

//                if (newAnimation != currentAnimation)
//                {
//                    PlayAnimation(newAnimation);
//                }
//            }

//            base.Update(gameTime);
//        }

//        /*public static void Update(GameTime gameTime)
//        {
//            handleInput(gameTime);
//            PlayerSprite.Update(gameTime);
//            clampToWorld();
//        }*/

//        //public static void Draw(SpriteBatch spriteBatch)
//        //{
//        //    Player.Draw(spriteBatch);
//        //}
//        #endregion
//    }
//}
