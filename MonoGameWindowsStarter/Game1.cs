﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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

        public int coinActive = 0;
        private int Points = 0;
        private SpriteFont font;

        public Random Random = new Random();
        Ball ball;
        //Vector2 ballPosition = Vector2.Zero; //xy tracking
        //Vector2 ballVelocity;
        Coin coin;
        Paddle paddle;

        KeyboardState oldKS;
        KeyboardState newKS;

        
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
            graphics.PreferredBackBufferWidth = 1042;
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
                Points++;
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
            //spriteBatch.Draw(ball, new Rectangle(100, 100, 100, 100), Color.White);
            spriteBatch.DrawString(font, "Points: " + Points, new Vector2(200, 200),Color.Black);

            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            coin.Draw(spriteBatch);
            spriteBatch.End();


            

            




            base.Draw(gameTime);
        }

        

    }
}
