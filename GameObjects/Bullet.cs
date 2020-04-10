using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Assignment 
{
    class Bullet: GameObject
    {


        public int damage;
        public bool special;
        bool IsHit = false;

        public float HitRadius;



        public Bullet(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
           // Rotation = 6.66f;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {

            Position += Direction * LinearVelocity;


            base.Update(gameTime, gameObjects);
        }


        public void Hit()
        {
            IsHit = true;
            IsActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
