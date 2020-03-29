using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    public delegate void ParticleSpawner(ref Particle particle);

    public delegate void ParticleUpdater(float deltaT, ref Particle particle);

    public class ParticleSystem
    {
        private int nextIndex = 0;
        private Particle[] particle;
        private Texture2D texture;
        private SpriteBatch sb2;
        private Random random2 = new Random();

        public Vector2 Emitter { get; set; }

        public int SpawnPerFrame { get; set; }

        public ParticleSystem(GraphicsDevice graphicsDevice, int size, Texture2D texture)
        {
            this.particle = new Particle[size];
            this.sb2 = new SpriteBatch(graphicsDevice);
            this.texture = texture;

        }

        /// <summary>
        /// Updates the particle system, spawining new particles and
        /// moving all live particles around the screen
        /// </summary>
        /// <param name="gameTime">A structure representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            // Make sure our delegate properties are set
            if (SpawnParticle == null || UpdateParticle == null) return;

            // Part 1: Spawn new particles
            for (int i = 0; i < SpawnPerFrame; i++)
            {
                // Create the particle
                SpawnParticle(ref particle[nextIndex]);

                // Advance the index
                nextIndex++;
                if (nextIndex > particle.Length - 1) nextIndex = 0;
            }

            // Part 2: Update Particles
            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particle.Length; i++)
            {
                // Skip any "dead" particles
                if (particle[i].Life <= 0) continue;

                // Update the individual particle
                UpdateParticle(deltaT, ref particle[i]);
            }
        }


        public void Draw()
        {
            sb2.Begin(SpriteSortMode.Deferred, BlendState.Additive);


            //todo: draw particles
            // Iterate through the particles
            for (int i = 0; i < particle.Length; i++)
            {

                if (particle[i].Life <= 0) continue;


                sb2.Draw(texture, particle[i].Position, null, particle[i].Color, 0f, Vector2.Zero, particle[i].Scale, SpriteEffects.None, 0);
            }


            sb2.End();
        }



        public ParticleSpawner SpawnParticle { get; set; }


        public ParticleUpdater UpdateParticle { get; set; }
    }
}
