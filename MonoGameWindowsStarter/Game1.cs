using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections;
using System;

namespace MonoGameWindowsStarter
{

    
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SoundEffect coinPickupSFX;
        private int Points = 0;
        private SpriteFont font;

        public Random Random = new Random();
        Ball ball;
        //Vector2 ballPosition = Vector2.Zero; //xy tracking
        //Vector2 ballVelocity;
        Coin coin;

        public int CoinsNeeded = 0;
        Paddle paddle;

        KeyboardState oldKS;
        KeyboardState newKS;


        ParticleSystem smokeParticleSystem;
        ParticleSystem coinParticleSystem;
        ParticleSystem playerParticleSystem;
        Texture2D smokeTexture;
        Texture2D coinSparkTexture;
        Texture2D playerParticleTexture;

        Random rand = new Random();



        const int ANIMATION_FRAME_RATE = 124;
        const int FRAME_WIDTH = 40;
        const int FRAME_HEIGTH = 40;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle = new Paddle(this);
            ball = new Ball(this);
            coin = new Coin(this);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            ball.Initialize();
            paddle.Initialize();
            coin.Initialize();

            //paddleRect.X = 0;
            //paddleRect.Y= 0;
            //paddleRect.Width = 50;
            //paddleRect.Height = 150;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Points");
            coinPickupSFX = Content.Load<SoundEffect>("coinPickupSound");

            // TODO: use this.Content to load your game content here
            ball.LoadContent(Content);
            paddle.LoadContent(Content);
            coin.LoadContent(Content);

            smokeTexture = Content.Load<Texture2D>("smoke");
            smokeParticleSystem = new ParticleSystem(GraphicsDevice, 1000, smokeTexture);
            smokeParticleSystem.SpawnPerFrame = 4;

            // Set the SpawnParticle method
            smokeParticleSystem.SpawnParticle = (ref Particle particle) =>
            {
              
                particle.Position = new Vector2(ball.bounds.X, ball.bounds.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)rand.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)rand.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-rand.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };

            // Set the UpdateParticle method
            smokeParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            //////////////////COIN

            coinSparkTexture = Content.Load<Texture2D>("pixelYellow");
            coinParticleSystem = new ParticleSystem(GraphicsDevice, 1000, coinSparkTexture);
            coinParticleSystem.SpawnPerFrame = 4;

            // Set the SpawnParticle method
            coinParticleSystem.SpawnParticle = (ref Particle particle) =>
            {

                particle.Position = new Vector2(coin.bounds.X, coin.bounds.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-70, 70, (float)rand.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(60, -60, (float)rand.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-rand.NextDouble()/2);
                particle.Color = Color.Gold;
                particle.Scale = 2f;
                particle.Life = 0.85f;
            };

            // Set the UpdateParticle method
            coinParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration/10;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            ///////////////////PLAYER TRAIL

            playerParticleTexture = Content.Load<Texture2D>("pixelWhite");
            playerParticleSystem = new ParticleSystem(GraphicsDevice, 1000, playerParticleTexture);
            playerParticleSystem.SpawnPerFrame = 16;

            // Set the SpawnParticle method
            playerParticleSystem.SpawnParticle = (ref Particle particle) =>
            {

                particle.Position = new Vector2(paddle.bounds.X, paddle.bounds.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-5, 5, (float)rand.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(-5, 5, (float)rand.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-rand.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 2f;
                particle.Life = 2.0f;
            };

            // Set the UpdateParticle method
            playerParticleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            smokeParticleSystem.Update(gameTime);
            coinParticleSystem.Update(gameTime);
            playerParticleSystem.Update(gameTime);
            
            newKS = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            paddle.Update(gameTime);
            ball.Update(gameTime);

            if (paddle.bounds.CollidesWith(ball.bounds))
            {
                ball.Velocity *= -1;
                Console.WriteLine("collided");
                Exit();
            }
            if (paddle.bounds.CollidesWith(coin.bounds))
            {
                coinPickupSFX.Play();
                Points += 1;
                Console.WriteLine("+1 point");

                double randX = Random.NextDouble();
                double randY = Random.NextDouble();

                randX = MathHelper.Lerp(0, GraphicsDevice.Viewport.Width, (float)randX);
                randY = MathHelper.Lerp(0, GraphicsDevice.Viewport.Height, (float)randY);
                coin.bounds.X = (float)randX;
                coin.bounds.Y = (float) randY;
            }

            oldKS = newKS;
            base.Update(gameTime);




            // TODO: Add your update logic here


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Points: " + Points, new Vector2(5, 5),Color.Black);
            //spriteBatch.DrawString(font, "Player Coord X: " + paddle.playerPosition.X, new Vector2(10, 10), Color.Black);
            //spriteBatch.DrawString(font, "Player Coord Y: " + paddle.playerPosition.Y, new Vector2(10, 30), Color.Black);
            //spriteBatch.DrawString(font, "Viewport Width: " + GraphicsDevice.Viewport.Width, new Vector2(10, 60), Color.Black);
            //spriteBatch.DrawString(font, "Viewport Height: " + GraphicsDevice.Viewport.Height, new Vector2(10, 90), Color.Black);
            //spriteBatch.DrawString(font, "ball Coord X: " + ball.bounds.X, new Vector2(10, 120), Color.Black);
            //spriteBatch.DrawString(font, "ball Coord Y: " + ball.bounds.Y, new Vector2(10, 150), Color.Black);
            smokeParticleSystem.Draw();
            coinParticleSystem.Draw();
            playerParticleSystem.Draw();

            //spriteBatch.End();

            //var offset = new Vector2(450,300) - paddle.playerPosition;
            //var matrix = Matrix.CreateTranslation(offset.X, offset.Y, 0);

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, matrix);
            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            coin.Draw(spriteBatch);
            spriteBatch.End();

            
            
            
           

            
            



            base.Draw(gameTime);
        }

        

    }
}
