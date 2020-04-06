using Final_Assignment.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment.GameObjects
{
    class Player : GameObject
    {
        public GameObject Bullet;

        public Keys Left;
        public Keys Right;
        public Keys Forward;
        public Keys Fire;

        private float ROTATIONSEN = 0.05f;
        private float ACCELERATIONSEN = 300;
        private float MAXSPEED = 300f;

        private float BULLETSPEED = 1000;

        private bool isAlive = true;
        private float timer = 0;

        public Player(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            //if we are not alive, ignore all inputs and physics

            Acceleration = Vector2.Zero;

            if (Singleton.Instance.CurrentKey.IsKeyDown(Left))
            {
                Acceleration.X = ACCELERATIONSEN * (float)Math.Cos(Rotation - MathHelper.Pi);
                Acceleration.Y = ACCELERATIONSEN * (float)Math.Sin(Rotation - MathHelper.Pi);
                Velocity += Acceleration;
                Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            }
            if (Singleton.Instance.CurrentKey.IsKeyDown(Right))
            {
                Acceleration.X = ACCELERATIONSEN * (float)Math.Cos(Rotation - 0);
                Acceleration.Y = ACCELERATIONSEN * (float)Math.Sin(Rotation - 0);
                Velocity += Acceleration;
                Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            }
            /*if (Singleton.Instance.CurrentKey.IsKeyDown(Forward))
            {
                Acceleration.X = ACCELERATIONSEN * (float)Math.Cos(Rotation - MathHelper.Pi / 2.0f);
                Acceleration.Y = ACCELERATIONSEN * (float)Math.Sin(Rotation - MathHelper.Pi / 2.0f);
            }*/
            if (Singleton.Instance.CurrentKey.IsKeyDown(Fire) &&
                Singleton.Instance.CurrentKey != Singleton.Instance.PreviousKey)
            {
                var newBullet = Bullet.Clone() as GameObject;
                newBullet.Name = "Bullet";
                newBullet.Viewport = new Rectangle(0, 0, 5, 5);
                newBullet.Position = new Vector2(Position.X + Viewport.Width / 2 * (float)Math.Cos(Rotation - MathHelper.Pi / 2.0f),
                                                Position.Y + Viewport.Height / 2 * (float)Math.Sin(Rotation - MathHelper.Pi / 2.0f));
                newBullet.Rotation = Rotation - MathHelper.Pi / 2.0f;
                newBullet.Velocity = new Vector2(BULLETSPEED * (float)Math.Cos(newBullet.Rotation),
                                                    BULLETSPEED * (float)Math.Sin(newBullet.Rotation));
                newBullet.Reset();


                gameObjects.Add(newBullet);


            }

            //Clamp Velocity
            Velocity.X = MathHelper.Clamp(Velocity.X, -MAXSPEED, MAXSPEED);
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -MAXSPEED, MAXSPEED);

            /*                Velocity += Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                        Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;*/

            

            //timer += (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
            if (timer >= 0.5f)
            {
                timer = 0;
            }

            //Bring back when off screen
            switch (Name)
            {
                case "Player":
                    if (Position.X < Rectangle.Width / 2)
                    {
                        Position.X = Rectangle.Width / 2;
                    }
                    if (Position.X > Singleton.SCREENWIDTH / 2 - Rectangle.Width / 2)
                    {
                        Position.X = Singleton.SCREENWIDTH / 2 - Rectangle.Width / 2;
                    }
                   /* if (Position.Y < 0 - Rectangle.Height)
                    {
                        Position.Y = 0 - Rectangle.Height;
                    }
                    if (Position.Y > Singleton.SCREENHEIGHT + Rectangle.Height)
                    {
                        Position.Y = Singleton.SCREENHEIGHT + Rectangle.Height;
                    }*/
                 break;

                    case "Player2":

                    if (Position.X < Singleton.SCREENWIDTH / 2 + Rectangle.Width)
                    {
                        Position.X = Singleton.SCREENWIDTH / 2 + Rectangle.Width;
                    }
                    if (Position.X > Singleton.SCREENWIDTH - Rectangle.Height / 2)
                    {
                        Position.X = Singleton.SCREENWIDTH - Rectangle.Height / 2;
                    }
                    /*if (Position.Y < 0 - Rectangle.Height)
                    {
                        Position.Y = 0 - Rectangle.Height;
                    }
                    if (Position.Y > Singleton.SCREENHEIGHT + Rectangle.Height)
                    {
                        Position.Y = Singleton.SCREENHEIGHT + Rectangle.Height;
                    }*/
                    break;
            }


            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
          

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
