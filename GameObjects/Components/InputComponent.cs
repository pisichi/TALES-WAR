using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class InputComponent : Component
    {

        public Dictionary<string, Keys> InputList;
        public InputComponent(Game currentScene)
        {
            InputList = new Dictionary<string, Keys>();
        }
        public override void Update(GameTime gameTime, List<GameObject> gameObjects,
        GameObject parent)
        {
        }
        public override void Reset()
        {
        }
        public virtual void ChangeMappingKey(string Key, Keys newInput)
        {
            InputList[Key] = newInput;
        }
    }
}
