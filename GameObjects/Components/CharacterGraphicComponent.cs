using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class CharacterGraphicComponent : GraphicComponent
    {
        int CurrentCharState;

        public CharacterGraphicComponent()
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            base.Update(gameTime, gameObjects, parent);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {

            switch (CurrentCharState)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;

            }
            spriteBatch.Draw(parent._texture, parent.Position, null, Color.White, 0f, parent.Origin, 1f, SpriteEffects.None, 0);
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
    }
}
