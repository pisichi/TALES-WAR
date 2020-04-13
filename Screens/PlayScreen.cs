
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

        public bool isPlayerTurn = true;

        private bool select = false;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;


        private GameObject player;
        private GameObject boss;
        private GameObject enemy;
        private GameObject bullet;

        List<GameObject> enemyList;

        Texture2D _bg;
        Texture2D _char;


        Texture2D _zeus;
        Texture2D _guan;

        Texture2D _arrow;
        Texture2D _gauge;
        Texture2D _pin;
        SpriteFont _font;

        float Rotation = 0f;
        private ContentManager content;

        public PlayScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            this.content = content;

            _bg = content.Load<Texture2D>("sprites/bg");
            _pin = content.Load<Texture2D>("sprites/pin");
            _gauge = content.Load<Texture2D>("sprites/gauge");

            _zeus = content.Load<Texture2D>("sprites/sheet_zeus");
            _guan = content.Load<Texture2D>("sprites/sheet_guan");

            _arrow = content.Load<Texture2D>("sprites/arrow");
            _char = content.Load<Texture2D>("sprites/char");
            _font = content.Load<SpriteFont>("font/File");
            _gameObjects = new List<GameObject>();
            enemyList = new List<GameObject>();
            enemyIndex = 0;
            select = false;

            Set();

            _camera = new Camera();

        }

        private void Set()
        {
            player = new GameObject( new CharacterInputComponent(content),
                                                new CharacterPhysicComponent(),
                                                new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                    {
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_zeus, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_zeus, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_zeus, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_zeus, new Rectangle(0,1000,400,250),2) },
                                            { "Die", new Animation(_zeus, new Rectangle(0,1250,400,250),2) }
                                                    }))
            {
                Position = new Vector2(100, 600),
                InTurn = true,
                Viewport = new Rectangle(0, 0, 200, 250)
                

        };
            _gameObjects.Add(player);



            boss = new GameObject( null,
                                   new CharacterPhysicComponent(),
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_zeus, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_zeus, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_zeus, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_zeus, new Rectangle(0,1000,400,250),2) },
                                            { "Die", new Animation(_zeus, new Rectangle(0,1250,400,250),2) }
                                       }))
            {
                Position = new Vector2(600, 600),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 200, 250)
            };
            _gameObjects.Add(boss);
            enemyList.Add(boss);

            enemy = new GameObject(null,
                      new CharacterPhysicComponent(),
                      new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                          {
                                            { "Idle", new Animation(_guan, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_guan, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_guan, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_guan, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_guan, new Rectangle(0,1000,400,250),2) },
                                            { "Die", new Animation(_guan, new Rectangle(0,1250,400,250),2) }
                          }))
            {
                Position = new Vector2(300, 600),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 200, 250)
            };
            _gameObjects.Add(enemy);
            enemyList.Add(enemy);


            Singleton.Instance.follow = player;
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

            _camera.Follow(Singleton.Instance.follow);


            if (player.InTurn)
            {
                if (!select)
                {
                    Singleton.Instance.CurrentTurnState = Singleton.TurnState.skill;
                    select = true;
                }
                PlayerModule();
            }
            else
            {
                select = false;
                EnemyModule(gameTime);
            }


            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].IsActive)
                    _gameObjects[i].Update(gameTime, _gameObjects);
            }
        }

        private void EnemyModule(GameTime gameTime)
        {
            if (enemyIndex >= enemyList.Count)
            {
                player.InTurn = true;
            }
            else
            {
                Singleton.Instance.follow = enemyList[enemyIndex];

                if (enemyList[enemyIndex].InTurn)
                {
                    enemyList[enemyIndex].action = true;
                    enemyList[enemyIndex].InTurn = false;
                }

                if (enemyList[enemyIndex].shooting)
                {
                    //_camera.Follow(enemyList[enemyIndex].Child);
                }

                if (!enemyList[enemyIndex].InTurn)
                {
                    enemyIndex++;

                    if (enemyIndex < enemyList.Count)
                    {
                        enemyList[enemyIndex].InTurn = true;
                    }
                }

            }
        }


        private void PlayerModule()
        {
            enemyIndex = 0;

            if(Singleton.Instance.CurrentTurnState != Singleton.TurnState.shoot)
            Singleton.Instance.follow = player;

            switch (Singleton.Instance.CurrentTurnState)
            {
                case Singleton.TurnState.skill:
                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        player.Rotation = 0f;
                        Singleton.Instance.CurrentTurnState = Singleton.TurnState.angle;
                    }
                    break;
                case Singleton.TurnState.angle:
                    {
                        Rotation += 0.1f;
                        player.Rotation += 0.1f;
                        if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                        {
                            Singleton.Instance.CurrentTurnState = Singleton.TurnState.force;
                        }
                    }
                    break;
                case Singleton.TurnState.force:
                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        player.action = true;
                        Singleton.Instance.CurrentTurnState = Singleton.TurnState.shoot;
                        Rotation = 0;
                    }
                    break;

                case Singleton.TurnState.shoot:
                    break;
            }

            if (!player.shooting)
            {
                enemyList[enemyIndex].InTurn = true;
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

            spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 6000, 2000));

            if (player.InTurn)
                spriteBatch.DrawString(_font, "player turn", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);
            else
            {
                spriteBatch.DrawString(_font, "enemy turn " + enemyIndex, new Vector2(2000, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("Playing") / 2, Color.White);
                spriteBatch.DrawString(_font, "press f to skip", new Vector2(2000, (Singleton.SCREENHEIGHT + 100) / 2) - _font.MeasureString("Playing") / 2, Color.White);
            }

            spriteBatch.DrawString(_font, "press ESC to exit", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 3) - _font.MeasureString("press ESC to exit") / 2, Color.White);

            switch (Singleton.Instance.CurrentTurnState)
            {
                case Singleton.TurnState.skill:
                    spriteBatch.DrawString(_font, "skill state - press space ", player.Position + new Vector2(0, -130), Color.White);
                    break;
                case Singleton.TurnState.angle:
                    spriteBatch.DrawString(_font, "angle state - press space ", player.Position + new Vector2(0, -130), Color.White);
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(0, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.force:
                    spriteBatch.DrawString(_font, "force state - press space ", player.Position + new Vector2(0, -150), Color.White);
                    spriteBatch.DrawString(_font, "angle is " + Rotation, player.Position + new Vector2(0, -130), Color.White);
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(0, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    spriteBatch.Draw(_gauge, player.Position + new Vector2(0, -100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.shoot:
                    spriteBatch.DrawString(_font, "shoot state", player.Position + new Vector2(0, -130), Color.White);
                    break;

            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if(_gameObjects[i].IsActive)
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



