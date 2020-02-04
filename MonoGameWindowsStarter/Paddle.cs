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
        BoundingRectangle bounds;
        Texture2D texture;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">The game this paddle be</param>
        public Paddle(Game1 game)
        {
            this.game = game;
            bounds.Width = 50;
            bounds.Height = 200;
            bounds.X = 0;
            bounds.Y = game.GraphicsDevice.Viewport.Height/2 - bounds.Y/2;
            


        }


        public void Update()
        {
            var newKS = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (newKS.IsKeyDown(Keys.Up) && !oldKS.IsKeyDown(Keys.Up))
            {
                bounds.Y -= 1;
                paddleSpeed -= 1;

            }
            if (newKS.IsKeyDown(Keys.Down) && !oldKS.IsKeyDown(Keys.Down))
            {
                paddleRect.Y += 1;
                paddleSpeed += 1;

            }

            if (paddleRect.Y < 0)
            {
                paddleRect.Y = 0;
            }
            if (paddleRect.Y > game.GraphicsDevice.Viewport.Height - paddleRect.Height)
            {
                paddleRect.Y = game.GraphicsDevice.Viewport.Height - paddleRect.Height;
            }


        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, bounds, Color.Green);

        }

        public void LoadContent(ContentManager Content)
        {
            bounds.Width = 50;
            bounds.Height = 200;
            bounds.X = 0;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - bounds.Y / 2;
        }

        

    }
}
