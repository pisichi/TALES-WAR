using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Final_Assignment 
{
    class Bullet: GameObject
    {


        public int damage;
        public bool special;

        public Bullet(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Position += Direction * LinearVelocity;
            //Position.Y *= -0.1f;
            base.Update(gameTime, gameObjects);
        }


        public void Hit()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
