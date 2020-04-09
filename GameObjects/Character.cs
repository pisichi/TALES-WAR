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

        Random rnd = new Random();
        public bool shooting = false;
        float waitTime = 0;

        


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
            //Console.WriteLine(" " + Rotation);
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            CheckRemove();

            HandleInput();

            base.Update(gameTime, gameObjects);
        }


        public void Skill()
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



        public void Shoot(List<GameObject> gameObjects)
        {

            var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity * 15;
                gameObjects.Add(bullet);
                this.bullet = bullet;
                shooting = true;
        }

        public void Auto(GameTime gameTime,List<GameObject> gameObjects)
        {
            // AI Stuff
            Skill();

            waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

                if (!shooting && waitTime > 2)
                {
                    var bullet = Bullet.Clone() as Bullet;
                    bullet.Direction = this.Direction * -1 ;
                    bullet.Position = this.Position;
                    bullet.LinearVelocity = this.LinearVelocity * 15;
                    gameObjects.Add(bullet);
                    this.bullet = bullet;
                    shooting = true;
                }
            Console.WriteLine(waitTime);
                
            

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
