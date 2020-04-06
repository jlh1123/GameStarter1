using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public class PlayerTrackingScrollController : IScrollController
    {
        Paddle player;
        public float ScrollRatio = 1.0f;
        public float Offset = 200;

        public Matrix Transform
        {
            get
            {
                float x = ScrollRatio * (Offset - player.bounds.X);
                return Matrix.CreateTranslation(x, 0, 0);
            }
        }

        public void Update(GameTime gametime)
        {

        }

        public PlayerTrackingScrollController(Paddle player, float ratio)
        {
            this.player = player;
            this.ScrollRatio = ratio;
        }
    }
}
