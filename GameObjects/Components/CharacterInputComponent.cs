using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterInputComponent : InputComponent
    {

        int message;

        public bool InTurn;
        public bool IsPlayer;
        public bool shooting = false;
        //public Bullet Bullet;
        //public Bullet bullet;

        public CharacterInputComponent() 
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            Singleton.Instance._previouskey = Singleton.Instance._currentkey;
            Singleton.Instance._currentkey = Keyboard.GetState();
            parent.Direction = new Vector2((float)Math.Cos(parent.Rotation), (float)Math.Sin(parent.Rotation));
            CheckRemove();


            if (Singleton.Instance.CurrentTurnState == Singleton.TurnState.shoot && Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                Shoot(gameObjects, parent);
            }

            base.Update(gameTime, gameObjects, parent);

        }

        private void CheckRemove()
        {
            //if (bullet != null && (bullet.Position.X > 3000 || bullet.Position.X < 0))
            //{
            //    shooting = false;
            //    InTurn = false;
            //    //bullet.Hit();
            //    bullet = null;
            //}
        }

        public void Shoot(List<GameObject> gameObjects, GameObject parent)
        {
            //var bullet = Bullet.Clone() as Bullet;
            //bullet.Direction = parent.Direction;
            //bullet.Position = parent.Position;
            //bullet.damage = parent.attack;
            //bullet.LinearVelocity = parent.LinearVelocity * 15;
            //gameObjects.Add(bullet);
            //this.bullet = bullet;
            //shooting = true;
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
