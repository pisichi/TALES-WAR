using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class ZeusSkillComponent:SkillComponent
    {

        Random rnd = new Random();
        int rng;

        public ZeusSkillComponent()
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
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
          

            if (parent.IsHit)
            {
                Console.WriteLine(parent.Name + " activated skill 2");
                rng = rnd.Next(1, 11);
                switch (Singleton.Instance.level_s2)
                {
                    case 1:
                        if (rng >= 5)
                        {
                            parent.HP += 2; //10%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                    case 2:
                        if (rng >= 9)
                        {
                            parent.HP += 2; //20%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                    case 3:
                        if (rng >= 8)
                        {
                            parent.HP += 2; //30%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                }
                parent.IsHit = false;
            }

            if (parent.skill == 1)
            {
                Console.WriteLine("use skill 1");
                rng = rnd.Next(1, 11);
                //parent.SendMessage(this, 201);
                switch (Singleton.Instance.level_s1)
                {
                    case 1:
                        if (rng >= 1)
                            parent.SendMessage(this, 201);
                        break;
                    case 2:
                        if (rng >= 6)
                            parent.SendMessage(this, 201);
                        break;
                    case 3:
                        if (rng >= 3)
                            parent.SendMessage(this, 201);
                        break;
                }
                parent.skill = 0;
            }

            if (parent.skill == 3)
            {
                switch (Singleton.Instance.level_s3)
                {
                    case 1:
                        parent.attack += 1;
                        break;
                    case 2:
                        parent.attack += 2;
                        break;
                    case 3:
                        parent.attack += 3;
                        break;
                }
                parent.skill = 0;
            }

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
