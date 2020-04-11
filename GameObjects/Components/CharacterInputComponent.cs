using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterInputComponent : InputComponent
    {
        public CharacterInputComponent(Game currentScene) : base(currentScene)
        {
        }

        public override void ChangeMappingKey(string Key, Keys newInput)
        {
            base.ChangeMappingKey(Key, newInput);
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
            base.Update(gameTime, gameObjects, parent);
        }
    }
}
