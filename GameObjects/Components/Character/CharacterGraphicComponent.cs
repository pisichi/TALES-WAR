using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class CharacterGraphicComponent : GraphicComponent
    {

        int CurrentCharState;

        public CharacterGraphicComponent(ContentManager content, Dictionary<string, Animation> animations) : base(animations)
        {
            CurrentCharState = 1;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            switch (CurrentCharState)
            {
                case 1:
                    _animationManager.Play(_animations["Idle"]);
                    break;
                case 2:
                    _animationManager.Play(_animations["Throw"]);
                    break;
                case 3:
                    _animationManager.Play(_animations["Skill"]);
                    break;
                case 4:
                    _animationManager.Play(_animations["Hit"]);
                    break;
                case 5:
                    _animationManager.Play(_animations["Stunt"]);
                    break;
                case 6:
                    _animationManager.Play(_animations["Die"]);
                    break;
            }

            
            _animationManager.Update(gameTime);
            base.Update(gameTime, gameObjects, parent);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {

            //spriteBatch.Draw(_char, parent.Position, parent.Viewport, Color.White, 0f, parent.Origin, 1f, SpriteEffects.None, 0);
            _animationManager.Draw(spriteBatch, parent.Position, 0f, new Vector2(1, 1));
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
