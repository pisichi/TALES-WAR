using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Assignment
{
    class SelectCharScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        //private bool m_exitGame;
        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        private GameObject zeus;
        private GameObject thor;
        private List<GameObject> _gameObjects;
        private List<Vector2> _charPosition;

        Texture2D _bg;
        SpriteFont _font;
        Texture2D _thor;
        Texture2D _zeus;
        Texture2D _KeyboardCursor;

        Rectangle Rectangle;

        private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private bool isMouseActive;

        private Vector2 KeyboardCursorPos;

        public SelectCharScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }

        public bool IsPaused { get; private set; }
        public void Init(ContentManager content)
        {
            _gameObjects = new List<GameObject>();
            _bg = content.Load<Texture2D>("sprites/stage_bg");
            _font = content.Load<SpriteFont>("font/File");
            _thor = content.Load<Texture2D>("sprites/sheet_thor");
            _zeus = content.Load<Texture2D>("sprites/sheet_zeus");
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");
            _charPosition = new List<Vector2>();
            _charPosition.Add(new Vector2(Singleton.SCREENWIDTH / 2 - 150, Singleton.SCREENHEIGHT / 2));
            _charPosition.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 150, Singleton.SCREENHEIGHT / 2));
            zeus = new GameObject(null,
                                    null,
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,250),2) }
                                        }),
                                    null)
            {
                Position = _charPosition[0],
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                /*_hit = _hit,
                Name = Singleton.Instance.CurrentHero,*/
                /*Weapon = "hammer",*/
                HP = 1,
                attack = 1,
            };
            thor = new GameObject(null,
                                    null,
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_thor, new Rectangle(0,0,400,250),2) }
                                        }),
                                    null)
            {
                Position = _charPosition[1],
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                /*_hit = _hit,
                Name = "guan",*/
                /* Weapon = "lance",*/
                HP = 1,
                attack = 1
            };
            _gameObjects.Add(zeus);
            _gameObjects.Add(thor);

            KeyboardCursorPos = _charPosition[0];
        }
        public void ChangeBetweenScreen()
        {
        }

        public void Dispose()
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 3000, 800), color: Color.Blue);
            //_animationManager.Draw(spriteBatch, new Vector2(Singleton.SCREENWIDTH / 2 - 300, Singleton.SCREENHEIGHT / 2 - 125), 0f, new Vector2(1, 1));
            if (isKeyboardCursorActive)
                spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(50, 50), 0, new Vector2(2.5f, 3.5f), Color.Red, 0);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.DrawString(_font,
                    Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
                    new Vector2(200, 200),
                    Color.White);
            spriteBatch.End();
        }

        public void HandleInput(GameTime gameTime)
        {
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Back) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                isKeyboardCursorActive = true;//Keyboard detect

                keyboardCursorPosCounter++;
                if (keyboardCursorPosCounter > _charPosition.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = _charPosition[keyboardCursorPosCounter];


                //Console.WriteLine(keyboardCursorPosCounter);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                isKeyboardCursorActive = true;//Keyboard detect

                keyboardCursorPosCounter--;
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = _charPosition.Count - 1;
                KeyboardCursorPos = _charPosition[keyboardCursorPosCounter];
                //Console.WriteLine(keyboardCursorPosCounter);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && isKeyboardCursorActive)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        Singleton.Instance.CurrentHero = "zeus";
                        break;

                    case 1:
                        Singleton.Instance.CurrentHero = "thor";
                        break;
                }
                _gameObjects.Remove(zeus);
                _gameObjects.Remove(thor);
                m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
            }
        }



        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Update(GameTime gameTime)
        {
            Singleton.Instance._previouskey = Singleton.Instance._currentkey;
            Singleton.Instance._currentkey = Keyboard.GetState();
            Singleton.Instance._previousmouse = Singleton.Instance._currentmouse;
            Singleton.Instance._currentmouse = Mouse.GetState();
            // _animationManager.Update(gameTime);

            //Mouse and Keyboard Detect
            if (Singleton.Instance._currentmouse.Position != Singleton.Instance._previousmouse.Position || Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed || !isKeyboardCursorActive)
            {
                isMouseActive = true;
                isKeyboardCursorActive = false;
            }
            else isMouseActive = false;
            //End Mouse and Keyboard Detect

            if (Singleton.Instance._currentmouse.Position.X > 350 && Singleton.Instance._currentmouse.Position.X < 520 &&
                Singleton.Instance._currentmouse.Position.Y > 280 && Singleton.Instance._currentmouse.Position.Y < 520 && isMouseActive)
            {
                zeus.Scale = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = _charPosition[0];
                keyboardCursorPosCounter = 0;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    Singleton.Instance.CurrentHero = "zeus";
                    _gameObjects.Remove(zeus);
                    _gameObjects.Remove(thor);
                    m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                }
            }
            else zeus.Scale = Vector2.One;
            if (Singleton.Instance._currentmouse.Position.X > 680 && Singleton.Instance._currentmouse.Position.X < 840 &&
                Singleton.Instance._currentmouse.Position.Y > 280 && Singleton.Instance._currentmouse.Position.Y < 520 && isMouseActive)
            {
                thor.Scale = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = _charPosition[1];
                keyboardCursorPosCounter = 1;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    Singleton.Instance.CurrentHero = "thor";
                    _gameObjects.Remove(zeus);
                    _gameObjects.Remove(thor);
                    m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                }
            }
            else thor.Scale = Vector2.One;
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }

        }
    }
}