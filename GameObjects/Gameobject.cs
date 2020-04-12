
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Final_Assignment
{
    class GameObject : ICloneable
    {


        protected InputComponent _input;
        protected PhysicComponent _physics;
        protected GraphicComponent _graphics;

        public int attack;
        public bool InTurn;
        public bool shooting = false;

        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;

        public Vector2 Position;

        public float Rotation;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 1f;


        public Vector2 Scale;
        public Vector2 Direction;
        public Vector2 Origin;

        public Vector2 Velocity;
        public Vector2 Acceleration;

        public string Name;

        public bool IsActive = true;



        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public Rectangle Viewport;
        #endregion

        #region PROTECTED_VARIABLES

        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        public Texture2D _texture;
        #endregion


        public GameObject(Texture2D texture, InputComponent input,PhysicComponent physics,GraphicComponent graphics)
        {
            _input = input;
            _physics = physics;
            _graphics = graphics;

            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            if (_input != null) _input.Update(gameTime, gameObjects, this);
            if (_physics != null) _physics.Update(gameTime, gameObjects, this);
            if (_graphics != null) _graphics.Update(gameTime, gameObjects, this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_graphics != null) _graphics.Draw(spriteBatch, this);
        }

        public void SendMessage(Component sender,int message)
        {
            if (_input != null) _input.ReceiveMessage(message ,sender);
            if (_physics != null) _physics.ReceiveMessage(message,sender);
            if (_graphics != null) _graphics.ReceiveMessage(message,sender);
        }

        public virtual void Reset()
        {
            if (_input != null) _input.Reset();
            if (_physics != null) _physics.Reset();
            if (_graphics != null) _graphics.Reset();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        #region Collision
        public bool IsTouching(GameObject g)
        {
            return IsTouchingLeft(g) ||
                   IsTouchingTop(g) ||
                   IsTouchingRight(g) ||
                   IsTouchingBottom(g);
        }

        public bool IsTouchingLeft(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Left &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingRight(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Right &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingTop(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Top;
        }

        public bool IsTouchingBottom(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Bottom &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion

    }
}
