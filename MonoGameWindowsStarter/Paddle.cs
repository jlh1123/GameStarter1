using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{

    enum DirectionFacing
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3
    }
    public class Paddle
    {
        Game1 game;

        public Vector2 playerPosition;
        private DirectionFacing dirFacing = DirectionFacing.Up;
        public BoundingRectangle bounds;

        Texture2D texture;
        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">The game this paddle be</param>
        public Paddle(Game1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            bounds.Width = 40;
            bounds.Height = 40;
            bounds.X = 0;
            bounds.Y = 0;
            playerPosition = new Vector2(bounds.X, bounds.Y);
        }

        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 40;
        const int FRAME_HEIGHT = 40;





        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            playerPosition.X = bounds.X;
            playerPosition.Y = bounds.Y;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                dirFacing = DirectionFacing.Up;
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                dirFacing = DirectionFacing.Down;
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                dirFacing = DirectionFacing.Left;
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;



            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                dirFacing = DirectionFacing.Right;
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            }



            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            if (bounds.X < 0)
            {
                bounds.X = 0;
            }
            if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }


        }


        public void Draw(SpriteBatch sb)
        {


            var src = new Rectangle(
                (int)dirFacing * FRAME_WIDTH,
                0,
                FRAME_WIDTH,
                FRAME_HEIGHT);

            sb.Draw(texture, bounds, src, Color.White);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("playerSpriteSheet");

        }



    }
}
