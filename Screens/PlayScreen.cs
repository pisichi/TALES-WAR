
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

        private bool swap;

        private bool select = false;
        private bool freecam = false;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;

        float waitTime = 0;
        

        private GameObject player;
        private GameObject boss;
        private GameObject enemy;

        private GameObject cam;

        List<GameObject> enemyList;

        Texture2D _bg;
        Texture2D _sky;
        Texture2D _platform;

        Texture2D _hit;
        Texture2D _arrow;
        Texture2D _gauge;
        Texture2D _pin;

        Texture2D _skill1;
        Texture2D _skill2;

        SpriteFont _font;

        Texture2D _player;
        Texture2D _guan;
        Texture2D _sung;
        Texture2D _mob;





        private ContentManager content;

        public PlayScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            this.content = content;

            _gameObjects = new List<GameObject>();
            enemyList = new List<GameObject>();
            _camera = new Camera();

            _bg = content.Load<Texture2D>("sprites/stage_bg");
            _pin = content.Load<Texture2D>("sprites/pin");
            _gauge = content.Load<Texture2D>("sprites/gauge");
            _arrow = content.Load<Texture2D>("sprites/arrow");
            _font = content.Load<SpriteFont>("font/File");
            _hit = content.Load<Texture2D>("sprites/hitbox");

            _guan = content.Load<Texture2D>("sprites/sheet_guan");
            _sung = content.Load<Texture2D>("sprites/sheet_sung");
            _mob = content.Load<Texture2D>("sprites/sheet_mob");


            cam = new GameObject(null, null, null, null)
            {
                Position = new Vector2(_bg.Width / 2, _bg.Height / 2),
            };

            SkillComponent skill = null;
            String _weapon = null;

            if (Singleton.Instance.CurrentHero == "zeus")
            {
                _player = content.Load<Texture2D>("sprites/sheet_zeus");
                _skill1 = content.Load<Texture2D>("sprites/skill_zeus_1");
                _skill2 = content.Load<Texture2D>("sprites/skill_zeus_2");
                skill = new ZeusSkillComponent();
                _weapon = "thunder";
            }
            else if (Singleton.Instance.CurrentHero == "thor")
            {
                _player = content.Load<Texture2D>("sprites/sheet_thor");
                _skill1 = content.Load<Texture2D>("sprites/skill_thor_1");
                _skill2 = content.Load<Texture2D>("sprites/skill_thor_2");
                skill = new ThorSkillComponent();
                _weapon = "hammer";
            }

            player = new GameObject(new CharacterInputComponent(content),
                                    new CharacterPhysicComponent(),
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_player, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_player, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_player, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_player, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_player, new Rectangle(0,1000,400,250),2) },
                                             { "Die", new Animation(_player, new Rectangle(200,1250,200,250),1) }
                                        }),
                                    skill)
            {
                Position = new Vector2(600, 800),
                InTurn = true,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = Singleton.Instance.CurrentHero,
                Weapon = _weapon,
                HP = 5,
                attack = 1,
            };
            _gameObjects.Add(player);



            enemyIndex = 0;

            select = false;

            Singleton.Instance.CurrentTurnState = Singleton.TurnState.skill;

            if (Singleton.Instance.CurrentStage == 1)
            {
                _sky = content.Load<Texture2D>("sprites/stage_sky_1");
                _platform = content.Load<Texture2D>("sprites/stage_platform_1");
                SetStage1();
            }
            else if (Singleton.Instance.CurrentStage == 2)
            {
                _sky = content.Load<Texture2D>("sprites/stage_sky_2");
                _platform = content.Load<Texture2D>("sprites/stage_platform_2");
                SetStage2();
            }

        }

        private void SetStage1()
        {
           
            #region character


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
                Weapon = "lance",
                HP = 10,
                attack = 1
                
            };
            _gameObjects.Add(boss);
            enemyList.Add(boss);

            enemy = new GameObject(new CharacterAIComponent(content),
                      new CharacterPhysicComponent(),
                      new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                          {
                                            { "Idle", new Animation(_mob, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_mob, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_mob, new Rectangle(0,0,400,250),2) },
                                            { "Hit", new Animation(_mob, new Rectangle(0,500,200,250),1) },
                                            { "Stunt", new Animation(_mob, new Rectangle(0,750,400,250),2) },
                                             { "Die", new Animation(_mob, new Rectangle(200,1000,200,250),1) }
                          }),
                      new MobSkillComponent())
            {
                Position = new Vector2(1000, 700),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "mob",
                Weapon = "rock",
                HP = 3,
                attack = 1
            };
            _gameObjects.Add(enemy);
            enemyList.Add(enemy);



            enemy = new GameObject(new CharacterAIComponent(content),
                     new CharacterPhysicComponent(),
                     new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                         {
                                            { "Idle", new Animation(_mob, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_mob, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_mob, new Rectangle(0,0,400,250),2) },
                                            { "Hit", new Animation(_mob, new Rectangle(0,500,200,250),1) },
                                            { "Stunt", new Animation(_mob, new Rectangle(0,750,400,250),2) },
                                             { "Die", new Animation(_mob, new Rectangle(200,1000,200,250),1) }
                         }),
                     new MobSkillComponent())
            {
                Position = new Vector2(2000, 700),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "mob",
                Weapon = "rock",
                HP = 3,
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
                Position = new Vector2(3800, 400),
                Viewport = new Rectangle(0, 0, 200, 150),
                Name = "floor",
                _hit = _hit
            }
           );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(3200, 650),
                Viewport = new Rectangle(0, 0, 200, 150),
                Name = "floor",
                _hit = _hit
            }
            );

            _gameObjects.Add(new GameObject(null, null, null, null)
            {
                Position = new Vector2(2420, 750),
                Viewport = new Rectangle(0, 0, 350, 150),
                Name = "floor",
                _hit = _hit
            });



            Singleton.Instance.follow = player;
        }

        private void SetStage2()
        {

            #region character
            boss = new GameObject(new CharacterAIComponent(content),
                                  new CharacterPhysicComponent(),
                                  new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                      {
                                            { "Idle", new Animation(_sung, new Rectangle(0,0,400,250),2) },
                                            { "Throw", new Animation(_sung, new Rectangle(0,250,400,250),2) },
                                            { "Skill", new Animation(_sung, new Rectangle(0,500,400,250),2) },
                                            { "Hit", new Animation(_sung, new Rectangle(0,750,200,250),1) },
                                            { "Stunt", new Animation(_sung, new Rectangle(0,1000,400,250),2) },
                                            { "Die", new Animation(_sung, new Rectangle(200,1250,200,250),1) }
                                      }),
                                  new SungSkillComponent())
            {
                Position = new Vector2(3400, 350),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                _hit = _hit,
                Name = "sung",
                Weapon = "bar",
                HP = 8,
                attack = 1
            };
            _gameObjects.Add(boss);
            enemyList.Add(boss);
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
                Position = new Vector2(3500, 670),
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

                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.NumPad1) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && Singleton.Instance.Cooldown_1 <= 0)
                    {
                        player.skill = 1;
                        Singleton.Instance.Cooldown_1 = 4;
                    }

                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.NumPad2) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && Singleton.Instance.Cooldown_2 <= 0)
                    {
                        player.skill = 2;
                        Singleton.Instance.Cooldown_2 = 5;
                    }

                    else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        player.Rotation = 0f;
                        Singleton.Instance.CurrentTurnState = Singleton.TurnState.angle;
                    }
                    break;
                case Singleton.TurnState.angle:
                    {


                        if (swap)
                        {
                            player.Rotation += 0.1f;
                        }
                        else
                        {
                            player.Rotation -= 0.1f;
                        }

                        if (player.Rotation < -3.25)
                        {
                            swap = true;
                        }
                        if (player.Rotation >= 0)
                        {
                            swap = false;
                        }


                        if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                        {
                            Singleton.Instance.CurrentTurnState = Singleton.TurnState.force;
                        }
                    }
                    break;
                case Singleton.TurnState.force:

                    if (swap)
                    {
                        player.force += 0.05f;
                    }
                    else
                    {
                        player.force -= 0.05f;
                    }

                    if (player.force <= 1)
                    {
                        swap = true;
                    }
                    if (player.force >= 3)
                    {
                        swap = false;
                    }

                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        player.action = true;
                        Singleton.Instance.CurrentTurnState = Singleton.TurnState.shoot;
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

        public void HandleInput(GameTime gameTime)
        {

            if ((Singleton.Instance._currentkey.IsKeyDown(Keys.L) && Singleton.Instance._currentkey != Singleton.Instance._previouskey) || player.HP <= 0 || boss.HP <= 0)
            {
                m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                m_screenManager.Exit();
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: _camera.Transform);
            spriteBatch.Draw(_sky, destinationRectangle: new Rectangle(0, 0, 4000, 1000));
            spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 4000, 1000));
            spriteBatch.Draw(_platform, destinationRectangle: new Rectangle(0, 0, 4000, 1000));


            switch (Singleton.Instance.CurrentTurnState)
            {
                case Singleton.TurnState.skill:
                    if (Singleton.Instance.Cooldown_1 <= 0)
                    {
                        spriteBatch.Draw(_skill1, player.Position + new Vector2(-80, -150), null, Color.White, 0, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 1f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(_skill1, player.Position + new Vector2(-80, -150), null, Color.Gray,0, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 1f, SpriteEffects.None, 0);
                    }

                    if (Singleton.Instance.Cooldown_2 <= 0)
                    {
                        spriteBatch.Draw(_skill2, player.Position + new Vector2(80, -150), null, Color.White, 0f, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 1f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(_skill2, player.Position + new Vector2(80, -150), null, Color.Gray, 0, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 1f, SpriteEffects.None, 0);
                    }
                    break;
                case Singleton.TurnState.angle:
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(120, -100), null, Color.White, player.Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.force:
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(120, -100), null, Color.White, player.Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    spriteBatch.Draw(_gauge, player.Position + new Vector2(120, -100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    spriteBatch.Draw(_pin, player.Position + new Vector2(120 + (player.force - 1) * 30, -100), null, Color.Red, 0f, Vector2.Zero, new Vector2(2, 0.5f), SpriteEffects.None, 0);
                    break;
                case Singleton.TurnState.shoot:
                    break;

            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].IsActive)
                    _gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.DrawString(_font,
                 "[console log] Res: " + Singleton.SCREENWIDTH + "x" + Singleton.SCREENHEIGHT + "  MousePos: " + Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
                 new Vector2(1, Singleton.SCREENHEIGHT - 20),
                 Color.White, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), 0, 0);

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



