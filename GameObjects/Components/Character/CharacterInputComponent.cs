using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterInputComponent : InputComponent
    {

        public KeyboardState _currentkey;
        public KeyboardState _previouskey;

        int message;



        public GameObject Bullet;
        public GameObject bullet;

        public CharacterInputComponent() 
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            _previouskey = _currentkey;
           _currentkey = Keyboard.GetState();

            parent.Direction = new Vector2((float)Math.Cos(parent.Rotation), (float)Math.Sin(parent.Rotation));
            CheckRemove(parent);

            if (Singleton.Instance.CurrentTurnState == Singleton.TurnState.shoot)
            {
                if (parent.action)
                {
                    Shoot(gameObjects, parent);
                    parent.action = false;
                }
            }

            base.Update(gameTime, gameObjects, parent);

        }

        private void CheckRemove(GameObject parent)
        {
            if (bullet != null && (bullet.Position.X > 3000 || bullet.Position.X < 0))
            {
                parent.shooting = false;
                parent.InTurn = false;
                //bullet.Hit();
                bullet = null;
            }
        }

        public void Shoot(List<GameObject> gameObjects, GameObject parent)
        {
            //var bullet = parent.Child.Clone() as GameObject;

            //Console.WriteLine("clone bullet");
            //bullet.Direction = parent.Direction;
            //bullet.Position = parent.Position;
            //bullet.attack = parent.attack;
            //bullet.LinearVelocity = parent.LinearVelocity * 15;
            //gameObjects.Add(bullet);
            //this.bullet = bullet;
            //parent.shooting = true;


           // var bullet = parent.Child.Clone() as GameObject;

            Console.WriteLine("add bullet");
            parent.Child.Direction = parent.Direction;
            parent.Child.Position = parent.Position;
            parent.Child.attack = parent.attack;
            parent.Child.LinearVelocity = parent.LinearVelocity * 15;
            gameObjects.Add(parent.Child);
            this.bullet = parent.Child;
            parent.shooting = true;
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
