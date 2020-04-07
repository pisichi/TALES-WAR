
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

        public Bullet shoot;

        public bool isPlayerTurn = true;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;
        private Character character;
        private Character character2;
        List<Character> enemy;

        public KeyboardState _currentkey;
        public KeyboardState _previouskey;


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
            enemy = new List<Character>();



            character = new Character(_char)
            {
                Position = new Vector2(100, 650),
                Bullet = new Bullet(_arrow)
            };
            _gameObjects.Add(character);




            character2 = new Character(_char)
            {
                Position = new Vector2(2000, 650),
                Bullet = new Bullet(_arrow),
                IsPlayer = false
            };
            _gameObjects.Add(character2);
            enemy.Add(character2);

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
            _previouskey = _currentkey;
            _currentkey = Keyboard.GetState();

            if (isPlayerTurn && character.IsPlayer)
            {
                _camera.Follow(character);

                if (_currentkey.IsKeyDown(Keys.Space) && _currentkey != _previouskey)
                {
                    character.Shoot(_gameObjects); 
                }
            }
            else
                _camera.Follow(enemy[0]);

                if (character.shooting)
                {
                    _camera.Follow(character.shoot);
                    if (character.shoot.Position.X > 3000)
                    {
                        character.shooting = false;
                        isPlayerTurn = false;
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

                if (isPlayerTurn)
                    spriteBatch.DrawString(_font, "player turn", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);
                else
                    spriteBatch.DrawString(_font, "enemy turn", new Vector2(2000, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);


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



