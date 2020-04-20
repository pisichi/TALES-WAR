
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

        private bool select = false;
        private bool freecam = false;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;

        float waitTime = 0;

        private GameObject player;
        private GameObject boss;
        private GameObject enemy;
        private GameObject bullet;

        private GameObject cam;

        List<GameObject> enemyList;

        Texture2D _bg;
        Texture2D _char;

        

        Texture2D _hit;

        Texture2D _zeus;
        Texture2D _guan;
        Texture2D _thor;

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
            _thor = content.Load<Texture2D>("sprites/sheet_thor");

            _arrow = content.Load<Texture2D>("sprites/arrow");
            _font = content.Load<SpriteFont>("font/File");

            _hit = content.Load<Texture2D>("sprites/hitbox");

            _gameObjects = new List<GameObject>();
            enemyList = new List<GameObject>();
            enemyIndex = 0;

            select = false;

            Set();

            _camera = new Camera();
            cam = new GameObject(null, null, null, null)
            {
                Position = new Vector2(_bg.Width / 2, _bg.Height / 2),
            };

        }

        private void Set()
        {

            #region character
            player = new GameObject(new CharacterInputComponent(content),
                                                new CharacterPhysicComponent(),
                                                new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                    {
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_zeus, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_zeus, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_zeus, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_zeus, new Rectangle(0,1000,400,250),2) },
                                             { "Die", new Animation(_zeus, new Rectangle(200,1250,200,250),1) }
                                                    }),
                                                new ZeusSkillComponent())
            {
                Position = new Vector2(600, 800),
                InTurn = true,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "zeus",
                HP = 5,
                attack = 10,
                skill = 1

            };
            _gameObjects.Add(player);




            boss = new GameObject(new CharacterAIComponent(content),
                                   new CharacterPhysicComponent(),
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Idle", new Animation(_guan, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_guan, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_guan, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_guan, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_guan, new Rectangle(0,1000,400,250),2) },
                                            { "Die", new Animation(_guan, new Rectangle(200,1250,200,250),1) }
                                       }),
                                   new GuanSkillComponent())
            {
                Position = new Vector2(3400, 350),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "guan",
                HP = 1,
                attack = 1
            };
            _gameObjects.Add(boss);
            enemyList.Add(boss);

            enemy = new GameObject(new CharacterAIComponent(content),
                      new CharacterPhysicComponent(),
                      new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                          {
                                            { "Idle", new Animation(_guan, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_guan, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_guan, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_guan, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_guan, new Rectangle(0,1000,400,250),2) },
                                             { "Die", new Animation(_guan, new Rectangle(200,1250,200,250),1) }
                          }),
                      new GuanSkillComponent())
            {
                Position = new Vector2(1000, 700),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "guan",
                HP = 1,
                attack = 1
            };
            _gameObjects.Add(enemy);
            enemyList.Add(enemy);

            #endregion

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(400, 950),
                Viewport = new Rectangle(0, 0, 900, 50),
                Name = "floor",
                _hit = _hit
            }
            );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(1300, 900),
                Viewport = new Rectangle(0, 0, 800, 50),
                Name = "floor",
                _hit = _hit
            }
           );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(3380, 550),
                Viewport = new Rectangle(0, 0, 200, 150),
                Name = "floor",
                _hit = _hit
            }
           );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(2950, 300),
                Viewport = new Rectangle(0, 0, 200, 150),
                Name = "floor",
                _hit = _hit
            }
            );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(2900, 900),
                Viewport = new Rectangle(0, 0, 350, 150),
                Name = "floor",
                _hit = _hit
            });



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


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                freecam = !freecam;
            }


            if (freecam)
                testcam();
            else
                _camera.Follow(Singleton.Instance.follow);


            if (player.InTurn)
            {
                if (!select)
                {
                    
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
                else _gameObjects.Remove(_gameObjects[i]);
            }
        }

        private void testcam()
        {
            _camera.Follow(cam);

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right))
            {
                if (cam.Position.X < 4000 - Singleton.SCREENWIDTH / 2)
                    cam.Position.X += 10;
            }
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left))
            {
                if (cam.Position.X > Singleton.SCREENWIDTH / 2)
                    cam.Position.X -= 10;
            }
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up))
            {
                if (cam.Position.Y > Singleton.SCREENHEIGHT / 2)
                    cam.Position.Y -= 10;
            }
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down))
            {
                if (cam.Position.Y < 1000 - Singleton.SCREENHEIGHT / 2)
                    cam.Position.Y += 10;
            }
        }

        private void EnemyModule(GameTime gameTime)
        {
            if (enemyIndex >= enemyList.Count)
            {
                player.InTurn = true;
                Singleton.Instance.CurrentTurnState = Singleton.TurnState.skill;
            }
            else
            {
                

                if (enemyList[enemyIndex].InTurn)
                {
                    if(!enemyList[enemyIndex].shooting)
                        Singleton.Instance.follow = enemyList[enemyIndex];

                    waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (waitTime > 2)
                    {
                        enemyList[enemyIndex].action = true;
                       // enemyList[enemyIndex].InTurn = false;
                        waitTime = 0;
                    }
                }

                if (!enemyList[enemyIndex].InTurn || enemyList[enemyIndex].HP <= 0)
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

            if (Singleton.Instance.CurrentTurnState != Singleton.TurnState.shoot && Singleton.Instance.CurrentTurnState != Singleton.TurnState.enemy)
                Singleton.Instance.follow = player;

            switch (Singleton.Instance.CurrentTurnState)
            {
                case Singleton.TurnState.skill:
                    if (player.status == 1)
                    { Singleton.Instance.CurrentTurnState = Singleton.TurnState.shoot; }

                    else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
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
            spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 4000, 1000));


            switch (Singleton.Instance.CurrentTurnState)
            {
                case Singleton.TurnState.skill:
                    break;
                case Singleton.TurnState.angle:
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(120, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.force:
                    spriteBatch.DrawString(_font, "angle is " + Rotation, player.Position + new Vector2(0, -130), Color.White);
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(120, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    spriteBatch.Draw(_gauge, player.Position + new Vector2(120, -100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.shoot:
                    break;

            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].IsActive)
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



