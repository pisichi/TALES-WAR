using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class CharacterGraphicComponent : GraphicComponent
    {

        int CurrentCharState;
        Texture2D _char;

        public CharacterGraphicComponent(ContentManager content, Dictionary<string, Animation> animations) : base(animations)
        {
            _char = content.Load<Texture2D>("sprites/ZEUSSHEET");
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            _animationManager.Play(_animations["Alive"]);
            _animationManager.Update(gameTime);
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
            //spriteBatch.Draw(_char, parent.Position, parent.Viewport, Color.White, 0f, parent.Origin, 1f, SpriteEffects.None, 0);
            _animationManager.Draw(spriteBatch, parent.Position, 0, new Vector2(1, 1));
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
