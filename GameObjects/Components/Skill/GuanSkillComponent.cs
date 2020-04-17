
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class GuanSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;
        private bool extra;

        public GuanSkillComponent()
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            rng = rnd.Next(1, 11);

            if (rng >= 8 && !parent.InTurn && !parent.action)
            {
                Console.WriteLine(parent.Name + "  activated skill 2");
                
                parent.InTurn = true;

                parent.SendMessage(this, 3);

            }
                
               
                //switch (Singleton.Instance.level_s2)
                //{
                //    case 1:
                //        if (rng >= 5)
                //        {
                            
                //        }
                //        break;
                //    case 2:
                //        if (rng >= 9)
                //        {

                //        }

                //        break;
                //    case 3:
                //        if (rng >= 8)
                //        {

                //        }
                //        break;
                //}



            base.Update(gameTime, gameObjects, parent);
        }
    }
}
