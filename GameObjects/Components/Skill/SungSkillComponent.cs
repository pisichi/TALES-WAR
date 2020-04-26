using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class SungSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;
        int count;

        public SungSkillComponent()
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
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }


            if (parent.skill == 1)
            {
                Console.WriteLine(parent.Name + " use skill 1");
                parent.SendMessage(this, 201);
                parent.skill = 0;
            }

            else if (parent.skill == 2)
            {
                Console.WriteLine(parent.Name + " use skill 2");
                parent.SendMessage(this, 206);
                parent.skill = 0;
            }

            count += 1;
            if (parent.InTurn && parent.status != 1 && count == 1)
            {
                Console.WriteLine(parent.Name + "  activated passive");
                parent.HP++;
                parent.SendMessage(this, 3);
            }

            if (!parent.InTurn)
            {
                count = 0;
            }


            base.Update(gameTime, gameObjects, parent);
        }
    }
}
