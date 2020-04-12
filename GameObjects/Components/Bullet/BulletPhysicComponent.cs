
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletPhysicComponent : PhysicComponent
    {
        public BulletPhysicComponent()
        {
            Console.WriteLine("bullet is here");
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            
            parent.Position += parent.Direction * parent.LinearVelocity;
            //Console.WriteLine(parent.Position.X);
            //Console.WriteLine(parent.Position.Y);
            //Console.WriteLine(parent.Direction.X);
            //Console.WriteLine(parent.Direction.Y);
            //Console.WriteLine(parent.LinearVelocity);
        }

        public void Hit(List<GameObject> gameObjects, GameObject parent)
        {
            parent.IsActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
        }
    }
}
