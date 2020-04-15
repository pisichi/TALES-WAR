using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterAIComponent : InputComponent
    {

        int message;
        int count = 0;

        int _action = 0;
        ContentManager content;
        Texture2D _bullet;


        public GameObject bullet;

        public CharacterAIComponent(ContentManager content)
        {
            _bullet = content.Load<Texture2D>("sprites/ball");
            this.content = content;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            parent.Direction = new Vector2((float)Math.Cos(parent.Rotation + (float)Math.PI / 2), (float)Math.Sin(parent.Rotation + (float)Math.PI / 2));
            CheckRemove(parent);


            if (parent.action)
            {

                if (parent.status == 1 && parent.InTurn)
                {
                    parent.shooting = false;
                    parent.InTurn = false;
                    parent.action = false;
                    parent.status = 0;
                    Console.WriteLine(parent.Name + " count : " +count);
                    Console.WriteLine(parent.Name + " status : " + parent.status);
                    // Singleton.Instance.CurrentTurnState = Singleton.TurnState.enemy;


                }
                else
                {
                    _action += 1;
                    parent.action = false;
                    if (_action == 1)
                        Shoot(gameObjects, parent);
                }
            }
                    //Singleton.Instance.CurrentTurnState = Singleton.TurnState.enemy;
            

            base.Update(gameTime, gameObjects, parent);

        }

        private void CheckRemove(GameObject parent)
        {

            if (bullet != null && bullet.IsActive == false)
            {
                _action = 0;
                parent.shooting = false;
                parent.InTurn = false;
                bullet = null;
            }


        }

        public void Shoot(List<GameObject> gameObjects, GameObject parent)
        {

            Console.WriteLine("add bullet");
            bullet = new GameObject(null,
                                    new BulletPhysicComponent(),
                                    new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(0,0,_bullet.Width,_bullet.Height),1) }
                                         }),
                                    null)
            {
                Viewport = new Rectangle(0, 0, _bullet.Width, _bullet.Height),
                _hit = parent._hit,

            };

            bullet.Direction = parent.Direction;
            bullet.Position = parent.Position + new Vector2(120, -100);
            bullet.attack = parent.attack;
            bullet.LinearVelocity = parent.LinearVelocity * 50;
            if (message == 201)
            {
                bullet.status = 1;
            }
            gameObjects.Add(bullet);
            parent.shooting = true;
            Singleton.Instance.follow = bullet;
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
            this.message = message;
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
