using Final_Assignment.GameObjects;
using Final_Assignment.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Assignment
{

    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch m_spriteBatch;

        private IGameScreenManager m_screenManager;

        List<GameObject> _gameObjects;
        int _numObject;

        SpriteFont _font;
        private Texture2D asteroidsTexture;
        private float timer;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Singleton.SCREENWIDTH,
                PreferredBackBufferHeight = Singleton.SCREENHEIGHT
            };

            IsMouseVisible = true;
            _gameObjects = new List<GameObject>();

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("font/File");

            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new GameScreenManager(m_spriteBatch, Content);

            m_screenManager.OnGameExit += Exit;
            Reset();


        }


        protected override void UnloadContent()
        {
            if (m_screenManager != null)
            {
                m_screenManager.Dispose();

                m_screenManager = null;
            }
        }


        protected override void Update(GameTime gameTime)
        {
            Singleton.Instance.CurrentKey = Keyboard.GetState();
            _numObject = _gameObjects.Count;
            //testing
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //   m_screenManager.ChangeScreen(new IntroScreen(m_screenManager));
            //}

            //m_screenManager.ChangeBetweenScreen();

            //m_screenManager.Update(gameTime);
            //m_screenManager.HandleInput(gameTime);
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.StartNewLife:
                    timer += (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (timer >= 3)
                    {
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
                        timer = 0;
                    }
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (_gameObjects[i].IsActive) _gameObjects[i].Update(gameTime, _gameObjects);
                    }
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (!_gameObjects[i].IsActive)
                        {
                            _gameObjects.RemoveAt(i);
                            i--;
                            _numObject--;
                        }
                    }
                    break;
                case Singleton.GameState.GamePlaying:


                    for (int i = 0; i < _numObject; i++)
                    {
                        if (_gameObjects[i].IsActive) _gameObjects[i].Update(gameTime, _gameObjects);
                    }
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (!_gameObjects[i].IsActive)
                        {
                            _gameObjects.RemoveAt(i);
                            i--;
                            _numObject--;
                        }
                    }
                    break;
            }

                    base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            m_spriteBatch.Begin();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(m_spriteBatch);
            }
            //testing
            //m_spriteBatch.DrawString(_font, "press space to continue", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("press space to continue") / 2, Color.White);

            m_screenManager.Draw(gameTime);
            
            m_spriteBatch.End();
            graphics.BeginDraw();
            base.Draw(gameTime);
        }

        protected void Reset()
        {
            timer = 0;
            Singleton.Instance.Score = 0;
            Singleton.Instance.Life = 3;
            Singleton.Instance.Level = 1;


            Singleton.Instance.CurrentGameState = Singleton.GameState.StartNewLife;

            Singleton.Instance.MasterBGMVolume = 0.1f;
            Singleton.Instance.MasterSFXVolume = 0.3f;

          

            Singleton.Instance.Random = new System.Random();

            asteroidsTexture = this.Content.Load<Texture2D>("Asteroid_Spritesheet");


            Texture2D rect = new Texture2D(GraphicsDevice, 5, 5);

            Color[] data = new Color[5 * 5];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Yellow;
            rect.SetData(data);

            _gameObjects.Clear();
            _gameObjects.Add(new Player(new Dictionary<string, Animation>()
                            {
                                { "Alive", new Animation(asteroidsTexture, new Rectangle(0, 0, 100, 100), 1) },
                                { "Dead", new Animation(asteroidsTexture, new Rectangle(100, 50, 50, 50), 1) },
                            })
            {
                Name = "Player",
                Viewport = new Rectangle(0, 0, 100, 100),
                Position = new Vector2(Singleton.SCREENWIDTH / 4, Singleton.SCREENHEIGHT - 150),
                Left = Keys.Left,
                Right = Keys.Right,
                Forward = Keys.Up,
                Fire = Keys.Enter,
                Bullet = new Bullet(rect)
                {
                    Name = "Bullet",
                    Viewport = new Rectangle(0, 0, 5, 5),
                },
            });

            _gameObjects.Add(new Player(new Dictionary<string, Animation>()
                            {
                                { "Alive", new Animation(asteroidsTexture, new Rectangle(0, 0, 100, 100), 1) },
                                { "Dead", new Animation(asteroidsTexture, new Rectangle(100, 50, 50, 50), 1) },
                            })
            {
                Name = "Player2",
                Viewport = new Rectangle(0, 0, 100, 100),
                Position = new Vector2(Singleton.SCREENWIDTH - Singleton.SCREENWIDTH / 4, Singleton.SCREENHEIGHT - 150),
                Left = Keys.A,
                Right = Keys.D,
                Forward = Keys.W,
                Fire = Keys.Space,
                Bullet = new Bullet(rect)
                {
                    Name = "Bullet",
                    Viewport = new Rectangle(0, 0, 5, 5),
                },
            });
            ResetMap();

            foreach (GameObject s in _gameObjects)
            {
                s.Reset();
            }

        }

        private void ResetMap()
        {
            Texture2D rect = new Texture2D(GraphicsDevice, 5, 5);

            Color[] data = new Color[5 * 5];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Yellow;
            rect.SetData(data);
            _gameObjects.Add(new Floor(rect)
            {
                Name = "Floor",
                Viewport = new Rectangle(0, 0, Singleton.SCREENWIDTH, 100),
                Position = new Vector2(Singleton.SCREENWIDTH/2, Singleton.SCREENHEIGHT-50),
            });
        }
    }
}
