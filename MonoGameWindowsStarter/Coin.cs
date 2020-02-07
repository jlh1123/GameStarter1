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
      Game1 game;
      Texture2D texture;
      public BoundingCircle bounds;
      public Vector2 Position;

      public Coin(Game1 game)
      {
          this.game = game;
      }

      public void Initialize()
      {
            bounds.Radius = 15;

            //Help for these next two lines came from: http://rbwhitaker.wikidot.com/random-number-generation   under "Generating Float Point Numbers" //
            double randX = game.Random.NextDouble();
            double randY = game.Random.NextDouble();

            bounds.X = MathHelper.Lerp(0, game.GraphicsDevice.Viewport.Width, (float)randX);
            bounds.Y = MathHelper.Lerp(0, game.GraphicsDevice.Viewport.Height, (float)randY);

        }

      public void LoadContent(ContentManager content)
      {
          texture = content.Load<Texture2D>("coin");
      }

      public void Draw(SpriteBatch spriteBatch)
      {
        spriteBatch.Draw(texture, bounds, Color.Yellow);
      }
    }
}
