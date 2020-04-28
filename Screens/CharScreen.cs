using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment.Screens
{
    class CharScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        //private bool m_exitGame;
        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        private GameObject zeus;
        private GameObject guan;
        private List<GameObject> _gameObjects;

        Texture2D _bg;
        SpriteFont _font;
        Texture2D _guan;
        Texture2D _zeus;

        public CharScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }

        public bool IsPaused { get; private set; }
        public void Init(ContentManager content)
        {
            _gameObjects = new List<GameObject>();
            _bg = content.Load<Texture2D>("sprites/bg");
            _font = content.Load<SpriteFont>("font/File");
            _guan = content.Load<Texture2D>("sprites/sheet_guan");
            _zeus = content.Load<Texture2D>("sprites/sheet_zeus");
            zeus = new GameObject(null,
                                    new CharacterPhysicComponent(),
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_zeus, new Rectangle(0,0,400,250),2) }
                                        }),
                                    null)
            {
                Position = new Vector2(Singleton.SCREENWIDTH / 2 - 150, Singleton.SCREENHEIGHT / 2),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                /*_hit = _hit,
                Name = Singleton.Instance.CurrentHero,*/
                /*Weapon = "hammer",*/
                HP = 1,
                attack = 1,
            };
            guan = new GameObject(null,
                                    new CharacterPhysicComponent(),
                                    new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                        {
                                            { "Idle", new Animation(_guan, new Rectangle(0,0,400,250),2) }
                                        }),
                                    null)
            {
                Position = new Vector2(Singleton.SCREENWIDTH / 2 + 150, Singleton.SCREENHEIGHT / 2),
                InTurn = false,
                Viewport = new Rectangle(0, 0, 150, 230),
                /*_hit = _hit,
                Name = "guan",*/
               /* Weapon = "lance",*/
                HP = 1,
                attack = 1
            };
            _gameObjects.Add(zeus);
            _gameObjects.Add(guan);
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
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.DrawString(_font,
                    Singleton.Instance._currentmouse.Position.X+", "+ Singleton.Instance._currentmouse.Position.Y,
                    new Vector2(200,200),
                    Color.White);
            spriteBatch.End();
        }

        public void HandleInput(GameTime gameTime)
        {
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Back) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
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
            if (Singleton.Instance._currentmouse.Position.X > 350 && Singleton.Instance._currentmouse.Position.X < 520 &&
                Singleton.Instance._currentmouse.Position.Y > 280 && Singleton.Instance._currentmouse.Position.Y < 520)
            {
                zeus.Scale = new Vector2(1.2f, 1.2f);

                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    Singleton.Instance.CurrentHero = "zeus";
                    m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else zeus.Scale = Vector2.One;
            if (Singleton.Instance._currentmouse.Position.X > 680 && Singleton.Instance._currentmouse.Position.X < 840 &&
                Singleton.Instance._currentmouse.Position.Y > 280 && Singleton.Instance._currentmouse.Position.Y < 520)
            {
                guan.Scale = new Vector2(1.2f, 1.2f);
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    Singleton.Instance.CurrentHero = "guan";
                    m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                }
            }
            else guan.Scale = Vector2.One;
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }

        }
    }
}
