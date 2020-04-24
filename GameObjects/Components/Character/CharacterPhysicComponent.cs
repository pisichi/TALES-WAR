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

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
