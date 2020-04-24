using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class ThorSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;
        int count;

        public ThorSkillComponent()
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

            rng = rnd.Next(1, 11);
            count += 1;
            if ( parent.InTurn && parent.status != 1 && count == 1) {
                switch (Singleton.Instance.level_s2)
                {
                    case 1:
                        if (rng >= 10)
                        {
                            Console.WriteLine(parent.Name + "  activated passive");
                            parent.attack++;

                            parent.SendMessage(this, 3);
                        }
                      
                        break;
                    case 2:
                        if (rng >= 9)
                        {
                            Console.WriteLine(parent.Name + "  activated passive");
                            parent.attack++;

                            parent.SendMessage(this, 3);
                        }
                     
                        break;
                    case 3:
                        if (rng >= 8)
                        {
                            Console.WriteLine(parent.Name + "  activated passive");
                            parent.attack++;

                            parent.SendMessage(this, 3);
                        }
                      
                        break;
                }
            }
           
            if (!parent.InTurn)
            {
                count = 0;
            }

            if (parent.skill == 1)
            {

            }

            if(parent.skill == 2)
            {

            }

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
