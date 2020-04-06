using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Ball
    {
        /// <summary>
        /// reference to the game
        /// </summary>
        Game1 game;

        /// <summary>
        /// reference of the black texture
        /// </summary>
        Texture2D texture;

        
        /// <summary>
        /// bounds to check for collisions
        /// </summary>
        public BoundingCircle bounds;

        /// <summary>
        /// Speed in a certain direction refrence variable
        /// </summary>
        public Vector2 Velocity;




        /// <summary>
        /// Ball constructor to make ball objects and reference it to our instance of a game
        /// </summary>
        /// <param name="game">Reference to our game instance</param>
        public Ball(Game1 game)
        {
            this.game = game;
        }


        /// <summary>
        /// Initializes the set values for certain starting variables
        /// </summary>
        public void Initialize()
        {
            bounds.Radius = 25;

            bounds.X = game.GraphicsDevice.Viewport.Width;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2;

            Velocity = new Vector2(
                (float)game.Random.NextDouble(),
                (float)game.Random.NextDouble());

            Velocity = new Vector2(-1,-1);
            Velocity.Normalize();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">Reference to our content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ball");
        }


        /// <summary>
        /// Update method to update the things on screen
        /// </summary>
        /// <param name="gameTime">GameTime object to be reference to keep it updated</param>
        public void Update(GameTime gameTime)
        {
            var viewport = game.GraphicsDevice.Viewport;

            bounds.Center += 0.7f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;

            if(bounds.Center.Y < bounds.Radius)
            {
                Velocity.Y *= -1;
                float delta = bounds.Radius - bounds.Y;
                bounds.Y += 2 * delta;
            }
            if(bounds.Center.Y > viewport.Height - bounds.Radius)
            {
              Velocity.Y *= -1;
              float delta = viewport.Height - bounds.Radius - bounds.Y;
              bounds.Y += 2* delta;
            }
            if(bounds.X < 0)
            {
                Velocity.X *= -1;
                float delta = bounds.Radius - bounds.X;
                bounds.X += 2 * delta;

            }
            if (bounds.X > viewport.Width - bounds.Radius)
            {
              Velocity.X *= -1;
              float delta = viewport.Width - bounds.Radius - bounds.X;
              bounds.X += 2 * delta;
            }


        }

        /// <summary>
        /// Method to draw any the ball.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
          spriteBatch.Draw(texture, bounds, Color.White);
        }


      

    }
}
