using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class Character : GameObject
    {


        public bool InTurn;
        public bool IsPlayer;
        public Bullet Bullet;
        public Bullet bullet;


        //attribute
         String name;
         public int HitPoint;
         public int attack;

         public int stunted = 0;
         public int dot = 0;
         public int buff = 0;

        public enum status
        {
            Normal,
            Buffed,
            Stunted,
            DoT
        }
        public status CurrentStatus;
        //
        public bool shooting = false;
        public float waitTime = 0;


        Random rnd = new Random();

        public Character(Texture2D texture) : base(texture)
        {
            this._texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            IsPlayer = true;

        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Singleton.Instance._previouskey = Singleton.Instance._currentkey;
            Singleton.Instance._currentkey = Keyboard.GetState();
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            CheckRemove();
            HandleInput();
            base.Update(gameTime, gameObjects);
        }


        public virtual void Skill()
        {

        }

        private void CheckRemove()
        {
            if (bullet != null && (bullet.Position.X > 3000 || bullet.Position.X < 0))
            {
                shooting = false;
                InTurn = false;
                bullet.Hit();
                bullet = null;
                waitTime = 0;
            }
        }


        public virtual void Shoot(List<GameObject> gameObjects)
        {

        }


        public virtual void Auto(GameTime gameTime,List<GameObject> gameObjects)
        {
           
        }

        private void HandleInput()
        {

        }


        public void IsHit()
        {

        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, 0f , Origin , 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
    }
}
