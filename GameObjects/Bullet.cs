using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment.GameObjects
{
    class Bullet : GameObject
    {

        public Bullet(Texture2D texture) : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Velocity += Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //destroy when off screen
            if (Position.X < 0 - Rectangle.Width ||
                Position.X > Singleton.SCREENWIDTH + Rectangle.Width ||
                Position.Y < 0 - Rectangle.Height ||
                Position.Y > Singleton.SCREENHEIGHT + Rectangle.Height)
            {
                IsActive = false;
            }

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
