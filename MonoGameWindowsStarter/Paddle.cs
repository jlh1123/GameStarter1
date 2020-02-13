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
    class Paddle
    {
        Game1 game;
        Vector2 startingPos = new Vector2(0, 0);
        int frame;
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
        }

        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 40;
        const int FRAME_HEIGHT = 40;





        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();


            if(keyboardState.IsKeyDown(Keys.Up))
            {
                dirFacing = DirectionFacing.Up;
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                dirFacing = DirectionFacing.Down;
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                dirFacing = DirectionFacing.Left;
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                dirFacing = DirectionFacing.Right;
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                frame = (int)dirFacing;
            }
            else
            {
                dirFacing = DirectionFacing.Up;
            }


            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            if(bounds.X < 0)
            {
              bounds.X = 0;
            }
            if(bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
              bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }


        }


        public void Draw(SpriteBatch sb)
        {
            

            var src = new Rectangle(
                frame * FRAME_WIDTH,
                (int)dirFacing % 4 * FRAME_HEIGHT,
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
