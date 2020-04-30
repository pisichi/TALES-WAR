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
        private List<Color> nav_button_colorlist;
        private List<Vector2> nav_button_poslist;
        private List<Vector2> nav_button_scalelist;

        Texture2D _bg;
        SpriteFont _font;
        Texture2D _thor;
        Texture2D _zeus;
        Texture2D _KeyboardCursor;

        Rectangle Rectangle;

        private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private Keycursorstate keycursorstate;
        private bool isMouseActive;
        private enum Keycursorstate
        {
            Charater,
            Navigation,
            Size
        }

        private Vector2 KeyboardCursorPos;

        public SelectCharScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }

        public bool IsPaused { get; private set; }
        public void Init(ContentManager content)
        {
            _gameObjects = new List<GameObject>();
            nav_button_poslist = new List<Vector2>();
            nav_button_scalelist = new List<Vector2>();
            nav_button_colorlist = new List<Color>();

            _bg = content.Load<Texture2D>("sprites/select");
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
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,240),2) }
                                        }),
                                    null)
            {
                Position = _charPosition[0],
                InTurn = false,
                HP = 1,
                IsActive = false
            };
            thor = new GameObject(null,
                                    null,
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_thor, new Rectangle(0,0,400,240),2) }
                                        }),
                                    null)
            {
                Position = _charPosition[1],
                InTurn = false,
                HP = 1,
                IsActive = false
            };
            _gameObjects.Add(zeus);
            _gameObjects.Add(thor);

            nav_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2, 650));
            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));
            nav_button_colorlist.Add(Color.White);

            Singleton.Instance.level_s1 = 1;
            Singleton.Instance.level_s2 = 1;
            Singleton.Instance.level_s3 = 1;

            Singleton.Instance.CurrentStage = 0;

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
            spriteBatch.Draw(_bg, Vector2.Zero);
            //_animationManager.Draw(spriteBatch, new Vector2(Singleton.SCREENWIDTH / 2 - 300, Singleton.SCREENHEIGHT / 2 - 125), 0f, new Vector2(1, 1));

            spriteBatch.DrawString(_font,
                "Select character",
                 new Vector2(Singleton.SCREENWIDTH / 2 - 200, 100),
                 Color.White, 0, new Vector2(0, 0), new Vector2(2f, 2f), 0, 0);

            spriteBatch.DrawString(_font,
                                    "Back",
                                    nav_button_poslist[0],
                                    nav_button_colorlist[0], 0, _font.MeasureString("Back") / 2, nav_button_scalelist[0], 0, 0);

            if (isKeyboardCursorActive)
            {
                //spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(50, 50), 0, new Vector2(2.5f, 3.5f), Color.Red, 0);
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                    case 1:
                        spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(_KeyboardCursor.Width / 2, _KeyboardCursor.Height / 2), 0, new Vector2(2.2f, 3.5f), Color.Red, 0);
                        break;
                    case 2:
                        spriteBatch.DrawString(_font,
                                    "Back",
                                    nav_button_poslist[0],
                                    Color.Red, 0, _font.MeasureString("Back") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        break;
                }
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.DrawString(_font,
                 Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y + "   Res: " + Singleton.SCREENWIDTH + "x" + Singleton.SCREENHEIGHT,
                 new Vector2(1, Singleton.SCREENHEIGHT - 20),
                 Color.White, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), 0, 0);
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
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter++;
                /*if (keyboardCursorPosCounter > skill_button_poslist.Count - 1 && keycursorstate == Keycursorstate.Skill)
                {
                    keyboardCursorPosCounter = 0;
                    KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                    Console.WriteLine("Skill Right key Pos: "+ keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                    Console.WriteLine("State = " + (int)keycursorstate);
                }
                else if(keyboardCursorPosCounter > (nav_button_poslist.Count - 1) + (skill_button_poslist.Count - 1) && keycursorstate == Keycursorstate.Navigation)
                {
                    keyboardCursorPosCounter = 3;
                    KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                    Console.WriteLine("Nav Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                    Console.WriteLine("State = " + (int)keycursorstate);
                }*/
                switch (keycursorstate)
                {
                    case Keycursorstate.Charater:
                        if (keyboardCursorPosCounter > _gameObjects.Count - 1)
                        {
                            keyboardCursorPosCounter = 0;
                        }
                        KeyboardCursorPos = _gameObjects[keyboardCursorPosCounter].Position;

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter >= nav_button_poslist.Count + _gameObjects.Count)
                        {
                            keyboardCursorPosCounter = _gameObjects.Count;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - _gameObjects.Count];

                        break;
                }
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                isKeyboardCursorActive = true;

                keyboardCursorPosCounter--;
                /*if (keyboardCursorPosCounter < 0 && keycursorstate == Keycursorstate.Skill)
                {
                    keyboardCursorPosCounter = skill_button_poslist.Count - 1;
                    KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                    Console.WriteLine("Skill Left key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                    Console.WriteLine("State = " + (int)keycursorstate);
                }
                else if (keyboardCursorPosCounter < 3 && keycursorstate == Keycursorstate.Navigation)
                {
                    keyboardCursorPosCounter = (nav_button_poslist.Count - 1) + (skill_button_poslist.Count - 1);
                    KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                    Console.WriteLine("Nav Left key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                    Console.WriteLine("State = " + (int)keycursorstate);
                }*/
                switch (keycursorstate)
                {
                    case Keycursorstate.Charater:
                        if (keyboardCursorPosCounter < 0)
                        {
                            keyboardCursorPosCounter = _gameObjects.Count - 1;
                        }
                        KeyboardCursorPos = _gameObjects[keyboardCursorPosCounter].Position;
                        Console.WriteLine("Skill Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                        Console.WriteLine("State = " + (int)keycursorstate);

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter < _gameObjects.Count)
                        {
                            keyboardCursorPosCounter = nav_button_poslist.Count + _gameObjects.Count - 1;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - _gameObjects.Count];

                        break;
                }

            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Charater)

            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = _gameObjects.Count;
                KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - _gameObjects.Count];
                keycursorstate = Keycursorstate.Navigation;
            }
            else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Navigation
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = 0;
                KeyboardCursorPos = _gameObjects[keyboardCursorPosCounter].Position;
                keycursorstate = Keycursorstate.Charater;
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Charater
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = _gameObjects.Count;
                KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - _gameObjects.Count];
                keycursorstate = Keycursorstate.Navigation;
            }
            else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Navigation
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = 0;
                KeyboardCursorPos = _gameObjects[keyboardCursorPosCounter].Position;
                keycursorstate = Keycursorstate.Charater;
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && isKeyboardCursorActive)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        Singleton.Instance.CurrentHero = "zeus";
                        _gameObjects.Remove(zeus);
                        _gameObjects.Remove(thor);
                        m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                        break;

                    case 1:
                        Singleton.Instance.CurrentHero = "thor";
                        _gameObjects.Remove(zeus);
                        _gameObjects.Remove(thor);
                        m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                        break;
                    case 2:
                        _gameObjects.Remove(zeus);
                        _gameObjects.Remove(thor);
                        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                        break;
                }

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
                    zeus.Scale = new Vector2(1.1f, 1.1f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
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
                    thor.Scale = new Vector2(1.1f, 1.1f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    Singleton.Instance.CurrentHero = "thor";
                    _gameObjects.Remove(zeus);
                    _gameObjects.Remove(thor);
                    m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                }
            }
            else thor.Scale = Vector2.One;

            if (Singleton.Instance._currentmouse.Position.X > nav_button_poslist[0].X - _font.MeasureString("Back").X
                    && Singleton.Instance._currentmouse.Position.X < nav_button_poslist[0].X + _font.MeasureString("Back").X
                    && Singleton.Instance._currentmouse.Position.Y > nav_button_poslist[0].Y - _font.MeasureString("Back").Y
                    && Singleton.Instance._currentmouse.Position.Y < nav_button_poslist[0].Y + _font.MeasureString("Back").Y && isMouseActive)
            //Back button
            {
                KeyboardCursorPos = nav_button_poslist[0];
                nav_button_colorlist[0] = Color.Red;
                nav_button_scalelist[0] = new Vector2(1.7f, 1.7f);
                keycursorstate = Keycursorstate.Navigation;
                keyboardCursorPosCounter = _gameObjects.Count;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[0] = Color.OrangeRed;
                    nav_button_scalelist[0] = new Vector2(1.6f, 1.6f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[0] = Color.Red;

                    //Do when click Back button
                    m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                }
            }
            else
            {
                nav_button_colorlist[0] = Color.White;
                nav_button_scalelist[0] = new Vector2(1.5f, 1.5f);
            }
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }

        }
    }
}