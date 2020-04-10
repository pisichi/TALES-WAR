using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class Zeus : Character
    {
        public Zeus(Texture2D texture) : base(texture)
        {

        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Skill()
        {
            base.Skill();
        }

        public override void Shoot(List<GameObject> gameObjects)
        {

            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 15;
            gameObjects.Add(bullet);
            this.bullet = bullet;
            shooting = true;

            base.Shoot(gameObjects);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.Red);
            base.Draw(spriteBatch);
        }
    }
}
