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
    class Paddle
    {
        Game1 game;
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
          bounds.Width = 40-+;
          bounds.Height = 40;
          bounds.X = 0;
          bounds.Y = 0;
        }







        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();


            if(keyboardState.IsKeyDown(Keys.Up))
            {
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(keyboardState.IsKeyDown(Keys.Left))
            {
              bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
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
            sb.Draw(texture, bounds, Color.Green);

        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("pixel");
        }



    }
}
