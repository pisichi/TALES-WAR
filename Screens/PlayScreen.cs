
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Assignment
{
    class PlayScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
        private Camera _camera;

        private int enemyIndex;

        public Bullet shoot;

        public bool isPlayerTurn = true;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;


        private Character player;
        private Character enemy;
        List<Character> enemyList;

        Texture2D _bg;
        Texture2D _char;
        Texture2D _arrow;
        SpriteFont _font;

        public PlayScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            _bg = content.Load<Texture2D>("sprites/bg");
            _arrow = content.Load<Texture2D>("sprites/arrow");
            _char = content.Load<Texture2D>("sprites/char");
            _font = content.Load<SpriteFont>("font/File");
            _gameObjects = new List<GameObject>();
            enemyList = new List<Character>();
            enemyIndex = 0;


            player = new Character(_char)
            {
                Position = new Vector2(100, 650),
                Bullet = new Bullet(_arrow),
                InTurn = true
            };
            _gameObjects.Add(player);

            enemy = new Character(_char)
            {
                Position = new Vector2(2000, 650),
                Bullet = new Bullet(_arrow),
                IsPlayer = false,
                InTurn = false
            };
            _gameObjects.Add(enemy);
            enemyList.Add(enemy);


            enemy = new Character(_char)
            {
                Position = new Vector2(2200, 650),
                Bullet = new Bullet(_arrow),
                IsPlayer = false,
                InTurn = false
            };

            _gameObjects.Add(enemy);
            enemyList.Add(enemy);

            _camera = new Camera();

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
           Singleton.Instance._previouskey = Singleton.Instance._currentkey;
            Singleton.Instance._currentkey = Keyboard.GetState();

            if (player.InTurn)
            {
                enemyIndex = 0;
                _camera.Follow(player);

                if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                {
                    player.Shoot(_gameObjects);
                }

                if (player.shooting)
                {
                    _camera.Follow(player.shoot);
                    if (player.shoot.Position.X > 3000)
                    {
                        player.shooting = false;
                        player.InTurn = false;
                        enemyList[enemyIndex].InTurn = true;
                    }
                }
            }

            else
            {
                if (enemyIndex >= enemyList.Count)
                {
                    player.InTurn = true;
                }

                else
                {

                    _camera.Follow(enemyList[enemyIndex]);

                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.F) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        enemyIndex++;
                    }
                
                }

            }

                for (int i = 0; i < _gameObjects.Count; i++)
                {
                    _gameObjects[i].Update(gameTime, _gameObjects);
                }
            }

        

            public void HandleInput(GameTime gameTime)
            {

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    m_screenManager.Exit();
                }

            }

            public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {

                spriteBatch.Begin(transformMatrix: _camera.Transform);

                spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 3000, 800));

            if (player.InTurn)
                spriteBatch.DrawString(_font, "player turn", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);
            else
            {

                spriteBatch.DrawString(_font, "enemy turn " + enemyIndex, new Vector2(2000, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);

                spriteBatch.DrawString(_font, "press f to skip", new Vector2(2000, (Singleton.SCREENHEIGHT +100) / 2) - _font.MeasureString("Playing") / 2, Color.White);
            }

                spriteBatch.DrawString(_font, "press ESC to exit", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 3) - _font.MeasureString("press ESC to exit") / 2, Color.White);

                for (int i = 0; i < _gameObjects.Count; i++)
                {
                    _gameObjects[i].Draw(spriteBatch);
                }

                spriteBatch.End();

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

            void Reset()
            {

            }
        }
    }



