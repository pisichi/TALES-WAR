
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final_Assignment
{
    class MenuScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;

        public bool IsPaused { get; private set; }


        Texture2D _bg;
        SpriteFont _font;

        public MenuScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            _bg = content.Load<Texture2D>("sprites/bg");
            _font = content.Load<SpriteFont>("font/File");

        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void HandleInput(GameTime gameTime)
        {
            Console.WriteLine("still in menu");
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                m_screenManager.Exit();

            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 3000, 800), color: Color.Blue);
            spriteBatch.DrawString(_font, "Game Menu", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT  / 3) - _font.MeasureString("Game Menu") / 2, Color.White);
            spriteBatch.DrawString(_font, "press space to continue", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("press space to continue") / 2, Color.White);
            spriteBatch.DrawString(_font, "press ESC to exit", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT  / 4) - _font.MeasureString("press ESC to exit") / 2, Color.White);


        }

        public void ChangeBetweenScreen()
        {
            if (m_exitGame)
            {
                m_screenManager.Exit();
            }
        }

        public void Dispose()
        {

        }
    }
}
