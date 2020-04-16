using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class CharacterPhysicComponent : PhysicComponent
    {

        public CharacterPhysicComponent()
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

            //parent.Position.X += 10;

            if (parent.status == 2 && parent.InTurn)
            {
                parent.HP -= 1;
                parent.SendMessage(this, 4);
                parent.status = 0;
            }

            if (parent.IsHit)
            {
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
