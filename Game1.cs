using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_1_5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D beforeBackGroud;
        Texture2D afterBackGroud;
        Texture2D explosionBackGroud;
        Texture2D bomb;

        Texture2D rock;
        Vector2 rock1;
        Vector2 rock2;
        Vector2 rock3;
        Vector2 rock4;
        Vector2 rock5;
        Vector2 rock6;
        Vector2 rockSpeed;

        Rectangle backgroundRect;

        Screen screen;
        MouseState mouseState;

        SoundEffect boom;
        float seconds;
        float startTime;
        SpriteFont timer;
        SpriteFont beforeText;
        SpriteFont afterText;

        enum Screen
        {
            Before,
            Explosion,
            After
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 780;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.ApplyChanges();

            backgroundRect = new Rectangle(0, 0, 780, 400);

            screen = Screen.Before;

            startTime = 0;

            rock1 = new Vector2(20, -76);
            rock2 = new Vector2(270, -140);
            rock3 = new Vector2(140, -220);
            rock4 = new Vector2(400, -90);
            rock5 = new Vector2(540, -280);
            rock6 = new Vector2(670, -158);
            rockSpeed = new Vector2(0, 1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            beforeBackGroud = Content.Load<Texture2D>("before");
            afterBackGroud = Content.Load<Texture2D>("after");
            explosionBackGroud = Content.Load<Texture2D>("explosion");
            bomb = Content.Load<Texture2D>("bomb");
            rock = Content.Load<Texture2D>("rock");
            boom = Content.Load<SoundEffect>("boom");
            timer = Content.Load<SpriteFont>("Timer");
            beforeText = Content.Load<SpriteFont>("beforeText");
            afterText = Content.Load<SpriteFont>("afterText");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

        if (screen == Screen.Before)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Explosion;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    boom.Play();
                }
            }
        else if (screen == Screen.Explosion)
            {
                rock1 += rockSpeed;
                rock2 += rockSpeed;
                rock3 += rockSpeed;
                rock4 += rockSpeed;
                rock5 += rockSpeed;
                rock6 += rockSpeed;

                if (seconds >= 12)
                {
                    screen = Screen.After;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Before)
            {
                _spriteBatch.Draw(beforeBackGroud, new Vector2(-10, 0), Color.White);
                _spriteBatch.Draw(bomb, new Vector2(370, 100), Color.White);
                _spriteBatch.DrawString(beforeText, "Left click to cause an avalanche", new Vector2(170, 20), Color.Black);
            }
            else if (screen == Screen.Explosion)
            {
                _spriteBatch.Draw(explosionBackGroud, backgroundRect, Color.White);
                _spriteBatch.Draw(rock, rock1, Color.White);
                _spriteBatch.Draw(rock, rock2, Color.White);
                _spriteBatch.Draw(rock, rock3, Color.White);
                _spriteBatch.Draw(rock, rock4, Color.White);
                _spriteBatch.Draw(rock, rock5, Color.White);
                _spriteBatch.Draw(rock, rock6, Color.White);

                //The timer is not a part of the finish program and is for making sure everything is timed correctly
                //_spriteBatch.DrawString(timer, (12 - seconds).ToString("00.0"), new Vector2(20, 20), Color.Black);
            }
            else if (screen == Screen.After)
            {
                _spriteBatch.Draw(afterBackGroud, backgroundRect, Color.White);
                _spriteBatch.DrawString(afterText, "You have caused an avalanche", new Vector2(160, 20), Color.Black);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}