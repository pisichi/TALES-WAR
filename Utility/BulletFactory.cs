using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class BulletFactory
    {
        public BulletFactory()
        {
            
        }

        public static GameObject create(ContentManager content,string weapon)
        {

            Texture2D _bullet = content.Load<Texture2D>("sprites/ball");
            Texture2D hit = content.Load<Texture2D>("sprites/hitbox");

            GameObject bullet = new GameObject(null,
                        new BulletPhysicComponent(),
                        new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(300,400,100,100),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(300,400,100,100),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(300,400,100,100),1) },
                             }),
                        null)
            {
                Viewport = new Rectangle(0, 0, 50, 50),
                _hit = hit,
            };

            return bullet;
        }
    }
}
