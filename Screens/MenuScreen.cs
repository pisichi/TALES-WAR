
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Assignment
{
    class MenuScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
        private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private bool isMouseActive;

        Random rnd = new Random();

        public bool IsPaused { get; private set; }


        Texture2D _bg;
        SpriteFont _font;
        private List<Vector2> skill_button_poslist;
        private List<Vector2> skill_button_scalelist;



        Texture2D _KeyboardCursor;
        Texture2D _selectedChar;

        private Vector2 KeyboardCursorPos;

        public MenuScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            int rand = rnd.Next(1, 5);
            _bg = content.Load<Texture2D>("sprites/menu_"+rand);
            _font = content.Load<SpriteFont>("font/File");

            skill_button_scalelist = new List<Vector2>();
            skill_button_poslist = new List<Vector2>();
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");

            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);

            skill_button_poslist.Add(new Vector2(300, 300));
            skill_button_poslist.Add(new Vector2(300, 400));
            skill_button_poslist.Add(new Vector2(300, 500));
            skill_button_poslist.Add(new Vector2(300, 600));

            KeyboardCursorPos = skill_button_poslist[0];
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

            Button(0);
            Button(1);
            Button(2);
            Button(3);

        }

        private void Button(int i)
        {
            if (Singleton.Instance._currentmouse.Position.X > skill_button_poslist[i].X - _font.MeasureString("PLAY").X / 2
                 && Singleton.Instance._currentmouse.Position.X < skill_button_poslist[i].X + _font.MeasureString("PLAY").X / 2
                 && Singleton.Instance._currentmouse.Position.Y > skill_button_poslist[i].Y - _font.MeasureString("PLAY").Y / 2
                 && Singleton.Instance._currentmouse.Position.Y < skill_button_poslist[i].Y + _font.MeasureString("PLAY").Y / 2
                && isMouseActive)

            {
                skill_button_scalelist[i] = new Vector2(1.2f, 1.2f);
                KeyboardCursorPos = skill_button_poslist[i];
                keyboardCursorPosCounter = i;
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    switch (i)
                    {
                        case 0:
                            m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            m_screenManager.Exit();
                            break;
                    }
                }
            }
            else if(!isKeyboardCursorActive)
            {
                skill_button_scalelist[i] = Vector2.One;
            }
        }

        public void HandleInput(GameTime gameTime)
        {

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                skill_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter++;
                if (keyboardCursorPosCounter > skill_button_poslist.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                skill_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                skill_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter--;
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = skill_button_poslist.Count - 1;
                KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                skill_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);


            }


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:
                        m_screenManager.Exit();
                        break;
                }

            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(_bg, Vector2.Zero, color: Color.White);
            spriteBatch.DrawString(_font, "TALES WAR", new Vector2(300,200) - _font.MeasureString("TALE WARS") / 2, Color.White);

        
            spriteBatch.DrawString(_font, "PLAY", skill_button_poslist[0], Color.White, 0, _font.MeasureString("PLAY")/2, skill_button_scalelist[0], SpriteEffects.None,0);
            spriteBatch.DrawString(_font, "CONTROL", skill_button_poslist[1], Color.White, 0, _font.MeasureString("CONTROL")/2, skill_button_scalelist[1], SpriteEffects.None,0);
            spriteBatch.DrawString(_font, "ABOUT", skill_button_poslist[2], Color.White, 0, _font.MeasureString("ABOUT")/2,skill_button_scalelist[2], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "EXIT", skill_button_poslist[3], Color.White, 0, _font.MeasureString("EXIT")/2,skill_button_scalelist[3], SpriteEffects.None, 0);



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
