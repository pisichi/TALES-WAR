using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterInputComponent : InputComponent
    {

        int message;
        ContentManager content;
        Texture2D _bullet;

        public GameObject bullet;

        public CharacterInputComponent(ContentManager content) 
        {
            _bullet = content.Load<Texture2D>("sprites/ball");
            this.content = content;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {


            parent.Direction = new Vector2((float)Math.Cos(parent.Rotation + (float)Math.PI / 2), (float)Math.Sin(parent.Rotation + (float)Math.PI / 2));
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

            Console.WriteLine("add bullet");
            parent.Child = new GameObject(_bullet, null,
                                               new BulletPhysicComponent(),
                                               new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                            { "Shoot", new Animation(_bullet, new Rectangle(0,0,_bullet.Width,_bullet.Height),1) }
                                                   }));

            parent.Child.Direction = parent.Direction;
            parent.Child.Position = parent.Position - new Vector2(0,100);
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
