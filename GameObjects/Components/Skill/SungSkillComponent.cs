using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class SungSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;

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

            if (parent.skill == 1)
            {
                Console.WriteLine(parent.Name + " use skill 1");
                parent.SendMessage(this, 201);
                parent.skill = 0;
            }

            else if (parent.skill == 2)
            {
                Console.WriteLine(parent.Name + " use skill 2");
                parent.SendMessage(this, 201);
                parent.skill = 0;
            }

            if (parent.IsHit && parent.status != 1)
            {
                if (rng >= 5)
                {
                    parent.SendMessage(this, 3);
                }
            }
                base.Update(gameTime, gameObjects, parent);
        }
    }
}
