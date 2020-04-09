using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace Final_Assignment 
{
    class Bullet: GameObject
    {


        public int damage;
        public bool special;
        //public World _world;
        public Body body;

        public Bullet(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            //Set physics
            world = new World(Vector2.Zero);
            //

            Console.WriteLine(Position.X+" "+Position.Y);

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Position.X += Direction.X * LinearVelocity;

            //Body position
            body.Position += new Vector2(Direction.X * LinearVelocity, 0);
            //
            base.Update(gameTime, gameObjects);
        }


        public void Hit()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, body.Position, null, Color.White, body.Rotation, Origin, 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
