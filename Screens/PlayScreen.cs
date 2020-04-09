
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using VelcroPhysics.Dynamics;

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

        private bool select = false;

        public bool IsPaused { get; private set; }

        List<GameObject> _gameObjects;


        private Character player;
        private Character enemy;
        List<Character> enemyList;

        Texture2D _bg;
        Texture2D _char;
        Texture2D _bullet;
        Texture2D _arrow;
        Texture2D _gauge;
        Texture2D _pin;
        SpriteFont _font;

        float Rotation = 0f;

        World world;


        public enum TurnState
        {
            skill,
            angle,
            force,
            shoot,
            enemy
        }

        public TurnState CurrentTurnState;


        public PlayScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            _bg = content.Load<Texture2D>("sprites/bg");
            _bullet = content.Load<Texture2D>("sprites/ball");
            _pin = content.Load<Texture2D>("sprites/pin");
            _gauge = content.Load<Texture2D>("sprites/gauge");




            _arrow = content.Load<Texture2D>("sprites/arrow");
            _char = content.Load<Texture2D>("sprites/char");
            _font = content.Load<SpriteFont>("font/File");
            _gameObjects = new List<GameObject>();
            enemyList = new List<Character>();
            enemyIndex = 0;
            select = false;
            world = new World(Vector2.Zero);
            Set();

            _camera = new Camera();

        }

        private void Set()
        {
            player = new Character(_char)
            {
                Position = new Vector2(100, 650),
                Bullet = new Bullet(_bullet),
                InTurn = true
            };
            _gameObjects.Add(player);

            enemy = new Character(_char)
            {
                Position = new Vector2(2000, 650),
                Bullet = new Bullet(_bullet),
                IsPlayer = false,
                InTurn = false
            };
            _gameObjects.Add(enemy);
            enemyList.Add(enemy);


            enemy = new Character(_char)
            {
                Position = new Vector2(2200, 650),
                Bullet = new Bullet(_bullet),
                IsPlayer = false,
                InTurn = false
            };

            _gameObjects.Add(enemy);
            enemyList.Add(enemy);
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

                if (!select)
                {
                    CurrentTurnState = TurnState.skill;
                    select = true;
                }


                PlayerModule(gameTime);
            }
            else
            {
                select = false;
                EnemyModule(gameTime);
            }

            CheckColision();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }
        }

        private void CheckColision()
        {

        }

        private void EnemyModule(GameTime gameTime)
        {
            if (enemyIndex >= enemyList.Count)
            {
                player.InTurn = true;
            }

            else
            {
                _camera.Follow(enemyList[enemyIndex]);

                if (enemyList[enemyIndex].InTurn)
                {
                    enemyList[enemyIndex].Auto(gameTime,_gameObjects);
                }

                if (enemyList[enemyIndex].shooting)
                {
                    _camera.Follow(enemyList[enemyIndex].bullet);
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


        private void PlayerModule(GameTime gameTime)
        {
            enemyIndex = 0;
            
            _camera.Follow(player);


            switch (CurrentTurnState)
            {
                case TurnState.skill:
                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        CurrentTurnState = TurnState.angle;
                    }
                    break;
                case TurnState.angle:
                    {
                        Rotation += 0.1f;
                        if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                        {
                            CurrentTurnState = TurnState.force;
                        }
                    }
                    break;
                case TurnState.force:
                    if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                    {
                        player.Shoot(gameTime, _gameObjects);
                        CurrentTurnState = TurnState.shoot;
                        Rotation = 0;
                    }
                    break;
                case TurnState.shoot:
                    break;

            }

            if (player.shooting)
            {
                _camera.Follow(player.bullet);
            }
            else
            {
                enemyList[enemyIndex].InTurn = true;
            }
        }


        public void HandleInput(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                m_screenManager.Exit();
                //Pause();

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
                spriteBatch.DrawString(_font, "press f to skip", new Vector2(2000, (Singleton.SCREENHEIGHT + 100) / 2) - _font.MeasureString("Playing") / 2, Color.White);
            }

            spriteBatch.DrawString(_font, "press ESC to exit", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 3) - _font.MeasureString("press ESC to exit") / 2, Color.White);



            switch (CurrentTurnState)
            {
                case TurnState.skill:
                    spriteBatch.DrawString(_font, "skill state - press space ", player.Position + new Vector2(0, -130), Color.White);
                    break;
                case TurnState.angle:
                    spriteBatch.DrawString(_font, "angle state - press space ", player.Position + new Vector2(0, -130), Color.White);
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(0, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case TurnState.force:
                    spriteBatch.DrawString(_font, "force state - press space ", player.Position + new Vector2(0, -150), Color.White);
                    spriteBatch.DrawString(_font, "angle is " + Rotation, player.Position + new Vector2(0, -130), Color.White);
                    spriteBatch.Draw(_arrow, player.Position + new Vector2(0, -100), null, Color.White, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    spriteBatch.Draw(_gauge, player.Position + new Vector2(0, -100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    break;
                case TurnState.shoot:
                    spriteBatch.DrawString(_font, "shoot state", player.Position + new Vector2(0, -130), Color.White);
                    break;

            }

               


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



