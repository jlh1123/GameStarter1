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
    public class Coin
    {
        /// <summary>
        /// game reference
        /// </summary>
        Game1 game;


        /// <summary>
        /// texture of coin
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// bounds reference to check for collisions. 
        /// </summary>
        public BoundingCircle bounds;

       

        /// <summary>
        /// coin constructor 
        /// </summary>
        /// <param name="game">game reference.</param>
        public Coin(Game1 game)
        {
            
            this.game = game;
        }

        
        /// <summary>
        /// initialize default values and sets the random starting position of the coin
        /// </summary>
        public void Initialize()
        {
            bounds.Radius = 15;

            //Help for these next two lines came from: http://rbwhitaker.wikidot.com/random-number-generation   under "Generating Float Point Numbers" //
            double randX = game.Random.NextDouble();
            double randY = game.Random.NextDouble();

            bounds.X = MathHelper.Lerp(0, game.GraphicsDevice.Viewport.Width, (float)randX);
            bounds.Y = MathHelper.Lerp(0, game.GraphicsDevice.Viewport.Height, (float)randY);

        }

        

        /// <summary>
        /// load the content manager for the coin
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("coin");
            
        }

        /// <summary>
        /// actually draws the doin.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            
                spriteBatch.Draw(texture, bounds, Color.LightYellow);
            
            

        }

       
    }
}
