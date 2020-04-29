using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Assignment
{
    class UpgradeScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
        private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private Keycursorstate keycursorstate;
        private bool isMouseActive;
        private enum Keycursorstate
        {
            Skill,
            Navigation,
            Size
        }

        public bool IsPaused { get; private set; }

        private GameObject selectedChar;
        private List<GameObject> _gameObjects;
        private List<Vector2> skill_button_poslist;
        private List<Vector2> nav_button_poslist;
        private List<Vector2> skill_button_scalelist;
        private List<Vector2> nav_button_scalelist;
        private List<Texture2D> skill_button_texturelist;
        private List<Color> nav_button_colorlist;
        private List<Vector2> _addskill_button_scalelist;
        //ContentManager content;

        Texture2D _bg;
        Texture2D _bg2;
        SpriteFont _font;
        Texture2D _KeyboardCursor;
        Texture2D _selectedChar;
        Texture2D _skillwindow;
        Texture2D _addskill;

        private Vector2 KeyboardCursorPos;
        public UpgradeScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {

            _gameObjects = new List<GameObject>();
            skill_button_texturelist = new List<Texture2D>();
            skill_button_scalelist = new List<Vector2>();
            skill_button_poslist = new List<Vector2>();
            nav_button_scalelist = new List<Vector2>();
            nav_button_poslist = new List<Vector2>();
            nav_button_colorlist = new List<Color>();
            _addskill_button_scalelist = new List<Vector2>();
            _bg2 = content.Load<Texture2D>("sprites/fram1");
            _font = content.Load<SpriteFont>("font/File");
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");
            _skillwindow = content.Load<Texture2D>("sprites/newmainskill");
            _addskill = content.Load<Texture2D>("sprites/newplus");
            Singleton.Instance.CurrentStage += 1;
            switch (Singleton.Instance.CurrentHero)
            {
                case "zeus":
                    _bg = content.Load<Texture2D>("sprites/z1");
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_zeus");
                    selectedChar = new GameObject(null,
                                   null,
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,240),2) }
                                       }),
                                   null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        HP = 1,
                        IsActive = false
                    };
                    _gameObjects.Add(selectedChar);

                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_1"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_2"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_3"));
                    break;

                case "thor":
                    _bg = content.Load<Texture2D>("sprites/t1");
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_thor");
                    selectedChar = new GameObject(null,
                                            null,
                                            new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,240),2) }
                                                }),
                                            null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        HP = 1,
                        IsActive = false
                    };
                    _gameObjects.Add(selectedChar);

                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_1"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_2"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_3"));
                    break;
            }

            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);

            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 - 50, Singleton.SCREENHEIGHT / 2));
            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 200, Singleton.SCREENHEIGHT / 2));
            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 450, Singleton.SCREENHEIGHT / 2));

            _addskill_button_scalelist.Add(Vector2.One);
            _addskill_button_scalelist.Add(Vector2.One);
            _addskill_button_scalelist.Add(Vector2.One);

            nav_button_poslist.Add(new Vector2(100, Singleton.SCREENHEIGHT - 150));
            nav_button_poslist.Add(new Vector2(300, Singleton.SCREENHEIGHT - 150));

            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));
            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));

            nav_button_colorlist.Add(Color.White);
            nav_button_colorlist.Add(Color.White);


            KeyboardCursorPos = skill_button_poslist[0];
            isKeyboardCursorActive = false;

            isMouseActive = false;

            keycursorstate = Keycursorstate.Skill;
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

            Singleton.Instance._previousmouse = Singleton.Instance._currentmouse;
            Singleton.Instance._currentmouse = Mouse.GetState();
            //Mouse and Keyboard Detect
            if (Singleton.Instance._currentmouse.Position != Singleton.Instance._previousmouse.Position || Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed || !isKeyboardCursorActive)
            {
                isMouseActive = true;
                isKeyboardCursorActive = false;
            }
            else isMouseActive = false;
            //End Mouse and Keyboard Detect
            if (Singleton.Instance._currentmouse.Position.X > skill_button_poslist[0].X - _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.X < skill_button_poslist[0].X + _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.Y > skill_button_poslist[0].Y - _skillwindow.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < skill_button_poslist[0].Y + _skillwindow.Height / 2 && isMouseActive)
            //skill1 button
            {
                skill_button_scalelist[0] = new Vector2(1.2f, 1.2f);
                _addskill_button_scalelist[0] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[0];
                keycursorstate = Keycursorstate.Skill;
                keyboardCursorPosCounter = 0;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    //skill_button_scalelist[0] = new Vector2(1.1f, 1.1f);
                    _addskill_button_scalelist[0] = new Vector2(1.1f, 1.1f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    //Do when click skill1 button
                    //m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else
            {
                skill_button_scalelist[0] = Vector2.One;
                _addskill_button_scalelist[0] = Vector2.One;
            }

            if (Singleton.Instance._currentmouse.Position.X > skill_button_poslist[1].X - _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.X < skill_button_poslist[1].X + _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.Y > skill_button_poslist[1].Y - _skillwindow.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < skill_button_poslist[1].Y + _skillwindow.Height / 2 && isMouseActive)
            //skill2 button
            {
                skill_button_scalelist[1] = new Vector2(1.2f, 1.2f);
                _addskill_button_scalelist[1] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[1];
                keycursorstate = Keycursorstate.Skill;
                keyboardCursorPosCounter = 1;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    //skill_button_scalelist[1] = new Vector2(1.1f, 1.1f);
                    _addskill_button_scalelist[1] = new Vector2(1.1f, 1.1f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    //Do when click skill2 button
                    //m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else
            {
                skill_button_scalelist[1] = Vector2.One;
                _addskill_button_scalelist[1] = Vector2.One;
            }

            if (Singleton.Instance._currentmouse.Position.X > skill_button_poslist[2].X - _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.X < skill_button_poslist[2].X + _skillwindow.Width / 2
                    && Singleton.Instance._currentmouse.Position.Y > skill_button_poslist[2].Y - _skillwindow.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < skill_button_poslist[2].Y + _skillwindow.Height / 2 && isMouseActive)
            //skill3 button
            {
                skill_button_scalelist[2] = new Vector2(1.2f, 1.2f);
                _addskill_button_scalelist[2] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[2];
                keycursorstate = Keycursorstate.Skill;
                keyboardCursorPosCounter = 2;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    //skill_button_scalelist[2] = new Vector2(1.1f, 1.1f);
                    _addskill_button_scalelist[2] = new Vector2(1.1f, 1.1f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    //Do when click skill3 button
                    //m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else
            {
                skill_button_scalelist[2] = Vector2.One;
                _addskill_button_scalelist[2] = Vector2.One;
            }

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
                keyboardCursorPosCounter = skill_button_poslist.Count;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[0] = Color.OrangeRed;
                    nav_button_scalelist[0] = new Vector2(1.6f, 1.6f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[0] = Color.Red;
                    //Do when click Back button
                    m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                }
            }
            else
            {
                nav_button_colorlist[0] = Color.White;
                nav_button_scalelist[0] = new Vector2(1.5f, 1.5f);
            }

            if (Singleton.Instance._currentmouse.Position.X > nav_button_poslist[1].X - _font.MeasureString("Start").X
                    && Singleton.Instance._currentmouse.Position.X < nav_button_poslist[1].X + _font.MeasureString("Start").X
                    && Singleton.Instance._currentmouse.Position.Y > nav_button_poslist[1].Y - _font.MeasureString("Start").Y
                    && Singleton.Instance._currentmouse.Position.Y < nav_button_poslist[1].Y + _font.MeasureString("Start").Y && isMouseActive)
            //Start button
            {
                KeyboardCursorPos = nav_button_poslist[0];
                nav_button_colorlist[1] = Color.Red;
                nav_button_scalelist[1] = new Vector2(1.7f, 1.7f);
                keycursorstate = Keycursorstate.Navigation;
                keyboardCursorPosCounter = skill_button_poslist.Count + 1;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[1] = Color.OrangeRed;
                    nav_button_scalelist[1] = new Vector2(1.6f, 1.6f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    nav_button_colorlist[1] = Color.Red;
                    //Do when click Start button
                    m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else
            {
                nav_button_colorlist[1] = Color.White;
                nav_button_scalelist[1] = new Vector2(1.5f, 1.5f);
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }


        }

        public void HandleInput(GameTime gameTime)
        {

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
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
                    case Keycursorstate.Skill:
                        if (keyboardCursorPosCounter > skill_button_poslist.Count - 1)
                        {
                            keyboardCursorPosCounter = 0;
                        }
                        KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                        Console.WriteLine("Skill Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                        Console.WriteLine("State = " + (int)keycursorstate);

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter >= nav_button_poslist.Count + skill_button_poslist.Count)
                        {
                            keyboardCursorPosCounter = skill_button_poslist.Count;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                        Console.WriteLine("Nav Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + (nav_button_poslist.Count + skill_button_poslist.Count));
                        Console.WriteLine("State = " + (int)keycursorstate);

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
                    case Keycursorstate.Skill:
                        if (keyboardCursorPosCounter < 0)
                        {
                            keyboardCursorPosCounter = skill_button_poslist.Count - 1;
                        }
                        KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                        Console.WriteLine("Skill Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                        Console.WriteLine("State = " + (int)keycursorstate);

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter < skill_button_poslist.Count)
                        {
                            keyboardCursorPosCounter = nav_button_poslist.Count + skill_button_poslist.Count - 1;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                        Console.WriteLine("Nav Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + ((nav_button_poslist.Count - 1) + (skill_button_poslist.Count - 1)));
                        Console.WriteLine("State = " + (int)keycursorstate);

                        break;
                }

            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Skill)

            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = 3;
                KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                keycursorstate = Keycursorstate.Navigation;
                Console.WriteLine("Nav Down key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                Console.WriteLine("State = " + (int)keycursorstate);
            }
            else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Navigation
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = 0;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                keycursorstate = Keycursorstate.Skill;
                Console.WriteLine("Skill Down key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                Console.WriteLine("State = " + (int)keycursorstate);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Skill
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = skill_button_poslist.Count;
                KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                keycursorstate = Keycursorstate.Navigation;
                Console.WriteLine("Nav Up key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                Console.WriteLine("State = " + (int)keycursorstate);
            }
            else if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey
                && keycursorstate == Keycursorstate.Navigation
                )
            {
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter = 0;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                keycursorstate = Keycursorstate.Skill;
                Console.WriteLine("Skill Up key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                Console.WriteLine("State = " + (int)keycursorstate);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Back) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                m_screenManager.Exit();

            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0://skill1
                        //Do when enter skill1 button
                        break;

                    case 1://skill2
                        //Do when enter skill2 button
                        break;

                    case 2://skill3
                        //Do when enter skill3 button
                        break;
                    case 3://Back
                        //Do when enter Back button
                        m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                        break;
                    case 4://Start
                        //Do when enter Start button
                        m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                        break;

                }
                //m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero);
            spriteBatch.Draw(_bg2, Vector2.Zero);
            spriteBatch.DrawString(_font, "press A to Continue", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("press A to Continue") / 2, Color.White);

            /*spriteBatch.Draw(_skill1, skill_button_poslist[0], null, null, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 0, skill_button_scalelist[0], null, 0);
            spriteBatch.Draw(_skill2, skill_button_poslist[1], null, null, new Vector2(_skill2.Width / 2, _skill2.Height / 2), 0, skill_button_scalelist[1], null, 0);
            spriteBatch.Draw(_skill3, skill_button_poslist[2], null, null, new Vector2(_skill3.Width / 2, _skill3.Height / 2), 0, skill_button_scalelist[2], null, 0);
*/

            for (int i = 0; i <= skill_button_texturelist.Count - 1; i++)
            {
                spriteBatch.Draw(_skillwindow, skill_button_poslist[i], null, null, new Vector2(_skillwindow.Width / 2, _skillwindow.Height / 2), 0, Vector2.One, null, 0);
                spriteBatch.Draw(skill_button_texturelist[i], new Vector2(skill_button_poslist[i].X, skill_button_poslist[i].Y - 75), null, null, new Vector2(skill_button_texturelist[i].Width / 2, skill_button_texturelist[i].Height / 2), 0, skill_button_scalelist[i], null, 0);
                spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[i].X, skill_button_poslist[i].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, _addskill_button_scalelist[i], null, 0);
            }

            spriteBatch.DrawString(_font,
                    "Back",
                    nav_button_poslist[0],
                    nav_button_colorlist[0], 0, _font.MeasureString("Back") / 2, nav_button_scalelist[0], 0, 0);
            spriteBatch.DrawString(_font,
                    "Start",
                    nav_button_poslist[1],
                    nav_button_colorlist[1], 0, _font.MeasureString("Start") / 2, nav_button_scalelist[1], 0, 0);

            if (isKeyboardCursorActive)
            {
                /*if (keyboardCursorPosCounter <= 2)
                    spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(_KeyboardCursor.Width / 2, _KeyboardCursor.Height / 2), 0, new Vector2(2.2f, 3.5f), Color.Red, 0);
                else if (keyboardCursorPosCounter <= 4)
                {*/
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                    case 1:
                    case 2:
                        spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(_KeyboardCursor.Width / 2, _KeyboardCursor.Height / 2), 0, new Vector2(2.2f, 3.5f), Color.Red, 0);
                        break;
                    case 3:
                        spriteBatch.DrawString(_font,
                                    "Back",
                                    nav_button_poslist[0],
                                    Color.Red, 0, _font.MeasureString("Back") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        break;
                    case 4:
                        spriteBatch.DrawString(_font,
                        "Start",
                        nav_button_poslist[1],
                        Color.Red, 0, _font.MeasureString("Start") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        break;
                }


                //}

            }


            spriteBatch.DrawString(_font,
                    Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
                    new Vector2(1, Singleton.SCREENHEIGHT - 20),
                    Color.White, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), 0, 0);

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
    }
}