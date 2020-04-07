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

        public Bullet Bullet;

        Random rnd = new Random();
        bool shooting = true;
        float cooldowntime = 0;
        Texture2D _arrow;

        public Character(Texture2D texture ,Texture2D texture2) : base(texture)
        {
            this._texture = texture;
            _arrow = texture2;
            

        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {


            _previouskey = _currentkey;
            _currentkey = Keyboard.GetState();

            Console.WriteLine(" "+Rotation);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            cooldowntime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;


            if (_currentkey.IsKeyDown(Keys.Space))
            {
                var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity * 15;
                gameObjects.Add(bullet);
            }


            CheckInput();

            base.Update(gameTime, gameObjects);
        }


        private void CheckInput()
        {
            if (_currentkey.IsKeyDown(Keys.A))
            {
                if (Rotation > -2.6)
                    Rotation -= MathHelper.ToRadians(RotationVelocity);
            }
            else if (_currentkey.IsKeyDown(Keys.D))
            {
                if (Rotation < -0.6)
                    Rotation += MathHelper.ToRadians(RotationVelocity);
            }



        }


        private void Shoot(List<GameObject> gameObjects)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, 0f , Origin , 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_arrow, Position + new Vector2(100,0), null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
    }
}
