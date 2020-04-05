using Final_Assignment.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class GameObject : ICloneable
    {

        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;

        public Vector2 Position;

        public float Rotation;
        public Vector2 Scale;

        public Vector2 Velocity;
        public Vector2 Acceleration;

        public string Name;

        public bool IsActive;

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

        protected Texture2D _texture;
        #endregion

        public GameObject()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;
        }

        public GameObject(Texture2D texture)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;
            _texture = texture;
        }

        public GameObject(Dictionary<string, Animation> animations)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;
            _animations = animations;
            //_animationManager = new AnimationManager(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        
        }

        public virtual void Reset()
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
