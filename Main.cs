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

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("font/File");

            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new GameScreenManager(m_spriteBatch, Content);

            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));

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

            //switch (Singleton.Instance.CurrentGameState)
            //{
            //    case Singleton.GameState.GameIntro:
            //        m_screenManager.ChangeScreen(new IntroScreen(m_screenManager));
            //        break;
            //    case Singleton.GameState.GameMenu:
            //        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
            //        break;
            //    case Singleton.GameState.GamePlaying:
            //        m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
            //        break;

            //}
            


            m_screenManager.ChangeBetweenScreen();

            m_screenManager.Update(gameTime);

            m_screenManager.HandleInput(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

           


           

            m_screenManager.Draw(gameTime);
            
            graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
