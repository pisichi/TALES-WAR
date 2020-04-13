using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletGraphicComponent : GraphicComponent
    {

        public BulletGraphicComponent(ContentManager content, Dictionary<string, Animation> animations) : base(animations)
        {

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
            _animationManager.Play(_animations["Shoot"]);
            _animationManager.Update(gameTime);

            base.Update(gameTime, gameObjects, parent);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {

            _animationManager.Draw(spriteBatch, parent.Position, 0, new Vector2(1, 1));
        }
    }
}
