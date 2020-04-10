using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class Mob2 : Character
    {


        public Mob2(Texture2D texture) : base(texture)
        {
        }

        public override void Auto(GameTime gameTime, List<GameObject> gameObjects)
        {

            // AI Stuff
            Skill();

            waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

            if (!shooting && waitTime > 2)
            {
                var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction * -1;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity * 50;
                bullet.Scale = new Vector2(2, 2);

                gameObjects.Add(bullet);
                this.bullet = bullet;
                shooting = true;
            }
            Console.WriteLine(waitTime);

            base.Auto(gameTime, gameObjects);
        }


        public override void Reset()
        {
            base.Reset();
        }

        public override void Skill()
        {
            base.Skill();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.Green);
            base.Draw(spriteBatch);
        }
    }
}

