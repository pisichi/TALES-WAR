
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
        private bool isMouseActive;

        public bool IsPaused { get; private set; }

        private GameObject selectedChar;
        private List<GameObject> _gameObjects;
        private List<Vector2> skill_button_poslist;
        private List<Vector2> skill_button_scalelist;
        private AnimationManager _animationManager;
        //ContentManager content;

        Texture2D _bg;
        SpriteFont _font;
        Texture2D _KeyboardCursor;
        Texture2D _selectedChar;
        Texture2D _skill1;
        Texture2D _skill2;
        Texture2D _skill3;


        private Vector2 skill1_button_scale;
        private Vector2 skill2_button_scale;
        private Vector2 skill3_button_scale;

        private Vector2 skill1_button_pos;
        private Vector2 skill2_button_pos;
        private Vector2 skill3_button_pos;

        private Vector2 KeyboardCursorPos;

        Rectangle Rectangle;
        public UpgradeScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
             
            _gameObjects = new List<GameObject>();
            skill_button_scalelist = new List<Vector2>();
            skill_button_poslist = new List<Vector2>();
            _bg = content.Load<Texture2D>("sprites/bg");
            _font = content.Load<SpriteFont>("font/File");
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");
            Singleton.Instance.CurrentStage += 1;
            switch (Singleton.Instance.CurrentHero)
            {
                case "zeus":
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_zeus");
                    selectedChar = new GameObject(null,
                                   null,
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,250),2) }
                                       }),
                                   null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        InTurn = false,
                        Viewport = new Rectangle(0, 0, 150, 230),
                        /*_hit = _hit,
                        Name = Singleton.Instance.CurrentHero,*/
                        /*Weapon = "hammer",*/
                        HP = 1,
                        attack = 1,
                    };
                    _gameObjects.Add(selectedChar);

                    _skill1 = content.Load<Texture2D>("sprites/skill_zeus_1");
                    _skill2 = content.Load<Texture2D>("sprites/skill_zeus_2");
                    _skill3 = content.Load<Texture2D>("sprites/skill_zeus_3");

                    break;

                case "thor":
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_thor");
                    selectedChar = new GameObject(null,
                                            null,
                                            new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,250),2) }
                                                }),
                                            null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        InTurn = false,
                        Viewport = new Rectangle(0, 0, 150, 230),
                        /*_hit = _hit,
                        Name = "guan",*/
                        /* Weapon = "lance",*/
                        HP = 1,
                        attack = 1
                    };
                    _gameObjects.Add(selectedChar);

                    _skill1 = content.Load<Texture2D>("sprites/skill_thor_1");
                    _skill2 = content.Load<Texture2D>("sprites/skill_thor_2");
                    _skill3 = content.Load<Texture2D>("sprites/skill_thor_3");

                    break;
            }
            skill1_button_scale = Vector2.One;
            skill2_button_scale = Vector2.One;
            skill3_button_scale = Vector2.One;
            skill_button_scalelist.Add(skill1_button_scale);
            skill_button_scalelist.Add(skill2_button_scale);
            skill_button_scalelist.Add(skill3_button_scale);

            skill1_button_pos = new Vector2(Singleton.SCREENWIDTH / 2 - 150, Singleton.SCREENHEIGHT / 2);
            skill2_button_pos = new Vector2(Singleton.SCREENWIDTH / 2 + 150, Singleton.SCREENHEIGHT / 2);
            skill3_button_pos = new Vector2(Singleton.SCREENWIDTH / 2 + 450, Singleton.SCREENHEIGHT / 2);
            skill_button_poslist.Add(skill1_button_pos);
            skill_button_poslist.Add(skill2_button_pos);
            skill_button_poslist.Add(skill3_button_pos);

            KeyboardCursorPos = skill1_button_pos;
            isKeyboardCursorActive = false;

            isMouseActive = false;
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
            if (Singleton.Instance._currentmouse.Position.X > (Singleton.SCREENWIDTH / 2 - _skill1.Width / 2) - 150
                    && Singleton.Instance._currentmouse.Position.X < (Singleton.SCREENWIDTH / 2 + _skill1.Width / 2) - 150
                    && Singleton.Instance._currentmouse.Position.Y > Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < Singleton.SCREENHEIGHT / 2 + _skill1.Height / 2 && isMouseActive)

            {
                skill_button_scalelist[0] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[0];
                keyboardCursorPosCounter = 0;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {

                }
            }
            else 
            {
                skill_button_scalelist[0] = Vector2.One; 
            }

            if (Singleton.Instance._currentmouse.Position.X > (Singleton.SCREENWIDTH / 2 - _skill1.Width / 2) + 150
                    && Singleton.Instance._currentmouse.Position.X < (Singleton.SCREENWIDTH / 2 + _skill1.Width / 2) + 150
                    && Singleton.Instance._currentmouse.Position.Y > Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < Singleton.SCREENHEIGHT / 2 + _skill1.Height / 2 && isMouseActive)

            {
                skill_button_scalelist[1] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[1];
                keyboardCursorPosCounter = 1;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {

                }
            }
            else
            {
                skill_button_scalelist[1] = Vector2.One;
            }

            if (Singleton.Instance._currentmouse.Position.X > (Singleton.SCREENWIDTH / 2 - _skill1.Width / 2) + 450
                    && Singleton.Instance._currentmouse.Position.X < (Singleton.SCREENWIDTH / 2 + _skill1.Width / 2) + 450
                    && Singleton.Instance._currentmouse.Position.Y > Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2
                    && Singleton.Instance._currentmouse.Position.Y < Singleton.SCREENHEIGHT / 2 + _skill1.Height / 2 && isMouseActive)

            {
                skill_button_scalelist[2] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[2];
                keyboardCursorPosCounter = 2;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {

                }
            }
            else
            {
                skill_button_scalelist[2] = Vector2.One;
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }

            //Console.WriteLine(keyboardCursorPosCounter);
        }

        public void HandleInput(GameTime gameTime)
        {
            // Console.WriteLine("still in menu");
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)             
            {
                isKeyboardCursorActive = true;//Keyboard detect

                keyboardCursorPosCounter++;
                if (keyboardCursorPosCounter > skill_button_poslist.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                //Console.WriteLine(keyboardCursorPosCounter);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                isKeyboardCursorActive = true;//Keyboard detect

                keyboardCursorPosCounter--;
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = skill_button_poslist.Count - 1;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                //Console.WriteLine(keyboardCursorPosCounter);
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
                    case 0:
                        
                        break;

                    case 1:
                        
                        break;

                    case 2:

                        break;
                }
                //m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, destinationRectangle: new Rectangle(0, 0, 3000, 800),color: Color.Brown);
            spriteBatch.DrawString(_font, "press A to Continue", new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2) - _font.MeasureString("press A to Continue") / 2, Color.White);

            /*spriteBatch.Draw(_skill1, new Vector2((Singleton.SCREENWIDTH / 2 - _skill1.Width / 2) - 150, (Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2)), null, null, null, 0, skill1_scale);
            spriteBatch.Draw(_skill2, new Vector2((Singleton.SCREENWIDTH / 2 - _skill2.Width / 2) + 150, (Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2)), null, null, null, 0, skill2_scale);
            spriteBatch.Draw(_skill3, new Vector2((Singleton.SCREENWIDTH / 2 - _skill3.Width / 2) + 450, (Singleton.SCREENHEIGHT / 2 - _skill1.Height / 2)), null, null, null, 0, skill3_scale);*/
            spriteBatch.Draw(_skill1, skill1_button_pos, null, null, new Vector2(_skill1.Width / 2, _skill1.Height / 2), 0, skill_button_scalelist[0], null, 0);
            spriteBatch.Draw(_skill2, skill2_button_pos, null, null, new Vector2(_skill2.Width / 2, _skill2.Height / 2), 0, skill_button_scalelist[1], null, 0);
            spriteBatch.Draw(_skill3, skill3_button_pos, null, null, new Vector2(_skill3.Width / 2, _skill3.Height / 2), 0, skill_button_scalelist[2], null, 0);

            if(isKeyboardCursorActive)
                spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(50, 50), 0, new Vector2(1.5f, 1.5f), Color.Red, 0);
                                           

            spriteBatch.DrawString(_font,
                    Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
                    new Vector2(200, 200),
                    Color.White);
            
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
