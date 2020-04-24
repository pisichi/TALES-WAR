using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletPhysicComponent : PhysicComponent
    {

       private bool hitting;
       private bool touch;
       private GameObject target;
       private float waitTime;

        public BulletPhysicComponent()
        {
            hitting = false;
            touch = false;
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
            //Start Potato Physics

            parent.Rotation+=0.001f;
            //parent.force = 2000f;
            if (!hitting)
            {
                parent.Direction.Y += parent.gravity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                parent.force = 1.3f;

                //if (parent.PreviousDirection.Y < parent.Direction.Y)
                    parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                /*else
                    parent.Position += parent.Direction * 1000f * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;*/
                parent.Position.X += parent.Direction.X * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                //End Potato Physics



                if (parent.Position.X > 4000 || parent.Position.X < 0
                   || parent.Position.Y > 1000)
                {
                    parent.IsActive = false;
                }

                if (!touch)
                {
                    foreach (GameObject s in gameObjects)
                    {

                        if (s.IsActive && parent.IsActive && IsTouching(parent, s))
                        {
                            parent.Scale = new Vector2(2, 2);
                            parent.Viewport = new Rectangle(0,0,150, 150);
                            parent.IsHit = true;
                            touch = true;
                        }
                    }
                }

                else
                {
                    foreach (GameObject s in gameObjects)
                    {

                        if (s.IsActive && parent.IsActive && IsTouching(parent, s))
                        {

                            s.HP -= parent.attack;
                            s.IsHit = true;
                            s.status = parent.status;
                            target = s;
                            hitting = true;

                        }
                    }
                }
            }

            else
            {
                waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

                if (waitTime > 0.5)
                {
                    Console.WriteLine("hitting  " + target.Name);
                    parent.IsActive = false;
                    hitting = false;
                    touch = false;
                    waitTime = 0;
                }
            }
            parent.PreviousDirection = parent.Direction;
        }


        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
        }
    }
}