
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletPhysicComponent : PhysicComponent
    {
        public float _tick;
        public float PreviousTime;
        public float CurrentTime;
        float dt;
        bool hasPreviousPosition = false;
        public BulletPhysicComponent()
        {
            //Console.WriteLine("bullet is here");
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
            /*PreviousTime = _tick;
            _tick = gameTime.ElapsedGameTime.Milliseconds;
            Console.WriteLine(_tick);
            dt = _tick - PreviousTime;
            if (dt > 0.15f) dt = 0.15f;
            //parent.Direction += parent.Gravity * dt;
            parent.Position += parent.Velocity * dt;
            parent.Velocity += parent.Gravity * dt;*/

            //Start Potato Physics
            parent.Direction.Y += parent.gravity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            parent.force = 0.1f;
            /*if (parent.PreviousDirection.Y > parent.Direction.Y)
            {
                parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                Console.WriteLine("if");
            }

            else 
            {
                parent.Position.Y += parent.Direction.Y * 1000f * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                Console.WriteLine("else");
            }*/
                

            if (parent.HasMaxY)
            {
                if (parent.maxY >= parent.Position.Y)
                {
                    parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    parent.maxY = parent.Position.Y;
                    //if ()
                        parent.HasMaxY = true;
                    Console.WriteLine("has y up" + parent.maxY);
                }
                else
                {
                    parent.Position.Y += parent.Direction.Y * 1000f * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    Console.WriteLine("has y down" + parent.maxY);
                }

            }
            else
            {
                parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                parent.maxY = parent.Position.Y;
                parent.HasMaxY = true;
                Console.WriteLine("no y up" + parent.maxY);
            }



            parent.Position.X += parent.Direction.X * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (parent.PreviousPosition.Y < parent.Position.Y && parent.PreviousPosition.Y < parent.maxY && hasPreviousPosition)
            {
                parent.maxY = parent.PreviousPosition.Y;
            }
            
            parent.PreviousPosition = parent.Position;
            hasPreviousPosition = true;
            //End Potato Physics
            if (parent.Position.X > 4000 || parent.Position.X < 0
                /*|| parent.Position.Y < 0*/ || parent.Position.Y > 1000)
            {
                parent.IsActive = false;
            }


                foreach (GameObject s in gameObjects)
            {
                if(s.IsActive && parent.IsActive && IsTouching(parent,s))
                {
                    //Console.WriteLine("hitting  " + s.Name);
                    s.HP -= parent.attack;
                    s.IsHit = true;
                    s.status = parent.status;
                    parent.IsActive = false;
                    
                }
            }
            
            
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
