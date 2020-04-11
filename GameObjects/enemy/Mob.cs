using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class Mob : Character
    {

        public Mob(Texture2D texture) : base(texture)
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
                bullet.LinearVelocity = this.LinearVelocity * 15;
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
            spriteBatch.Draw(_texture, Position, Color.Blue);
            base.Draw(spriteBatch);
        }
    }
}
