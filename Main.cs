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

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 700
            };

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {

            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new GameScreenManager(m_spriteBatch, Content);

            m_screenManager.OnGameExit += Exit;

            m_screenManager.ChangeScreen(new IntroScreen(m_screenManager));
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


            m_screenManager.ChangeBetweenScreen();

            m_screenManager.Update(gameTime);
            m_screenManager.HandleInput(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            m_spriteBatch.Begin();
            m_screenManager.Draw(gameTime);
            base.Draw(gameTime);
            m_spriteBatch.End();
            graphics.BeginDraw();
        }
    }
}
