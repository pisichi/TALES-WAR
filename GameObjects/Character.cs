using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class Character : GameObject
    {

        public bool IsPlayer;

        public Bullet Bullet;

        public Bullet shoot;

        Random rnd = new Random();
        public bool shooting = false;
        float cooldowntime = 0;

        


        public Character(Texture2D texture) : base(texture)
        {
            this._texture = texture;
            IsPlayer = true;

        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {


            _previouskey = _currentkey;
            _currentkey = Keyboard.GetState();

            Console.WriteLine(" " + Rotation);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            cooldowntime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;



            if (_currentkey.IsKeyDown(Keys.A))
            {
                Position.X -= 10;
            }

            if (_currentkey.IsKeyDown(Keys.D))
            {
                Position.X += 10;
            }

            CheckInput();

            base.Update(gameTime, gameObjects);
        }

        public void Shoot(List<GameObject> gameObjects)
        {
         
                var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity * 15;
                gameObjects.Add(bullet);
                shoot = bullet;
                shooting = true;

        }

        private void CheckInput()
        {

        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, 0f , Origin , 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
    }
}
