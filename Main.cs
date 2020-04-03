using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{

    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch m_spriteBatch;

        private IGameScreenManager m_screenManager;

        SpriteFont _font;


        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Singleton.SCREENWIDTH,
                PreferredBackBufferHeight = Singleton.SCREENHEIGHT
            };

            IsMouseVisible = true;
            

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("font/File");

            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new GameScreenManager(m_spriteBatch, Content);

            m_screenManager.OnGameExit += Exit;

            
        }


        protected override void UnloadContent()
        {
         if(m_screenManager != null)
            {
                m_screenManager.Dispose();

                m_screenManager = null;
            }
        }


        protected override void Update(GameTime gameTime)
        {

            //testing
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                m_screenManager.ChangeScreen(new IntroScreen(m_screenManager));
            }

            m_screenManager.ChangeBetweenScreen();

            m_screenManager.Update(gameTime);
            m_screenManager.HandleInput(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

           
            m_spriteBatch.Begin();

            //testing
            m_spriteBatch.DrawString(_font, "press space to continue", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("press space to continue") / 2, Color.White);

            m_screenManager.Draw(gameTime);
            base.Draw(gameTime);
            m_spriteBatch.End();
            graphics.BeginDraw();
        }
    }
}
